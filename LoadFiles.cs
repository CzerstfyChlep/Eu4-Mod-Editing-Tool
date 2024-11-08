using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eu4ModEditor
{
    public static class LoadFilesClass
    {
        public static NodeFile[] ReadFiles(string path, LoadingProgress progress)
        {
            List<NodeFile> files = new List<NodeFile>();
            if (!Directory.Exists(path))
            {
                progress.ReportError($"Error: Directory '{path}' doesn't exist!");
                return files.ToArray();
            }

            foreach (string file in Directory.GetFiles(path))
            {
                if(!file.Contains('.'))
                {
                    continue;
                }

                if (file.Split('.')[1] != "txt")
                {
                    continue;
                }

                NodeFile nf = new NodeFile(file);

                if (nf.LastStatus.HasError)
                    progress.ReportError($"Critical error: File '{file}' has an error in line {nf.LastStatus.LineError}");
                else
                    files.Add(nf);
                
            }
            return files.ToArray();
        }

        public static async void LoadFilesWork(LoadingProgress progress)
        {
            try
            {
                NodeFile nodef = new NodeFile();
                List<NodeFile> tradegoodsfiles = new List<NodeFile>();
                List<NodeFile> tradegoodspricesfiles = new List<NodeFile>();
                List<NodeFile> culturesfiles = new List<NodeFile>();
                List<NodeFile> religionsfiles = new List<NodeFile>();
                List<NodeFile> governmentsfiles = new List<NodeFile>();
                Dictionary<string, string> NameToTag = new Dictionary<string, string>();
                Dictionary<string, NodeFile> NameToFile = new Dictionary<string, NodeFile>();
                List<NodeFile> countrytagsfiles = new List<NodeFile>();
                List<NodeFile> buildingsfiles = new List<NodeFile>();
                List<NodeFile> CountryCommonFiles = new List<NodeFile>();
                NodeFile technology;
                NodeFile areas;
                NodeFile climate;
                NodeFile regions;
                NodeFile continents;
                List<NodeFile> tradenodesfiles = new List<NodeFile>();
                List<NodeFile> tradecompanyfiles = new List<NodeFile>();
                NodeFile Superregions;
                GlobalVariables.Countries.Add(Country.NoCountry);
                Task llocalisation = new Task(() =>
                {
                    ReadLocalisationFiles(progress);
                });
                llocalisation.Start();
                progress.UpdateProgress(22, 0);

                Task ldefinition = new Task(() =>
                {
                    int linen = 0;
                    try
                    {
                        StreamReader Reader;
                        if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.definition] > 0)
                            Reader = new StreamReader(GlobalVariables.pathtomod + "map\\definition.csv");
                        else
                            Reader = new StreamReader(GlobalVariables.pathtogame + "map\\definition.csv");

                        while (!Reader.EndOfStream)
                        {
                            string data = Reader.ReadLine();
                            linen++;
                            string[] values = data.Split(';');
                            if(values.Count() < 5)
                            {
                                progress.ReportError($"Error: Incorrect line number {linen} in definition.csv");
                                continue;
                            }
                            if (!int.TryParse(values[0], out int ID))
                            {
                                if(linen == 1)
                                {
                                    continue;
                                }
                                else
                                {
                                    progress.ReportError($"Error: Invalid ID in line {linen} of definition.csv");
                                    continue;
                                }
                            }
                            if (!int.TryParse(values[1], out int Red))
                            {
                                progress.ReportError($"Error: Invalid red value in line {linen} of definition.csv");
                                continue;
                            }
                            if (!int.TryParse(values[2], out int Green))
                            {
                                progress.ReportError($"Error: Invalid green value in line {linen} of definition.csv");
                                continue;
                            }
                            if (!int.TryParse(values[3], out int Blue))
                            {
                                progress.ReportError($"Error: Invalid blue value in line {linen} of definition.csv");
                                continue;
                            }

                            Province p = new Province(ID, Red, Green, Blue, values[4]);
                            GlobalVariables.CubeArray[Red, Green, Blue] = p;
                            GlobalVariables.Provinces.Add(p);
                        }
                    }
                    catch
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Critical error: Unexpected error with definition.csv! Program will exit after continuing!");
                    }
                });
                ldefinition.Start();
                progress.UpdateProgress(0, 0);

                Task ltradegoods = new Task(() =>
                {
                    try
                    {
                        if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.tradegoods] != 0)
                        {
                            foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\tradegoods\\"))
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
                                            tradegoodsfiles.Add(nf);
                                            GlobalVariables.ModTradeGoodsFiles.Add(nf);
                                        }
                                    }
                                }
                            }
                        }

                        if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.tradegoods] != 1)
                        {
                            foreach (string file in Directory.GetFiles(GlobalVariables.pathtogame + "common\\tradegoods\\"))
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
                                            if (!tradegoodsfiles.Any(x => x.FileName == Path.GetFileNameWithoutExtension(file)))
                                                tradegoodsfiles.Add(nf);
                                            GlobalVariables.GameTradeGoodsFile = nf;
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
                        progress.ReportError("Critical error: Unexpected issue with trade good files! Program will exit after continuing!");
                        throw new Exception();
                    }
                });
                ltradegoods.Start();
                progress.UpdateProgress(1, 0);
                //DONE
                Task lcultures = new Task(() =>
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

                });
                lcultures.Start();
                progress.UpdateProgress(4, 0);

                //DONE
                Task lreligions = new Task(() =>
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
                                                        r.Color = Color.FromArgb((int)(double.Parse(colorstring[0], CultureInfo.InvariantCulture)*255), (int)(double.Parse(colorstring[1], CultureInfo.InvariantCulture) * 255), (int)(double.Parse(colorstring[2], CultureInfo.InvariantCulture) * 255));
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
                });
                lreligions.Start();
                progress.UpdateProgress(5, 0);

                //DONE
                Task lgovernments = new Task(() =>
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
                                    if(reformLevels == null)
                                    {
                                        progress.ReportError($"Error: Government '{n.Name}' has no reform levels!");
                                        continue;
                                    }
                                    if(!reformLevels.Nodes.Any())
                                    {
                                        progress.ReportError($"Error: Government '{n.Name}' has no reforms!");
                                        continue;
                                    }
                                    Node reforms = reformLevels.Nodes[0].Nodes.Find(x => x.Name.ToLower() == "reforms");
                                    if(reforms == null)
                                    {
                                        progress.ReportError($"Error: Government '{n.Name}' has no reforms!");
                                        continue;
                                    }
                                    gv.reforms.AddRange(reforms.GetPureValuesAsArray());
                                    Node colornode = n.Nodes.Find(x => x.Name.ToLower() == "color");
                                    if(colornode == null)
                                    {
                                        progress.ReportError($"Error: Government '{n.Name}' has no color set!");
                                    }
                                    else
                                    {
                                        if(colornode.PureValues.Count() < 3)
                                        {
                                            progress.ReportError($"Error: Government '{n.Name}' has incorrect number of color values!");
                                        }
                                        else
                                        {
                                            if(colornode.PureValues[0].Name.Contains(".") || colornode.PureValues[1].Name.Contains(".")|| colornode.PureValues[2].Name.Contains("."))
                                            {
                                                double R = 0;
                                                double G = 0;
                                                double B = 0;
                                                if (!double.TryParse(colornode.PureValues[0].Name, NumberStyles.Any, CultureInfo.InvariantCulture,out R))
                                                    progress.ReportError($"Error: Government '{n.Name}' has incorrect color values!");
                                                else if (!double.TryParse(colornode.PureValues[1].Name, NumberStyles.Any, CultureInfo.InvariantCulture, out G))
                                                    progress.ReportError($"Error: Government '{n.Name}' has incorrect color values!");
                                                else if (!double.TryParse(colornode.PureValues[2].Name, NumberStyles.Any, CultureInfo.InvariantCulture, out B))
                                                    progress.ReportError($"Error: Government '{n.Name}' has incorrect color values!");
                                                else
                                                    gv.Color = Color.FromArgb((int)(R*255), (int)(G * 255), (int)(B * 255));

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

                });
                lgovernments.Start();
                progress.UpdateProgress(6, 0);


                //DONE
                Task ltechnology = new Task(() =>
                {
                    try 
                    {
                        if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.technology] > 0)
                            technology = new NodeFile(GlobalVariables.pathtomod + "common\\technology.txt");
                        else
                            technology = new NodeFile(GlobalVariables.pathtogame + "common\\technology.txt");

                        if (technology.LastStatus.HasError)
                            progress.ReportError($"Critical error: File '{technology.Path}' has an error in line {technology.LastStatus.LineError}");
                        else
                        {
                            Node groups = technology.MainNode.Nodes.Find(x => x.Name.ToLower() == "groups");
                            if (groups == null)
                            {
                                progress.ReportError($"Alert: No technology groups found!");
                            }
                            else
                            {
                                foreach (Node node in groups.Nodes)
                                {
                                    GlobalVariables.TechGroups.Add(node.Name);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Critical error: Unexpected issue with techgroups! Program will exit after continuing!");
                        progress.ReportError(e.ToString());
                    }
                });
                ltechnology.Start();
                progress.UpdateProgress(7, 0);

                //DONE
                Task ltags = new Task(() =>
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
                });
                ltags.Start();
                progress.UpdateProgress(8, 0);


                //DONE
                Task lbuildings = new Task(() =>
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
                                    if(modifier.TryGetVariableValue("fort_level", out string value))
                                    {
                                        if(int.TryParse(value, out int fortL))
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
                });
                lbuildings.Start();
                progress.UpdateProgress(20, 0);

                await ldefinition;
                if (ldefinition.IsFaulted)
                    progress.UpdateProgress(0, 1);
                else if (ldefinition.IsCompleted)
                    progress.UpdateProgress(0, 2);

                //DONE
                Task larea = new Task(() =>
                {
                    try
                    {
                        if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.area] > 0)
                            areas = new NodeFile(GlobalVariables.pathtomod + "map\\area.txt");
                        else
                            areas = new NodeFile(GlobalVariables.pathtogame + "map\\area.txt");

                        if (areas.LastStatus.HasError)
                            progress.ReportError($"Critical error: File '{areas.Path}' has an error in line {areas.LastStatus.LineError}");
                        else
                        {

                            foreach (Node n in areas.MainNode.Nodes)
                            {
                                List<Province> pr = new List<Province>();
                                foreach (PureValue vr in n.PureValues)
                                {
                                    if (vr.Name != "")
                                    {
                                        int id = 0;
                                        if (int.TryParse(vr.Name, out id))
                                        {
                                            Province p = GlobalVariables.Provinces.Find(x => x.ID == id);
                                            if (p == null)
                                            {
                                                progress.ReportError($"Error: Invalid province ID found in area '{n.Name}'");
                                            }
                                            else
                                            {
                                                if(p.Area != null)
                                                {
                                                    if (p.Area == Area.NoArea)
                                                    {
                                                        Area.NoArea.Provinces.Remove(p);
                                                    }
                                                    else
                                                    {
                                                        progress.ReportError($"Alert: Province '{p.ID}' is in multiple areas, '{p.Area}' and '{n.Name}'! Using the second one!");
                                                    }
                                                }
                                                pr.Add(p);
                                            }
                                        }
                                        else
                                        {
                                            progress.ReportError($"Error: Unexpected value found in area '{n.Name}'");
                                        }
                                    }
                                }
                                Area a = new Area(n.Name, pr)
                                {
                                    OriginalName = n.Name
                                };
                                foreach (Province pro in a.Provinces)
                                    pro.Variables[Province.Variable.Area] = a;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Critical error: Unexpected issue with areas! Program will exit after continuing!");
                        progress.ReportError(e.ToString());
                        throw new Exception();
                    }
                });
                larea.Start();
                progress.UpdateProgress(12, 0);

                //DONE
                Task lcontinent = new Task(() =>
                {
                    try
                    {
                        if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.continent] > 0)
                            continents = new NodeFile(GlobalVariables.pathtomod + "map\\continent.txt");
                        else
                            continents = new NodeFile(GlobalVariables.pathtogame + "map\\continent.txt");
                        if (continents.LastStatus.HasError)
                            progress.ReportError($"Critical error: File '{continents.Path}' has an error in line {continents.LastStatus.LineError}");
                        else
                        {
                            foreach (Node n in continents.MainNode.Nodes)
                            {
                                List<Province> ctp = new List<Province>();
                                foreach (PureValue s in n.PureValues)
                                {
                                    int id = 0;
                                    if (int.TryParse(s.Name.Trim(), out id)) {
                                        Province p = GlobalVariables.Provinces.Find(x => x.ID == id);
                                    if(p == null)
                                        progress.ReportError($"Error: Invalid province ID found in continent '{n.Name}'");
                                    else
                                        ctp.Add(p);
                                    }
                                    else
                                        progress.ReportError($"Error: Unexpected value in continent '{n.Name}'");
                                }
                                Continent c = new Continent(n.Name, ctp);
                                c.OriginalName = n.Name;
                                foreach (Province pr in c.Provinces)
                                    pr.Variables[Province.Variable.Continent] = c;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Critical error: Unexpected issue with continents! Program will exit after continuing!");
                        progress.ReportError(e.ToString());
                    }
                });
                lcontinent.Start();
                progress.UpdateProgress(14, 0);

                //DONE
                Task ltradenodes = new Task(() =>
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
                                    if(dest == null)
                                    {
                                        progress.ReportError($"Error: Tradenode '{tn.Name}' has an invalid outgoing trade node!");
                                        continue;
                                    }
                                    Destination dn = new Destination() { TradeNode =  dest };
                                    Node path = outgoing.Nodes.Find(x => x.Name.ToLower() == "path");
                                    if (path == null) {
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
                                if (members == null) {
                                    progress.ReportError($"Error: Tradenode '{tn.Name}' has no provinces!");
                                }
                                else { 
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
                                                if(p.TradeNode != null)
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
                                    if(locationp == null)
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
                });
                ltradenodes.Start();
                progress.UpdateProgress(15, 0);

                //DONE
                Task ldefaultmap = new Task(() =>
                {
                    try
                    {
                        NodeFile defaultmap;
                        if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.mapdefault] > 0)
                            defaultmap = new NodeFile(GlobalVariables.pathtomod + "map\\default.map");
                        else
                            defaultmap = new NodeFile(GlobalVariables.pathtogame + "map\\default.map");

                        if (defaultmap.LastStatus.HasError)
                            progress.ReportError($"Critical error: File '{defaultmap.Path}' has an error in line {defaultmap.LastStatus.LineError}");
                        else
                        {

                            Variable width = defaultmap.MainNode.Variables.Find(x => x.Name.ToLower() == "width");
                            Variable height = defaultmap.MainNode.Variables.Find(x => x.Name.ToLower() == "height");

                            if (width == null || height == null)
                            {
                                progress.ReportError($"Error: No width or heigh specified in default.map!");
                            }
                            else {

                                if (!int.TryParse(width.Value, out GlobalVariables.MapWidth) || !int.TryParse(height.Value, out GlobalVariables.MapHeight))
                                {
                                    progress.ReportError($"Error: Invalid values given for width or height in default.map!");
                                }
                            }

                            Node seastarts = defaultmap.MainNode.Nodes.Find(x => x.Name.ToLower() == "sea_starts");
                            if (seastarts == null)
                            {
                                progress.ReportError($"Alert: No sea_starts in default.map.");
                            }
                            else
                            {
                                foreach (string sea in seastarts.GetPureValuesAsArray())
                                {
                                    if (int.TryParse(sea, out int id))
                                    {
                                        Province f = GlobalVariables.Provinces.Find(x => x.ID == id);
                                        if (f == null)
                                            progress.ReportError($"Error: In default.map, sea_starts has invalid province ID!");
                                        else
                                            f.Sea = true;
                                    }
                                    else
                                    {
                                        progress.ReportError($"Error: In default.map, sea_starts has unexpected value!");
                                    }
                                }
                            }

                            foreach (string lake in defaultmap.MainNode.Nodes.Find(x => x.Name.ToLower() == "lakes").GetPureValuesAsArray())
                            {
                                if (int.TryParse(lake, out int id))
                                {
                                    Province f = GlobalVariables.Provinces.Find(x => x.ID == id);
                                    if (f == null)
                                        progress.ReportError($"Error: In default.map, lakes has invalid province ID!");
                                    else
                                        f.Lake = true;
                                }
                                else
                                {
                                    progress.ReportError($"Error: In default.map, lakes has unexpected value!");
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Critical error: Issue with default.map! Program will exit after continuing!");
                        progress.ReportError(e.ToString());
                        throw new Exception();
                    }
                });
                ldefaultmap.Start();
                progress.UpdateProgress(17, 0);

                //DONE
                Task lmap = new Task(() =>
                {
                    try
                    {
                        Bitmap copiedBitmap = new Bitmap(GlobalVariables.ProvincesMapBitmap);
                        LockBitmap bitmap = new LockBitmap(copiedBitmap);
                        bitmap.LockBits();

                        int heightInterval = bitmap.Height / 10;
                        int heightValue = 0;
                        int va = 10;

                        for (int y = 1; y < bitmap.Height; y++)
                        {
                            for (int x = 1; x < bitmap.Width; x += 2)
                            {
                                Color c = bitmap.GetPixel(x, y);
                                if (c != Color.FromArgb(1, 255, 255, 255))
                                {
                                    Province p = GlobalVariables.CubeArray[c.R, c.G, c.B];
                                    if (p != null)
                                    {
                                        p.Pixel = new Point(x, y);
                                        GraphicsMethods.FloodFill(ref bitmap, new Point(x, y), c, Color.FromArgb(1, 255, 255, 255), ref p.Pixels);
                                    }
                                }
                            }
                            heightValue++;
                            if (heightInterval == heightValue)
                            {
                                heightValue = 0;
                                va += 1;
                            }
                        }

                        bitmap.UnlockBits();
                    }
                    catch (Exception e)
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Critical error: Issue with the map! Program will exit after continuing!");
                        progress.ReportError(e.ToString());
                    }
                });
                lmap.Start();
                progress.UpdateProgress(2, 0);

                //DONE
                Task tradegoodsAll = ltradegoods.ContinueWith(new Action<Task>(ac =>
                {
                    try
                    {
                        GlobalVariables.TradeGoods.Add(TradeGood.nothing);
                        foreach (NodeFile tradegoods in tradegoodsfiles)
                        {
                            foreach (Node node in tradegoods.MainNode.Nodes)
                            {
                                if (GlobalVariables.TradeGoods.Any(x => x.Name == node.Name))
                                    continue;

                                TradeGood tg = new TradeGood
                                {
                                    Name = node.Name,
                                    ReadableName = node.Name[0].ToString().ToUpper() + node.Name.Substring(1).Replace('_', ' '),
                                    NodeFile = tradegoods
                                };

                                Node colornode = node.Nodes.Find(x => x.Name.ToLower() == "color");
                                if (colornode == null)
                                {
                                    progress.ReportError($"Error: Trade good '{tg.Name}' has no specified color! Using random!");
                                    tg.Color = AdditionalElements.GenerateColor(GlobalVariables.GlobalRandom);
                                }
                                else {
                                    string[] colorv = colornode.GetPureValuesAsArray();
                                    if (colorv.Count() == 3)
                                    {
                                        double r = 0;
                                        double g = 0;
                                        double b = 0;
                                        if (!double.TryParse(colorv[0], NumberStyles.Any, CultureInfo.InvariantCulture, out r) || !double.TryParse(colorv[1], NumberStyles.Any, CultureInfo.InvariantCulture, out g) || !double.TryParse(colorv[2], NumberStyles.Any, CultureInfo.InvariantCulture, out b))
                                        {
                                            progress.ReportError($"Error: Trade good '{tg.Name}' has invalid color values! Using random!");
                                            tg.Color = AdditionalElements.GenerateColor(GlobalVariables.GlobalRandom);
                                        }
                                        else
                                        {
                                            if (r > 0 && r <= 1)
                                                r *= 255;
                                            if (g > 0 && g <= 1)
                                                g *= 255;
                                            if (b > 0 && b <= 1)
                                                b *= 255;
                                            tg.Color = Color.FromArgb((int)r, (int)g, (int)b);
                                        }
                                    }
                                    else
                                    {
                                        progress.ReportError($"Error: Trade good '{tg.Name}' has missing color values! Using random!");
                                        tg.Color = AdditionalElements.GenerateColor(GlobalVariables.GlobalRandom);
                                    }
                                }

                                GlobalVariables.TradeGoods.Add(tg);

                                Variable latent = node.Variables.Find(x => x.Name.ToLower() == "is_latent");
                                if (latent != null)
                                    if (latent.Value == "yes")
                                        GlobalVariables.LatentTradeGoods.Add(tg);
                            }

                        }
                    }
                    catch (Exception e)
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Critical error: Unexpected issue with tradegoods! Program will exit after continuing!");
                        progress.ReportError(e.ToString());
                        throw new Exception();
                    }
                }))
                .ContinueWith(new Action<Task>(ac =>
                {
                    try
                    {
                        List<string> done = new List<string>();
                        if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.prices] != 0)
                        {

                            if (!Directory.Exists(GlobalVariables.pathtomod + "common\\prices\\"))
                            {
                                progress.ReportError($"Error: Directory '{GlobalVariables.pathtomod + "common\\prices\\"}' not found!");
                            }
                            else
                            {
                                foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\prices\\"))
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
                                                tradegoodspricesfiles.Add(nf);
                                                GlobalVariables.ModPricesFiles.Add(nf);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.prices] != 1)
                        {
                            if (!Directory.Exists(GlobalVariables.pathtogame + "common\\prices\\"))
                            {
                                progress.ReportError($"Error: Directory '{GlobalVariables.pathtogame + "common\\prices\\"}' not found!");
                            }
                            else
                            {
                                foreach (string file in Directory.GetFiles(GlobalVariables.pathtogame + "common\\prices\\"))
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
                                                if (!tradegoodspricesfiles.Any(x => x.FileName == file.Split('\\').Last().Replace(".txt", "")))
                                                    tradegoodspricesfiles.Add(nf);
                                                GlobalVariables.GamePricesFile = nf;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        foreach (NodeFile tradegoodsprices in tradegoodspricesfiles)
                        {
                            foreach (Node node in tradegoodsprices.MainNode.Nodes)
                            {
                                if (done.Contains(node.Name))
                                    continue;
                                done.Add(node.Name);
                                TradeGood tg = GlobalVariables.TradeGoods.Find(x => x.Name.ToLower() == node.Name);
                                if (tg != null)
                                {
                                    double value = 0;
                                    if (!double.TryParse(node.Variables.Find(x => x.Name.ToLower() == "base_price").Value, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
                                    {
                                        progress.ReportError($"Error: Unexpected price value for trade good '{tg.Name}'");
                                    }
                                    else
                                    {
                                        tg.Price = value;
                                        if (node.Variables.Find(x => x.Name.ToLower() == "goldtype") != null)
                                        {
                                            if (node.Variables.Find(x => x.Name.ToLower() == "goldtype").Value.Trim() == "yes")
                                                tg.GoldLike = true;
                                        }
                                    }
                                }
                                else
                                {
                                    progress.ReportError($"Alert: Price for {node.Name} is specified but the trade good hasn't been found!");
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Critical error: Unexpected issue with tradegoods! Program will exit after continuing!");
                        progress.ReportError(e.ToString());
                        throw new Exception();
                    }
                }));

                //DONE
                Task ltradecomapnies = new Task(() =>
                {
                    try
                    {
                        if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.tradecompanies] != 0)
                        {
                            if (!Directory.Exists(GlobalVariables.pathtomod + "common\\trade_companies\\"))
                            {
                                progress.ReportError($"Error: Directory '{GlobalVariables.pathtomod + "common\\trade_companies\\"}' not found!");
                            }
                            else
                            {
                                foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\trade_companies\\"))
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
                                                tradecompanyfiles.Add(nf);
                                                GlobalVariables.ModTradeCompanyFiles.Add(nf);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.tradecompanies] != 1)
                        {
                            if (!Directory.Exists(GlobalVariables.pathtogame + "common\\trade_companies\\"))
                            {
                                progress.ReportError($"Error: Directory '{GlobalVariables.pathtogame + "common\\trade_companies\\"}' not found!");
                            }
                            else
                            {
                                foreach (string file in Directory.GetFiles(GlobalVariables.pathtogame + "common\\trade_companies\\"))
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
                                                if (!tradecompanyfiles.Any(x => x.FileName == file.Split('\\').Last().Replace(".txt", "")))
                                                    tradecompanyfiles.Add(nf);
                                                GlobalVariables.GameTradeCompanyFile = nf;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        foreach (NodeFile tradecompanies in tradecompanyfiles)
                        {
                            foreach (Node node in tradecompanies.MainNode.Nodes)
                            {
                                TradeCompany tc = TradeCompany.TradeCompanies.Find(x => x.Name.ToLower() == node.Name.ToLower());
                                if (tc == null)
                                {
                                    tc = new TradeCompany() { Name = node.Name };
                                }
                                tc.NodeFile = tradecompanies;
                                Node ColorNode = node.Nodes.Find(x => x.Name.ToLower() == "color");
                                if (ColorNode != null)
                                    tc.Color = Color.FromArgb(int.Parse(ColorNode.PureValues[0].Name), int.Parse(ColorNode.PureValues[1].Name), int.Parse(ColorNode.PureValues[2].Name));
                                else                                
                                    tc.Color = AdditionalElements.GenerateColor(GlobalVariables.GlobalRandom);

                                Node provinces = node.Nodes.Find(x => x.Name.ToLower() == "provinces");
                                if (provinces == null)
                                {
                                    progress.ReportError($"Alert: Trade company '{tc.Name}' has no provinces specified!");
                                }
                                else
                                {
                                    foreach (PureValue value in provinces.PureValues)
                                    {
                                        if (!int.TryParse(value.Name, out int id))
                                        {
                                            progress.ReportError($"Error: Trade company '{tc.Name}' has unexpected value in provinces!");
                                        }
                                        else
                                        {
                                            Province p = GlobalVariables.Provinces.Find(x => x.ID == id);
                                            if (p == null)
                                            {
                                                progress.ReportError($"Alert: Trade company '{tc.Name}' has invalid province ID {id}!");
                                            }
                                            else
                                            {
                                                if (p.TradeCompany != null)
                                                {
                                                    if (p.TradeCompany == TradeCompany.NoTradeCompany)
                                                    {
                                                        p.TradeCompany.Provinces.Remove(p);
                                                    }
                                                    else
                                                    {
                                                        progress.ReportError($"Alert: Province {p.ID} belongs to many trade companies! '{p.TradeCompany.Name}' and '{tc.Name}'. Using second!");
                                                    }
                                                }
                                                tc.Provinces.Add(p);
                                                p.TradeCompany = tc;
                                            }
                                        }
                                    }
                                }
                                foreach (Node TCnames in node.Nodes.FindAll(x => x.Name.ToLower() == "names"))
                                {
                                    tc.Names.Add(TCnames.Variables.Find(x => x.Name.ToLower() == "name").Value);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Critical error: Unexpected issue with trade companies! Program will exit after continuing!");
                        progress.ReportError(e.ToString());
                        throw new Exception();
                    }
                });
                ltradecomapnies.Start();
                progress.UpdateProgress(21, 0);

                await lmap;
                if (lmap.IsFaulted)
                    progress.UpdateProgress(2, 1);
                else if (lmap.IsCompleted)
                    progress.UpdateProgress(2, 2);

                //DONE
                Task provincecentre = new Task(() =>
                {
                    try
                    {
                        foreach (Province p in GlobalVariables.Provinces)
                        {
                            if (p.Pixels.Any())
                            {
                                int minX = p.Pixels.Min(x => x.X);
                                int minY = p.Pixels.Min(x => x.Y);
                                int maxX = p.Pixels.Max(x => x.X);
                                int maxY = p.Pixels.Max(x => x.Y);
                                p.ContainingRectangle = new Rectangle(minX, minY, maxX - minX, maxY - minY);

                                int X = 0;
                                int Y = 0;
                                int N = 0;
                                for (int A = 0; A < p.Pixels.Count(); A += 4)
                                {
                                    N++;
                                    X += p.Pixels[A].X;
                                    Y += p.Pixels[A].Y;
                                }
                                p.Center = new Point(X / N, Y / N);
                            }
                        }
                    }
                    catch(Exception e)
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Critical error: Uexpected issue with province centres! Program will exit after continuing!");
                        progress.ReportError(e.ToString());
                    }
                });
                provincecentre.Start();
                progress.UpdateProgress(3, 0);


                //DONE
                Task lclimate = new Task(() =>
                {
                    try
                    {
                        if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.climate] > 0)
                            climate = new NodeFile(GlobalVariables.pathtomod + "map\\climate.txt");
                        else
                            climate = new NodeFile(GlobalVariables.pathtogame + "map\\climate.txt");

                        if (climate.LastStatus.HasError)
                            progress.ReportError($"Critical error: File '{climate.Path}' has an error in line {climate.LastStatus.LineError}");
                        else
                        {
                            Node tropical = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "tropical");                           
                            if (tropical != null)
                            {
                                int id = 0;
                                Province p = null;
                                foreach (PureValue pv in tropical.PureValues)
                                {
                                    if (!int.TryParse(pv.Name, out id))
                                    {
                                        progress.ReportError($"Error: tropical in climate.txt has unexpected value '{id}'!");
                                    }
                                    else
                                    {
                                        p = GlobalVariables.Provinces.Find(x => x.ID == id);
                                        if(p == null)
                                        {
                                            progress.ReportError($"Error: tropical in climate.txt has invalid province ID '{id}'!");
                                        }
                                        else
                                        {
                                            p.Climate = 1;
                                        }
                                    }                           
                                }
                            }
                            Node arid = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "arid");
                            if (arid != null)
                            {
                                int id = 0;
                                Province p = null;
                                foreach (PureValue pv in arid.PureValues)
                                {
                                    if (!int.TryParse(pv.Name, out id))
                                    {
                                        progress.ReportError($"Error: arid in climate.txt has unexpected value '{id}'!");
                                    }
                                    else
                                    {
                                        p = GlobalVariables.Provinces.Find(x => x.ID == id);
                                        if (p == null)
                                        {
                                            progress.ReportError($"Error: arid in climate.txt has invalid province ID '{id}'!");
                                        }
                                        else
                                        {
                                            p.Climate = 2;
                                        }
                                    }
                                }
                            }
                            Node arctic = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "arctic");
                            if (arctic != null)
                            {
                                int id = 0;
                                Province p = null;
                                foreach (PureValue pv in arctic.PureValues)
                                {
                                    if (!int.TryParse(pv.Name, out id))
                                    {
                                        progress.ReportError($"Error: arctic in climate.txt has unexpected value '{id}'!");
                                    }
                                    else
                                    {
                                        p = GlobalVariables.Provinces.Find(x => x.ID == id);
                                        if (p == null)
                                        {
                                            progress.ReportError($"Error: arctic in climate.txt has invalid province ID '{id}'!");
                                        }
                                        else
                                        {
                                            p.Climate = 3;
                                        }
                                    }
                                }
                            }


                            Node mild_winter = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "mild_winter");
                            if (mild_winter != null)
                            {
                                int id = 0;
                                Province p = null;
                                foreach (PureValue pv in mild_winter.PureValues)
                                {
                                    if (!int.TryParse(pv.Name, out id))
                                    {
                                        progress.ReportError($"Error: mild_winter in climate.txt has unexpected value '{id}'!");
                                    }
                                    else
                                    {
                                        p = GlobalVariables.Provinces.Find(x => x.ID == id);
                                        if (p == null)
                                        {
                                            progress.ReportError($"Error: mild_winter in climate.txt has invalid province ID '{id}'!");
                                        }
                                        else
                                        {
                                            p.Winter = 1;
                                        }
                                    }
                                }

                            }
                            Node normal_winter = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "normal_winter");
                            if (normal_winter != null)
                            {
                                int id = 0;
                                Province p = null;
                                foreach (PureValue pv in normal_winter.PureValues)
                                {
                                    if (!int.TryParse(pv.Name, out id))
                                    {
                                        progress.ReportError($"Error: normal_winter in climate.txt has unexpected value '{id}'!");
                                    }
                                    else
                                    {
                                        p = GlobalVariables.Provinces.Find(x => x.ID == id);
                                        if (p == null)
                                        {
                                            progress.ReportError($"Error: normal_winter in climate.txt has invalid province ID '{id}'!");
                                        }
                                        else
                                        {
                                            p.Winter = 2;
                                        }
                                    }
                                }
                            }
                            Node severe_winter = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "severe_winter");
                            if (severe_winter != null)
                            {
                                int id = 0;
                                Province p = null;
                                foreach (PureValue pv in severe_winter.PureValues)
                                {
                                    if (!int.TryParse(pv.Name, out id))
                                    {
                                        progress.ReportError($"Error: severe_winter in climate.txt has unexpected value '{id}'!");
                                    }
                                    else
                                    {
                                        p = GlobalVariables.Provinces.Find(x => x.ID == id);
                                        if (p == null)
                                        {
                                            progress.ReportError($"Error: severe_winter in climate.txt has invalid province ID '{id}'!");
                                        }
                                        else
                                        {
                                            p.Winter = 3;
                                        }
                                    }
                                }
                            }



                            Node impassable = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "impassable");
                            if (impassable != null)
                            {
                                int id = 0;
                                Province p = null;
                                foreach (PureValue pv in impassable.PureValues)
                                {
                                    if (!int.TryParse(pv.Name, out id))
                                    {
                                        progress.ReportError($"Error: impassable in climate.txt has unexpected value '{id}'!");
                                    }
                                    else
                                    {
                                        p = GlobalVariables.Provinces.Find(x => x.ID == id);
                                        if (p == null)
                                        {
                                            progress.ReportError($"Error: impassable in climate.txt has invalid province ID '{id}'!");
                                        }
                                        else
                                        {
                                            p.Impassable = 1;
                                        }
                                    }
                                }
                            }



                            Node mild_monsoon = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "mild_monsoon");
                            if (mild_monsoon != null)
                            {
                                int id = 0;
                                Province p = null;
                                foreach (PureValue pv in mild_monsoon.PureValues)
                                {
                                    if (!int.TryParse(pv.Name, out id))
                                    {
                                        progress.ReportError($"Error: mild_monsoon in climate.txt has unexpected value '{id}'!");
                                    }
                                    else
                                    {
                                        p = GlobalVariables.Provinces.Find(x => x.ID == id);
                                        if (p == null)
                                        {
                                            progress.ReportError($"Error: mild_monsoon in climate.txt has invalid province ID '{id}'!");
                                        }
                                        else
                                        {
                                            p.Monsoon = 1;
                                        }
                                    }
                                }
                            }
                            Node normal_monsoon = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "normal_monsoon");
                            if (normal_monsoon != null)
                            {
                                int id = 0;
                                Province p = null;
                                foreach (PureValue pv in normal_monsoon.PureValues)
                                {
                                    if (!int.TryParse(pv.Name, out id))
                                    {
                                        progress.ReportError($"Error: normal_monsoon in climate.txt has unexpected value '{id}'!");
                                    }
                                    else
                                    {
                                        p = GlobalVariables.Provinces.Find(x => x.ID == id);
                                        if (p == null)
                                        {
                                            progress.ReportError($"Error: normal_monsoon in climate.txt has invalid province ID '{id}'!");
                                        }
                                        else
                                        {
                                            p.Monsoon = 2;
                                        }
                                    }
                                }
                            }
                            Node severe_monsoon = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "severe_monsoon");
                            if (severe_monsoon != null)
                            {
                                int id = 0;
                                Province p = null;
                                foreach (PureValue pv in severe_monsoon.PureValues)
                                {
                                    if (!int.TryParse(pv.Name, out id))
                                    {
                                        progress.ReportError($"Error: severe_monsoon in climate.txt has unexpected value '{id}'!");
                                    }
                                    else
                                    {
                                        p = GlobalVariables.Provinces.Find(x => x.ID == id);
                                        if (p == null)
                                        {
                                            progress.ReportError($"Error: severe_monsoon in climate.txt has invalid province ID '{id}'!");
                                        }
                                        else
                                        {
                                            p.Monsoon = 3;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Critical error: Unexpected issue with climate! Program will exit after continuing!");
                        progress.ReportError(e.ToString());
                    }
                });
                lclimate.Start();
                progress.UpdateProgress(24, 0);

                await provincecentre;
                if (provincecentre.IsFaulted)
                    progress.UpdateProgress(3, 1);
                else if (provincecentre.IsCompleted)
                    progress.UpdateProgress(3, 2);



                await ldefaultmap;
                if (ldefaultmap.IsFaulted)
                    progress.UpdateProgress(17, 1);
                else if (ldefaultmap.IsCompleted)
                    progress.UpdateProgress(17, 2);

                //TODO raycast
                Task raycast = new Task(() =>
                {
                    /*
                    Province[,] pixelstoprovinces = new Province[10000, 10000];

                    foreach (Province p in GlobalVariables.Provinces)
                    {
                        foreach (Point pt in p.Pixels)
                        {
                            pixelstoprovinces[pt.X, pt.Y] = p;
                        }
                    }


                    foreach (Province p in GlobalVariables.Provinces)
                    {
                        for (int a = 0; a < 90; a += 5)
                        {
                            ProvinceNeighbourRay ray = new ProvinceNeighbourRay();
                            ray.currentPosition = p.Center;
                            ray.MoveX = a;
                            ray.MoveY = 90 - a;

                            Province pfound = null;
                            do
                            {
                                ray.PerformMove();
                                if (ray.currentPosition.X < 0 || ray.currentPosition.Y < 0)
                                    break;
                                if (ray.currentPosition.X > GlobalVariables.MapWidth || ray.currentPosition.Y > GlobalVariables.MapHeight)
                                    break;
                                pfound = pixelstoprovinces[ray.currentPosition.X, ray.currentPosition.Y];
                            }
                            while (pfound == null || pfound == p);

                            if (!p.BorderingProvinces.Contains(pfound))
                                p.BorderingProvinces.Add(pfound);
                        }

                        for (int a = 0; a < 90; a += 5)
                        {
                            ProvinceNeighbourRay ray = new ProvinceNeighbourRay();
                            ray.currentPosition = p.Center;
                            ray.MoveX = 90 - a;
                            ray.MoveY = -a;

                            Province pfound = null;
                            do
                            {
                                ray.PerformMove();
                                if (ray.currentPosition.X < 0 || ray.currentPosition.Y < 0)
                                    break;
                                if (ray.currentPosition.X > GlobalVariables.MapWidth || ray.currentPosition.Y > GlobalVariables.MapHeight)
                                    break;
                                pfound = pixelstoprovinces[ray.currentPosition.X, ray.currentPosition.Y];
                            }
                            while (pfound == null || pfound == p);

                            if (!p.BorderingProvinces.Contains(pfound))
                                p.BorderingProvinces.Add(pfound);
                        }

                        for (int a = 0; a < 90; a += 5)
                        {
                            ProvinceNeighbourRay ray = new ProvinceNeighbourRay();
                            ray.currentPosition = p.Center;
                            ray.MoveX = -a;
                            ray.MoveY = -90 + a;

                            Province pfound = null;
                            do
                            {
                                ray.PerformMove();
                                if (ray.currentPosition.X < 0 || ray.currentPosition.Y < 0)
                                    break;
                                if (ray.currentPosition.X > GlobalVariables.MapWidth || ray.currentPosition.Y > GlobalVariables.MapHeight)
                                    break;
                                pfound = pixelstoprovinces[ray.currentPosition.X, ray.currentPosition.Y];
                            }
                            while (pfound == null || pfound == p);

                            if (!p.BorderingProvinces.Contains(pfound))
                                p.BorderingProvinces.Add(pfound);
                        }

                        for (int a = 0; a < 90; a += 5)
                        {
                            ProvinceNeighbourRay ray = new ProvinceNeighbourRay();
                            ray.currentPosition = p.Center;
                            ray.MoveX = -90 + a;
                            ray.MoveY = a;

                            Province pfound = null;
                            do
                            {
                                ray.PerformMove();
                                if (ray.currentPosition.X < 0 || ray.currentPosition.Y < 0)
                                    break;
                                if (ray.currentPosition.X > GlobalVariables.MapWidth || ray.currentPosition.Y > GlobalVariables.MapHeight)
                                    break;
                                pfound = pixelstoprovinces[ray.currentPosition.X, ray.currentPosition.Y];
                            }
                            while (pfound == null || pfound == p);

                            if (!p.BorderingProvinces.Contains(pfound))
                                p.BorderingProvinces.Add(pfound);
                        }
                    }
                    */
                });
                raycast.Start();
                progress.UpdateProgress(23, 0);
                await tradegoodsAll;
                if (tradegoodsAll.IsFaulted)
                    progress.UpdateProgress(1, 1);
                else if (tradegoodsAll.IsCompleted)
                    progress.UpdateProgress(1, 2);

                await lcultures;
                if (lcultures.IsFaulted)
                    progress.UpdateProgress(4, 1);
                else if (lcultures.IsCompleted)
                    progress.UpdateProgress(4, 2);
                await lreligions;
                if (lreligions.IsFaulted)
                    progress.UpdateProgress(5, 1);
                else if (lreligions.IsCompleted)
                    progress.UpdateProgress(5, 2);
                await lgovernments;
                if (lgovernments.IsFaulted)
                    progress.UpdateProgress(6, 1);
                else if (lgovernments.IsCompleted)
                    progress.UpdateProgress(6, 2);
                await ltechnology;
                if (ltechnology.IsFaulted)
                    progress.UpdateProgress(7, 1);
                else if (ltechnology.IsCompleted)
                    progress.UpdateProgress(7, 2);
                await ltags;
                if (ltags.IsFaulted)
                    progress.UpdateProgress(8, 1);
                else if (ltags.IsCompleted)
                    progress.UpdateProgress(8, 2);
                //DONE
                Task lcomcountries = new Task(() =>
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

                            if(colorn == null)
                            {
                                progress.ReportError($"Error: Country '{c.FullName}' has no color specified!");
                            }
                            else
                            {
                                string[] colort = colorn.GetPureValuesAsArray();
                                if(colort.Count() < 3)
                                {
                                    progress.ReportError($"Error: Country '{c.FullName}' has incorrect number of color values specified!");
                                }
                                else
                                {
                                    if(colort[0].Contains(".") || colort[1].Contains(".") || colort[2].Contains("."))
                                    {
                                        double r = 0;
                                        double g = 0;
                                        double b = 0;
                                        if (!double.TryParse(colort[0], NumberStyles.Any, CultureInfo.InvariantCulture , out r))
                                            progress.ReportError($"Error: Country '{c.FullName}' has incorrect color values!");
                                        else if (!double.TryParse(colort[1], NumberStyles.Any, CultureInfo.InvariantCulture, out g))
                                            progress.ReportError($"Error: Country '{c.FullName}' has incorrect color values!");
                                        else if (!double.TryParse(colort[2], NumberStyles.Any, CultureInfo.InvariantCulture, out b))
                                            progress.ReportError($"Error: Country '{c.FullName}' has incorrect color values!");
                                        else
                                            c.Color = Color.FromArgb((int)(r*255), (int)(g * 255), (int)(b * 255));
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
                });
                lcomcountries.Start();
                progress.UpdateProgress(10, 0);

                await lcomcountries;
                if (lcomcountries.IsFaulted)
                    progress.UpdateProgress(10, 1);
                else if (lcomcountries.IsCompleted)
                    progress.UpdateProgress(10, 2);
                //DONE
                Task lcountries = new Task(() =>
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
                                        if(g == null)
                                        {
                                            progress.ReportError($"Error: Government '{v.Value}' in country '{c.FullName}' wasn't found!");
                                        }
                                        c.Government = g;                                                                              
                                        break;
                                    case "add_government_reform":
                                        c.GovernmentReform = v.Value;
                                        break;
                                    case "technology_group":
                                        if(!GlobalVariables.TechGroups.Contains(v.Value.ToLower()))
                                            progress.ReportError($"Error: Technology group '{v.Value}' in country '{c.FullName}' wasn't found!");
                                        c.TechnologyGroup = v.Value;
                                        break;
                                    case "capital":
                                        int val = 0;
                                        if (int.TryParse(v.Value, out val))
                                        {
                                            c.CapitalID = val;
                                            Province p = GlobalVariables.Provinces.Find(x => x.ID == c.CapitalID);
                                            if(p != null)
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
                                        if(r == null)
                                        {
                                            progress.ReportError($"Error: Religion '{v.Value}' in country '{c.FullName}' not found!");
                                        }
                                        c.Religion = r;
                                        break;
                                    case "primary_culture":
                                        Culture cl = Culture.Cultures.Find(x => x.Name.ToLower() == v.Value.ToLower());
                                        if(cl == null)
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
                });
                lcountries.Start();
                progress.UpdateProgress(9, 0);

                //DONE
                Task lprovhistory = new Task(() =>
                {
                    try
                    {
                        List<NodeFile> Files = new List<NodeFile>();
                        if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.historyProvinces] != 0)
                        {
                            if (!Directory.Exists(GlobalVariables.pathtomod + "history\\provinces\\"))
                            {
                                progress.ReportError($"Error: Directory '{GlobalVariables.pathtomod + "history\\provinces\\"}' not found!");
                            }
                            else
                            {
                                foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "history\\provinces\\"))
                                {
                                    if (file.Contains('.'))
                                    {
                                        if (file.Split('.').Last() == "txt")
                                        {
                                            try
                                            {
                                                NodeFile nf = new NodeFile(file);
                                                if (nf.LastStatus.HasError)
                                                    progress.ReportError($"Critical error: File '{file}' has an error in line {nf.LastStatus.LineError}");
                                                else
                                                {
                                                    Files.Add(nf);
                                                }
                                            }
                                            catch
                                            {
                                                progress.ReportError($"File '{file}' is unusable!");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.historyProvinces] != 1)
                        {
                            if (!Directory.Exists(GlobalVariables.pathtogame + "history\\provinces\\"))
                            {
                                progress.ReportError($"Error: Directory '{GlobalVariables.pathtogame + "history\\provinces\\"}' not found!");
                            }
                            else
                            {
                                foreach (string file in Directory.GetFiles(GlobalVariables.pathtogame + "history\\provinces\\"))
                                {
                                    if (file.Contains('.'))
                                    {
                                        if (file.Split('.').Last() == "txt")
                                        {
                                            if (!Files.Any(x => x.Path.Split('\\').Last() == file.Split('\\').Last()))
                                            {
                                                try
                                                {
                                                    NodeFile nf = new NodeFile(file, true);
                                                    if (nf.LastStatus.HasError)
                                                        progress.ReportError($"Critical error: File '{file}' has an error in line {nf.LastStatus.LineError}");
                                                    else
                                                        Files.Add(nf);
                                                }
                                                catch
                                                {
                                                    progress.ReportError($"File '{file}' is unusable!");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        foreach (NodeFile file in Files)
                        {
                            string tocheck = file.Path.Split('\\').Last();
                            string created = "";
                            
                            for(int a = 0; a < tocheck.Length; a++)
                            {
                                if (char.IsDigit(tocheck[a]))
                                    created += tocheck[a];
                                else
                                    break;
                            }
                            bool b = int.TryParse(created, out int id);
                            if (b)
                            {
                                Province province = GlobalVariables.Provinces.Find(x => x.ID == id);
                                if (province == null)
                                {
                                    progress.ReportError($"Alert: File '{file.FileName}' couldn't be loaded because it's not in definition.csv!");
                                    continue;
                                }

                                province.HistoryFile = file;
                                NodeFile nodefile = file;
                                if (nodefile.MainNode.Variables.Any())
                                    GlobalVariables.TotalUsableProvinces++;

                                //CHECK
                                ReadProvinceValuesFromNode(province, GlobalVariables.CurrentDate, nodefile.MainNode, progress);


                                //TODO
                                //RESTRUCTURE DATE NODES HERE !!!!

                                foreach (Node dateNode in nodefile.MainNode.Nodes)
                                {
                                    if (dateNode.Name.Contains("."))
                                    {
                                        try
                                        {
                                            if (dateNode.Name.Where(x => x == '.').Count() >= 2)
                                            {

                                                int y = 0;
                                                int m = 0;
                                                int d = 0;

                                                if (!int.TryParse(dateNode.Name.Split('.')[0], out y))
                                                    progress.ReportError($"Error: Date entry '{dateNode.Name.Split('.')[0]}.{dateNode.Name.Split('.')[1]}.{dateNode.Name.Split('.')[2]}' in province '{province.ID}' is incorrect!");
                                                else if (!int.TryParse(dateNode.Name.Split('.')[1], out m))
                                                    progress.ReportError($"Error: Date entry '{dateNode.Name.Split('.')[0]}.{dateNode.Name.Split('.')[1]}.{dateNode.Name.Split('.')[2]}' in province '{province.ID}' is incorrect!");
                                                else if (!int.TryParse(dateNode.Name.Split('.')[2], out d))
                                                    progress.ReportError($"Error: Date entry '{dateNode.Name.Split('.')[0]}.{dateNode.Name.Split('.')[1]}.{dateNode.Name.Split('.')[2]}' in province '{province.ID}' is incorrect!");
                                                else
                                                {
                                                    DateTime date = new DateTime(y, m, d);
                                                    ReadProvinceValuesFromNode(province, date, dateNode, progress);
                                                }
                                            }
                                        }
                                        catch
                                        {
                                            progress.ReportError($"Alert: Date entry '{dateNode.Name}' in province {id} is incorrect! Ignoring.");
                                        }
                                    }
                                }


                                if (province.TradeGood != null)
                                {
                                    province.TradeGood.TotalDev += province.Tax + province.Production + province.Manpower;
                                }
                                if (province.LatentTradeGood != null)
                                {
                                    province.TradeGood.TotalDev += province.Tax + province.Production + province.Manpower;
                                }

                                
                            }
                        }

                        if (GlobalVariables.pathtomod != "")
                        {
                            foreach (Province p in GlobalVariables.Provinces)
                            {
                                if (p.HistoryFile == null)
                                {
                                    string s = GlobalVariables.pathtomod + "history\\provinces\\" + (p.ID) + ".txt";
                                    NodeFile nf = new NodeFile(s, dontread: true);
                                    p.HistoryFile = nf;
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Critical error: Unexpected issue with province history! Program will exit after continuing!");
                        progress.ReportError(e.ToString());
                        throw new Exception();
                    }
                });
                lprovhistory.Start();
                progress.UpdateProgress(11, 0);

                await lcountries;
                if (lcountries.IsFaulted)
                    progress.UpdateProgress(9, 1);
                else if (lcountries.IsCompleted)
                    progress.UpdateProgress(9, 2);



                await lprovhistory;
                if (lprovhistory.IsFaulted)
                    progress.UpdateProgress(11, 1);
                else if (lprovhistory.IsCompleted)
                    progress.UpdateProgress(11, 2);
                await larea;
                if (larea.IsFaulted)
                    progress.UpdateProgress(12, 1);
                else if (larea.IsCompleted)
                    progress.UpdateProgress(12, 2);
                //DONE
                Task lregion = new Task(() =>
                {
                    try
                    {
                        if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.region] > 0)
                            regions = new NodeFile(GlobalVariables.pathtomod + "map\\region.txt");
                        else
                            regions = new NodeFile(GlobalVariables.pathtogame + "map\\region.txt");

                        if (regions.LastStatus.HasError)
                            progress.ReportError($"Critical error: File '{regions}' has an error in line {regions.LastStatus.LineError}");
                        else
                        {
                            foreach (Node n in regions.MainNode.Nodes)
                            {
                                List<Area> ar = new List<Area>();
                                Node nd = n.Nodes.Find(x => x.Name.ToLower() == "areas");
                                if (nd != null)
                                {
                                    foreach (PureValue vr in nd.PureValues)
                                    {
                                        if (vr.Name != "")
                                        {
                                            Area are = Area.Areas.Find(x => x.Name.ToLower() == vr.Name.ToLower());
                                            if (are != null)
                                            {
                                                if (are.Region != null)
                                                {
                                                    if (are.Region == Region.NoRegion)
                                                    {
                                                        are.Region.Areas.Remove(are);
                                                    }
                                                    else
                                                    {
                                                        progress.ReportError($"Alert: Area '{are}' is part of two regions. '{n.Name}' and '{are.Region}'. Picking second.");
                                                    }
                                                }
                                                ar.Add(are);
                                            }
                                            else
                                            {
                                                progress.ReportError($"Error: Region '{n.Name}' has unknown area '{vr.Name}'.");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    progress.ReportError($"Alert: Region '{n.Name}' has no areas.");
                                }

                                Region r = new Region(n.Name, ar);
                                r.OriginalName = n.Name;
                                foreach (Area are in r.Areas)
                                    are.Region = r;
                            }
                        }
                    }
                    catch
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Critical error: Unexpected issue with regions! Program will exit after continuing!");
                    }
                });
                lregion.Start();
                progress.UpdateProgress(13, 0);

                await lregion;
                if (lregion.IsFaulted)
                    progress.UpdateProgress(13, 1);
                else if (lregion.IsCompleted)
                    progress.UpdateProgress(13, 2);
                //DONE
                Task lsuperregion = new Task(() =>
                {
                    try
                    {
                        if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.superregion] > 0)
                            Superregions = new NodeFile(GlobalVariables.pathtomod + "map\\superregion.txt");
                        else
                            Superregions = new NodeFile(GlobalVariables.pathtogame + "map\\superregion.txt");

                        if (Superregions.LastStatus.HasError)
                            progress.ReportError($"Critical error: File '{Superregions.Path}' has an error in line {Superregions.LastStatus.LineError}");

                        foreach (Node n in Superregions.MainNode.Nodes)
                        {
                            List<Region> reg = new List<Region>();
                            bool res = false;
                            foreach (PureValue s in n.PureValues)
                            {
                                if(s.Name.ToLower() == "restrict_charter")
                                {
                                    res = true;
                                    continue;
                                }
                                Region r = Region.Regions.Find(x => x.Name.ToLower() == s.Name.ToLower());
                                if (r != null)
                                {
                                    if(r.Superregion != null)
                                    {
                                        if(r.Superregion == Superregion.NoSuperregion)
                                        {
                                            r.Superregion.Regions.Remove(r);
                                        }
                                        else
                                        {
                                            progress.ReportError($"Alert: Region '{r}' is part of two superregions. '{r.Name}' and '{r.Superregion}'. Picking second.");
                                        }
                                    }
                                    reg.Add(r);
                                }
                                else
                                {
                                    progress.ReportError($"Error: Superregion '{n.Name}' has unknown region '{s.Name}'.");
                                }
                            }
                            Superregion sr = new Superregion(n.Name, reg);
                            sr.RestrictCharter = res;
                            sr.OriginalName = n.Name;
                            foreach (Region re in sr.Regions)
                                re.Superregion = sr;
                        }
                    }
                    catch
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Critical error: Unexpected issue with superregions! Program will exit after continuing!");
                    }
                });
                lsuperregion.Start();
                progress.UpdateProgress(16, 0);





                await lcontinent;
                if (lcontinent.IsFaulted)
                    progress.UpdateProgress(14, 1);
                else if (lcontinent.IsCompleted)
                    progress.UpdateProgress(14, 2);
                await ltradenodes;
                if (ltradenodes.IsFaulted)
                    progress.UpdateProgress(15, 1);
                else if (ltradenodes.IsCompleted)
                    progress.UpdateProgress(15, 2);
                await lsuperregion;
                if (lsuperregion.IsFaulted)
                    progress.UpdateProgress(16, 1);
                else if (lsuperregion.IsCompleted)
                    progress.UpdateProgress(16, 2);



                await ltradecomapnies;
                if (ltradecomapnies.IsFaulted)
                    progress.UpdateProgress(21, 1);
                else if (ltradecomapnies.IsCompleted)
                    progress.UpdateProgress(21, 2);

                await lclimate;
                if (lclimate.IsFaulted)
                    progress.UpdateProgress(24, 1);
                else if (lclimate.IsCompleted)
                    progress.UpdateProgress(24, 2);


                await llocalisation;
                if (llocalisation.IsFaulted)
                    progress.UpdateProgress(22, 1);
                else if (llocalisation.IsCompleted)
                    progress.UpdateProgress(22, 2);

                Task umapmisc = new Task(() =>
                {
                    foreach (Province p in GlobalVariables.Provinces)
                    {
                        p.BorderPixels = GraphicsMethods.CreateBorders(p);
                        p.NonBorderPixels = p.Pixels.Except(p.BorderPixels).ToList();
                    }
                    MapManagement.CreateClickMask();
                });
                umapmisc.Start();
                await umapmisc;

                List<Task> MapTasks = new List<Task>();
                progress.UpdateProgress(18, 0);
                Task umapdev = new Task(() =>
                {
                });
                umapdev.Start();
                MapTasks.Add(umapdev);

                Task ucontrol = new Task(() =>
                {
                    
                });
                ucontrol.Start();
                progress.UpdateProgress(19, 0);

                foreach (Task t in MapTasks)
                {
                    await t;
                }
                progress.UpdateProgress(18, 2);
                await ucontrol;
                if (ucontrol.IsFaulted)
                    progress.UpdateProgress(19, 1);
                else if (ucontrol.IsCompleted)
                    progress.UpdateProgress(19, 2);
                await lbuildings;
                if (lbuildings.IsFaulted)
                    progress.UpdateProgress(20, 1);
                else if (lbuildings.IsCompleted)
                    progress.UpdateProgress(20, 2);

                await raycast;
                if (raycast.IsFaulted)
                    progress.UpdateProgress(23, 1);
                else if (raycast.IsCompleted)
                    progress.UpdateProgress(23, 2);
                /*
                List<CountryModifier> modifiers = new List<CountryModifier>();
                modifiers.Add(new CountryModifier("drill_gain_modifier", "military", 3, 0.05, 1, false));
                modifiers.Add(new CountryModifier("drill_decay_modifier", "military", 2, 0.05, 0.75, false));
                modifiers.Add(new CountryModifier("infantry_cost", "military", 4, -0.05, -0.5, false));
                modifiers.Add(new CountryModifier("infantry_power", "military", 6, 0.05, 0.4, false));
                modifiers.Add(new CountryModifier("infantry_fire", "military", 9, 0.1, 2, false));
                modifiers.Add(new CountryModifier("infantry_shock", "military", 10, 0.1, 2, false));
                modifiers.Add(new CountryModifier("cavalry_cost", "military", 5, -0.05, -0.5, false));
                modifiers.Add(new CountryModifier("cavalry_power", "military", 6, 0.05, 0.5, false));
                modifiers.Add(new CountryModifier("cavalry_fire", "military", 10, 0.1, 2, false));
                modifiers.Add(new CountryModifier("cavalry_shock", "military", 9, 0.1, 2, false));
                modifiers.Add(new CountryModifier("artillery_cost", "military", 6, -0.05, -0.5, false));
                modifiers.Add(new CountryModifier("artillery_power", "military", 6, 0.05, 0.5, false));
                modifiers.Add(new CountryModifier("artillery_fire", "military", 10, 0.1, 2, false));
                modifiers.Add(new CountryModifier("artillery_shock", "military", 10, 0.1, 2, false));
                modifiers.Add(new CountryModifier("cav_to_inf_ratio", "military", 7, 0.1, 0.75, false));
                modifiers.Add(new CountryModifier("cavalry_flanking", "military", 6, 0.25, 1.5, false));
                modifiers.Add(new CountryModifier("artillery_bonus_vs_fort", "military", 8, 1, 2, true));
                modifiers.Add(new CountryModifier("backrow_artillery_damage", "military", 7, 0.05, 0.5, false));
                modifiers.Add(new CountryModifier("discipline", "military", 8, 0.025, 0.15, false));
                modifiers.Add(new CountryModifier("mercenary_discipline", "military", 5, 0.05, 0.15, false));
                modifiers.Add(new CountryModifier("land_morale", "military", 8, 0.05, 0.3, false));
                modifiers.Add(new CountryModifier("defensiveness", "military", 4, 0.05, 0.5, false));
                modifiers.Add(new CountryModifier("siege_ability", "military", 6, 0.05, 0.5, false));
                modifiers.Add(new CountryModifier("movement_speed", "military", 5, 0.05, 0.5, false));
                modifiers.Add(new CountryModifier("fire_damage", "military", 7, 0.05, 0.25, false));
                modifiers.Add(new CountryModifier("fire_damage_received", "military", 7, -0.05, -0.25, false));
                modifiers.Add(new CountryModifier("shock_damage", "military", 8, 0.05, 0.25, false));
                modifiers.Add(new CountryModifier("shock_damage_received", "military", 8, -0.05, -0.25, false));
                modifiers.Add(new CountryModifier("recover_army_morale_speed", "military", 4, 0.05, 0.5, false));
                modifiers.Add(new CountryModifier("siege_blockade_progress", "military", 10, 1, 2, true));
                modifiers.Add(new CountryModifier("reserves_organisation", "military", 6, -0.05, -0.50, false));
                modifiers.Add(new CountryModifier("land_attrition", "military", 6, -0.05, -0.5, false));
                modifiers.Add(new CountryModifier("reinforce_cost_modifier", "military", 6, -0.05, -0.5, false));
                */

                List<string> modifiers = new List<string>()
                {
                "can_not_build_colonies", "can_not_build_buildings", "can_not_build_missionaries", "can_not_declare_war",
                "can_not_send_merchants", "capped_by_forcelimit", "can_claim_states", "free_concentrate_development", "build_cost",
                "local_build_cost", "build_time", "local_build_time", "local_unrest", "global_unrest", "development_cost",
                "development_cost_modifier", "local_development_cost", "local_development_cost_modifier", "trade_efficiency",
                "province_trade_power_value", "province_trade_power_modifier", "global_prov_trade_power_modifier",
                "production_efficiency", "local_production_efficiency", "trade_goods_size", "trade_goods_size_modifier",
                "raze_power_gain", "monarch_power_tribute", "tributary_conversion_cost_modifier", "expand_infrastructure_cost_modifier",
                "max_absolutism_effect", "centralize_state_cost", "local_centralize_state_cost", "land_morale_constant",
                "naval_morale_constant", "max_general_shock", "max_general_fire", "max_general_maneuver", "max_general_siege",
                "max_admiral_shock", "max_admiral_fire", "max_admiral_maneuver", "max_admiral_siege", "coast_raid_range",
                "development_cost_in_primary_culture", "reduced_trade_penalty_on_non_main_tradenode", "colony_cost_modifier",
                "local_colony_cost_modifier", "spy_action_cost_modifier", "placed_merchant_power_modifier",
                "reduced_liberty_desire_on_other_continent", "overextension_impact_modifier", "artillery_level_modifier",
                "local_tolerance_of_heretics", "local_tolerance_of_heathens", "tax_income", "global_tax_income", "local_tax_modifier",
                "global_tax_modifier", "stability_cost_modifier", "inflation_reduction", "inflation_reduction_local", "interest",
                "colonists", "missionaries", "merchants", "diplomats", "global_trade_power", "global_foreign_trade_power",
                "global_own_trade_power", "colonist_placement_chance", "local_colonist_placement_chance", "global_missionary_strength",
                "local_missionary_strength", "land_morale", "naval_morale", "local_manpower", "global_manpower",
                "local_manpower_modifier", "global_manpower_modifier", "manpower_recovery_speed", "morale_damage_received",
                "morale_damage", "military_tactics", "local_sailors", "global_sailors", "local_sailors_modifier",
                "global_sailors_modifier", "sailors_recovery_speed", "land_forcelimit", "naval_forcelimit", "overlord_naval_forcelimit",
                "overlord_naval_forcelimit_modifier", "land_forcelimit_modifier", "naval_forcelimit_modifier",
                "land_maintenance_modifier", "naval_maintenance_modifier", "merc_maintenance_modifier", "fort_maintenance_modifier",
                "local_fort_maintenance_modifier", "mercenary_cost", "infantry_cost", "cavalry_cost", "artillery_cost",
                "heavy_ship_cost", "light_ship_cost", "galley_cost", "transport_cost", "infantry_power", "cavalry_power",
                "artillery_power", "heavy_ship_power", "light_ship_power", "galley_power", "transport_power", "attrition",
                "hostile_attrition", "artillery_barrage_cost", "transport_attrition", "land_attrition", "naval_attrition",
                "max_attrition", "max_hostile_attrition", "supply_limit", "war_exhaustion", "war_exhaustion_cost",
                "local_hostile_attrition", "army_tradition", "navy_tradition", "army_tradition_decay", "navy_tradition_decay",
                "leader_land_fire", "leader_land_shock", "leader_naval_fire", "leader_naval_shock", "leader_siege",
                "leader_naval_manuever", "leader_land_manuever", "state_maintenance_modifier", "local_state_maintenance_modifier",
                "global_spy_defence", "spy_offence", "trade_value", "fort_level", "blockade_efficiency", "ship_recruit_speed",
                "regiment_recruit_speed", "global_ship_recruit_speed", "global_regiment_recruit_speed", "prestige", "prestige_decay",
                "prestige_from_land", "prestige_from_naval", "trade_value_modifier", "garrison_growth", "global_garrison_growth",
                "advisor_cost", "advisor_pool", "female_advisor_chance", "technology_cost", "discipline", "reinforce_speed", "range",
                "global_colonial_growth", "local_colonial_growth", "tolerance_own", "tolerance_heretic", "tolerance_heathen",
                "defensiveness", "local_defensiveness", "global_ship_cost", "global_ship_repair", "global_regiment_cost",
                "global_tariffs", "diplomatic_reputation", "papal_influence", "devotion", "legitimacy", "horde_unity",
                "republican_tradition", "monthly_splendor", "local_ship_cost", "local_ship_repair", "local_regiment_cost",
                "local_friendly_movement_speed", "local_hostile_movement_speed", "trade_range_modifier",
                "global_heretic_missionary_strength", "global_heathen_missionary_strength", "improve_relation_modifier",
                "trade_steering", "all_power_cost", "core_creation", "enemy_core_creation", "free_leader_pool", "idea_cost",
                "heir_chance", "embargo_efficiency", "recover_army_morale_speed", "recover_navy_morale_speed", "diplomatic_upkeep",
                "unjustified_demands", "mercenary_manpower", "fabricate_claims_cost", "claim_duration", "regiment_manpower_usage",
                "all_estate_influence_modifier", "justify_trade_conflict_cost", "rebel_support_efficiency",
                "discovered_relations_impact", "annexation_relations_impact", "vassal_income", "religious_unity",
                "inflation_action_cost", "migration_cost", "add_tribal_land_cost", "settle_cost", "monthly_fervor_increase",
                "monthly_piety", "monthly_piety_accelerator", "monthly_karma", "monthly_karma_accelerator",
                "global_rebel_suppression_efficiency", "caravan_power", "yearly_corruption", "min_autonomy", "global_autonomy",
                "min_local_autonomy", "local_autonomy", "siege_ability", "privateer_efficiency", "global_trade_goods_size_modifier",
                "global_trade_goods_size", "envoy_travel_time", "imperial_authority", "imperial_authority_value", "imperial_mandate",
                "ae_impact", "province_warscore_cost", "supply_limit_modifier", "global_supply_limit_modifier", "national_focus_years",
                "vassal_forcelimit_bonus", "vassal_naval_forcelimit_bonus", "vassal_manpower_bonus", "vassal_sailors_bonus",
                "years_of_nationalism", "local_years_of_nationalism", "num_accepted_cultures", "culture_conversion_cost",
                "local_culture_conversion_cost", "culture_conversion_time", "local_culture_conversion_time",
                "diplomatic_annexation_cost", "chance_to_inherit", "ship_durability", "liberty_desire", "reduced_liberty_desire",
                "allowed_num_of_buildings", "global_allowed_num_of_buildings", "allowed_num_of_manufactories",
                "global_allowed_num_of_manufactories", "church_power_modifier", "monthly_church_power", "garrison_size",
                "local_garrison_size", "loot_amount", "embracement_cost", "local_institution_spread", "global_institution_spread",
                "native_uprising_chance", "native_assimilation", "may_recruit_female_generals", "block_introduce_heir",
                "can_transfer_vassal_wargoal", "can_chain_claim", "free_maintenance_on_expl_conq", "colony_development_boost",
                "attack_bonus_in_capital_terrain", "can_bypass_forts", "ignore_coring_distance", "force_march_free",
                "possible_condottieri", "global_ship_trade_power", "local_naval_engagement_modifier",
                "global_naval_engagement_modifier", "global_naval_engagement", "block_slave_raid", "may_perform_slave_raid",
                "may_perform_slave_raid_on_same_religion", "cavalry_flanking", "movement_speed", "capture_ship_chance",
                "sunk_ship_morale_hit_recieved", "naval_tradition_from_battle", "army_tradition_from_battle", "local_core_creation",
                "immortal", "amount_of_banners", "local_amount_of_banners", "has_banners", "local_has_banners", "has_carolean",
                "local_has_carolean", "amount_of_carolean", "local_amount_of_carolean", "can_recruit_hussars", "amount_of_hussars",
                "local_amount_of_hussars", "hussars_cost_modifier", "free_land_leader_pool", "free_navy_leader_pool", "amount_of_cawa",
                "local_amount_of_cawa", "fire_damage", "shock_damage", "fire_damage_received", "shock_damage_received",
                "reinforce_cost_modifier", "garrison_damage", "local_garrison_damage", "assault_fort_cost_modifier",
                "local_assault_fort_cost_modifier", "assault_fort_ability", "local_assault_fort_ability",
                "local_religious_conversion_resistance", "global_religious_conversion_resistance", "placed_merchant_power",
                "ship_power_propagation", "institution_spread_from_true_faith", "prestige_per_development_from_conversion",
                "administrative_efficiency", "yearly_absolutism", "max_absolutism", "core_decay_on_your_own", "autonomy_change_time",
                "expand_administration_cost", "rival_change_cost", "rival_border_fort_maintenance", "harsh_treatment_cost",
                "reduced_liberty_desire_on_same_continent", "backrow_artillery_damage", "enforce_religion_cost",
                "liberty_desire_from_subject_development", "monarch_admin_power", "monarch_diplomatic_power", "monarch_military_power",
                "local_heir_adm", "local_heir_dip", "local_heir_mil", "artillery_levels_available_vs_fort", "country_admin_power",
                "country_diplomatic_power", "country_military_power", "meritocracy", "yearly_harmony", "harmonization_speed",
                "cav_to_inf_ratio", "local_monthly_devastation", "global_monthly_devastation", "global_prosperity_growth",
                "local_prosperity_growth", "monthly_favor_modifier", "monthly_gold_inflation_modifier",
                "gold_depletion_chance_modifier", "local_gold_depletion_chance_modifier", "tolerance_of_heretics_capacity",
                "tolerance_of_heathens_capacity", "move_capital_cost_modifier", "war_taxes_cost_modifier", "siege_blockade_progress",
                "warscore_cost_vs_other_religion", "mercenary_discipline", "sailor_maintenance_modifer", "yearly_army_professionalism",
                "general_cost", "reserves_organisation", "drill_gain_modifier", "drill_decay_modifier", "same_culture_advisor_cost",
                "same_religion_advisor_cost", "promote_culture_cost", "own_coast_naval_combat_bonus",
                "local_own_coast_naval_combat_bonus", "global_defender_dice_roll_bonus", "local_defender_dice_roll_bonus",
                "global_attacker_dice_roll_bonus", "local_attacker_dice_roll_bonus", "own_territory_dice_roll_bonus",
                "can_revoke_parliament_seats", "parliament_backing_chance", "parliament_effect_duration", "parliament_debate_duration",
                "parliament_chance_of_decision", "num_of_parliament_issues", "max_possible_parliament_seats", "institution_growth",
                "innovativeness_gain", "possible_policy", "free_policy", "possible_adm_policy", "possible_dip_policy",
                "possible_mil_policy", "free_adm_policy", "free_dip_policy", "free_mil_policy", "adm_advisor_cost", "dip_advisor_cost",
                "mil_advisor_cost", "reform_progress_growth", "reform_progress_growth_building", "monthly_reform_progress",
                "monthly_reform_progress_modifier", "monthly_reform_progress_building", "min_autonomy_in_territories",
                "reelection_cost", "leader_cost", "candidate_random_bonus", "election_cycle", "monthly_support_heir_gain",
                "power_projection_from_insults", "local_religious_unity_contribution", "trade_company_investment_cost",
                "cawa_cost_modifier", "janissary_cost_modifier", "blockade_force_required", "hostile_disembark_speed",
                "hostile_fleet_attrition", "regiment_disembark_speed", "allowed_tercio_fraction", "amount_of_tercio",
                "local_has_tercio", "allowed_musketeer_fraction", "amount_of_musketeers", "local_has_musketeers",
                "allowed_samurai_fraction", "amount_of_samurai", "local_has_samurai", "allowed_geobukseon_fraction",
                "amount_of_geobukseon", "local_has_geobukseon", "allowed_man_of_war_fraction", "amount_of_man_of_war",
                "local_has_man_of_war", "allowed_galleon_fraction", "amount_of_galleon", "local_has_galleon",
                "allowed_galleass_fraction", "amount_of_galleass", "local_has_galleass", "allowed_caravel_fraction",
                "amount_of_caravel", "local_has_caravel", "allowed_voc_indiamen_fraction", "amount_of_voc_indiamen",
                "local_has_voc_indiamen", "allowed_marine_fraction", "can_recruit_janissaries", "can_recruit_cawa",
                "can_recruit_cossacks", "can_recruit_rajputs", "can_recruit_revolutionary_guards", "allow_janissaries_from_own_faith",
                "allow_mercenary_drill", "merc_leader_army_tradition", "merc_independent_from_trade_range",
                "allow_mercenaries_to_split", "may_explore", "sea_repair", "cb_on_government_enemies", "cb_on_primitives",
                "no_religion_penalty", "auto_explore_adjacent_to_colony", "reduced_stab_impacts", "extra_manpower_at_religious_war",
                "idea_claim_colonies", "may_establish_frontier", "can_fabricate_for_vassals", "cb_on_overseas",
                "reduced_native_attacks", "may_not_reduce_inflation", "no_cost_for_reinforcing", "may_build_supply_depot",
                "may_refill_garrison", "may_return_manpower_on_disband", "may_not_convert_territories", "allow_client_states",
                "enable_forced_march", "number_of_cannons_modifier", "heavy_ship_number_of_cannons_modifier",
                "light_ship_number_of_cannons_modifier", "galley_number_of_cannons_modifier", "transport_number_of_cannons_modifier",
                "hull_size", "hull_size_modifier", "heavy_ship_hull_size_modifier", "light_ship_hull_size_modifier",
                "galley_hull_size_modifier", "transport_hull_size_modifier", "engagement_cost", "engagement_cost_modifier",
                "special_unit_cost_modifier", "special_unit_manpower_cost_modifier", "number_of_cannons_flagship_modifier",
                "number_of_cannons_flagship", "number_of_cannons", "max_flagships", "flagship_morale",
                "naval_maintenance_flagship_modifier", "trade_power_in_fleet_modifier", "ship_trade_power", "ship_trade_power_modifier",
                "can_transport_units", "flagship_naval_engagement_modifier", "blockade_impact_on_siege_in_fleet_modifier",
                "movement_speed_in_fleet_modifier", "flagship_durability", "morale_in_fleet_modifier",
                "exploration_mission_range_in_fleet_modifier", "barrage_cost_in_fleet_modifier", "naval_attrition_in_fleet_modifier",
                "cannons_for_hunting_pirates_in_fleet", "movement_speed_onto_off_boat_modifier", "admiral_skill_gain_modifier",
                "privateering_efficiency_in_fleet_modifier", "prestige_from_battles_in_fleet_modifier",
                "naval_tradition_in_fleet_modifier", "landing_penalty", "establish_order_cost", "treasure_fleet_income",
                "global_naval_barrage_cost", "center_of_trade_upgrade_cost", "local_center_of_trade_upgrade_cost",
                "missionary_maintenance_cost", "local_missionary_maintenance_cost", "naval_tradition_from_trade", "admiral_cost",
                "expel_minorities_cost", "infantry_fire", "cavalry_fire", "artillery_fire", "infantry_shock", "cavalry_shock",
                "artillery_shock", "curia_treasury_contribution", "cb_on_religious_enemies", "yearly_patriarch_authority",
                "yearly_authority", "yearly_karma_decay", "available_province_loot", "relation_with_heretics", "relation_with_heathens",
                "relation_with_same_religion", "reverse_relation_with_same_religion", "relation_with_same_culture",
                "relation_with_same_culture_group", "relation_with_accepted_culture", "relation_with_other_culture",
                "stability_cost_to_declare_war", "special_unit_forcelimit", "curia_powers_cost", "appoint_cardinal_cost",
                "papal_influence_from_cardinals", "unrest_catholic_provinces", "imperial_reform_catholic_approval",
                "disengagement_chance", "manpower_in_true_faith_provinces", "manpower_in_own_culture_provinces",
                "manpower_in_culture_group_provinces", "manpower_in_accepted_culture_provinces", "free_city_imperial_authority",
                "imperial_mercenary_cost", "max_free_cities", "max_electors", "manpower_against_imperial_enemies", "monarch_lifespan",
                "max_revolutionary_zeal", "yearly_revolutionary_zeal", "flagship_cost", "governing_capacity",
                "governing_capacity_modifier", "governing_cost", "trade_company_governing_cost", "state_governing_cost",
                "territories_governing_cost", "local_governing_cost", "local_governing_cost_increase", "state_governing_cost_increase",
                "statewide_governing_cost", "reasons_to_elect", "years_to_integrate_personal_union", "legitimate_subject_elector",
                "accept_vassalization_reasons", "transfer_trade_power_reasons", "local_warscore_cost_modifier", "mercantilism_cost",
                "tribal_development_growth", "monthly_federation_favor_growth", "monthly_heir_claim_increase",
                "monthly_heir_claim_increase_modifier", "great_project_upgrade_cost", "local_great_project_upgrade_cost",
                "great_project_upgrade_time", "local_great_project_upgrade_time", "colonial_type_change_cost_modifier",
                "colonial_subject_type_upgrade_cost_modifier", "yearly_doom_reduction", "all_estate_loyalty_equilibrium",
                "loyalty_change_on_revoked", "estate_interaction_cooldown_modifier", "all_estate_possible_privileges",
                "no_stability_loss_on_monarch_death", "allow_free_estate_privilege_revocation", "warscore_from_battles_modifier",
                "yearly_innovativeness", "yearly_government_power", "no_claim_cost_increasement", "naval_morale_damage",
                "naval_morale_damage_received", "has_tercio", "has_musketeer", "has_samurai", "has_geobukseon", "has_man_of_war",
                "has_galleon", "has_galleass", "has_caravel", "has_voc_indiamen", "has_streltsy", "allowed_streltsy_fraction",
"amount_of_streltsy", "local_has_streltsy",
                };

                GlobalVariables.CountryModifiers.AddRange(modifiers);

                GlobalVariables.FullyLoaded = true;
            }
            catch (Exception e)
            {
                progress.ReportError(e.ToString());
                progress.MakeContinueAvailable();
                if (GlobalVariables.__DEBUG)
                    throw;
                
            }
        
        }

        public static void ReadLocalisationFiles(LoadingProgress progress)
        {
            if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.localisation] != 1)
            {
                ReadLocalisationDirectory(progress, false);
            }
            if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.localisation] != 0)
            {
                ReadLocalisationDirectory(progress, true);
            }

            string ModEdtFileName = GlobalVariables.GetModEdtLocName();
            LocalisationFile EdtFile = GlobalVariables.LocalisationFiles.Find(x => x.Filename == ModEdtFileName);

            if (EdtFile == null)
            {
                EdtFile = new LocalisationFile();
                EdtFile.Filename = ModEdtFileName;
                GlobalVariables.LocalisationFiles.Add(EdtFile);
            }

            GlobalVariables.ModLocalisationFile = EdtFile;

        }

        public static void ReadLocalisationDirectory(LoadingProgress progress, bool mod)
        {
            string[] splitValues;
            string[] apostrophSplit;
            string LocPath = mod ? GlobalVariables.pathtomod : GlobalVariables.pathtogame;
            LocPath += "localisation\\";

            if (!Directory.Exists(LocPath))
            {
                progress.ReportError($"Error: Localisation directory missing! Expected path: {LocPath}");
                return;
            }

            try
            {
                foreach (string file in Directory.GetFiles(LocPath))
                {
                    if (file.Contains('.'))
                    {
                        if (file.Contains("l_english") && GlobalVariables.LocalisationLanguage != GlobalVariables.Languages.English)
                            continue;
                        if (file.Contains("l_french") && GlobalVariables.LocalisationLanguage != GlobalVariables.Languages.French)
                            continue;
                        if (file.Contains("l_spanish") && GlobalVariables.LocalisationLanguage != GlobalVariables.Languages.Spanish)
                            continue;
                        if (file.Contains("l_german") && GlobalVariables.LocalisationLanguage != GlobalVariables.Languages.German)
                            continue;
                        if (!file.Contains("l_english") && !file.Contains("l_french") && !file.Contains("l_spanish") && !file.Contains("l_german"))
                            continue;
                        if (file.Split('.')[1] != "yml")
                            continue;

                        LocalisationFile locFile = new LocalisationFile();
                        locFile.Filename = Path.GetFileName(file);
                     
                        string[] allLines = File.ReadAllLines(file, Encoding.GetEncoding(1252));
                        for (int lineNumber = 1; lineNumber < allLines.Length; lineNumber++)
                        {
                            string linetoread = allLines[lineNumber].Split('#')[0];
                            if (string.IsNullOrWhiteSpace(linetoread))
                                continue;
                            if (!linetoread.Contains("\""))
                            {
                                progress.ReportError($"Alert: Strange line number {lineNumber} in localisation file '{Path.GetFileName(file)}'. Skipping.");
                                continue;
                            }
                            try
                            {
                                splitValues = linetoread.Split(':');
                                splitValues[0] = splitValues[0].Trim();
                                if (string.IsNullOrWhiteSpace(splitValues[0]))
                                {
                                    progress.ReportError($"Alert: Strange line number {lineNumber} in localisation file '{Path.GetFileName(file)}'. Skipping.");
                                    continue;
                                }
                                if (splitValues.Count() < 2)
                                {
                                    progress.ReportError($"Alert: Strange line number {lineNumber} in localisation file '{Path.GetFileName(file)}'. Skipping.");
                                    continue;
                                }
                                apostrophSplit = splitValues[1].Split('"');
                                if (apostrophSplit.Count() < 2)
                                {
                                    progress.ReportError($"Alert: Strange line number {lineNumber} in localisation file '{Path.GetFileName(file)}'. Skipping.");
                                    continue;
                                }
                                if (!locFile.TryChange(splitValues[0], apostrophSplit[1]))
                                {
                                    locFile.AddNew(splitValues[0], apostrophSplit[1]);
                                }
                            }
                            catch
                            {
                                if (GlobalVariables.__DEBUG)
                                    throw;
                                progress.ReportError($"Critical error: Localisation issue! -> {Path.GetFileName(file)} -> Line '{lineNumber}' is invalid!");
                            }
                        }

                        //To not save it again
                        locFile.Changed = false;

                        LocalisationFile existing = GlobalVariables.LocalisationFiles.Find(x => x.Filename == locFile.Filename);
                        if(existing != null)
                        {
                            GlobalVariables.LocalisationFiles.Remove(existing);
                        }

                        GlobalVariables.LocalisationFiles.Add(locFile);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                if (GlobalVariables.__DEBUG)
                    throw;
                progress.ReportError("Error: No access to localisation files! Program will exit after continuing");
            }
        }

        public static void LoadFiles()
        {
            
            LoadingProgress lp = new LoadingProgress();
            //BackgroundWorker bw = new BackgroundWorker();
            //bw.WorkerReportsProgress = true;
            //bw.DoWork += LoadFilesWork;
            //bw.RunWorkerCompleted += WorkerDone;
            //bw.ProgressChanged += lp.UpdateProgressLabel;
            //bw.RunWorkerAsync();
            Task loadfile = new Task(() => { LoadFilesWork(lp); });
            loadfile.Start();
            lp.ShowDialog();         
            //LoadFilesSync();
            
        }

        public static void ReadProvinceValuesFromNode(Province province, DateTime date, Node n, LoadingProgress progress = null)
        {
            GlobalVariables.CurrentDate = date;
            foreach (Variable v in n.Variables)
            {
                Building bl = GlobalVariables.Buildings.Find(x => x.Name.ToLower() == v.Name.ToLower());
                if (bl != null && v.Value.ToLower().Trim() == "yes")
                {
                    province.AddBuilding(bl, date, true);
                }
                else if(bl != null && v.Value == "no")
                    province.RemoveBuilding(bl, date, true);
                switch (v.Name)
                {
                    case "add_core":
                        province.AddCore(v.Value.ToUpper(), date, true);
                        if (v.Value == "---")
                            continue;
                        if (progress != null)
                        {
                            if (!GlobalVariables.Countries.Any(x => x.Tag == v.Value.ToUpper()))
                                progress.ReportError($"Alert: Province {province.ID} has unknown country tag in cores {v.Value.ToUpper()}");
                        }
                        break; 
                    case "add_claim":
                        province.AddClaim(v.Value.ToUpper(),date, true);
                        if (v.Value == "---")
                            continue;
                        if (progress != null)
                        {
                            if (!GlobalVariables.Countries.Any(x => x.Tag == v.Value.ToUpper()))
                                progress.ReportError($"Alert: Province {province.ID} has unknown country tag in claims {v.Value.ToUpper()}");
                        }
                        break;
                    case "owner":
                        //if (province.OwnerCountry != null && DateTime.Compare(GlobalVariables.StartDate, date)==0)
                        //    province.OwnerCountry.Provinces.Remove(province);
                        if (v.Value == "---")
                            province.OwnerCountry = null;
                        else
                        {
                            Country c = GlobalVariables.Countries.Find(x => x.Tag == v.Value.ToUpper());
                            if (c == null)
                            {
                                if (progress != null)
                                {
                                    progress.ReportError($"Error: Province {province.ID} has unknown owner '{v.Value.ToUpper()}'");
                                }
                            }
                            if (c != null && DateTime.Compare(GlobalVariables.StartDate, date) >= 0)
                            {
                                if (province.OwnerCountry != null) {
                                    province.OwnerCountry.Provinces.Remove(province);
                                }
                                province.OwnerCountry = c;
                                c.Provinces.Add(province);
                            }
                        }
                        break;
                    case "controller":
                        if (v.Value == "---")
                            province.Controller = null;
                        else
                        {
                            Country cont = GlobalVariables.Countries.Find(x => x.Tag == v.Value.ToUpper());
                            if (cont == null)
                            {
                                if (progress != null)
                                {
                                    progress.ReportError($"Error: Province {province.ID} has unknown controller '{v.Value.ToUpper()}'");
                                }
                            }
                            else
                                province.Controller = cont;
                        }
                        break;
                    case "culture":
                        Culture cul = Culture.Cultures.Find(x => x.Name.ToLower() == v.Value.ToLower());
                        if(cul == null)
                        {
                            if (progress != null)
                                progress.ReportError($"Error: Province {province.ID} has unknown culture '{v.Value.ToUpper()}'");
                        }
                        else
                            province.Culture = cul;

                        break;
                    case "religion":
                        Religion r = Religion.Religions.Find(x => x.Name.ToLower() == v.Value.Replace("\"", "").ToLower());
                        if (r == null)
                        {
                            if (progress != null)
                                progress.ReportError($"Error: Province {province.ID} has unknown religion '{v.Value.ToUpper()}'");
                        }
                        else {
                            province.Religion = r;
                        }
                        break;
                    case "hre":
                        if (v.Value == "yes")
                            province.HRE = true;
                        else
                            province.HRE = false;
                        break;
                    case "base_tax":
                        int tax = 0;
                        if(!int.TryParse(v.Value, out tax))
                        {
                            progress.ReportError($"Error: Province {province.ID} has unexpected value '{v.Value.ToUpper()}' in base tax!");
                        }
                        province.Tax = tax;
                        break;
                    case "base_production":
                        int production = 0;
                        if (!int.TryParse(v.Value, out production))
                        {
                            progress.ReportError($"Error: Province {province.ID} has unexpected value '{v.Value.ToUpper()}' in base production!");
                        }
                        province.Production = production;
                        break;
                    case "base_manpower":
                        int manpower = 0;
                        if (!int.TryParse(v.Value, out manpower))
                        {
                            progress.ReportError($"Error: Province {province.ID} has unexpected value '{v.Value.ToUpper()}' in base manpower!");
                        }
                        province.Manpower = manpower;
                        break;
                    case "trade_goods":
                        TradeGood tg = GlobalVariables.TradeGoods.Find(x => x.Name.ToLower() == v.Value.ToLower());                        
                        province.TradeGood = tg;
                        if (province.TradeGood != null && GlobalVariables.CurrentDate == GlobalVariables.StartDate)
                        {
                            province.TradeGood.TotalProvinces++;
                        }
                        else if(province.TradeGood == null)
                        {
                            progress.ReportError($"Error: Province {province.ID} has unknown trade good '{v.Value.ToUpper()}'!");
                        }
                        break;
                    case "capital":
                        province.Capital = v.Value.Replace("\"", "");
                        break;
                    case "center_of_trade":
                        int cot = 0;
                        if(!int.TryParse(v.Value, out cot))
                        {
                            progress.ReportError($"Error: Province {province.ID} has unexpected value '{v.Value.ToUpper()}' as a center of trade!");
                        }
                        province.CenterOfTrade = cot;
                        break;
                    case "discovered_by":
                        province.AddDiscoveredBy(v.Value,date, true);
                        break;
                    case "is_city":
                        if (v.Value == "yes")
                            province.City = true;
                        else
                            province.City = false;
                        break;
                }
            }

            Node revoltNode = n.Nodes.Find(x => x.Name.ToLower() == "revolt");
            if(revoltNode != null)
            {
                ProvinceDateEntry.Revolt revolt = new ProvinceDateEntry.Revolt();
                if(revoltNode.TryGetVariableValue("type", out string outputType))                
                    revolt.Type = outputType;
                if (revoltNode.TryGetVariableValue("size", out string outputSize))
                    if (int.TryParse(outputSize, out int intSize))
                        revolt.Size = intSize;
                if (revoltNode.TryGetVariableValue("name", out string outputName))
                    revolt.Name = outputName;
                if (revoltNode.TryGetVariableValue("leader", out string outputLeader))
                    revolt.Leader = outputLeader;
                province.AddRevolt(date,revolt);
            }


            Node ltgn = n.Nodes.Find(x => x.Name.ToLower() == "latent_trade_goods");
            if (ltgn != null)
            {
                if (ltgn.PureValues.Any())
                {
                    province.LatentTradeGood = GlobalVariables.TradeGoods.Find(x => x.Name.ToLower() == ltgn.PureValues[0].Name.Trim().ToLower());
                    if(province.LatentTradeGood != null && GlobalVariables.CurrentDate == GlobalVariables.StartDate)
                        province.LatentTradeGood.TotalProvinces++;
                    else if(province.LatentTradeGood == null)
                    {
                        progress.ReportError($"Error: Latent trade good in province {province.ID} is incorrect! Ignoring.");
                    }
                }
            }
            GlobalVariables.CurrentDate = GlobalVariables.StartDate;
        }
    }

    public class ProvinceNeighbourRay
    {
        public Point currentPosition = new Point();

        public int MoveX = 1;
        public int MoveY = 1;
     
        int CurrentDistance = 0;
        bool UsingY = false;

        public void PerformMove()
        {
            if (UsingY)
            {
                if(MoveY > 0)
                    currentPosition.Y++;
                else if (MoveY < 0)
                    currentPosition.Y--;
                CurrentDistance++;
                if (CurrentDistance >= Math.Abs(MoveY)) {
                    UsingY = false;
                    CurrentDistance = 0;
                }
            }
            else
            {
                if(MoveX > 0)
                    currentPosition.X++;
                else if(MoveX < 0)
                    currentPosition.X--;
                CurrentDistance++;
                if (CurrentDistance >= Math.Abs(MoveX))
                {
                    UsingY = true;
                    CurrentDistance = 0;
                }
            }
            
        }
    }
}
