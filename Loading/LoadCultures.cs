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
        public static void LoadCultures(LoadingProgress progress, List<NodeFile> culturesfiles)
        {
            try
            {
                List<string> done = new List<string>();
                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.cultures] != 0)
                {
                    if (!Directory.Exists(GlobalVariables.pathtomod + "common\\cultures\\"))
                    {
                        progress.ReportError($"Error: Directory '{GlobalVariables.pathtomod + "common\\cultures\\"}' doesn't exist!");
                    }
                    else
                    {
                        foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\cultures\\"))
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
                                        culturesfiles.Add(nf);
                                        GlobalVariables.ModCulturesFiles.Add(nf);
                                    }
                                }
                            }
                        }
                    }
                }
                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.cultures] != 1)
                {
                    if (!Directory.Exists(GlobalVariables.pathtogame + "common\\cultures\\"))
                    {
                        progress.ReportError($"Error: Directory '{GlobalVariables.pathtogame + "common\\cultures\\"}' doesn't exist!");
                    }
                    else
                    {
                        foreach (string file in Directory.GetFiles(GlobalVariables.pathtogame + "common\\cultures\\"))
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
                                        if (!culturesfiles.Any(x => x.FileName == file.Split('\\').Last().Replace(".txt", "")))
                                            culturesfiles.Add(nf);
                                        GlobalVariables.GameCulturesFile = nf;
                                    }
                                }
                            }
                        }
                    }
                }
                done = new List<string>();
                foreach (NodeFile cultures in culturesfiles)
                {
                    foreach (Node node in cultures.MainNode.Nodes)
                    {
                        if (done.Contains(node.Name))
                            continue;
                        done.Add(node.Name);
                        CultureGroup cg = new CultureGroup
                        {
                            Name = node.Name
                        };
                        foreach (Node innernode in node.Nodes)
                        {
                            if (innernode.Name != "dynasty_names" && innernode.Name != "female_names" && innernode.Name != "male_names" && innernode.Name != "graphical_culture")
                            {
                                Culture c = new Culture
                                {
                                    Name = innernode.Name,
                                    Group = cg,
                                    NodeFile = cultures
                                };
                                cg.Cultures.Add(c);
                                Variable v = innernode.Variables.Find(x => x.Name.ToLower() == "primary");
                                if (v != null)
                                    c.PrimaryTag = v.Value;
                            }
                        }
                    }
                }
            }
            catch
            {
                if (GlobalVariables.__DEBUG)
                    throw;
                progress.ReportError("Critical error: Unexpected issue with cultures! Program will exit after continuing!");
                throw new Exception();
            }
        }
    }
}
