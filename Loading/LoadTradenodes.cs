using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public static partial class LoadFilesClass
    {
        public static void LoadTradenodes(LoadingProgress progress, List<NodeFile> tradenodesfiles)
        {
            try
            {
                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.tradenodes] != 0)
                {
                    if (!Directory.Exists(GlobalVariables.pathtomod + "common\\tradenodes\\"))
                    {
                        progress.ReportError($"Error: Directory '{GlobalVariables.pathtomod + "common\\tradenodes\\"}' doesn't exist!");
                    }
                    else
                    {
                        foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\tradenodes\\"))
                        {
                            if (file.Contains('.'))
                            {
                                if (file.Split('.')[1] == "txt")
                                {
                                    NodeFile nf = new NodeFile(file);
                                    if (nf.LastStatus.HasError)
                                        progress.ReportError($"Critical error: File '{file}' has an error in line {nf.LastStatus.LineError}");
                                    else
                                    {
                                        tradenodesfiles.Add(nf);
                                        GlobalVariables.ModTradeNodesFiles.Add(nf);
                                    }
                                }
                            }
                        }
                    }
                }
                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.tradenodes] != 1)
                {
                    if (!Directory.Exists(GlobalVariables.pathtogame + "common\\tradenodes\\"))
                    {
                        progress.ReportError($"Error: Directory '{GlobalVariables.pathtogame + "common\\tradenodes\\"}' doesn't exist!");
                    }
                    else
                    {
                        foreach (string file in Directory.GetFiles(GlobalVariables.pathtogame + "common\\tradenodes\\"))
                        {
                            if (file.Contains('.'))
                            {
                                if (file.Split('.')[1] == "txt")
                                {
                                    NodeFile nf = new NodeFile(file, true);
                                    if (nf.LastStatus.HasError)
                                        progress.ReportError($"Critical error: File '{file}' has an error in line {nf.LastStatus.LineError}");
                                    else
                                    {
                                        if (!tradenodesfiles.Any(x => x.FileName == file.Split('\\').Last().Replace(".txt", "")))
                                            tradenodesfiles.Add(nf);
                                        GlobalVariables.GameTradeNodesFile = nf;
                                    }
                                }
                            }
                        }
                    }
                }
                foreach (NodeFile tradenodes in tradenodesfiles)
                {
                    foreach (Node node in tradenodes.MainNode.Nodes)
                    {
                        if (GlobalVariables.TradeNodes.Any(x => x.Name.ToLower() == node.Name.ToLower()))
                            continue;
                        Tradenode tn = new Tradenode();
                        tn.Name = node.Name;
                        tn.NodeFile = tradenodes;
                        GlobalVariables.TradeNodes.Add(tn);
                    }
                }
                string lastTradeNode = "";

                foreach (NodeFile tradenodes in tradenodesfiles)
                {
                    foreach (Node node in tradenodes.MainNode.Nodes)
                    {
                        lastTradeNode = node.Name;
                        Tradenode tn = GlobalVariables.TradeNodes.Find(x => x.Name.ToLower() == node.Name.ToLower());
                        tn.NodeFile = tradenodes;
                        tn.Name = node.Name;
                        Node ColorNode = node.Nodes.Find(x => x.Name.ToLower() == "color");
                        if (ColorNode != null)
                            tn.Color = Color.FromArgb(int.Parse(ColorNode.PureValues[0].Name), int.Parse(ColorNode.PureValues[1].Name), int.Parse(ColorNode.PureValues[2].Name));
                        else
                        {
                            tn.Color = AdditionalElements.GenerateColor(GlobalVariables.GlobalRandom);
                            //progress.ReportError($"Alert: Trade node '{tn.Name}' has no set colour. Using random! It will be saved with that colour!");
                        }

                        Variable v = node.Variables.Find(x => x.Name.ToLower() == "inland");
                        if (v == null)
                            tn.Inland = false;
                        else
                        {
                            if (v.Value == "yes")
                                tn.Inland = true;
                            else
                                tn.Inland = false;
                        }
                        v = node.Variables.Find(x => x.Name.ToLower() == "end");
                        if (v == null)
                            tn.Endnode = false;
                        else
                        {
                            if (v.Value == "yes")
                                tn.Endnode = true;
                            else
                                tn.Endnode = false;
                        }

                        foreach (Node outgoing in node.Nodes.FindAll(x => x.Name == "outgoing"))
                        {
                            Tradenode dest = GlobalVariables.TradeNodes.Find(x => x.Name.ToLower() == outgoing.Variables.Find(y => y.Name == "name").Value.Replace("\"", "").ToLower());
                            if (dest == null)
                            {
                                progress.ReportError($"Error: Tradenode '{tn.Name}' has an invalid outgoing trade node!");
                                continue;
                            }
                            Destination dn = new Destination() { TradeNode = dest };
                            Node path = outgoing.Nodes.Find(x => x.Name.ToLower() == "path");
                            if (path == null)
                            {
                                progress.ReportError($"Error: Tradenode '{tn.Name}' has no path!");
                                continue;
                            }
                            dn.Path.AddRange(path.GetPureValuesAsArray());
                            if (outgoing.Nodes.Find(x => x.Name.ToLower() == "control") != null)
                                dn.Control.AddRange(outgoing.Nodes.Find(x => x.Name.ToLower() == "control").GetPureValuesAsArray());
                            tn.Destination.Add(dn);
                            dn.TradeNode.Incoming.Add(tn);
                        }

                        Node members = node.Nodes.Find(x => x.Name.ToLower() == "members");
                        if (members == null)
                        {
                            progress.ReportError($"Error: Tradenode '{tn.Name}' has no provinces!");
                        }
                        else
                        {
                            foreach (PureValue value in members.PureValues)
                            {
                                int id = 0;
                                if (!int.TryParse(value.Name, out id))
                                {
                                    progress.ReportError($"Error: Tradenode '{tn.Name}' has an unexpected value in provinces!");
                                }
                                else
                                {

                                    Province p = GlobalVariables.Provinces.Find(x => x.ID == id);

                                    if (p == null)
                                    {
                                        progress.ReportError($"Error: Tradenode '{tn.Name}' has an invalid ID in provinces!");
                                    }
                                    else
                                    {
                                        if (p.TradeNode != null)
                                        {
                                            progress.ReportError($"Error: Province '{p.ID}' belongs to multiple tradenodes! '{p.TradeNode}' and '{tn}'. Using the second one!");
                                            p.TradeNode.Provinces.Remove(p);
                                        }
                                        tn.Provinces.Add(p);
                                        p.TradeNode = tn;
                                    }
                                }
                            }
                        }

                        //

                        int location = 0;
                        Province locationp = null;
                        Variable locationVar = node.Variables.Find(x => x.Name.ToLower() == "location");
                        if (locationVar == null)
                        {
                            progress.ReportError($"Alert: '{node.Name}' tradenode has no set location! Picking first province...");
                        }
                        else if (!int.TryParse(locationVar.Value, out location))
                        {
                            progress.ReportError($"Error: '{node.Name}' node has incorrect location '{location}', using first valid node province member.");
                        }
                        else
                        {
                            locationp = GlobalVariables.Provinces.Find(x => x.ID == location);
                            if (locationp == null)
                                progress.ReportError($"Error: Location of '{node.Name}' not found, using first valid node province member.");
                        }



                        if (locationp == null)
                        {
                            if (!tn.Provinces.Any())
                            {
                                progress.ReportError($"Error: Tradenode '{node.Name}' has no provinces that can be used as it's location!");
                            }
                            else
                            {
                                locationp = tn.Provinces.First();
                            }
                        }
                        else
                        {
                            tn.Location = locationp;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (GlobalVariables.__DEBUG)
                    throw;
                progress.ReportError("Critical error: Issue with tradenodes! Program will exit after continuing!");
                progress.ReportError(e.ToString());
            }
        }
    }
}
