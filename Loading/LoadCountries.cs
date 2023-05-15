using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public static partial class LoadFilesClass
    {
        public static void LoadCommonCountries(LoadingProgress progress, List<NodeFile> CountryCommonFiles, Dictionary<string, string> NameToTag, Dictionary<string, NodeFile> NameToFile)
        {
            try
            {
                List<bool> GameFiles = new List<bool>();
                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.commonCountries] != 0)
                {

                    if (!Directory.Exists(GlobalVariables.pathtomod + "common\\countries\\"))
                    {
                        progress.ReportError($"Error: Directory '{GlobalVariables.pathtomod + "common\\countries\\"}' not found!");
                    }
                    else
                    {
                        foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\countries\\"))
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
                                        CountryCommonFiles.Add(nf);
                                    }
                                }
                            }
                        }
                    }
                }
                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.commonCountries] != 1)
                {
                    if (!Directory.Exists(GlobalVariables.pathtogame + "common\\countries\\"))
                    {
                        progress.ReportError($"Error: Directory '{GlobalVariables.pathtogame + "common\\countries\\"}' not found!");
                    }
                    else
                    {
                        foreach (string file in Directory.GetFiles(GlobalVariables.pathtogame + "common\\countries\\"))
                        {
                            if (file.Contains('.'))
                            {
                                if (file.Split('.')[1] == "txt")
                                {
                                    if (!CountryCommonFiles.Any(x => x.Path.Split('\\').Last().Replace(".txt", "") == file.Split('\\').Last().Replace(".txt", "")))
                                    {
                                        NodeFile nf = new NodeFile(file, true);
                                        if (nf.LastStatus.HasError)
                                            progress.ReportError($"Critical error: File '{file}' has an error in line {nf.LastStatus.LineError}");
                                        else
                                        {
                                            CountryCommonFiles.Add(nf);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //bw.ReportProgress(84);

                foreach (NodeFile file in CountryCommonFiles)
                {
                    string name = file.Path.Split('\\').Last().Split('.')[0];
                    if (!NameToTag.ContainsKey(name))
                        continue;
                    Country c = new Country();
                    GlobalVariables.Countries.Add(c);
                    c.CountryTagsFile = NameToFile[name];
                    c.CommonFile = file;
                    NodeFile nodefile = file;

                    c.FullName = name;
                    c.OriginalFullName = name;
                    c.Tag = NameToTag[name];
                    c.OriginalTag = c.Tag;

                    Node colorn = nodefile.MainNode.Nodes.Find(x => x.Name.ToLower() == "color");

                    if (colorn == null)
                    {
                        progress.ReportError($"Error: Country '{c.FullName}' has no color specified!");
                    }
                    else
                    {
                        string[] colort = colorn.GetPureValuesAsArray();
                        if (colort.Count() < 3)
                        {
                            progress.ReportError($"Error: Country '{c.FullName}' has incorrect number of color values specified!");
                        }
                        else
                        {
                            if (colort[0].Contains(".") || colort[1].Contains(".") || colort[2].Contains("."))
                            {
                                double r = 0;
                                double g = 0;
                                double b = 0;
                                if (!double.TryParse(colort[0], NumberStyles.Any, CultureInfo.InvariantCulture, out r))
                                    progress.ReportError($"Error: Country '{c.FullName}' has incorrect color values!");
                                else if (!double.TryParse(colort[1], NumberStyles.Any, CultureInfo.InvariantCulture, out g))
                                    progress.ReportError($"Error: Country '{c.FullName}' has incorrect color values!");
                                else if (!double.TryParse(colort[2], NumberStyles.Any, CultureInfo.InvariantCulture, out b))
                                    progress.ReportError($"Error: Country '{c.FullName}' has incorrect color values!");
                                else
                                    c.Color = Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255));
                            }
                            else
                            {
                                int r = 0;
                                int g = 0;
                                int b = 0;
                                if (!int.TryParse(colort[0], out r))
                                    progress.ReportError($"Error: Country '{c.FullName}' has incorrect color values!");
                                else if (!int.TryParse(colort[1], out g))
                                    progress.ReportError($"Error: Country '{c.FullName}' has incorrect color values!");
                                else if (!int.TryParse(colort[2], out b))
                                    progress.ReportError($"Error: Country '{c.FullName}' has incorrect color values!");
                                else
                                    c.Color = Color.FromArgb(r, g, b);
                            }
                        }
                    }


                    Variable gfxcul = nodefile.MainNode.Variables.Find(x => x.Name.ToLower() == "graphical_culture");
                    if (gfxcul != null)
                        c.GraphicalCulture = gfxcul.Value;
                    else
                        progress.ReportError($"Error: Country '{c.FullName}' has no graphical culture set!");

                    Node monarchNamesNode = nodefile.MainNode.Nodes.Find(x => x.Name.ToLower() == "monarch_names");
                    if (monarchNamesNode != null)
                    {
                        foreach (Variable monarchName in monarchNamesNode.Variables)
                        {
                            int v = 0;
                            if (int.TryParse(monarchName.Value, out v))
                            {
                                if (!c.MonarchNames.Any(x => x.Name == monarchName.Name.Replace("\"", "").Trim()))
                                {
                                    c.MonarchNames.Add(new MonarchName(monarchName.Name.Replace("\"", "").Trim(), v));
                                }
                            }
                            else
                                progress.ReportError($"Error: Country '{c.FullName}' has invalid monarch name chance!");
                        }
                    }
                    Node leaderNamesNode = nodefile.MainNode.Nodes.Find(x => x.Name.ToLower() == "leader_names");
                    if (leaderNamesNode != null)
                        foreach (PureValue leadername in leaderNamesNode.PureValues)
                            if (!c.LeaderNames.Contains(leadername.Name.Replace("\"", "").Trim()))
                                c.LeaderNames.Add(leadername.Name.Replace("\"", "").Trim());
                    Node shipNamesNode = nodefile.MainNode.Nodes.Find(x => x.Name.ToLower() == "ship_names");
                    if (shipNamesNode != null)
                        foreach (PureValue shipname in shipNamesNode.PureValues)
                            if (!c.ShipNames.Contains(shipname.Name.Replace("\"", "").Trim()))
                                c.ShipNames.Add(shipname.Name.Replace("\"", "").Trim());
                    Node armyNamesNode = nodefile.MainNode.Nodes.Find(x => x.Name.ToLower() == "army_names");
                    if (armyNamesNode != null)
                        foreach (PureValue armyname in armyNamesNode.PureValues)
                            if (!c.ArmyNames.Contains(armyname.Name.Replace("\"", "").Trim()))
                                c.ArmyNames.Add(armyname.Name.Replace("\"", "").Trim());
                    Node fleetNamesNode = nodefile.MainNode.Nodes.Find(x => x.Name.ToLower() == "fleet_names");
                    if (fleetNamesNode != null)
                        foreach (PureValue fleetname in fleetNamesNode.PureValues)
                            if (!c.FleetNames.Contains(fleetname.Name.Replace("\"", "").Trim()))
                                c.FleetNames.Add(fleetname.Name.Replace("\"", "").Trim());



                }
            }
            catch (Exception e)
            {
                if (GlobalVariables.__DEBUG)
                    throw;
                progress.ReportError("Critical error: Unexpected issue with common countries! Program will exit after continuing!");
                progress.ReportError(e.ToString());
                throw new Exception();
            }
        }
        public static void LoadHistoryCountries(LoadingProgress progress)
        {
            try
            {
                List<NodeFile> CountryHistoryFiles = new List<NodeFile>();

                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.historyCountries] != 0)
                {

                    if (!Directory.Exists(GlobalVariables.pathtomod + "history\\countries\\"))
                    {
                        progress.ReportError($"Error: Directory '{GlobalVariables.pathtomod + "history\\countries\\"}' wasn't found!");
                    }
                    else
                    {
                        foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "history\\countries\\"))
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
                                        CountryHistoryFiles.Add(nf);
                                    }
                                }
                            }
                        }
                    }
                }
                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.historyCountries] != 1)
                {
                    if (!Directory.Exists(GlobalVariables.pathtogame + "history\\countries\\"))
                    {
                        progress.ReportError($"Error: Directory '{GlobalVariables.pathtogame + "history\\countries\\"}' wasn't found!");
                    }
                    else
                    {
                        foreach (string file in Directory.GetFiles(GlobalVariables.pathtogame + "history\\countries\\"))
                        {
                            if (file.Contains('.'))
                            {
                                if (file.Split('.')[1] == "txt")
                                {
                                    if (!CountryHistoryFiles.Any(x => x.Path.Split('\\').Last().Replace(".txt", "") == file.Split('\\').Last().Replace(".txt", "")))
                                    {
                                        NodeFile nf = new NodeFile(file, true);
                                        if (nf.LastStatus.HasError)
                                            progress.ReportError($"Critical error: File '{file}' has an error in line {nf.LastStatus.LineError}");
                                        else
                                        {
                                            CountryHistoryFiles.Add(nf);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                foreach (NodeFile file in CountryHistoryFiles)
                {
                    string fs = file.Path.Split('\\').Last();
                    string tag = "";

                    tag = fs.Split('-')[0].Trim().ToUpper();
                    if (tag.Length > 3)
                        tag = tag.Substring(0, 3);

                    Country c = GlobalVariables.Countries.Find(x => x.Tag == tag);
                    if (c == null)
                    {
                        progress.ReportError($"Alert: History file '{fs}' has no matching country!");
                        continue;
                    }

                    c.HistoryFile = file;
                    NodeFile nodefile = file;
                    foreach (Variable v in nodefile.MainNode.Variables)
                    {
                        switch (v.Name.ToLower())
                        {
                            case "government":
                                Government g = GlobalVariables.Governments.Find(x => x.Type.ToLower() == v.Value.ToLower());
                                if (g == null)
                                {
                                    progress.ReportError($"Error: Government '{v.Value}' in country '{c.FullName}' wasn't found!");
                                }
                                c.Government = g;
                                break;
                            case "add_government_reform":
                                c.GovernmentReform = v.Value;
                                break;
                            case "technology_group":
                                if (!GlobalVariables.TechGroups.Contains(v.Value.ToLower()))
                                    progress.ReportError($"Error: Technology group '{v.Value}' in country '{c.FullName}' wasn't found!");
                                c.TechnologyGroup = v.Value;
                                break;
                            case "capital":
                                int val = 0;
                                if (int.TryParse(v.Value, out val))
                                {
                                    c.CapitalID = val;
                                    Province p = GlobalVariables.Provinces.Find(x => x.ID == c.CapitalID);
                                    if (p != null)
                                        c.Capital = p;
                                    else
                                        progress.ReportError($"Error: Province ID '{val}' in country '{c.FullName}' capital isn't valid!");
                                }
                                else
                                {
                                    progress.ReportError($"Error: Unexpected value '{v.Value}' in country '{c.FullName}' capital!");
                                }
                                break;
                            case "religion":
                                Religion r = Religion.Religions.Find(x => x.Name.ToLower() == v.Value.ToLower());
                                if (r == null)
                                {
                                    progress.ReportError($"Error: Religion '{v.Value}' in country '{c.FullName}' not found!");
                                }
                                c.Religion = r;
                                break;
                            case "primary_culture":
                                Culture cl = Culture.Cultures.Find(x => x.Name.ToLower() == v.Value.ToLower());
                                if (cl == null)
                                {
                                    progress.ReportError($"Error: Culture '{v.Value}' in country '{c.FullName}' not found!");
                                }
                                c.PrimaryCulture = cl;
                                break;
                            case "government_rank":
                                int va = 0;
                                if (int.TryParse(v.Value, out va))
                                {
                                    c.GovernmentRank = va;
                                }
                                else
                                {
                                    progress.ReportError($"Error: Unexpected value '{v.Value}' in country '{c.FullName}' government rank!");
                                }
                                break;
                        }
                    }
                }


                foreach (Country c in GlobalVariables.Countries)
                {
                    if (c != Country.NoCountry)
                    {
                        if (c.HistoryFile == null)
                        {
                            c.HistoryFile = new NodeFile(GlobalVariables.pathtomod + $"history\\countries\\{c.Tag} - {c.FullName}.txt");
                            if (c.HistoryFile.LastStatus.HasError)
                                progress.ReportError($"Critical error: File '{c.HistoryFile.Path}' has an error in line {c.HistoryFile.LastStatus.LineError}");
                            c.HistoryFile.SaveFile();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (GlobalVariables.__DEBUG)
                    throw;
                progress.ReportError("Critical error: Unexpected issue with history countries! Program will exit after continuing!");
                progress.ReportError(e.ToString());
                throw new Exception();
            }
        }
    }
}
