using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Eu4ModEditor
{
    public static partial class LoadFilesClass
    {
        public static void LoadGovernments(LoadingProgress progress, List<NodeFile> governmentsfiles)
        {
            try
            {
                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.governments] != 0)
                {
                    NodeFile[] files = ReadFiles(GlobalVariables.pathtomod + "common\\governments\\", progress);

                    if (!Directory.Exists(GlobalVariables.pathtomod + "common\\governments\\"))
                        progress.ReportError($"Error: Directory '{GlobalVariables.pathtomod + "common\\governments\\"}' doesn't exist!");
                    else
                    {
                        foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\governments\\"))
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
                                        governmentsfiles.Add(nf);
                                        GlobalVariables.ModGovernmentsFiles.Add(nf);
                                    }
                                }
                            }
                        }
                    }
                }
                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.governments] != 1)
                {
                    if (!Directory.Exists(GlobalVariables.pathtogame + "common\\governments\\"))
                        progress.ReportError($"Error: Directory '{GlobalVariables.pathtogame + "common\\governments\\"}' doesn't exist!");
                    foreach (string file in Directory.GetFiles(GlobalVariables.pathtogame + "common\\governments\\"))
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
                                    if (!governmentsfiles.Any(x => x.FileName == file.Split('\\').Last().Replace(".txt", "")))
                                        governmentsfiles.Add(nf);
                                    GlobalVariables.GameGovernmentsFile = nf;
                                }
                            }
                        }
                    }
                }
                foreach (NodeFile government in governmentsfiles)
                {
                    foreach (Node n in government.MainNode.Nodes)
                    {
                        if (n.Name.ToLower() != "pre_dharma_mapping")
                        {
                            Government gv = new Government(n.Name);
                            Node reformLevels = n.Nodes.Find(x => x.Name.ToLower() == "reform_levels");
                            if (reformLevels == null)
                            {
                                progress.ReportError($"Error: Government '{n.Name}' has no reform levels!");
                                continue;
                            }
                            if (!reformLevels.Nodes.Any())
                            {
                                progress.ReportError($"Error: Government '{n.Name}' has no reforms!");
                                continue;
                            }
                            Node reforms = reformLevels.Nodes[0].Nodes.Find(x => x.Name.ToLower() == "reforms");
                            if (reforms == null)
                            {
                                progress.ReportError($"Error: Government '{n.Name}' has no reforms!");
                                continue;
                            }
                            gv.reforms.AddRange(reforms.GetPureValuesAsArray());
                            Node colornode = n.Nodes.Find(x => x.Name.ToLower() == "color");
                            if (colornode == null)
                            {
                                progress.ReportError($"Error: Government '{n.Name}' has no color set!");
                            }
                            else
                            {
                                if (colornode.PureValues.Count() < 3)
                                {
                                    progress.ReportError($"Error: Government '{n.Name}' has incorrect number of color values!");
                                }
                                else
                                {
                                    if (colornode.PureValues[0].Name.Contains(".") || colornode.PureValues[1].Name.Contains(".") || colornode.PureValues[2].Name.Contains("."))
                                    {
                                        double R = 0;
                                        double G = 0;
                                        double B = 0;
                                        if (!double.TryParse(colornode.PureValues[0].Name, NumberStyles.Any, CultureInfo.InvariantCulture, out R))
                                            progress.ReportError($"Error: Government '{n.Name}' has incorrect color values!");
                                        else if (!double.TryParse(colornode.PureValues[1].Name, NumberStyles.Any, CultureInfo.InvariantCulture, out G))
                                            progress.ReportError($"Error: Government '{n.Name}' has incorrect color values!");
                                        else if (!double.TryParse(colornode.PureValues[2].Name, NumberStyles.Any, CultureInfo.InvariantCulture, out B))
                                            progress.ReportError($"Error: Government '{n.Name}' has incorrect color values!");
                                        else
                                            gv.Color = Color.FromArgb((int)(R * 255), (int)(G * 255), (int)(B * 255));

                                    }
                                    else
                                    {
                                        int R = 0;
                                        int G = 0;
                                        int B = 0;
                                        if (!int.TryParse(colornode.PureValues[0].Name, out R))
                                            progress.ReportError($"Error: Government '{n.Name}' has incorrect color values!");
                                        else if (!int.TryParse(colornode.PureValues[1].Name, out G))
                                            progress.ReportError($"Error: Government '{n.Name}' has incorrect color values!");
                                        else if (!int.TryParse(colornode.PureValues[2].Name, out B))
                                            progress.ReportError($"Error: Government '{n.Name}' has incorrect color values!");
                                        else
                                            gv.Color = Color.FromArgb(R, G, B);
                                    }
                                    gv.Color = Color.FromArgb(int.Parse(colornode.PureValues[0].Name), int.Parse(colornode.PureValues[1].Name), int.Parse(colornode.PureValues[2].Name));
                                }
                            }
                            GlobalVariables.Governments.Add(gv);
                        }
                    }
                }
            }
            catch
            {
                if (GlobalVariables.__DEBUG)
                    throw;
                progress.ReportError("Critical error: Unexpected issue with governments! Program will exit after continuing!");
                throw new Exception();
            }
        }
    }
}
