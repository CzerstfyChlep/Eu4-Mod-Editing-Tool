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
        public static void LoadReligions(LoadingProgress progress, List<NodeFile> religionsfiles)
        {
            try
            {
                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.religions] != 0)
                {
                    if (!Directory.Exists(GlobalVariables.pathtomod + "common\\religions\\"))
                    {
                        progress.ReportError($"Error: Directory '{GlobalVariables.pathtomod + "common\\religions\\"}' doesn't exist!");
                    }
                    else
                    {
                        foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\religions\\"))
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
                                        religionsfiles.Add(nf);
                                        GlobalVariables.ModReligionsFiles.Add(nf);
                                    }
                                }
                            }
                        }
                    }
                }
                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.religions] != 1)
                {
                    if (!Directory.Exists(GlobalVariables.pathtogame + "common\\religions\\"))
                    {
                        progress.ReportError($"Error: Directory '{GlobalVariables.pathtogame + "common\\religions\\"}' doesn't exist!");
                    }
                    else
                    {
                        foreach (string file in Directory.GetFiles(GlobalVariables.pathtogame + "common\\religions\\"))
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
                                        if (!religionsfiles.Any(x => x.FileName == file.Split('\\').Last().Replace(".txt", "")))
                                            religionsfiles.Add(nf);
                                        GlobalVariables.GameReligionsFile = nf;
                                    }
                                }
                            }
                        }
                    }
                }
                string[] religionforbidden = new string[] { };
                foreach (NodeFile religions in religionsfiles)
                {
                    foreach (Node node in religions.MainNode.Nodes)
                    {
                        ReligionGroup rg = new ReligionGroup
                        {
                            Name = node.Name
                        };
                        foreach (Node innernode in node.Nodes)
                        {
                            if (innernode.Name != "flag_emblem_index_range" && innernode.Name != "religious_schools")
                            {
                                try
                                {
                                    Religion r = new Religion
                                    {
                                        Name = innernode.Name,
                                        ReadableName = innernode.Name[0].ToString().ToUpper() + innernode.Name.Substring(1).Replace('_', ' '),
                                        Group = rg,
                                        NodeFile = religions
                                    };
                                    rg.Religions.Add(r);
                                    Node colourNode = innernode.Nodes.Find(x => x.Name.ToLower() == "color");
                                    if (colourNode == null)
                                        progress.ReportError($"Error: No colour set for religion '{innernode.Name}'");
                                    else
                                    {
                                        string[] colorstring = colourNode.GetPureValuesAsArray();
                                        if (colorstring.Count() < 3)
                                            progress.ReportError($"Error: Invalid colour set for religion '{innernode.Name}'");
                                        else
                                        {
                                            if (colorstring[0].Contains(".") || colorstring[1].Contains(".") || colorstring[2].Contains("."))
                                            {
                                                r.Color = Color.FromArgb((int)(double.Parse(colorstring[0], CultureInfo.InvariantCulture) * 255), (int)(double.Parse(colorstring[1], CultureInfo.InvariantCulture) * 255), (int)(double.Parse(colorstring[2], CultureInfo.InvariantCulture) * 255));
                                            }
                                            else
                                            {
                                                r.Color = Color.FromArgb(int.Parse(colorstring[0]), int.Parse(colorstring[1]), int.Parse(colorstring[2]));
                                            }
                                        }
                                    }
                                    Variable iconVariable = innernode.Variables.Find(x => x.Name.ToLower() == "icon");
                                    if (iconVariable == null)
                                        progress.ReportError($"Error: No icon set for religion {innernode.Name}");
                                    else
                                        r.Icon = int.Parse(iconVariable.Value);
                                }
                                catch
                                {
                                    progress.ReportError($"Error: Unexpected erorr in religion {innernode.Name}!");
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                if (GlobalVariables.__DEBUG)
                    throw;
                progress.ReportError("Critical error: Unexpected issue with religions! Program will exit after continuing!");
                throw new Exception();
            }
        }
    }
}
