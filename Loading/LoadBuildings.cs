using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public static partial class LoadFilesClass
    {
        public static void LoadBuildings(LoadingProgress progress, List<NodeFile> buildingsfiles)
        {
            try
            {
                List<bool> GameFiles = new List<bool>();
                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.buildings] != 0)
                {
                    if (!Directory.Exists(GlobalVariables.pathtomod + "common\\buildings\\"))
                    {
                        progress.ReportError($"Error: Directory '{GlobalVariables.pathtomod + "common\\buildings\\"}' doesn't exist!");
                    }
                    else
                    {
                        foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\buildings\\"))
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
                                        buildingsfiles.Add(nf);
                                    }
                                }
                            }
                        }
                    }
                }
                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.buildings] != 1)
                {
                    if (!Directory.Exists(GlobalVariables.pathtogame + "common\\buildings\\"))
                    {
                        progress.ReportError($"Error: Directory '{GlobalVariables.pathtogame + "common\\buildings\\"}' doesn't exist!");
                    }
                    else
                    {
                        foreach (string file in Directory.GetFiles(GlobalVariables.pathtogame + "common\\buildings\\"))
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
                                        if (!buildingsfiles.Any(x => x.FileName == file.Split('\\').Last().Replace(".txt", "")))
                                        {
                                            buildingsfiles.Add(nf);
                                        }
                                    }

                                }
                            }
                        }
                    }
                }

                List<(string, int)> fortBuildings = new List<(string, int)>();
                foreach (NodeFile buildings in buildingsfiles)
                {
                    foreach (Node node in buildings.MainNode.Nodes)
                    {
                        if (GlobalVariables.Buildings.Any(x => x.Name.ToLower() == node.Name.ToLower()))
                            continue;
                        Building bl = new Building();
                        bl.Name = node.Name;
                        bl.NodeFile = buildings;

                        Node modifier = node.Nodes.Find(x => x.Name == "modifier");
                        if (modifier != null)
                        {
                            if (modifier.TryGetVariableValue("fort_level", out string value))
                            {
                                if (int.TryParse(value, out int fortL))
                                    fortBuildings.Add((node.Name, fortL));
                                else
                                    progress.ReportError($"Error: Unexpected value in fort_level of '{node.Name}'!");
                            }

                        }

                        GlobalVariables.Buildings.Add(bl);
                    }
                }

                GlobalVariables.FortBuildings = fortBuildings.ToArray();

            }
            catch (Exception e)
            {
                if (GlobalVariables.__DEBUG)
                    throw;
                progress.ReportError("Critical error: Unexpected issue with buildings! Program will exit after continuing!");
                progress.ReportError(e.ToString());
            }
        }
    }
}
