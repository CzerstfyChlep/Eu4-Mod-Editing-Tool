using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public static async void LoadFilesWork(LoadingProgress progress)
        {
            try
            {
                //BackgroundWorker bw = (BackgroundWorker)sender;
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

                    if (GlobalVariables.UseMod[20] != 1)
                    {
                        if (!Directory.Exists(GlobalVariables.pathtogame + "localisation\\"))
                            progress.ReportError($"Localisation directory missing! Expected path: {GlobalVariables.pathtogame + "localisation\\"}");
                        else
                        {
                            try
                            {
                                foreach (string file in Directory.GetFiles(GlobalVariables.pathtogame + "localisation\\"))
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
                                        if (file.Split('.')[1] == "yml")
                                        {
                                            foreach (string line in File.ReadAllLines(file, Encoding.Default))
                                            {
                                                string linetoread = line.Split('#')[0];

                                                if (linetoread.Contains("\""))
                                                {
                                                    string name = "";
                                                    string value = "";
                                                    try
                                                    {
                                                        name = linetoread.Split(':')[0].Trim();
                                                        value = linetoread.Split(':')[1].Split('"')[1];
                                                        if (GlobalVariables.LocalisationEntries.Keys.Contains(name))
                                                            GlobalVariables.LocalisationEntries[name] = value;
                                                        else
                                                            GlobalVariables.LocalisationEntries.Add(name, value);
                                                    }
                                                    catch
                                                    {
                                                        if (GlobalVariables.__DEBUG)
                                                            throw;
                                                        progress.ReportError($"Localisation issue! -> { Path.GetFileName(file) } -> Line '{line}' is invalid!");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (UnauthorizedAccessException)
                            {
                                if (GlobalVariables.__DEBUG)
                                    throw;
                                progress.ReportError("No access to localisation files! Program will exit after continuing");
                            }
                        }
                    }


                    if (GlobalVariables.UseMod[20] != 0)
                    {
                        foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "localisation\\"))
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
                                if (file.Split('.')[1] == "yml")
                                {
                                    foreach (string line in File.ReadAllLines(file, Encoding.Default))
                                    {
                                        string linetoread = line.Split('#')[0];

                                        if (linetoread.Contains("\""))
                                        {
                                            string name = "";
                                            string value = "";
                                            try
                                            {
                                                name = linetoread.Split(':')[0].Trim();
                                                value = linetoread.Split(':')[1].Split('"')[1];
                                                if (GlobalVariables.ModLocalisationEntries.Keys.Contains(name))
                                                    GlobalVariables.ModLocalisationEntries[name] = value;
                                                else
                                                    GlobalVariables.ModLocalisationEntries.Add(name, value);
                                            }
                                            catch
                                            {
                                                if (GlobalVariables.__DEBUG)
                                                    throw;
                                                progress.ReportError($"Localisation issue! -> { Path.GetFileName(file) } -> Line '{line}' is invalid!");
                                            }

                                        }

                                    }

                                }
                            }
                        }
                    }
                });
                llocalisation.Start();
                progress.UpdateProgress(22, 0);

                Task ldefinition = new Task(() =>
                {
                    try
                    {
                        StreamReader Reader;
                        if (GlobalVariables.UseMod[0] > 0)
                            Reader = new StreamReader(GlobalVariables.pathtomod + "map\\definition.csv");
                        else
                            Reader = new StreamReader(GlobalVariables.pathtogame + "map\\definition.csv");

                        while (!Reader.EndOfStream)
                        {
                            string data = Reader.ReadLine();
                            if (data.Contains("province;red;"))
                                continue;
                            string[] values = data.Split(';');
                            Province p = new Province(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]), int.Parse(values[3]), values[4]);
                            GlobalVariables.CubeArray[int.Parse(values[1]), int.Parse(values[2]), int.Parse(values[3])] = p;
                            GlobalVariables.Provinces.Add(p);
                        }
                    }
                    catch
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Issue with definition.csv! Program will exit after continuing!");
                        throw new Exception();
                    }
                });
                ldefinition.Start();

                progress.UpdateProgress(0, 0);
                Task ltradegoods = new Task(() =>
                {

                    try
                    {
                        if (GlobalVariables.UseMod[2] != 0)
                        {
                            foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\tradegoods\\"))
                            {
                                if (file.Contains('.'))
                                {
                                    if (file.Split('.')[1] == "txt")
                                    {
                                        NodeFile nf = new NodeFile(file);
                                        if (nf.LastStatus.HasError)
                                            progress.ReportError($"File '{file}' has an error in line {nf.LastStatus.LineError}");
                                        else
                                        {
                                            tradegoodsfiles.Add(nf);
                                            GlobalVariables.ModTradeGoodsFiles.Add(nf);
                                        }
                                    }
                                }
                            }
                        }

                        if (GlobalVariables.UseMod[2] != 1)
                        {
                            foreach (string file in Directory.GetFiles(GlobalVariables.pathtogame + "common\\tradegoods\\"))
                            {
                                if (file.Contains('.'))
                                {
                                    if (file.Split('.')[1] == "txt")
                                    {
                                        NodeFile nf = new NodeFile(file, true);
                                        if (nf.LastStatus.HasError)
                                            progress.ReportError($"File '{file}' has an error in line {nf.LastStatus.LineError}");
                                        else
                                        {
                                            if (!tradegoodsfiles.Any(x => x.FileName == file.Split('\\').Last().Replace(".txt", "")))
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
                        progress.ReportError("Issue with trade good files! Program will exit after continuing!");
                        throw new Exception();
                    }
                });
                ltradegoods.Start();
                progress.UpdateProgress(1, 0);
                Task lcultures = new Task(() =>
                {
                    try
                    {
                        List<string> done = new List<string>();
                        if (GlobalVariables.UseMod[4] != 0)
                        {
                            foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\cultures\\"))
                            {
                                if (file.Contains('.'))
                                {
                                    if (file.Split('.')[1] == "txt")
                                    {
                                        NodeFile nf = new NodeFile(file);
                                        if (nf.LastStatus.HasError)
                                            progress.ReportError($"File '{file}' has an error in line {nf.LastStatus.LineError}");
                                        else
                                        {
                                            culturesfiles.Add(nf);
                                            GlobalVariables.ModCulturesFiles.Add(nf);
                                        }
                                    }
                                }
                            }
                        }
                        if (GlobalVariables.UseMod[4] != 1)
                        {
                            foreach (string file in Directory.GetFiles(GlobalVariables.pathtogame + "common\\cultures\\"))
                            {
                                if (file.Contains('.'))
                                {
                                    if (file.Split('.')[1] == "txt")
                                    {
                                        NodeFile nf = new NodeFile(file, true);
                                        if (nf.LastStatus.HasError)
                                            progress.ReportError($"File '{file}' has an error in line {nf.LastStatus.LineError}");
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
                        progress.ReportError("Issue with cultures! Program will exit after continuing!");
                        throw new Exception();
                    }

                });
                lcultures.Start();
                progress.UpdateProgress(4, 0);
                Task lreligions = new Task(() =>
                {
                    try
                    {
                        if (GlobalVariables.UseMod[5] != 0)
                        {
                            foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\religions\\"))
                            {
                                if (file.Contains('.'))
                                {
                                    if (file.Split('.')[1] == "txt")
                                    {
                                        NodeFile nf = new NodeFile(file);
                                        if (nf.LastStatus.HasError)
                                            progress.ReportError($"File '{file}' has an error in line {nf.LastStatus.LineError}");
                                        else
                                        {
                                            religionsfiles.Add(nf);
                                            GlobalVariables.ModReligionsFiles.Add(nf);
                                        }
                                    }
                                }
                            }
                        }
                        if (GlobalVariables.UseMod[5] != 1)
                        {
                            foreach (string file in Directory.GetFiles(GlobalVariables.pathtogame + "common\\religions\\"))
                            {
                                if (file.Contains('.'))
                                {
                                    if (file.Split('.')[1] == "txt")
                                    {
                                        NodeFile nf = new NodeFile(file, true);
                                        if (nf.LastStatus.HasError)
                                            progress.ReportError($"File '{file}' has an error in line {nf.LastStatus.LineError}");
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
                                            string[] colorstring = innernode.Nodes.Find(x => x.Name.ToLower() == "color").GetPureValuesAsArray();
                                            r.Color = Color.FromArgb(int.Parse(colorstring[0]), int.Parse(colorstring[1]), int.Parse(colorstring[2]));
                                            r.Icon = int.Parse(innernode.Variables.Find(x => x.Name.ToLower() == "icon").Value);
                                        }
                                        catch
                                        {

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
                        progress.ReportError("Issue with religions! Program will exit after continuing!");
                        throw new Exception();
                    }
                });
                lreligions.Start();
                progress.UpdateProgress(5, 0);
                Task lgovernments = new Task(() =>
                {
                    try
                    {
                        if (GlobalVariables.UseMod[16] != 0)
                        {
                            foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\governments\\"))
                            {
                                if (file.Contains('.'))
                                {
                                    if (file.Split('.')[1] == "txt")
                                    {
                                        NodeFile nf = new NodeFile(file);
                                        if (nf.LastStatus.HasError)
                                            progress.ReportError($"File '{file}' has an error in line {nf.LastStatus.LineError}");
                                        else
                                        {
                                            governmentsfiles.Add(nf);
                                            GlobalVariables.ModGovernmentsFiles.Add(nf);
                                        }
                                    }
                                }
                            }
                        }
                        if (GlobalVariables.UseMod[16] != 1)
                        {
                            foreach (string file in Directory.GetFiles(GlobalVariables.pathtogame + "common\\governments\\"))
                            {
                                if (file.Contains('.'))
                                {
                                    if (file.Split('.')[1] == "txt")
                                    {
                                        NodeFile nf = new NodeFile(file, true);
                                        if (nf.LastStatus.HasError)
                                            progress.ReportError($"File '{file}' has an error in line {nf.LastStatus.LineError}");
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

                                    gv.reforms.AddRange(n.Nodes.Find(x => x.Name.ToLower() == "reform_levels").Nodes[0].Nodes.Find(x => x.Name.ToLower() == "reforms").GetPureValuesAsArray());
                                    Node colornode = n.Nodes.Find(x => x.Name.ToLower() == "color");
                                    gv.Color = Color.FromArgb(int.Parse(colornode.PureValues[0].Name), int.Parse(colornode.PureValues[1].Name), int.Parse(colornode.PureValues[2].Name));
                                    GlobalVariables.Governments.Add(gv);
                                }
                            }
                        }
                    }
                    catch
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Issue with governments! Program will exit after continuing!");
                        throw new Exception();
                    }

                });
                lgovernments.Start();
                progress.UpdateProgress(6, 0);
                Task ltechnology = new Task(() =>
                {
                    try 
                    {
                        if (GlobalVariables.UseMod[15] > 0)
                            technology = new NodeFile(GlobalVariables.pathtomod + "common\\technology.txt");
                        else
                            technology = new NodeFile(GlobalVariables.pathtogame + "common\\technology.txt");

                        if (technology.LastStatus.HasError)
                            progress.ReportError($"File '{technology.Path}' has an error in line {technology.LastStatus.LineError}");
                        else
                        { 
                            foreach (Node node in technology.MainNode.Nodes.Find(x => x.Name.ToLower() == "groups").Nodes)
                            {
                                GlobalVariables.TechGroups.Add(node.Name);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Issue with techgroups! Program will exit after continuing!");
                        progress.ReportError(e.ToString());
                        throw new Exception();
                    }
                });
                ltechnology.Start();
                progress.UpdateProgress(7, 0);
                Task ltags = new Task(() =>
                {
                    try
                    {
                        if (GlobalVariables.UseMod[14] != 0)
                        {
                            foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\country_tags\\"))
                            {
                                if (file.Contains('.'))
                                {
                                    if (file.Split('.')[1] == "txt")
                                    {
                                        NodeFile nf = new NodeFile(file);
                                        if (nf.LastStatus.HasError)
                                            progress.ReportError($"File '{file}' has an error in line {nf.LastStatus.LineError}");
                                        else
                                        {
                                            countrytagsfiles.Add(nf);
                                            GlobalVariables.ModCountryTagsFiles.Add(nf);
                                        }
                                    }
                                }
                            }
                        }
                        if (GlobalVariables.UseMod[14] != 1)
                        {
                            foreach (string file in Directory.GetFiles(GlobalVariables.pathtogame + "common\\country_tags\\"))
                            {
                                if (file.Contains('.'))
                                {
                                    if (file.Split('.')[1] == "txt")
                                    {
                                        NodeFile nf = new NodeFile(file, true);
                                        if (nf.LastStatus.HasError)
                                            progress.ReportError($"File '{file}' has an error in line {nf.LastStatus.LineError}");
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

                        foreach (NodeFile countrytags in countrytagsfiles)
                        {
                            foreach (Variable v in countrytags.MainNode.Variables)
                            {
                                //TODO
                                //tolerate both slashes!!!!!!!!
                                string n = v.Value.Replace("\"", "").Trim().Split('/')[1].Split('.')[0];
                                if (!NameToTag.Keys.Contains(n))
                                    NameToTag.Add(n, v.Name.Trim());
                                if (!NameToFile.Keys.Contains(n))
                                    NameToFile.Add(n, countrytags);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Issue with tag files! Program will exit after continuing!");
                        progress.ReportError(e.ToString());
                        throw new Exception();
                    }
                });
                ltags.Start();
                progress.UpdateProgress(8, 0);
                Task lbuildings = new Task(() =>
                {
                    try
                    {
                        List<bool> GameFiles = new List<bool>();
                        if (GlobalVariables.UseMod[17] != 0)
                        {
                            foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\buildings\\"))
                            {
                                if (file.Contains('.'))
                                {
                                    if (file.Split('.')[1] == "txt")
                                    {
                                        NodeFile nf = new NodeFile(file);
                                        if (nf.LastStatus.HasError)
                                            progress.ReportError($"File '{file}' has an error in line {nf.LastStatus.LineError}");
                                        else
                                        {
                                            buildingsfiles.Add(nf);
                                        }
                                    }
                                }
                            }
                        }
                        if (GlobalVariables.UseMod[17] != 1)
                        {
                            foreach (string file in Directory.GetFiles(GlobalVariables.pathtogame + "common\\buildings\\"))
                            {
                                if (file.Contains('.'))
                                {
                                    if (file.Split('.')[1] == "txt")
                                    {
                                        NodeFile nf = new NodeFile(file, true);
                                        if (nf.LastStatus.HasError)
                                            progress.ReportError($"File '{file}' has an error in line {nf.LastStatus.LineError}");
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


                        foreach (NodeFile buildings in buildingsfiles)
                        {
                            foreach (Node node in buildings.MainNode.Nodes)
                            {
                                if (GlobalVariables.Buildings.Any(x => x.Name == node.Name))
                                    continue;
                                Building bl = new Building();
                                bl.Name = node.Name;
                                bl.NodeFile = buildings;
                                GlobalVariables.Buildings.Add(bl);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Issue with buildings! Program will exit after continuing!");
                        progress.ReportError(e.ToString());
                        throw new Exception();
                    }
                });
                lbuildings.Start();
                progress.UpdateProgress(20, 0);

                await ldefinition;
                if (ldefinition.IsFaulted)
                    progress.UpdateProgress(0, 1);
                else if (ldefinition.IsCompleted)
                    progress.UpdateProgress(0, 2);

                Task larea = new Task(() =>
                {
                    try
                    {
                        if (GlobalVariables.UseMod[9] > 0)
                            areas = new NodeFile(GlobalVariables.pathtomod + "map\\area.txt");
                        else
                            areas = new NodeFile(GlobalVariables.pathtogame + "map\\area.txt");
                        //bw.ReportProgress(92);

                        if (areas.LastStatus.HasError)
                            progress.ReportError($"File '{areas.Path}' has an error in line {areas.LastStatus.LineError}");
                        else
                        {

                            foreach (Node n in areas.MainNode.Nodes)
                            {
                                List<Province> pr = new List<Province>();
                                foreach (PureValue vr in n.PureValues)
                                {
                                    if (vr.Name != "")
                                    {
                                        pr.Add(GlobalVariables.Provinces.Find(x => x.ID == int.Parse(vr.Name)));
                                    }
                                }
                                Area a = new Area(n.Name, pr)
                                {
                                    OriginalName = n.Name
                                };
                                foreach (Province pro in a.Provinces)
                                    pro.Area = a;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Issue with areas! Program will exit after continuing!");
                        progress.ReportError(e.ToString());
                        throw new Exception();
                    }
                });
                larea.Start();
                progress.UpdateProgress(12, 0);
                Task lcontinent = new Task(() =>
                {
                    try
                    {
                        if (GlobalVariables.UseMod[13] > 0)
                            continents = new NodeFile(GlobalVariables.pathtomod + "map\\continent.txt");
                        else
                            continents = new NodeFile(GlobalVariables.pathtogame + "map\\continent.txt");
                        //bw.ReportProgress(100);
                        if (continents.LastStatus.HasError)
                            progress.ReportError($"File '{continents.Path}' has an error in line {continents.LastStatus.LineError}");
                        else
                        {
                            foreach (Node n in continents.MainNode.Nodes)
                            {
                                List<Province> ctp = new List<Province>();
                                foreach (PureValue s in n.PureValues)
                                {
                                    if (!GlobalVariables.Provinces.Any(x => x.ID == int.Parse(s.Name.Trim())))
                                        progress.ReportError($"In: continents.txt -> Province {s.Name.Trim()} isn't in the definition.csv!");
                                    else
                                        ctp.Add(GlobalVariables.Provinces.Find(x => x.ID == int.Parse(s.Name.Trim())));
                                }
                                Continent c = new Continent(n.Name, ctp);
                                c.OriginalName = n.Name;
                                foreach (Province pr in c.Provinces)
                                    pr.Continent = c;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Issue with continents! Program will exit after continuing!");
                        progress.ReportError(e.ToString());
                        throw new Exception();
                    }
                });
                lcontinent.Start();
                progress.UpdateProgress(14, 0);
                Task ltradenodes = new Task(() =>
                {
                    try
                    {
                        if (GlobalVariables.UseMod[12] != 0)
                        {
                            foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\tradenodes\\"))
                            {
                                if (file.Contains('.'))
                                {
                                    if (file.Split('.')[1] == "txt")
                                    {
                                        NodeFile nf = new NodeFile(file);
                                        if (nf.LastStatus.HasError)
                                            progress.ReportError($"File '{file}' has an error in line {nf.LastStatus.LineError}");
                                        else
                                        {
                                            tradenodesfiles.Add(nf);
                                            GlobalVariables.ModTradeNodesFiles.Add(nf);
                                        }
                                    }
                                }
                            }
                        }
                        if (GlobalVariables.UseMod[12] != 1)
                        {
                            foreach (string file in Directory.GetFiles(GlobalVariables.pathtogame + "common\\tradenodes\\"))
                            {
                                if (file.Contains('.'))
                                {
                                    if (file.Split('.')[1] == "txt")
                                    {
                                        NodeFile nf = new NodeFile(file, true);
                                        if (nf.LastStatus.HasError)
                                            progress.ReportError($"File '{file}' has an error in line {nf.LastStatus.LineError}");
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
                        //bw.ReportProgress(105);
                        foreach (NodeFile tradenodes in tradenodesfiles)
                        {
                            foreach (Node node in tradenodes.MainNode.Nodes)
                            {
                                if (GlobalVariables.TradeNodes.Any(x => x.Name == node.Name))
                                    continue;
                                Tradenode tn = new Tradenode();
                                tn.Name = node.Name;
                                tn.NodeFile = tradenodes;
                                GlobalVariables.TradeNodes.Add(tn);
                            }
                        }
                        string lastTradeNode = "";
                        string lastMember = "";

                        foreach (NodeFile tradenodes in tradenodesfiles)
                        {
                            foreach (Node node in tradenodes.MainNode.Nodes)
                            {
                                lastTradeNode = node.Name;
                                Tradenode tn = GlobalVariables.TradeNodes.Find(x => x.Name.ToLower() == node.Name.ToLower());
                                tn.NodeFile = tradenodes;
                                tn.Name = node.Name;

                                int location = 0;
                                if (!int.TryParse(node.Variables.Find(x => x.Name.ToLower() == "location").Value, out location))
                                {
                                    progress.ReportError($"Trade nodes: '{node.Name}' node has incorrect location: '{location}', using first valid node province member.");
                                    location = -1;
                                }
                                else if (GlobalVariables.Provinces.Count() <= location)
                                {
                                    progress.ReportError($"Trade nodes: Location of '{node.Name}' node is outside of province ID limit '{location}', using first valid node province member.");
                                    location = -1;
                                }
                                if (location == -1)
                                {
                                    foreach (PureValue value in node.Nodes.Find(x => x.Name.ToLower() == "members").PureValues)
                                    {
                                        if (!int.TryParse(value.Name, out location))
                                            continue;
                                        else if (GlobalVariables.Provinces.Count() <= location)
                                        {
                                            location = -1;
                                            continue;
                                        }
                                        else
                                            break;
                                    }
                                }
                                if (location == -1)
                                {
                                    progress.ReportError($"Trade nodes: Couldn't find a suitable location for '{node.Name}' node!");
                                }
                                else
                                {
                                    tn.Location = GlobalVariables.Provinces.Find(x => x.ID == location);
                                }
                                Node ColorNode = node.Nodes.Find(x => x.Name.ToLower() == "color");
                                if (ColorNode != null)
                                    tn.Color = Color.FromArgb(int.Parse(ColorNode.PureValues[0].Name), int.Parse(ColorNode.PureValues[1].Name), int.Parse(ColorNode.PureValues[2].Name));
                                else
                                    tn.Color = AdditionalElements.GenerateColor(GlobalVariables.GlobalRandom);

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
                                    Destination dn = new Destination() { TradeNode = GlobalVariables.TradeNodes.Find(x => x.Name.ToLower() == outgoing.Variables.Find(y => y.Name == "name").Value.Replace("\"", "").ToLower()) };
                                    dn.Path.AddRange(outgoing.Nodes.Find(x => x.Name.ToLower() == "path").GetPureValuesAsArray());
                                    if (outgoing.Nodes.Find(x => x.Name.ToLower() == "control") != null)
                                        dn.Control.AddRange(outgoing.Nodes.Find(x => x.Name.ToLower() == "control").GetPureValuesAsArray());
                                    tn.Destination.Add(dn);
                                    dn.TradeNode.Incoming.Add(tn);
                                }
                                foreach (PureValue value in node.Nodes.Find(x => x.Name.ToLower() == "members").PureValues)
                                {
                                    if (int.Parse(value.Name) <= GlobalVariables.Provinces.Count())
                                    {
                                        tn.Provinces.Add(GlobalVariables.Provinces.Find(x => x.ID == int.Parse(value.Name)));
                                        GlobalVariables.Provinces.Find(x => x.ID == int.Parse(value.Name)).TradeNode = tn;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Issue with tradenodes! Program will exit after continuing!");
                        progress.ReportError(e.ToString());
                        throw new Exception();
                    }
                });
                ltradenodes.Start();
                progress.UpdateProgress(15, 0);
                Task ldefaultmap = new Task(() =>
                {
                    try
                    {
                        NodeFile defaultmap;
                        if (GlobalVariables.UseMod[10] > 0)
                            defaultmap = new NodeFile(GlobalVariables.pathtomod + "map\\default.map");
                        else
                            defaultmap = new NodeFile(GlobalVariables.pathtogame + "map\\default.map");

                        if (defaultmap.LastStatus.HasError)
                            progress.ReportError($"File '{defaultmap.Path}' has an error in line {defaultmap.LastStatus.LineError}");
                        else
                        {

                            GlobalVariables.MapWidth = int.Parse(defaultmap.MainNode.Variables.Find(x => x.Name.ToLower() == "width").Value);
                            GlobalVariables.MapHeight = int.Parse(defaultmap.MainNode.Variables.Find(x => x.Name.ToLower() == "height").Value);

                            foreach (string sea in defaultmap.MainNode.Nodes.Find(x => x.Name.ToLower() == "sea_starts").GetPureValuesAsArray())
                            {
                                if (int.TryParse(sea, out int id))
                                {
                                    Province f = GlobalVariables.Provinces.Find(x => x.ID == id);
                                    if (f == null)
                                        progress.ReportError($"In default.map -> sea_starts -> Province with ID {id} is not in definition.csv!");
                                    else
                                        f.Sea = true;
                                }
                            }

                            foreach (string lake in defaultmap.MainNode.Nodes.Find(x => x.Name.ToLower() == "lakes").GetPureValuesAsArray())
                            {
                                if (int.TryParse(lake, out int id))
                                {
                                    Province f = GlobalVariables.Provinces.Find(x => x.ID == id);
                                    if (f == null)
                                        progress.ReportError($"In default.map -> lakes -> Province with ID {id} is not in definition.csv!");
                                    else
                                        f.Lake = true;
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Issue with default.map! Program will exit after continuing!");
                        progress.ReportError(e.ToString());
                        throw new Exception();
                    }
                });
                ldefaultmap.Start();
                progress.UpdateProgress(17, 0);

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
                                    //Province p = GlobalVariables.Provinces.Find(pr => pr.c == c);
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
                                //bw.ReportProgress(va);
                            }
                        }

                        bitmap.UnlockBits();
                    }
                    catch (Exception e)
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Issue with the map! Program will exit after continuing!");
                        progress.ReportError(e.ToString());
                        throw new Exception();
                    }
                });
                lmap.Start();
                progress.UpdateProgress(2, 0);

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
                                string[] colorv = node.Nodes.Find(x => x.Name.ToLower() == "color").GetPureValuesAsArray();
                                if (colorv.Count() == 3)
                                {
                                    double r = double.Parse(colorv[0], CultureInfo.InvariantCulture);
                                    if (r > 0 && r <= 1)
                                        r *= 255;
                                    double g = double.Parse(colorv[1], CultureInfo.InvariantCulture);
                                    if (g > 0 && g <= 1)
                                        g *= 255;
                                    double b = double.Parse(colorv[2], CultureInfo.InvariantCulture);
                                    if (b > 0 && b <= 1)
                                        b *= 255;
                                    tg.Color = Color.FromArgb((int)r, (int)g, (int)b);
                                }
                                else
                                {
                                    tg.Color = AdditionalElements.GenerateColor(GlobalVariables.GlobalRandom);
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
                        progress.ReportError("Issue with tradegoods! Program will exit after continuing!");
                        progress.ReportError(e.ToString());
                        throw new Exception();
                    }
                }))
                .ContinueWith(new Action<Task>(ac =>
                {
                    try
                    {
                        List<string> done = new List<string>();
                        if (GlobalVariables.UseMod[3] != 0)
                        {
                            foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\prices\\"))
                            {
                                if (file.Contains('.'))
                                {
                                    if (file.Split('.')[1] == "txt")
                                    {
                                        NodeFile nf = new NodeFile(file);
                                        if (nf.LastStatus.HasError)
                                            progress.ReportError($"File '{file}' has an error in line {nf.LastStatus.LineError}");
                                        else
                                        {
                                            tradegoodspricesfiles.Add(nf);
                                            GlobalVariables.ModPricesFiles.Add(nf);
                                        }
                                    }
                                }
                            }
                        }
                        if (GlobalVariables.UseMod[3] != 1)
                        {
                            foreach (string file in Directory.GetFiles(GlobalVariables.pathtogame + "common\\prices\\"))
                            {
                                if (file.Contains('.'))
                                {
                                    if (file.Split('.')[1] == "txt")
                                    {
                                        NodeFile nf = new NodeFile(file, true);
                                        if (nf.LastStatus.HasError)
                                            progress.ReportError($"File '{file}' has an error in line {nf.LastStatus.LineError}");
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
                                    tg.Price = double.Parse(node.Variables.Find(x => x.Name.ToLower() == "base_price").Value, CultureInfo.InvariantCulture);
                                    if (node.Variables.Find(x => x.Name.ToLower() == "goldtype") != null)
                                    {
                                        if (node.Variables.Find(x => x.Name.ToLower() == "goldtype").Value.Trim() == "yes")
                                            tg.GoldLike = true;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Issue with tradegoods! Program will exit after continuing!");
                        progress.ReportError(e.ToString());
                        throw new Exception();
                    }
                }));

                Task ltradecomapnies = new Task(() =>
                {
                    try
                    {
                        if (GlobalVariables.UseMod[19] != 0)
                        {
                            foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\trade_companies\\"))
                            {
                                if (file.Contains('.'))
                                {
                                    if (file.Split('.')[1] == "txt")
                                    {
                                        NodeFile nf = new NodeFile(file);
                                        if (nf.LastStatus.HasError)
                                            progress.ReportError($"File '{file}' has an error in line {nf.LastStatus.LineError}");
                                        else
                                        {
                                            tradecompanyfiles.Add(nf);
                                            GlobalVariables.ModTradeCompanyFiles.Add(nf);
                                        }
                                    }
                                }
                            }
                        }
                        if (GlobalVariables.UseMod[19] != 1)
                        {
                            foreach (string file in Directory.GetFiles(GlobalVariables.pathtogame + "common\\trade_companies\\"))
                            {
                                if (file.Contains('.'))
                                {
                                    if (file.Split('.')[1] == "txt")
                                    {
                                        NodeFile nf = new NodeFile(file, true);
                                        if (nf.LastStatus.HasError)
                                            progress.ReportError($"File '{file}' has an error in line {nf.LastStatus.LineError}");
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
                        foreach (NodeFile tradecompanies in tradecompanyfiles)
                        {
                            foreach (Node node in tradecompanies.MainNode.Nodes)
                            {
                                TradeCompany tc = GlobalVariables.TradeCompanies.Find(x => x.Name.ToLower() == node.Name.ToLower());
                                if (tc == null)
                                {
                                    tc = new TradeCompany() { Name = node.Name };
                                    GlobalVariables.TradeCompanies.Add(tc);
                                }
                                tc.NodeFile = tradecompanies;
                                Node ColorNode = node.Nodes.Find(x => x.Name.ToLower() == "color");
                                if (ColorNode != null)
                                    tc.Color = Color.FromArgb(int.Parse(ColorNode.PureValues[0].Name), int.Parse(ColorNode.PureValues[1].Name), int.Parse(ColorNode.PureValues[2].Name));
                                else
                                    tc.Color = AdditionalElements.GenerateColor(GlobalVariables.GlobalRandom);
                                foreach (PureValue value in node.Nodes.Find(x => x.Name.ToLower() == "provinces").PureValues)
                                {
                                    if (int.Parse(value.Name) <= GlobalVariables.Provinces.Count())
                                    {
                                        tc.Provinces.Add(GlobalVariables.Provinces.Find(x => x.ID == int.Parse(value.Name)));
                                        GlobalVariables.Provinces.Find(x => x.ID == int.Parse(value.Name)).TradeCompany = tc;
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
                        progress.ReportError("Issue with trade companies! Program will exit after continuing!");
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

                Task provincecentre = new Task(() =>
                {
                    try
                    {
                        foreach (Province p in GlobalVariables.Provinces)
                        {
                            if (p.Pixels.Any())
                            {
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
                        progress.ReportError("Issue with province centres! Program will exit after continuing!");
                        progress.ReportError(e.ToString());
                        throw new Exception();
                    }
                });
                provincecentre.Start();
                progress.UpdateProgress(3, 0);

                Task lclimate = new Task(() =>
                {
                    try
                    {
                        if (GlobalVariables.UseMod[21] > 0)
                            climate = new NodeFile(GlobalVariables.pathtomod + "map\\climate.txt");
                        else
                            climate = new NodeFile(GlobalVariables.pathtogame + "map\\climate.txt");

                        if (climate.LastStatus.HasError)
                            progress.ReportError($"File '{climate.Path}' has an error in line {climate.LastStatus.LineError}");

                        else
                        {


                            Node tropical = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "tropical");
                            if (tropical != null)
                            {
                                foreach (PureValue pv in tropical.PureValues)
                                {
                                    GlobalVariables.Provinces.Find(x => x.ID == int.Parse(pv.Name)).Climate = 1;
                                }
                            }
                            Node arid = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "arid");
                            if (arid != null)
                            {
                                foreach (PureValue pv in arid.PureValues)
                                {
                                    GlobalVariables.Provinces.Find(x => x.ID == int.Parse(pv.Name)).Climate = 2;
                                }
                            }
                            Node arctic = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "arctic");
                            if (arctic != null)
                            {
                                foreach (PureValue pv in arctic.PureValues)
                                {
                                    GlobalVariables.Provinces.Find(x => x.ID == int.Parse(pv.Name)).Climate = 3;
                                }
                            }


                            Node mild_winter = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "mild_winter");
                            if (mild_winter != null)
                            {
                                foreach (PureValue pv in mild_winter.PureValues)
                                {
                                    GlobalVariables.Provinces.Find(x => x.ID == int.Parse(pv.Name)).Winter = 1;
                                }
                            }
                            Node normal_winter = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "normal_winter");
                            if (normal_winter != null)
                            {
                                foreach (PureValue pv in normal_winter.PureValues)
                                {
                                    GlobalVariables.Provinces.Find(x => x.ID == int.Parse(pv.Name)).Winter = 2;
                                }
                            }
                            Node severe_winter = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "severe_winter");
                            if (severe_winter != null)
                            {
                                foreach (PureValue pv in severe_winter.PureValues)
                                {
                                    GlobalVariables.Provinces.Find(x => x.ID == int.Parse(pv.Name)).Winter = 3;
                                }
                            }



                            Node impassable = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "impassable");
                            if (impassable != null)
                            {
                                foreach (PureValue pv in impassable.PureValues)
                                {
                                    GlobalVariables.Provinces.Find(x => x.ID == int.Parse(pv.Name)).Impassable = 1;
                                }
                            }



                            Node mild_monsoon = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "mild_monsoon");
                            if (mild_monsoon != null)
                            {
                                foreach (PureValue pv in mild_monsoon.PureValues)
                                {
                                    GlobalVariables.Provinces.Find(x => x.ID == int.Parse(pv.Name)).Monsoon = 1;
                                }
                            }
                            Node normal_monsoon = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "normal_monsoon");
                            if (normal_monsoon != null)
                            {
                                foreach (PureValue pv in normal_monsoon.PureValues)
                                {
                                    GlobalVariables.Provinces.Find(x => x.ID == int.Parse(pv.Name)).Monsoon = 2;
                                }
                            }
                            Node severe_monsoon = climate.MainNode.Nodes.Find(x => x.Name.ToLower() == "severe_monsoon");
                            if (severe_monsoon != null)
                            {
                                foreach (PureValue pv in severe_monsoon.PureValues)
                                {
                                    GlobalVariables.Provinces.Find(x => x.ID == int.Parse(pv.Name)).Monsoon = 3;
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Issue with climate! Program will exit after continuing!");
                        progress.ReportError(e.ToString());
                        throw new Exception();
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

                Task lcomcountries = new Task(() =>
                {
                    try
                    {
                        List<bool> GameFiles = new List<bool>();
                        if (GlobalVariables.UseMod[7] != 0)
                        {
                            foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\countries\\"))
                            {
                                if (file.Contains('.'))
                                {
                                    if (file.Split('.')[1] == "txt")
                                    {
                                        NodeFile nf = new NodeFile(file);
                                        if (nf.LastStatus.HasError)
                                            progress.ReportError($"File '{file}' has an error in line {nf.LastStatus.LineError}");
                                        else
                                        {
                                            CountryCommonFiles.Add(nf);
                                        }
                                    }
                                }
                            }
                        }
                        if (GlobalVariables.UseMod[7] != 1)
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
                                                progress.ReportError($"File '{file}' has an error in line {nf.LastStatus.LineError}");
                                            else
                                            {
                                                CountryCommonFiles.Add(nf);
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

                            //c.Tag = tag;
                            //c.OriginalTag = tag;
                            //c.FullName = name;
                            //c.OriginalFullName = name;

                            c.FullName = name;
                            c.OriginalFullName = name;
                            c.Tag = NameToTag[name];
                            c.OriginalTag = c.Tag;

                            string[] colort = nodefile.MainNode.Nodes.Find(x => x.Name.ToLower() == "color").GetPureValuesAsArray();
                            try
                            {
                                c.Color = Color.FromArgb(int.Parse(colort[0]), int.Parse(colort[1]), int.Parse(colort[2]));
                            }
                            catch
                            {
                                c.Color = AdditionalElements.GenerateColor(GlobalVariables.GlobalRandom);
                                MessageBox.Show("Some error in " + file);
                            }

                            Variable gfxcul = nodefile.MainNode.Variables.Find(x => x.Name.ToLower() == "graphical_culture");
                            if (gfxcul != null)
                                c.GraphicalCulture = gfxcul.Value;

                            Node monarchNamesNode = nodefile.MainNode.Nodes.Find(x => x.Name.ToLower() == "monarch_names");
                            if (monarchNamesNode != null)
                                foreach (Variable monarchName in monarchNamesNode.Variables)
                                    if (!c.MonarchNames.Any(x => x.Name == monarchName.Name.Replace("\"", "").Trim()))
                                        c.MonarchNames.Add(new MonarchName(monarchName.Name.Replace("\"", "").Trim(), int.Parse(monarchName.Value)));
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
                        progress.ReportError("Issue with common countries! Program will exit after continuing!");
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

                Task lcountries = new Task(() =>
                {
                    try
                    {
                        List<NodeFile> CountryHistoryFiles = new List<NodeFile>();

                        if (GlobalVariables.UseMod[11] != 0)
                        {
                            foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "history\\countries\\"))
                            {
                                if (file.Contains('.'))
                                {
                                    if (file.Split('.')[1] == "txt")
                                    {
                                        NodeFile nf = new NodeFile(file);
                                        if (nf.LastStatus.HasError)
                                            progress.ReportError($"File '{file}' has an error in line {nf.LastStatus.LineError}");
                                        else
                                        {
                                            CountryHistoryFiles.Add(nf);
                                        }
                                    }
                                }
                            }
                        }
                        if (GlobalVariables.UseMod[11] != 1)
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
                                                progress.ReportError($"File '{file}' has an error in line {nf.LastStatus.LineError}");
                                            else
                                            {
                                                CountryHistoryFiles.Add(nf);
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
                            string name = "";
                            try
                            {
                                tag = fs.Split('-')[0].Trim().ToUpper();
                                name = fs.Split('-')[1].Trim().Split('.')[0];
                            }
                            catch
                            {
                                tag = fs.Trim().ToUpper();
                            }

                            Country c = GlobalVariables.Countries.Find(x => x.Tag == tag);
                            if (c == null)
                                continue;

                            c.HistoryFile = file;
                            NodeFile nodefile = file;
                            foreach (Variable v in nodefile.MainNode.Variables)
                            {
                                switch (v.Name)
                                {
                                    case "government":
                                        c.Government = GlobalVariables.Governments.Find(x => x.Type.ToLower() == v.Value.ToLower());
                                        break;
                                    case "add_government_reform":
                                        c.GovernmentReform = v.Value;
                                        break;
                                    case "technology_group":
                                        c.TechnologyGroup = v.Value;
                                        break;
                                    case "capital":
                                        c.CapitalID = int.Parse(v.Value);
                                        c.Capital = GlobalVariables.Provinces.Find(x => x.ID == c.CapitalID);
                                        break;
                                    case "religion":
                                        c.Religion = Religion.Religions.Find(x => x.Name.ToLower() == v.Value.ToLower());
                                        break;
                                    case "primary_culture":
                                        c.PrimaryCulture = Culture.Cultures.Find(x => x.Name.ToLower() == v.Value.ToLower());
                                        break;
                                    case "government_rank ":
                                        c.GovernmentRank = int.Parse(v.Value);
                                        break;
                                }
                            }

                            //c.Tag = tag;
                            //c.OriginalTag = tag;
                            //c.FullName = name;
                            //c.OriginalFullName = name;
                        }


                        foreach (Country c in GlobalVariables.Countries)
                        {
                            if (c != Country.NoCountry)
                            {
                                if (c.HistoryFile == null)
                                {
                                    c.HistoryFile = new NodeFile(GlobalVariables.pathtomod + $"history\\countries\\{c.Tag} - {c.FullName}.txt");
                                    if (c.HistoryFile.LastStatus.HasError)
                                        progress.ReportError($"File '{c.HistoryFile.Path}' has an error in line {c.HistoryFile.LastStatus.LineError}");

                                    c.HistoryFile.SaveFile();
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Issue with history countries! Program will exit after continuing!");
                        progress.ReportError(e.ToString());
                        throw new Exception();
                    }
                });
                lcountries.Start();
                progress.UpdateProgress(9, 0);

                Task lprovhistory = new Task(() =>
                {
                    try
                    {
                        List<NodeFile> Files = new List<NodeFile>();
                        if (GlobalVariables.UseMod[8] != 0)
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
                                                progress.ReportError($"File '{file}' has an error in line {nf.LastStatus.LineError}");
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
                        if (GlobalVariables.UseMod[8] != 1)
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
                                                    progress.ReportError($"File '{file}' has an error in line {nf.LastStatus.LineError}");
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
                        //bw.ReportProgress(88);
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
                                    progress.ReportError($"Province history issue! -> {file.FileName} -> Province of this ID wasn't specified in definition.csv!");
                                    continue;
                                }

                                //NOT POSING ANY ERRORS
                                province.HistoryFile = file;
                                NodeFile nodefile = file;
                                if (nodefile.MainNode.Variables.Any())
                                    GlobalVariables.TotalUsableProvinces++;

                                ReadProvinceValuesFromNode(province, nodefile.MainNode, progress);

                                foreach (Node dateNode in nodefile.MainNode.Nodes)
                                {
                                    if (dateNode.Name.Contains("."))
                                    {
                                        try
                                        {
                                            if (dateNode.Name.Where(x => x == '.').Count() >= 2)
                                            {
                                                DateTime date = new DateTime(int.Parse(dateNode.Name.Split('.')[0]), int.Parse(dateNode.Name.Split('.')[1]), int.Parse(dateNode.Name.Split('.')[2]));
                                                if (DateTime.Compare(date, GlobalVariables.StartDate) <= 0)
                                                {
                                                    ReadProvinceValuesFromNode(province, dateNode);
                                                }
                                            }
                                        }
                                        catch
                                        {
                                            progress.ReportError($"Date entry '{dateNode.Name}' in province {id} is incorrect! Ignoring.");
                                        }
                                    }
                                }

                                if (province.TradeGood != null)
                                {
                                    province.TradeGood.TotalDev += province.Tax + province.Production + province.Manpower;
                                }

                                Node n = nodefile.MainNode.Nodes.Find(x => x.Name.ToLower() == "latent_trade_goods");
                                if (n != null)
                                {
                                    if (n.PureValues.Any())
                                    {
                                        province.LatentTradeGood = GlobalVariables.TradeGoods.Find(x => x.Name.ToLower() == n.PureValues[0].Name.Trim().ToLower());
                                        if (province.LatentTradeGood != null)
                                        {
                                            province.LatentTradeGood.TotalProvinces++;
                                            province.TradeGood.TotalDev += province.Tax + province.Production + province.Manpower;
                                        }
                                    }
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
                                    //File.Create(s);
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
                        progress.ReportError("Issue with province history! Program will exit after continuing!");
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

                Task lregion = new Task(() =>
                {
                    try
                    {
                        if (GlobalVariables.UseMod[6] > 0)
                            regions = new NodeFile(GlobalVariables.pathtomod + "map\\region.txt");
                        else
                            regions = new NodeFile(GlobalVariables.pathtogame + "map\\region.txt");
                        //bw.ReportProgress(96);

                        if (regions.LastStatus.HasError)
                            progress.ReportError($"File '{regions}' has an error in line {regions.LastStatus.LineError}");
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
                                            Area are = GlobalVariables.Areas.Find(x => x.Name.ToLower() == vr.Name.ToLower());
                                            if (are != null)
                                                ar.Add(are);
                                        }
                                    }
                                    Region r = new Region(n.Name, ar);
                                    r.OriginalName = n.Name;
                                    foreach (Area are in r.Areas)
                                        are.Region = r;
                                }
                            }
                        }
                    }
                    catch
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Issue with regions! Program will exit after continuing!");
                        throw new Exception();
                    }
                });
                lregion.Start();
                progress.UpdateProgress(13, 0);

                await lregion;
                if (lregion.IsFaulted)
                    progress.UpdateProgress(13, 1);
                else if (lregion.IsCompleted)
                    progress.UpdateProgress(13, 2);
                Task lsuperregion = new Task(() =>
                {
                    try
                    {
                        if (GlobalVariables.UseMod[18] > 0)
                            Superregions = new NodeFile(GlobalVariables.pathtomod + "map\\superregion.txt");
                        else
                            Superregions = new NodeFile(GlobalVariables.pathtogame + "map\\superregion.txt");
                        //bw.ReportProgress(107);

                        if (Superregions.LastStatus.HasError)
                            progress.ReportError($"File '{Superregions.Path}' has an error in line {Superregions.LastStatus.LineError}");

                        foreach (Node n in Superregions.MainNode.Nodes)
                        {
                            List<Region> reg = new List<Region>();
                            foreach (PureValue s in n.PureValues)
                            {
                                Region r = GlobalVariables.Regions.Find(x => x.Name.ToLower() == s.Name.ToLower());
                                if (r != null)
                                {
                                    reg.Add(r);
                                }
                            }
                            Superregion sr = new Superregion(n.Name, reg);
                            sr.OriginalName = n.Name;
                            foreach (Region re in sr.Regions)
                                re.Superregion = sr;
                        }
                    }
                    catch
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Issue with superregions! Program will exit after continuing!");
                        throw new Exception();
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
                    }
                    MapManagement.CreateClickMask();
                });
                umapmisc.Start();
                await umapmisc;



                List<Task> MapTasks = new List<Task>();
                progress.UpdateProgress(18, 0);
                Task umapdev = new Task(() =>
                {
                    MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Development);
                });
                umapdev.Start();
                MapTasks.Add(umapdev);
                Task umaptradegood = new Task(() =>
                {
                    MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.TradeGood);
                });
                umaptradegood.Start();
                MapTasks.Add(umaptradegood);
                Task umapreligion = new Task(() =>
                {
                    MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Religion);
                });
                umapreligion.Start();
                MapTasks.Add(umapreligion);
                Task umapculture = new Task(() =>
                {
                    MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Culture);
                });
                umapculture.Start();
                MapTasks.Add(umapculture);
                Task umappolitical = new Task(() =>
                {
                    MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Political);
                });
                umappolitical.Start();
                MapTasks.Add(umappolitical);
                Task umaparea = new Task(() =>
                {
                    MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Area);
                });
                umaparea.Start();
                MapTasks.Add(umaparea);
                Task umapregion = new Task(() =>
                {
                    MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Region);
                });
                umapregion.Start();
                MapTasks.Add(umapregion);
                Task umaptradenode = new Task(() =>
                {
                    MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.TradeNode);
                });
                umaptradenode.Start();
                MapTasks.Add(umaptradenode);
                Task umaphre = new Task(() =>
                {
                    MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.HRE);
                });
                umaphre.Start();
                MapTasks.Add(umaphre);
                Task umapfort = new Task(() =>
                {
                    MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Fort);
                });
                umapfort.Start();
                MapTasks.Add(umapfort);
                Task umapcontinent = new Task(() =>
                {
                    MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Continent);
                });
                umapcontinent.Start();
                MapTasks.Add(umapcontinent);
                Task umapsuperregion = new Task(() =>
                {
                    MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Superregion);
                });
                umapsuperregion.Start();
                MapTasks.Add(umapsuperregion);
                Task umaptradecompany = new Task(() =>
                {
                    MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.TradeCompany);
                });
                umaptradecompany.Start();
                MapTasks.Add(umaptradecompany);
                Task umapgovernment = new Task(() =>
                {
                    MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Government);
                });
                umapgovernment.Start();
                MapTasks.Add(umapgovernment);

                Task umaplocalisation = new Task(() =>
                {
                    MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Localisation);
                });
                umaplocalisation.Start();
                MapTasks.Add(umaplocalisation);


                Task umapwinter = new Task(() =>
                {
                    MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Winter);
                });
                umapwinter.Start();
                MapTasks.Add(umapwinter);

                Task umapclimate = new Task(() =>
                {
                    MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Climate);
                });
                umapclimate.Start();
                MapTasks.Add(umapclimate);

                Task umapterrain = new Task(() =>
                {
                    MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Terrain);
                });
                umapterrain.Start();
                MapTasks.Add(umapterrain);



                Task ucontrol = new Task(() =>
                {
                    foreach (TradeGood tg in GlobalVariables.TradeGoods)
                    {
                        if (!GlobalVariables.LatentTradeGoods.Contains(tg))
                            ModEditor.form.TradeGoodBox.Items.Add(tg.ReadableName);
                        else
                            ModEditor.form.LatentTradeGoodBox.Items.Add(tg.ReadableName);
                    }
                    foreach (Tradenode tn in GlobalVariables.TradeNodes)
                    {
                        ModEditor.form.ProvinceTradeNodeBox.Items.Add(tn.Name);
                        ModEditor.form.TradeNodeBox.Items.Add(tn.Name);
                    }
                    foreach (Area a in GlobalVariables.Areas)
                        ModEditor.form.AreaBox.Items.Add(a.Name);
                    foreach (Region r in GlobalVariables.Regions)
                        ModEditor.form.RegionBox.Items.Add(r.Name);
                    foreach (Continent c in GlobalVariables.Continents)
                        ModEditor.form.ContinentBox.Items.Add(c.Name);
                    foreach (Government g in GlobalVariables.Governments)
                        ModEditor.form.GovernmentTypeBox.Items.Add(g.Type);
                    foreach (Building bl in GlobalVariables.Buildings)
                        ModEditor.form.BuildingsBox.Items.Add(bl.Name);
                    foreach (Superregion sr in GlobalVariables.Superregions)
                        ModEditor.form.SuperregionBox.Items.Add(sr.Name);
                    foreach (TradeCompany tc in GlobalVariables.TradeCompanies)
                        ModEditor.form.TradeCompanyBox.Items.Add(tc);
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
                "drill_gain_modifier","drill_decay_modifier","infantry_cost","infantry_power","infantry_fire","infantry_shock","cavalry_cost",
                "cavalry_power","cavalry_fire","cavalry_shock","artillery_cost","artillery_power","artillery_fire","artillery_shock",
                "cav_to_inf_ratio","cavalry_flanking","artillery_bonus_vs_fort","backrow_artillery_damage","discipline","mercenary_discipline",
                "land_morale","defensiveness","siege_ability","movement_speed","fire_damage","fire_damage_received","shock_damage",
                "shock_damage_received","recover_army_morale_speed","siege_blockade_progress","reserves_organisation","land_attrition",
                "reinforce_cost_modifier","reinforce_speed","manpower_recovery_speed","global_manpower","global_manpower_modifier",
                "global_regiment_cost","global_regiment_recruit_speed","global_supply_limit_modifier","land_forcelimit","land_forcelimit_modifier",
                "land_maintenance_modifier","mercenary_cost","merc_maintenance_modifier","possible_condottieri","hostile_attrition","garrison_size",
                "global_garrison_growth","fort_maintenance_modifier","rival_border_fort_maintenance","war_exhaustion","war_exhaustion_cost",
                "leader_land_fire","leader_land_manuever","leader_land_shock","leader_siege","general_cost","free_leader_pool","raze_power_gain",
                "loot_amount","available_province_loot","prestige_from_land","amount_of_banners","war_taxes_cost_modifier","leader_cost",
                "may_recruit_female_generals","special_unit_forcelimit","cawa_cost_modifier","manpower_in_true_faith_provinces","mercenary_manpower",
                "military_tactics","navy_tradition","navy_tradition_decay","naval_tradition_from_battle","naval_tradition_from_trade",
                "heavy_ship_cost","heavy_ship_power","light_ship_cost","light_ship_power","galley_cost","galley_power","transport_cost",
                "transport_power","global_ship_cost","global_ship_recruit_speed","global_ship_repair","naval_forcelimit","naval_forcelimit_modifier",
                "naval_maintenance_modifier","global_sailors","global_sailors_modifier","sailor_maintenance_modifer","sailors_recovery_speed",
                "blockade_efficiency","capture_ship_chance","global_naval_engagement_modifier","naval_attrition","naval_morale","ship_durability",
                "sunk_ship_morale_hit_recieved","recover_navy_morale_speed","prestige_from_naval","leader_naval_fire","leader_naval_manuever",
                "leader_naval_shock","own_coast_naval_combat_bonus","admiral_cost","global_naval_barrage_cost","allowed_marine_fraction",
                "flagship_cost","disengagement_chance","may_perform_slave_raid","may_perform_slave_raid_on_same_religion",
                "movement_speed_in_fleet_modifier","diplomats","diplomatic_reputation","diplomatic_upkeep","envoy_travel_time","fabricate_claims_cost",
                "improve_relation_modifier","vassal_forcelimit_bonus","vassal_income","ae_impact","claim_duration","diplomatic_annexation_cost",
                "province_warscore_cost","unjustified_demands","enemy_core_creation","rival_change_cost","justify_trade_conflict_cost",
                "stability_cost_to_declare_war","accept_vassalization_reasons","transfer_trade_power_reasons","monthly_favor_modifier",
                "global_tax_income","global_tax_modifier","production_efficiency","state_maintenance_modifier","inflation_action_cost",
                "inflation_reduction","monthly_gold_inflation_modifier","gold_depletion_chance_modifier","interest","development_cost",
                "tribal_development_growth","build_cost","build_time","great_project_upgrade_cost","global_monthly_devastation",
                "global_prosperity_growth","administrative_efficiency","core_creation","core_decay_on_your_own","adm_tech_cost_modifier",
                "dip_tech_cost_modifier","mil_tech_cost_modifier","technology_cost","idea_cost","embracement_cost","global_institution_spread",
                "institution_spread_from_true_faith","native_advancement_cost","all_power_cost","innovativeness_gain","free_adm_policy",
                "free_dip_policy","free_mil_policy","possible_adm_policy","possible_dip_policy","possible_mil_policy","possible_policy","free_policy",
                "country_admin_power","country_diplomatic_power","country_military_power","prestige","prestige_decay","monthly_splendor",
                "yearly_corruption","advisor_cost","advisor_pool","female_advisor_chance","heir_chance","monthly_heir_claim_increase",
                "block_introduce_heir","monarch_admin_power","monarch_diplomatic_power","monarch_military_power","adm_advisor_cost","dip_advisor_cost",
                "mil_advisor_cost","monthly_support_heir_gain","power_projection_from_insults","monarch_lifespan","local_heir_adm","local_heir_dip",
                "local_heir_mil","yearly_absolutism","max_absolutism","legitimacy","republican_tradition","devotion","horde_unity","meritocracy",
                "monthly_militarized_society","monthly_federation_favor_growth","yearly_tribal_allegiance","burghers_influence","clergy_influence",
                "nobility_influence","imperial_mandate","election_cycle","candidate_random_bonus","reelection_cost","governing_capacity",
                "governing_capacity_modifier","governing_cost","state_governing_cost","trade_company_governing_cost","expand_administration_cost",
                "yearly_revolutionary_zeal","max_revolutionary_zeal","reform_progress_growth","monthly_reform_progress",
                "monthly_reform_progress_modifier","move_capital_cost_modifier","clergy_influence_modifier","nobility_influence_modifier",
                "burghers_influence_modifier","clergy_loyalty_modifier","nobility_loyalty_modifier","burghers_loyalty_modifier",
                "all_estate_loyalty_equilibrium","imperial_authority","imperial_authority_value","free_city_imperial_authority",
                "imperial_mercenary_cost","max_free_cities","max_electors","legitimate_subject_elector","manpower_against_imperial_enemies",
                "imperial_reform_catholic_approval","culture_conversion_cost","num_accepted_cultures","same_culture_advisor_cost",
                "promote_culture_cost","global_unrest","stability_cost_modifier","global_autonomy","min_autonomy","autonomy_change_time",
                "harsh_treatment_cost","global_rebel_suppression_efficiency","years_of_nationalism","min_autonomy_in_territories",
                "unrest_catholic_provinces","liberty_desire","liberty_desire_from_subject_development","reduced_liberty_desire",
                "reduced_liberty_desire_on_same_continent","spy_offence","global_spy_defence","discovered_relations_impact","rebel_support_efficiency",
                "global_missionary_strength","global_heretic_missionary_strength","missionaries","missionary_maintenance_cost","religious_unity",
                "tolerance_own","tolerance_heretic","tolerance_heathen","tolerance_of_heretics_capacity","tolerance_of_heathens_capacity",
                "papal_influence","church_power_modifier","monthly_fervor_increase","harmonization_speed","yearly_harmony","monthly_piety",
                "monthly_karma","yearly_authority","enforce_religion_cost","prestige_per_development_from_conversion","warscore_cost_vs_other_religion",
                "establish_order_cost","global_religious_conversion_resistance","relation_with_heretics","curia_treasury_contribution",
                "curia_powers_cost","yearly_patriarch_authority","cb_on_religious_enemies","global_heathen_missionary_strength",
                "appoint_cardinal_cost","papal_influence_from_cardinals","yearly_karma_decay","colonists","colonist_placement_chance",
                "global_colonial_growth","range","native_uprising_chance","native_assimilation","migration_cost","global_tariffs",
                "treasure_fleet_income","expel_minorities_cost","caravan_power","merchants","placed_merchant_power","global_trade_power",
                "global_foreign_trade_power","global_own_trade_power","global_prov_trade_power_modifier","global_trade_goods_size_modifier",
                "trade_efficiency","trade_range_modifier","trade_steering","global_ship_trade_power","privateer_efficiency","embargo_efficiency",
                "ship_power_propagation","center_of_trade_upgrade_cost","trade_company_investment_cost","mercantilism_cost" };

                GlobalVariables.CountryModifiers.AddRange(modifiers);

                GlobalVariables.FullyLoaded = true;

                /*
                File.WriteAllLines("owners.txt", tags);
                File.WriteAllLines("cultures.txt", cultu);
                File.WriteAllLines("religions.txt", relig);
                File.WriteAllLines("dev.txt", dev.Select(x => x.ToString()).ToArray());
                File.WriteAllLines("tradegoods.txt", tradego);
                */
                //File.WriteAllLines("ids.txt", ids.Select(x => x.ToString()).ToArray());
                //MessageBox.Show($"Provinces: {GlobalVariables.Provinces.Count()}\nName: {GlobalVariables.Provinces[0].DefinitionName}");
            }
            catch (Exception e)
            {
                progress.ReportError(e.ToString());
                progress.MakeContinueAvailable();
                if (GlobalVariables.__DEBUG)
                    throw;
                
            }
        
        }

       

        public static void WorkerDone(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show(GlobalVariables.CurrentLoadingProgress + "");
            if (e.Error != null)
                MessageBox.Show(e.Error.StackTrace);
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

        public static void ReadProvinceValuesFromNode(Province province, Node n, LoadingProgress progress = null)
        {
            foreach (Variable v in n.Variables)
            {
                Building bl = GlobalVariables.Buildings.Find(x => x.Name.ToLower() == v.Name.ToLower());
                if (bl != null && v.Value == "yes")
                {
                    province.AddBuilding(bl, true);
                }                   

                switch (v.Name)
                {
                    case "add_core":
                        province.AddCore(v.Value.ToUpper(), true);
                        if (v.Value == "---")
                            continue;
                        if (progress != null)
                        {
                            if (!GlobalVariables.Countries.Any(x => x.Tag == v.Value.ToUpper()))
                                progress.ReportError($"Alert: Province {province.ID} -> Unknown country tag in cores {v.Value.ToUpper()}");
                        }
                        break;
                    case "add_claim":
                        province.AddClaim(v.Value.ToUpper(), true);
                        if (v.Value == "---")
                            continue;
                        if (progress != null)
                        {
                            if (!GlobalVariables.Countries.Any(x => x.Tag == v.Value.ToUpper()))
                                progress.ReportError($"Alert: Province {province.ID} -> Unknown country tag in claims {v.Value.ToUpper()}");
                        }
                        break;
                    case "owner":
                        if (v.Value == "---")
                            province.OwnerCountry = null;
                        else
                        {
                            Country c = GlobalVariables.Countries.Find(x => x.Tag == v.Value.ToUpper());
                            if (c == null)
                            {
                                if (progress != null)
                                {
                                    progress.ReportError($"Error: Province {province.ID} -> Unknown owner {v.Value.ToUpper()}");
                                }
                            }
                            if (c != null)
                            {
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
                                    progress.ReportError($"Error: Province {province.ID} -> Unknown controller {v.Value.ToUpper()}");
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
                                progress.ReportError($"Error: Province {province.ID} -> Unknown culture '{v.Value.ToUpper()}'");
                        }
                        else
                            province.Culture = cul;

                        break;
                    case "religion":
                        province.Religion = Religion.Religions.Find(x => x.Name.ToLower() == v.Value.Replace("\"", "").ToLower());
                        break;
                    case "hre":
                        if (v.Value == "yes")
                            province.HRE = true;
                        else
                            province.HRE = false;
                        break;
                    case "fort_15th":
                        if (v.Value == "yes")
                            province.Fort = true;
                        else
                            province.Fort = false;
                        break;
                    case "base_tax":
                        province.Tax = int.Parse(v.Value);
                        break;
                    case "base_production":
                        province.Production = int.Parse(v.Value);
                        break;
                    case "base_manpower":
                        province.Manpower = int.Parse(v.Value);
                        break;
                    case "trade_goods":
                        province.TradeGood = GlobalVariables.TradeGoods.Find(x => x.Name.ToLower() == v.Value.ToLower());
                        if (province.TradeGood != null)
                        {
                            province.TradeGood.TotalProvinces++;
                        }
                        break;
                    case "capital":
                        province.Capital = v.Value.Replace("\"", "");
                        break;
                    case "center_of_trade":
                        province.CenterOfTrade = int.Parse(v.Value);
                        break;
                    case "discovered_by":
                        province.AddDiscoveredBy(v.Value, true);
                        break;
                    case "is_city":
                        if (v.Value == "yes")
                            province.City = true;
                        else
                            province.City = false;
                        break;
                }
            }
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
