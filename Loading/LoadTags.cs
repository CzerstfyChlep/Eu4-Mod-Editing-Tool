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
        public static void LoadTags(LoadingProgress progress, List<NodeFile> countrytagsfiles, Dictionary<string, string> NameToTag, Dictionary<string, NodeFile> NameToFile)
        {
            try
            {
                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.countrytags] != 0)
                {
                    if (!Directory.Exists(GlobalVariables.pathtomod + "common\\country_tags\\"))
                    {
                        progress.ReportError($"Error: Directory '{GlobalVariables.pathtomod + "common\\country_tags\\"}' doesn't exist!");
                    }
                    else
                    {
                        foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\country_tags\\"))
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
                                        countrytagsfiles.Add(nf);
                                        GlobalVariables.ModCountryTagsFiles.Add(nf);
                                    }
                                }
                            }
                        }
                    }
                }
                if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.countrytags] != 1)
                {
                    if (!Directory.Exists(GlobalVariables.pathtogame + "common\\country_tags\\"))
                    {
                        progress.ReportError($"Error: Directory '{GlobalVariables.pathtogame + "common\\country_tags\\"}' doesn't exist!");
                    }
                    else
                    {
                        foreach (string file in Directory.GetFiles(GlobalVariables.pathtogame + "common\\country_tags\\"))
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
                                        if (!countrytagsfiles.Any(x => x.FileName == file.Split('\\').Last().Replace(".txt", "")))
                                            countrytagsfiles.Add(nf);
                                        GlobalVariables.GameCountryTagsFile = nf;
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (NodeFile countrytags in countrytagsfiles)
                {
                    foreach (Variable v in countrytags.MainNode.Variables)
                    {
                        //TODO
                        //tolerate both slashes!!!!!!!!
                        string[] sp = v.Value.Replace("\"", "").Trim().Split('/');
                        if (sp.Count() < 2)
                        {
                            progress.ReportError($"Error: Issue with '{v.Name}' tag!");
                        }
                        else
                        {
                            string n = sp[1].Split('.')[0];
                            if (!NameToTag.Keys.Contains(n))
                                NameToTag.Add(n, v.Name.Trim());
                            if (!NameToFile.Keys.Contains(n))
                                NameToFile.Add(n, countrytags);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (GlobalVariables.__DEBUG)
                    throw;
                progress.ReportError("Critical error: Unexpected issue with tag files! Program will exit after continuing!");
                progress.ReportError(e.ToString());
                throw new Exception();
            }
        }
    }
}
