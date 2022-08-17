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
           
            //BackgroundWorker bw = (BackgroundWorker)sender;
            List<NodeFile> tradegoodsfiles = new List<NodeFile>();
            List<NodeFile> tradegoodspricesfiles = new List<NodeFile>();
            List<NodeFile> culturesfiles = new List<NodeFile>();
            List<NodeFile> religionsfiles = new List<NodeFile>();
            List<NodeFile> governmentsfiles = new List<NodeFile>();
            Dictionary<string, string> NameToTag = new Dictionary<string, string>();
            List<NodeFile> countrytagsfiles = new List<NodeFile>();
            List<NodeFile> buildingsfiles = new List<NodeFile>();
            List<string> CountryCommonFiles = new List<string>();
            NodeFile technology;
            NodeFile areas;
            NodeFile regions;
            NodeFile continents;
            List<NodeFile> tradenodesfiles = new List<NodeFile>();
            NodeFile Superregions;

            Task ldefinition = new Task(() => {
                StreamReader Reader;
                if (GlobalVariables.UseMod[0] > 0)
                    Reader = File.OpenText(GlobalVariables.pathtomod + "map\\definition.csv");
                else
                    Reader = File.OpenText(GlobalVariables.pathtogame + "map\\definition.csv");

                Reader.ReadLine();
                while (!Reader.EndOfStream)
                {
                    string data = Reader.ReadLine();
                    string[] values = data.Split(';');
                    Province p = new Province(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]), int.Parse(values[3]), values[4]);
                    GlobalVariables.Provinces.Add(p);
                }
            });
            ldefinition.Start();
            progress.UpdateProgress(0, 0);
            Task ltradegoods = new Task(() => {
                
                if (GlobalVariables.UseMod[2] != 0)
                {
                    foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\tradegoods\\"))
                    {
                        if (file.Contains('.'))
                        {
                            if (file.Split('.')[1] == "txt")
                            {
                                NodeFile nf = new NodeFile(file);
                                tradegoodsfiles.Add(nf);
                                GlobalVariables.ModTradeGoodsFiles.Add(nf);
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
                                if (!tradegoodsfiles.Any(x => x.FileName == file.Split('\\').Last().Replace(".txt", "")))
                                    tradegoodsfiles.Add(nf);
                                GlobalVariables.GameTradeGoodsFile = nf;
                            }
                        }
                    }
                }
            });
            ltradegoods.Start();
            progress.UpdateProgress(1, 0);
            Task lcultures = new Task(() => {
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
                                culturesfiles.Add(nf);
                                GlobalVariables.ModCulturesFiles.Add(nf);
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
                                if (!culturesfiles.Any(x => x.FileName == file.Split('\\').Last().Replace(".txt", "")))
                                    culturesfiles.Add(nf);
                                GlobalVariables.GameCulturesFile = nf;
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
                                    Group = cg
                                };
                                cg.Cultures.Add(c);
                                Variable v = innernode.Variables.Find(x => x.Name == "primary");
                                if (v != null)
                                    c.PrimaryTag = v.Value;
                            }
                        }
                    }
                }

            });
            lcultures.Start();
            progress.UpdateProgress(4, 0);
            Task lreligions = new Task(() => {
                if (GlobalVariables.UseMod[5] != 0)
                {
                    foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\religions\\"))
                    {
                        if (file.Contains('.'))
                        {
                            if (file.Split('.')[1] == "txt")
                            {
                                NodeFile nf = new NodeFile(file);
                                religionsfiles.Add(nf);
                                GlobalVariables.ModReligionsFiles.Add(nf);
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
                                if (!religionsfiles.Any(x => x.FileName == file.Split('\\').Last().Replace(".txt", "")))
                                    religionsfiles.Add(nf);
                                GlobalVariables.GameReligionsFile = nf;
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
                                        Group = rg
                                    };
                                    rg.Religions.Add(r);
                                    string[] colorstring = innernode.Nodes.Find(x => x.Name == "color").PureValues.ToArray();
                                    r.Color = Color.FromArgb(int.Parse(colorstring[0]), int.Parse(colorstring[1]), int.Parse(colorstring[2]));
                                    r.Icon = int.Parse(innernode.Variables.Find(x => x.Name == "icon").Value);
                                }
                                catch
                                {

                                }
                            }
                        }
                    }
                }
            });
            lreligions.Start();
            progress.UpdateProgress(5, 0);
            Task lgovernments = new Task(() => {
                if (GlobalVariables.UseMod[16] != 0)
                {
                    foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\governments\\"))
                    {
                        if (file.Contains('.'))
                        {
                            if (file.Split('.')[1] == "txt")
                            {
                                NodeFile nf = new NodeFile(file);
                                governmentsfiles.Add(nf);
                                GlobalVariables.ModGovernmentsFiles.Add(nf);
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
                                if (!governmentsfiles.Any(x => x.FileName == file.Split('\\').Last().Replace(".txt", "")))
                                    governmentsfiles.Add(nf);
                                GlobalVariables.GameGovernmentsFile = nf;
                            }
                        }
                    }
                }
                foreach (NodeFile government in governmentsfiles)
                {
                    foreach (Node n in government.MainNode.Nodes)
                    {
                        if (n.Name != "pre_dharma_mapping")
                        {
                            Government gv = new Government(n.Name);
                            gv.reforms.AddRange(n.Nodes.Find(x => x.Name == "reform_levels").Nodes[0].Nodes.Find(x => x.Name == "reforms").PureValues);
                            GlobalVariables.Governments.Add(gv);
                        }
                    }
                }

            });
            lgovernments.Start();
            progress.UpdateProgress(6, 0);
            Task ltechnology = new Task(() => {
                
                if (GlobalVariables.UseMod[15] > 0)
                    technology = new NodeFile(GlobalVariables.pathtomod + "common\\technology.txt");
                else
                    technology = new NodeFile(GlobalVariables.pathtogame + "common\\technology.txt");
                foreach (Node node in technology.MainNode.Nodes.Find(x => x.Name == "groups").Nodes)
                {
                    GlobalVariables.TechGroups.Add(node.Name);
                }
            });
            ltechnology.Start();
            progress.UpdateProgress(7, 0);
            Task ltags = new Task(() => {
                if (GlobalVariables.UseMod[14] != 0)
                {
                    foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\country_tags\\"))
                    {
                        if (file.Contains('.'))
                        {
                            if (file.Split('.')[1] == "txt")
                            {
                                NodeFile nf = new NodeFile(file);
                                countrytagsfiles.Add(nf);
                                GlobalVariables.ModCountryTagsFiles.Add(nf);
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
                                if (!countrytagsfiles.Any(x => x.FileName == file.Split('\\').Last().Replace(".txt", "")))
                                    countrytagsfiles.Add(nf);
                                GlobalVariables.GameCountryTagsFile = nf;
                            }
                        }
                    }
                }

                foreach (NodeFile countrytags in countrytagsfiles)
                {
                    foreach (Variable v in countrytags.MainNode.Variables)
                    {
                        string n = v.Value.Replace("\"", "").Trim().Split('/')[1].Split('.')[0];
                        if (!NameToTag.Keys.Contains(n))
                            NameToTag.Add(n, v.Name.Trim());
                    }
                }
            });
            ltags.Start();
            progress.UpdateProgress(8, 0);
            Task lbuildings = new Task(() =>
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
                                buildingsfiles.Add(nf);
                                GameFiles.Add(false);
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
                                if (!buildingsfiles.Any(x => x.FileName == file.Split('\\').Last().Replace(".txt", "")))
                                {
                                    buildingsfiles.Add(nf);
                                    GameFiles.Add(true);
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
                        bl.File = buildings.FileName;
                        if (GameFiles[buildingsfiles.IndexOf(buildings)])
                            bl.GameFile = true;
                        GlobalVariables.Buildings.Add(bl);
                    }
                }
            });
            lbuildings.Start();
            progress.UpdateProgress(20, 0);



            await ldefinition;
            if (ldefinition.IsFaulted)
                progress.UpdateProgress(0, 1);
            else if (ldefinition.IsCompleted)
                progress.UpdateProgress(0, 2);

            Task larea = new Task(() => {
                if (GlobalVariables.UseMod[9] > 0)
                    areas = new NodeFile(GlobalVariables.pathtomod + "map\\area.txt");
                else
                    areas = new NodeFile(GlobalVariables.pathtogame + "map\\area.txt");
                //bw.ReportProgress(92);
                foreach (Node n in areas.MainNode.Nodes)
                {
                    List<Province> pr = new List<Province>();
                    foreach (string vr in n.PureValues)
                    {
                        if (vr != "")
                        {
                            pr.Add(GlobalVariables.Provinces[int.Parse(vr) - 1]);
                        }
                    }
                    Area a = new Area(n.Name, pr);
                    foreach (Province pro in a.Provinces)
                        pro.Area = a;
                }
            });
            larea.Start();
            progress.UpdateProgress(12, 0);
            Task lcontinent = new Task(() => {
                if (GlobalVariables.UseMod[13] > 0)
                    continents = new NodeFile(GlobalVariables.pathtomod + "map\\continent.txt");
                else
                    continents = new NodeFile(GlobalVariables.pathtogame + "map\\continent.txt");
                //bw.ReportProgress(100);
                foreach (Node n in continents.MainNode.Nodes)
                {
                    List<Province> ctp = new List<Province>();
                    foreach (string s in n.PureValues)
                    {
                        ctp.Add(GlobalVariables.Provinces[int.Parse(s.Trim()) - 1]);
                    }
                    Continent c = new Continent(n.Name, ctp);
                    foreach (Province pr in c.Provinces)
                        pr.Continent = c;
                }
            });
            lcontinent.Start();
            progress.UpdateProgress(14, 0);
            Task ltradenodes = new Task(() => {
            if (GlobalVariables.UseMod[12] != 0)
            {
                foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\tradenodes\\"))
                {
                    if (file.Contains('.'))
                    {
                        if (file.Split('.')[1] == "txt")
                        {
                            NodeFile nf = new NodeFile(file);
                            tradenodesfiles.Add(nf);
                            GlobalVariables.ModTradeNodesFiles.Add(nf);
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
                            if (!tradenodesfiles.Any(x => x.FileName == file.Split('\\').Last().Replace(".txt", "")))
                                tradenodesfiles.Add(nf);
                            GlobalVariables.GameTradeNodesFile = nf;
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
                    Tradenode tn = GlobalVariables.TradeNodes.Find(x => x.Name == node.Name);
                    tn.File = tradenodes.FileName;
                    if (tradenodes == GlobalVariables.GameTradeNodesFile)
                        tn.GameFile = true;
                    tn.Name = node.Name;

                        int location = 0;
                        if (!int.TryParse(node.Variables.Find(x => x.Name == "location").Value, out location))
                        {
                            progress.ReportError($"Trade nodes: '{node.Name}' node has incorrect location: '{location}', using first valid node province member.");
                            location = -1;
                        }
                        else if (GlobalVariables.Provinces.Count() <= location)
                        {
                            progress.ReportError($"Trade nodes: Location of '{node.Name}' node is outside of province ID limit '{location}', using first valid node province member.");
                            location = -1;
                        }
                        if(location == -1)
                        {
                            foreach (string value in node.Nodes.Find(x => x.Name == "members").PureValues)
                            {
                                if (!int.TryParse(value, out location))
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
                            location -= 1;
                            tn.Location = GlobalVariables.Provinces[location];
                        }
                        Node ColorNode = node.Nodes.Find(x => x.Name == "color");
                        if (ColorNode != null)
                            tn.Color = Color.FromArgb(int.Parse(ColorNode.PureValues[0]), int.Parse(ColorNode.PureValues[1]), int.Parse(ColorNode.PureValues[2]));
                        else
                            tn.Color = AdditionalElements.GenerateColor(GlobalVariables.GlobalRandom);

                        Variable v = node.Variables.Find(x => x.Name == "inland");
                        if (v == null)
                            tn.Inland = false;
                        else
                        {
                            if (v.Value == "yes")
                                tn.Inland = true;
                            else
                                tn.Inland = false;
                        }
                        v = node.Variables.Find(x => x.Name == "end");
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
                            Destination dn = new Destination() { TradeNode = GlobalVariables.TradeNodes.Find(x => x.Name == outgoing.Variables.Find(y => y.Name == "name").Value.Replace("\"", "")) };
                            dn.Path.AddRange(outgoing.Nodes.Find(x => x.Name == "path").PureValues);
                            if (outgoing.Nodes.Find(x => x.Name == "control") != null)
                                dn.Control.AddRange(outgoing.Nodes.Find(x => x.Name == "control").PureValues);
                            tn.Destination.Add(dn);
                            dn.TradeNode.Incoming.Add(tn);
                        }
                        foreach (string value in node.Nodes.Find(x => x.Name == "members").PureValues)
                        {
                            if (int.Parse(value) <= GlobalVariables.Provinces.Count())
                            {
                                tn.Provinces.Add(GlobalVariables.Provinces[int.Parse(value) - 1]);
                                GlobalVariables.Provinces[int.Parse(value) - 1].TradeNode = tn;
                            }
                        }
                    }
                }
            });
            ltradenodes.Start();
            progress.UpdateProgress(15, 0);
            Task ldefaultmap = new Task(() => {
                StreamReader Reader;
                //lp.UpdateProgressLabel("Loading map variables...", 95);
                if (GlobalVariables.UseMod[10] > 0)
                    Reader = new StreamReader(GlobalVariables.pathtomod + "map\\default.map");
                else
                    Reader = new StreamReader(GlobalVariables.pathtogame + "map\\default.map");
                bool addlines = false;
                int addlinesto = 0;
                string seas = "";
                string lakes = "";
                while (!Reader.EndOfStream)
                {
                    string line = Reader.ReadLine();
                    if (line.Contains("sea_starts"))
                    {
                        addlines = true;
                        addlinesto = 0;
                    }
                    else if (line.Contains("lakes"))
                    {
                        addlines = true;
                        addlinesto = 1;
                    }

                    if (addlines)
                    {
                        if (addlinesto == 0)
                            seas += " " + line;
                        else if (addlinesto == 1)
                            lakes += " " + line;
                    }

                    if (line.Contains("}"))
                        addlines = false;
                }
                seas = seas.Split('{')[1].Split('}')[0];
                lakes = lakes.Split('{')[1].Split('}')[0];


                foreach (string sea in seas.Split(' '))
                {
                    if (sea != "" && sea != " ")
                    {
                        if (int.TryParse(sea, out int id))
                        {
                            try
                            {
                                GlobalVariables.Provinces[id - 1].Sea = true;
                            }
                            catch
                            {
                                MessageBox.Show("Sea Id: " + id);
                            }
                        }
                    }
                }
                foreach (string lake in lakes.Split(' '))
                {
                    if (lake != "" && lake != " ")
                    {
                        if (int.TryParse(lake, out int id))
                        {
                            try
                            {
                                GlobalVariables.Provinces[id - 1].Lake = true;
                            }
                            catch
                            {
                                MessageBox.Show("Lake Id: " + id);
                            }
                        }
                    }
                }

                Reader.Dispose();
            });
            ldefaultmap.Start();
            progress.UpdateProgress(17, 0);

            Task lmap = new Task(() =>
            {
                Bitmap copiedBitmap = new Bitmap(GlobalVariables.ProvincesMapBitmap);
                LockBitmap bitmap = new LockBitmap(copiedBitmap);
                bitmap.LockBits();

                int heightInterval = bitmap.Height / 10;
                int heightValue = 0;
                int va = 10;

                for (int y = 1; y < bitmap.Height; y++)
                {
                    for (int x = 1; x < bitmap.Width; x += 5)
                    {
                        Color c = bitmap.GetPixel(x, y);
                        if (c != Color.FromArgb(1, 255, 255, 255))
                        {
                            Province p = GlobalVariables.Provinces.Find(pr => pr.c == c);
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
            });
            lmap.Start();
            progress.UpdateProgress(2, 0);

            Task tradegoodsAll = ltradegoods.ContinueWith(new Action<Task>(ac => {
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
                            ReadableName = node.Name[0].ToString().ToUpper() + node.Name.Substring(1).Replace('_', ' ')
                        };
                        string[] colorv = node.Nodes.Find(x => x.Name == "color").PureValues.ToArray();
                        if (colorv.Count() == 3)
                        {
                            tg.Color = Color.FromArgb((int)(255 * double.Parse(colorv[0], CultureInfo.InvariantCulture)), (int)(255 * double.Parse(colorv[1], CultureInfo.InvariantCulture)), (int)(255 * double.Parse(colorv[2], CultureInfo.InvariantCulture)));
                        }
                        else
                        {
                            tg.Color = AdditionalElements.GenerateColor(GlobalVariables.GlobalRandom);
                        }

                        GlobalVariables.TradeGoods.Add(tg);

                        Variable latent = node.Variables.Find(x => x.Name == "is_latent");
                        if (latent != null)
                            if (latent.Value == "yes")
                                GlobalVariables.LatentTradeGoods.Add(tg);
                    }

                }
            }))
            .ContinueWith(new Action<Task>(ac => {
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
                                tradegoodspricesfiles.Add(nf);
                                GlobalVariables.ModPricesFiles.Add(nf);
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
                                if (!tradegoodspricesfiles.Any(x => x.FileName == file.Split('\\').Last().Replace(".txt", "")))
                                    tradegoodspricesfiles.Add(nf);
                                GlobalVariables.GamePricesFile = nf;
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
                        TradeGood tg = GlobalVariables.TradeGoods.Find(x => x.Name == node.Name);
                        if (tg != null)
                        {
                            tg.Price = double.Parse(node.Variables.Find(x => x.Name == "base_price").Value, CultureInfo.InvariantCulture);
                            if (node.Variables.Find(x => x.Name == "goldtype") != null)
                            {
                                if (node.Variables.Find(x => x.Name == "goldtype").Value.Trim() == "yes")
                                    tg.GoldLike = true;
                            }
                        }
                    }
                }
            }));

            await lmap;
            if (lmap.IsFaulted)
                progress.UpdateProgress(2, 1);
            else if (lmap.IsCompleted)
                progress.UpdateProgress(2, 2);



            Task provincecentre = new Task(() => {
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
            });
            provincecentre.Start();
            progress.UpdateProgress(3, 0);
            



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
            Task lcountries = new Task(() =>
            {
                List<string> CountryHistoryFiles = new List<string>();
                List<bool> GameFiles = new List<bool>();

                if (GlobalVariables.UseMod[11] != 0)
                {
                    foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "history\\countries\\"))
                    {
                        if (file.Contains('.'))
                        {
                            if (file.Split('.')[1] == "txt")
                            {
                                CountryHistoryFiles.Add(file);
                                GameFiles.Add(false);
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
                                if (!CountryHistoryFiles.Any(x => x.Split('\\').Last().Replace(".txt", "") == file.Split('\\').Last().Replace(".txt", "")))
                                {
                                    CountryHistoryFiles.Add(file);
                                    GameFiles.Add(true);
                                }
                            }
                        }
                    }
                }
                foreach (string file in CountryHistoryFiles)
                {
                    string fs = file.Split('\\').Last();
                    string tag = "";
                    string name = "";
                    try
                    {
                        tag = fs.Split('-')[0].Trim().ToUpper();
                        name = fs.Split('-')[1].Trim().Split('.')[0];
                    }
                    catch
                    {
                        continue;
                    }

                    Country c = new Country();
                    c.HistoryFile = file;
                    if (GameFiles[CountryHistoryFiles.IndexOf(file)])
                        c.HistoryFileGame = true;
                    GlobalVariables.Countries.Add(c);
                    NodeFile nodefile = new NodeFile(file);
                    foreach (Variable v in nodefile.MainNode.Variables)
                    {
                        switch (v.Name)
                        {
                            case "government":
                                c.Government = GlobalVariables.Governments.Find(x => x.Type == v.Value);
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
                                c.Religion = Religion.Religions.Find(x => x.Name == v.Value);
                                break;
                            case "primary_culture":
                                c.PrimaryCulture = v.Value;
                                break;
                            case "government_rank ":
                                c.GovernmentRank = int.Parse(v.Value);
                                break;
                        }
                    }
                    c.Tag = tag;
                    c.FullName = name;
                }
            });
            lcountries.Start();
            progress.UpdateProgress(9, 0);


            await lcountries;
            if (lcountries.IsFaulted)
                progress.UpdateProgress(9, 1);
            else if (lcountries.IsCompleted)
                progress.UpdateProgress(9, 2);


            Task lcomcountries = new Task(() => {
                List<bool> GameFiles = new List<bool>();
                if (GlobalVariables.UseMod[7] != 0)
                {
                    foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\countries\\"))
                    {
                        if (file.Contains('.'))
                        {
                            if (file.Split('.')[1] == "txt")
                            {
                                CountryCommonFiles.Add(file);
                                GameFiles.Add(false);
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
                                if (!CountryCommonFiles.Any(x => x.Split('\\').Last().Replace(".txt", "") == file.Split('\\').Last().Replace(".txt", "")))
                                {
                                    CountryCommonFiles.Add(file);
                                    GameFiles.Add(true);
                                }
                            }
                        }
                    }
                }

                //bw.ReportProgress(84);

                foreach (string file in CountryCommonFiles)
                {
                    string name = file.Split('\\').Last().Split('.')[0];
                    if (!NameToTag.ContainsKey(name))
                        continue;
                    Country c = GlobalVariables.Countries.Find(x => x.Tag == NameToTag[name]);
                    if (c != null)
                    {
                        c.CommonFile = file;
                        if (GameFiles.Count > CountryCommonFiles.IndexOf(file))
                        {
                            if (GameFiles[CountryCommonFiles.IndexOf(file)])
                                c.CommonFileGame = true;
                        }
                        NodeFile nodefile = new NodeFile(file);
                        string[] colort = nodefile.MainNode.Nodes.Find(x => x.Name == "color").PureValues.ToArray();
                        try
                        {
                            c.Color = Color.FromArgb(int.Parse(colort[0]), int.Parse(colort[1]), int.Parse(colort[2]));
                        }
                        catch
                        {
                            c.Color = AdditionalElements.GenerateColor(GlobalVariables.GlobalRandom);
                            MessageBox.Show("Some error in " + file);
                        }
                    }
                }
            });
            lcomcountries.Start();
            progress.UpdateProgress(10, 0);
            Task lprovhistory = new Task(() => {
                List<string> Files = new List<string>();
                List<bool> GameFiles = new List<bool>();
                if (GlobalVariables.UseMod[8] != 0)
                {
                    foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "history\\provinces\\"))
                    {
                        if (file.Contains('.'))
                        {
                            if (file.Split('.').Last() == "txt")
                            {
                                Files.Add(file);
                                GameFiles.Add(false);
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
                                if (!Files.Any(x => x.Split('\\').Last() == file.Split('\\').Last()))
                                {
                                    Files.Add(file);
                                    GameFiles.Add(true);
                                }
                            }
                        }
                    }
                }
                //bw.ReportProgress(88);
                foreach (string file in Files)
                {
                    bool b = int.TryParse(new String(file.Split('\\').Last().Where(x => char.IsDigit(x)).ToArray()), out int id);
                    if (b)
                    {
                        Province province = GlobalVariables.Provinces.Find(x => x.ID == id);
                        if (province != null)
                        {
                            province.HistoryFile = file;
                            try
                            {
                                province.HistoryFileGame = GameFiles[Files.IndexOf(file)];
                            }
                            catch
                            {
                                //bw.ReportProgress(-1, "Discrepency between Game files and Mod files");
                            }
                            NodeFile nodefile = new NodeFile(file);

                            if (nodefile.MainNode.Variables.Any())
                                GlobalVariables.TotalUsableProvinces++;

                            foreach (Variable v in nodefile.MainNode.Variables)
                            {
                                Building bl = GlobalVariables.Buildings.Find(x => x.Name == v.Name);
                                if (bl != null && v.Value == "yes")
                                {
                                    province.AddBuilding(bl, true);
                                }

                                switch (v.Name)
                                {
                                    case "add_core":
                                        province.AddCore(v.Value, true);
                                        break;
                                    case "add_claim":
                                        province.AddClaim(v.Value, true);
                                        break;
                                    case "owner":
                                        Country c = GlobalVariables.Countries.Find(x => x.Tag == v.Value.ToUpper());
                                        if (c != null)
                                        {
                                            province.OwnerCountry = c;
                                            c.Provinces.Add(province);
                                        }
                                        break;
                                    case "controller":
                                        province.Controller = v.Value;
                                        break;
                                    case "culture":
                                        province.Culture = Culture.Cultures.Find(x => x.Name == v.Value);
                                        break;
                                    case "religion":
                                        province.Religion = Religion.Religions.Find(x => x.Name == v.Value);
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
                                        province.TradeGood = GlobalVariables.TradeGoods.Find(x => x.Name == v.Value);
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

                            if (province.TradeGood != null)
                            {
                                province.TradeGood.TotalDev += province.Tax + province.Production + province.Manpower;
                            }

                            Node n = nodefile.MainNode.Nodes.Find(x => x.Name == "latent_trade_goods");
                            if (n != null)
                            {
                                if (n.PureValues.Any())
                                {
                                    province.LatentTradeGood = GlobalVariables.TradeGoods.Find(x => x.Name == n.PureValues[0].Trim());
                                    if (province.LatentTradeGood != null)
                                    {
                                        province.LatentTradeGood.TotalProvinces++;
                                        province.TradeGood.TotalDev += province.Tax + province.Production + province.Manpower;
                                    }
                                }
                            }
                        }
                    }
                }

                foreach(Province p in GlobalVariables.Provinces)
                {
                    if(p.HistoryFile == null)
                    {
                        string s = GlobalVariables.pathtomod + "history\\provinces\\" + (GlobalVariables.Provinces.IndexOf(p)+1) + ".txt";
                        File.Create(s);
                        p.HistoryFile = s;
                    }
                }
            });
            lprovhistory.Start();
            progress.UpdateProgress(11, 0);

            await lcomcountries;
            if (lcomcountries.IsFaulted)
                progress.UpdateProgress(10, 1);
            else if (lcomcountries.IsCompleted)
                progress.UpdateProgress(10, 2);
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

            Task lregion = new Task(() => {
                if (GlobalVariables.UseMod[6] > 0)
                    regions = new NodeFile(GlobalVariables.pathtomod + "map\\region.txt");
                else
                    regions = new NodeFile(GlobalVariables.pathtogame + "map\\region.txt");
                //bw.ReportProgress(96);
                foreach (Node n in regions.MainNode.Nodes)
                {
                    List<Area> ar = new List<Area>();
                    Node nd = n.Nodes.Find(x => x.Name == "areas");
                    if (nd != null)
                    {
                        foreach (string vr in nd.PureValues)
                        {
                            if (vr != "")
                            {
                                Area are = GlobalVariables.Areas.Find(x => x.Name == vr);
                                if (are != null)
                                    ar.Add(are);
                            }
                        }
                        Region r = new Region(n.Name, ar);
                        foreach (Area are in r.Areas)
                            are.Region = r;
                    }
                }
            });
            lregion.Start();
            progress.UpdateProgress(13, 0);

            await lregion;
            if (lregion.IsFaulted)
                progress.UpdateProgress(13, 1);
            else if (lregion.IsCompleted)
                progress.UpdateProgress(13, 2);
            Task lsuperregion = new Task(() => {
                if (GlobalVariables.UseMod[18] > 0)
                    Superregions = new NodeFile(GlobalVariables.pathtomod + "map\\superregion.txt");
                else
                    Superregions = new NodeFile(GlobalVariables.pathtogame + "map\\superregion.txt");
                //bw.ReportProgress(107);
                foreach (Node n in Superregions.MainNode.Nodes)
                {
                    List<Region> reg = new List<Region>();
                    foreach (string s in n.PureValues)
                    {
                        Region r = GlobalVariables.Regions.Find(x => x.Name == s);
                        if (r != null)
                        {
                            reg.Add(r);
                        }
                    }
                    Superregion sr = new Superregion(n.Name, reg);
                    foreach (Region re in sr.Regions)
                        re.Superregion = sr;
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
            await ldefaultmap;
            if (ldefaultmap.IsFaulted)
                progress.UpdateProgress(17, 1);
            else if (ldefaultmap.IsCompleted)
                progress.UpdateProgress(17, 2);
            await provincecentre;
            if (provincecentre.IsFaulted)
                progress.UpdateProgress(3, 1);
            else if (provincecentre.IsCompleted)
                progress.UpdateProgress(3, 2);

            Task umap = new Task(() => {
                foreach (Province p in GlobalVariables.Provinces)
                {
                    p.BorderPixels = GraphicsMethods.CreateBorders(p);
                }
                MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Development);
                MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.TradeGood);
                MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Religion);
                MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Culture);
                MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Political);
                MapManagement.CreateClickMask();
                MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Area);
                MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Region);
                MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.TradeNode);
                MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.HRE);
                MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Fort);
                MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Continent);
                MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Superregion);
            });
            umap.Start();
            progress.UpdateProgress(18, 0);
            Task ucontrol = new Task(() => {
                foreach (TradeGood tg in GlobalVariables.TradeGoods)
                {
                    if (!GlobalVariables.LatentTradeGoods.Contains(tg))
                        ModEditor.form.TradeGoodBox.Items.Add(tg.ReadableName);
                    else
                        ModEditor.form.LatentTradeGoodBox.Items.Add(tg.ReadableName);
                }
                foreach (Religion r in Religion.Religions)
                {
                    ModEditor.form.ReligionBox.Items.Add(r.ReadableName);
                    ModEditor.form.CountryReligionBox.Items.Add(r.ReadableName);
                }
                foreach (Culture c in Culture.Cultures)
                {
                    ModEditor.form.CultureBox.Items.Add(c.Name);
                    ModEditor.form.CountryPrimaryCultureBox.Items.Add(c.Name);
                }
                foreach (Country c in GlobalVariables.Countries)
                {
                    ModEditor.form.OwnerBox.Items.Add(c.FullName + ", " + c.Tag);
                    ModEditor.form.CountryBox.Items.Add(c.FullName + ", " + c.Tag);
                    ModEditor.form.AddCoreBox.Items.Add(c.FullName + ", " + c.Tag);
                    ModEditor.form.ControllerBox.Items.Add(c.Tag);
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
            });
            ucontrol.Start();
            progress.UpdateProgress(19, 0);

            await umap;
            if (umap.IsFaulted)
                progress.UpdateProgress(18, 1);
            else if (umap.IsCompleted)
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



            int[] ids = new int[5000];
            string[] tags = new string[5000];
            string[] cultu = new string[5000];
            string[] relig = new string[5000];
            int[] dev = new int[5000];
            string[] tradego = new string[5000];
            string[] terrains = new string[5000];
            string[] names = new string[5000];

            int nme = -1;
            foreach (Province p in GlobalVariables.Provinces)
            {
                if(p.OwnerCountry != null)
                {
                    nme++;
                    ids[nme] = p.ID;
                    /*
                    tags[nme] = p.OwnerCountry.Tag;
                    if(p.Culture != null)
                        cultu[nme] = p.Culture.Name;
                    if(p.Religion != null)
                        relig[nme] = p.Religion.ReadableName;
                    dev[nme] = p.Tax + p.Production + p.Manpower;
                    if(p.TradeGood != null)
                        tradego[nme] = p.TradeGood.ReadableName;
                        */
                }
                

            }
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

        public static void LoadFilesSync()
        {
            StreamReader Reader;
            if (GlobalVariables.UseMod[0] > 0)
                Reader = File.OpenText(GlobalVariables.pathtomod + "map\\definition.csv");
            else
                Reader = File.OpenText(GlobalVariables.pathtogame + "map\\definition.csv");

            Reader.ReadLine();
            while (!Reader.EndOfStream)
            {
                string data = Reader.ReadLine();
                string[] values = data.Split(';');
                Province p = new Province(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]), int.Parse(values[3]), values[4]);
                GlobalVariables.Provinces.Add(p);
                //ColorToProvince.Add(p.R + " " + p.G + " " + p.B, p);                 
            }
            //MessageBox.Show(Provinces.Count() + "");

            //lp.UpdateProgressLabel("Loading province map...", 2);
            Bitmap copiedBitmap = new Bitmap(GlobalVariables.ProvincesMapBitmap);
            LockBitmap bitmap = new LockBitmap(copiedBitmap);
            bitmap.LockBits();

            int heightInterval = bitmap.Height / 10;
            int heightValue = 0;
            int va = 10;

            for (int y = 1; y < bitmap.Height; y++)
            {
                for (int x = 1; x < bitmap.Width; x += 5)
                {
                    Color c = bitmap.GetPixel(x, y);
                    if (c != Color.FromArgb(1, 255, 255, 255))
                    {
                        Province p = GlobalVariables.Provinces.Find(pr => pr.c == c);
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


            List<NodeFile> tradegoodsfiles = new List<NodeFile>();
            if (GlobalVariables.UseMod[2] != 0)
            {
                foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\tradegoods\\"))
                {
                    if (file.Contains('.'))
                    {
                        if (file.Split('.')[1] == "txt")
                        {
                            NodeFile nf = new NodeFile(file);
                            tradegoodsfiles.Add(nf);
                            GlobalVariables.ModTradeGoodsFiles.Add(nf);
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
                            if (!tradegoodsfiles.Any(x => x.FileName == file.Split('\\').Last().Replace(".txt", "")))
                                tradegoodsfiles.Add(nf);
                            GlobalVariables.GameTradeGoodsFile = nf;
                        }
                    }
                }
            }


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
                        ReadableName = node.Name[0].ToString().ToUpper() + node.Name.Substring(1).Replace('_', ' ')
                    };
                    string[] colorv = node.Nodes.Find(x => x.Name == "color").PureValues.ToArray();
                    if (colorv.Count() == 3)
                    {
                        tg.Color = Color.FromArgb((int)(255 * double.Parse(colorv[0], CultureInfo.InvariantCulture)), (int)(255 * double.Parse(colorv[1], CultureInfo.InvariantCulture)), (int)(255 * double.Parse(colorv[2], CultureInfo.InvariantCulture)));
                    }
                    else
                    {
                        tg.Color = AdditionalElements.GenerateColor(GlobalVariables.GlobalRandom);
                    }

                    GlobalVariables.TradeGoods.Add(tg);

                    Variable latent = node.Variables.Find(x => x.Name == "is_latent");
                    if (latent != null)
                        if (latent.Value == "yes")
                            GlobalVariables.LatentTradeGoods.Add(tg);
                }

            }


            List<NodeFile> tradegoodspricesfiles = new List<NodeFile>();
            if (GlobalVariables.UseMod[3] != 0)
            {
                foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\prices\\"))
                {
                    if (file.Contains('.'))
                    {
                        if (file.Split('.')[1] == "txt")
                        {
                            NodeFile nf = new NodeFile(file);
                            tradegoodspricesfiles.Add(nf);
                            GlobalVariables.ModPricesFiles.Add(nf);
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
                            if (!tradegoodspricesfiles.Any(x => x.FileName == file.Split('\\').Last().Replace(".txt", "")))
                                tradegoodspricesfiles.Add(nf);
                            GlobalVariables.GamePricesFile = nf;
                        }
                    }
                }
            }

            List<string> done = new List<string>();
            foreach (NodeFile tradegoodsprices in tradegoodspricesfiles)
            {
                foreach (Node node in tradegoodsprices.MainNode.Nodes)
                {
                    if (done.Contains(node.Name))
                        continue;
                    done.Add(node.Name);
                    TradeGood tg = GlobalVariables.TradeGoods.Find(x => x.Name == node.Name);
                    if (tg != null)
                    {
                        tg.Price = double.Parse(node.Variables.Find(x => x.Name == "base_price").Value, CultureInfo.InvariantCulture);
                        if (node.Variables.Find(x => x.Name == "goldtype") != null)
                        {
                            if (node.Variables.Find(x => x.Name == "goldtype").Value.Trim() == "yes")
                                tg.GoldLike = true;
                        }
                    }
                }
            }

            List<NodeFile> culturesfiles = new List<NodeFile>();
            try
            {

                if (GlobalVariables.UseMod[4] != 0)
                {
                    foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\cultures\\"))
                    {
                        if (file.Contains('.'))
                        {
                            if (file.Split('.')[1] == "txt")
                            {
                                NodeFile nf = new NodeFile(file);
                                culturesfiles.Add(nf);
                                GlobalVariables.ModCulturesFiles.Add(nf);
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
                                if (!culturesfiles.Any(x => x.FileName == file.Split('\\').Last().Replace(".txt", "")))
                                    culturesfiles.Add(nf);
                                GlobalVariables.GameCulturesFile = nf;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

            done.Clear();

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
                                Group = cg
                            };
                            cg.Cultures.Add(c);
                            Variable v = innernode.Variables.Find(x => x.Name == "primary");
                            if (v != null)
                                c.PrimaryTag = v.Value;
                        }
                    }
                }
            }

            List<NodeFile> religionsfiles = new List<NodeFile>();
            if (GlobalVariables.UseMod[5] != 0)
            {
                foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\religions\\"))
                {
                    if (file.Contains('.'))
                    {
                        if (file.Split('.')[1] == "txt")
                        {
                            NodeFile nf = new NodeFile(file);
                            religionsfiles.Add(nf);
                            GlobalVariables.ModReligionsFiles.Add(nf);
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
                            if (!religionsfiles.Any(x => x.FileName == file.Split('\\').Last().Replace(".txt", "")))
                                religionsfiles.Add(nf);
                            GlobalVariables.GameReligionsFile = nf;
                        }
                    }
                }
            }
            string[] religionforbidden = new string[] { };
            done.Clear();
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
                                    Group = rg
                                };
                                rg.Religions.Add(r);
                                string[] colorstring = innernode.Nodes.Find(x => x.Name == "color").PureValues.ToArray();
                                r.Color = Color.FromArgb(int.Parse(colorstring[0]), int.Parse(colorstring[1]), int.Parse(colorstring[2]));
                                r.Icon = int.Parse(innernode.Variables.Find(x => x.Name == "icon").Value);
                            }
                            catch
                            {

                            }
                        }
                    }
                }
            }
            List<NodeFile> governmentsfiles = new List<NodeFile>();
            if (GlobalVariables.UseMod[16] != 0)
            {
                foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\governments\\"))
                {
                    if (file.Contains('.'))
                    {
                        if (file.Split('.')[1] == "txt")
                        {
                            NodeFile nf = new NodeFile(file);
                            governmentsfiles.Add(nf);
                            GlobalVariables.ModGovernmentsFiles.Add(nf);
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
                            if (!governmentsfiles.Any(x => x.FileName == file.Split('\\').Last().Replace(".txt", "")))
                                governmentsfiles.Add(nf);
                            GlobalVariables.GameGovernmentsFile = nf;
                        }
                    }
                }
            }
            foreach (NodeFile government in governmentsfiles)
            {
                foreach (Node n in government.MainNode.Nodes)
                {
                    if (n.Name != "pre_dharma_mapping")
                    {
                        Government gv = new Government(n.Name);
                        gv.reforms.AddRange(n.Nodes.Find(x => x.Name == "reform_levels").Nodes[0].Nodes.Find(x => x.Name == "reforms").PureValues);
                        GlobalVariables.Governments.Add(gv);
                    }
                }
            }
            NodeFile technology;
            if (GlobalVariables.UseMod[15] > 0)
                technology = new NodeFile(GlobalVariables.pathtomod + "common\\technology.txt");
            else
                technology = new NodeFile(GlobalVariables.pathtogame + "common\\technology.txt");

            foreach (Node node in technology.MainNode.Nodes.Find(x => x.Name == "groups").Nodes)
            {
                GlobalVariables.TechGroups.Add(node.Name);
            }
            Dictionary<string, string> NameToTag = new Dictionary<string, string>();

            List<NodeFile> countrytagsfiles = new List<NodeFile>();

            if (GlobalVariables.UseMod[14] != 0)
            {
                foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\country_tags\\"))
                {
                    if (file.Contains('.'))
                    {
                        if (file.Split('.')[1] == "txt")
                        {
                            NodeFile nf = new NodeFile(file);
                            countrytagsfiles.Add(nf);
                            GlobalVariables.ModCountryTagsFiles.Add(nf);
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
                            if (!countrytagsfiles.Any(x => x.FileName == file.Split('\\').Last().Replace(".txt", "")))
                                countrytagsfiles.Add(nf);
                            GlobalVariables.GameCountryTagsFile = nf;
                        }
                    }
                }
            }


            foreach (NodeFile countrytags in countrytagsfiles)
            {
                foreach (Variable v in countrytags.MainNode.Variables)
                {
                    string n = v.Value.Replace("\"", "").Trim().Split('/')[1].Split('.')[0];
                    if (!NameToTag.Keys.Contains(n))
                        NameToTag.Add(n, v.Name.Trim());
                }
            }

            List<string> CountryHistoryFiles = new List<string>();
            List<bool> GameFiles = new List<bool>();

            if (GlobalVariables.UseMod[11] != 0)
            {
                foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "history\\countries\\"))
                {
                    if (file.Contains('.'))
                    {
                        if (file.Split('.')[1] == "txt")
                        {
                            CountryHistoryFiles.Add(file);
                            GameFiles.Add(false);
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
                            if (!CountryHistoryFiles.Any(x => x.Split('\\').Last().Replace(".txt", "") == file.Split('\\').Last().Replace(".txt", "")))
                            {
                                CountryHistoryFiles.Add(file);
                                GameFiles.Add(true);
                            }
                        }
                    }
                }
            }
            foreach (string file in CountryHistoryFiles)
            {
                string fs = file.Split('\\').Last();
                string tag = "";
                string name = "";
                try
                {
                    tag = fs.Split('-')[0].Trim().ToUpper();
                    name = fs.Split('-')[1].Trim().Split('.')[0];
                }
                catch
                {
                    continue;
                }

                Country c = new Country();
                c.HistoryFile = file;
                if (GameFiles[CountryHistoryFiles.IndexOf(file)])
                    c.HistoryFileGame = true;
                GlobalVariables.Countries.Add(c);
                NodeFile nodefile = new NodeFile(file);
                foreach (Variable v in nodefile.MainNode.Variables)
                {
                    switch (v.Name)
                    {
                        case "government":
                            c.Government = GlobalVariables.Governments.Find(x => x.Type == v.Value);
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
                            c.Religion = Religion.Religions.Find(x => x.Name == v.Value);
                            break;
                        case "primary_culture":
                            c.PrimaryCulture = v.Value;
                            break;
                        case "government_rank ":
                            c.GovernmentRank = int.Parse(v.Value);
                            break;
                    }
                }
                c.Tag = tag;
                c.FullName = name;
            }


            List<NodeFile> buildingsfiles = new List<NodeFile>();
            GameFiles.Clear();
            if (GlobalVariables.UseMod[17] != 0)
            {
                foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\buildings\\"))
                {
                    if (file.Contains('.'))
                    {
                        if (file.Split('.')[1] == "txt")
                        {
                            NodeFile nf = new NodeFile(file);
                            buildingsfiles.Add(nf);
                            GameFiles.Add(false);
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
                            if (!buildingsfiles.Any(x => x.FileName == file.Split('\\').Last().Replace(".txt", "")))
                            {
                                buildingsfiles.Add(nf);
                                GameFiles.Add(true);
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
                    bl.File = buildings.FileName;
                    if (GameFiles[buildingsfiles.IndexOf(buildings)])
                        bl.GameFile = true;
                    GlobalVariables.Buildings.Add(bl);
                }
            }


            List<string> CountryCommonFiles = new List<string>();

            GameFiles.Clear();

            if (GlobalVariables.UseMod[7] != 0)
            {
                foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\countries\\"))
                {
                    if (file.Contains('.'))
                    {
                        if (file.Split('.')[1] == "txt")
                        {
                            CountryCommonFiles.Add(file);
                            GameFiles.Add(false);
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
                            if (!CountryCommonFiles.Any(x => x.Split('\\').Last().Replace(".txt", "") == file.Split('\\').Last().Replace(".txt", "")))
                            {
                                CountryCommonFiles.Add(file);
                                GameFiles.Add(true);
                            }
                        }
                    }
                }
            }


            foreach (string file in CountryCommonFiles)
            {
                string name = file.Split('\\').Last().Split('.')[0];
                if (!NameToTag.ContainsKey(name))
                    continue;
                Country c = GlobalVariables.Countries.Find(x => x.Tag == NameToTag[name]);
                if (c != null)
                {
                    c.CommonFile = file;
                    if (GameFiles.Count > CountryCommonFiles.IndexOf(file))
                    {
                        if (GameFiles[CountryCommonFiles.IndexOf(file)])
                            c.CommonFileGame = true;
                    }
                    NodeFile nodefile = new NodeFile(file);
                    string[] colort = nodefile.MainNode.Nodes.Find(x => x.Name == "color").PureValues.ToArray();
                    try
                    {
                        c.Color = Color.FromArgb(int.Parse(colort[0]), int.Parse(colort[1]), int.Parse(colort[2]));
                    }
                    catch
                    {
                        c.Color = AdditionalElements.GenerateColor(GlobalVariables.GlobalRandom);
                        MessageBox.Show("Some error in " + file);
                    }
                }
            }

            //lp.UpdateProgressLabel("Loading provinces...", 80);

            List<string> Files = new List<string>();
            GameFiles.Clear();
            if (GlobalVariables.UseMod[8] != 0)
            {
                foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "history\\provinces\\"))
                {
                    if (file.Contains('.'))
                    {
                        if (file.Split('.').Last() == "txt")
                        {
                            Files.Add(file);
                            GameFiles.Add(false);
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
                            if (!Files.Any(x => x.Split('\\').Last() == file.Split('\\').Last()))
                            {
                                Files.Add(file);
                                GameFiles.Add(true);
                            }
                        }
                    }
                }
            }
            foreach (string file in Files)
            {
                bool b = int.TryParse(new String(file.Split('\\').Last().Where(x => char.IsDigit(x)).ToArray()), out int id);
                if (b)
                {
                    Province province = GlobalVariables.Provinces.Find(x => x.ID == id);
                    if (province != null)
                    {
                        province.HistoryFile = file;
                        try
                        {
                            province.HistoryFileGame = GameFiles[Files.IndexOf(file)];
                        }
                        catch
                        {
                        }
                        NodeFile nodefile = new NodeFile(file);

                        if (nodefile.MainNode.Variables.Any())
                            GlobalVariables.TotalUsableProvinces++;

                        foreach (Variable v in nodefile.MainNode.Variables)
                        {
                            Building bl = GlobalVariables.Buildings.Find(x => x.Name == v.Name);
                            if (bl != null && v.Value == "yes")
                            {
                                province.AddBuilding(bl, true);
                            }

                            switch (v.Name)
                            {
                                case "add_core":
                                    province.AddCore(v.Value, true);
                                    break;
                                case "add_claim":
                                    province.AddClaim(v.Value, true);
                                    break;
                                case "owner":
                                    Country c = GlobalVariables.Countries.Find(x => x.Tag == v.Value.ToUpper());
                                    if (c != null)
                                    {
                                        province.OwnerCountry = c;
                                        c.Provinces.Add(province);
                                    }
                                    break;
                                case "controller":
                                    province.Controller = v.Value;
                                    break;
                                case "culture":
                                    province.Culture = Culture.Cultures.Find(x => x.Name == v.Value);
                                    break;
                                case "religion":
                                    province.Religion = Religion.Religions.Find(x => x.Name == v.Value);
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
                                    province.TradeGood = GlobalVariables.TradeGoods.Find(x => x.Name == v.Value);
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

                        if (province.TradeGood != null)
                        {
                            province.TradeGood.TotalDev += province.Tax + province.Production + province.Manpower;
                        }

                        Node n = nodefile.MainNode.Nodes.Find(x => x.Name == "latent_trade_goods");
                        if (n != null)
                        {
                            if (n.PureValues.Any())
                            {
                                province.LatentTradeGood = GlobalVariables.TradeGoods.Find(x => x.Name == n.PureValues[0].Trim());
                                if (province.LatentTradeGood != null)
                                {
                                    province.LatentTradeGood.TotalProvinces++;
                                    province.TradeGood.TotalDev += province.Tax + province.Production + province.Manpower;
                                }
                            }
                        }
                    }
                }
            }
            //lp.UpdateProgressLabel("Loading areas...", 90);

            NodeFile areas;
            if (GlobalVariables.UseMod[9] > 0)
                areas = new NodeFile(GlobalVariables.pathtomod + "map\\area.txt");
            else
                areas = new NodeFile(GlobalVariables.pathtogame + "map\\area.txt");
            foreach (Node n in areas.MainNode.Nodes)
            {
                List<Province> pr = new List<Province>();
                foreach (string vr in n.PureValues)
                {
                    if (vr != "")
                    {
                        pr.Add(GlobalVariables.Provinces[int.Parse(vr) - 1]);
                    }
                }
                Area a = new Area(n.Name, pr);
                foreach (Province pro in a.Provinces)
                    pro.Area = a;
            }
            NodeFile regions;
            if (GlobalVariables.UseMod[6] > 0)
                regions = new NodeFile(GlobalVariables.pathtomod + "map\\region.txt");
            else
                regions = new NodeFile(GlobalVariables.pathtogame + "map\\region.txt");
            foreach (Node n in regions.MainNode.Nodes)
            {
                List<Area> ar = new List<Area>();
                Node nd = n.Nodes.Find(x => x.Name == "areas");
                if (nd != null)
                {
                    foreach (string vr in nd.PureValues)
                    {
                        if (vr != "")
                        {
                            Area are = GlobalVariables.Areas.Find(x => x.Name == vr);
                            if (are != null)
                                ar.Add(are);
                        }
                    }
                    Region r = new Region(n.Name, ar);
                    foreach (Area are in r.Areas)
                        are.Region = r;
                }
            }


            NodeFile continents;
            if (GlobalVariables.UseMod[13] > 0)
                continents = new NodeFile(GlobalVariables.pathtomod + "map\\continent.txt");
            else
                continents = new NodeFile(GlobalVariables.pathtogame + "map\\continent.txt");
            foreach (Node n in continents.MainNode.Nodes)
            {
                List<Province> ctp = new List<Province>();
                foreach (string s in n.PureValues)
                {
                    ctp.Add(GlobalVariables.Provinces[int.Parse(s.Trim()) - 1]);
                }
                Continent c = new Continent(n.Name, ctp);
                foreach (Province pr in c.Provinces)
                    pr.Continent = c;
            }

            List<NodeFile> tradenodesfiles = new List<NodeFile>();
            GameFiles.Clear();
            if (GlobalVariables.UseMod[12] != 0)
            {
                foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "common\\tradenodes\\"))
                {
                    if (file.Contains('.'))
                    {
                        if (file.Split('.')[1] == "txt")
                        {
                            NodeFile nf = new NodeFile(file);
                            tradenodesfiles.Add(nf);
                            GlobalVariables.ModTradeNodesFiles.Add(nf);
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
                            if (!tradenodesfiles.Any(x => x.FileName == file.Split('\\').Last().Replace(".txt", "")))
                                tradenodesfiles.Add(nf);
                            GlobalVariables.GameTradeNodesFile = nf;
                        }
                    }
                }
            }
            foreach (NodeFile tradenodes in tradenodesfiles)
            {
                foreach (Node node in tradenodes.MainNode.Nodes)
                {
                    if (GlobalVariables.TradeNodes.Any(x => x.Name == node.Name))
                        continue;
                    Tradenode tn = new Tradenode();
                    tn.Name = node.Name;
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
                    Tradenode tn = GlobalVariables.TradeNodes.Find(x => x.Name == node.Name);
                    tn.File = tradenodes.FileName;
                    if (tradenodes == GlobalVariables.GameTradeNodesFile)
                        tn.GameFile = true;
                    tn.Name = node.Name;
                    tn.Location = GlobalVariables.Provinces[int.Parse(node.Variables.Find(x => x.Name == "location").Value) - 1];
                    Node ColorNode = node.Nodes.Find(x => x.Name == "color");
                    if (ColorNode != null)
                        tn.Color = Color.FromArgb(int.Parse(ColorNode.PureValues[0]), int.Parse(ColorNode.PureValues[1]), int.Parse(ColorNode.PureValues[2]));
                    else
                        tn.Color = AdditionalElements.GenerateColor(GlobalVariables.GlobalRandom);

                    Variable v = node.Variables.Find(x => x.Name == "inland");
                    if (v == null)
                        tn.Inland = false;
                    else
                    {
                        if (v.Value == "yes")
                            tn.Inland = true;
                        else
                            tn.Inland = false;
                    }
                    v = node.Variables.Find(x => x.Name == "end");
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
                        Destination dn = new Destination() { TradeNode = GlobalVariables.TradeNodes.Find(x => x.Name == outgoing.Variables.Find(y => y.Name == "name").Value.Replace("\"", "")) };
                        dn.Path.AddRange(outgoing.Nodes.Find(x => x.Name == "path").PureValues);
                        if (outgoing.Nodes.Find(x => x.Name == "control") != null)
                            dn.Control.AddRange(outgoing.Nodes.Find(x => x.Name == "control").PureValues);
                        tn.Destination.Add(dn);
                        dn.TradeNode.Incoming.Add(tn);
                    }
                    foreach (string value in node.Nodes.Find(x => x.Name == "members").PureValues)
                    {
                        if (int.Parse(value) <= GlobalVariables.Provinces.Count())
                        {
                            tn.Provinces.Add(GlobalVariables.Provinces[int.Parse(value) - 1]);
                            GlobalVariables.Provinces[int.Parse(value) - 1].TradeNode = tn;
                        }
                    }
                }
            }



            /////////////////////////////////////////////////////////////////////////
            ///

            NodeFile Superregions;
            if (GlobalVariables.UseMod[18] > 0)
                Superregions = new NodeFile(GlobalVariables.pathtomod + "map\\superregion.txt");
            else
                Superregions = new NodeFile(GlobalVariables.pathtogame + "map\\superregion.txt");
            foreach (Node n in Superregions.MainNode.Nodes)
            {
                List<Region> reg = new List<Region>();
                foreach (string s in n.PureValues)
                {
                    Region r = GlobalVariables.Regions.Find(x => x.Name == s);
                    if (r != null)
                    {
                        reg.Add(r);
                    }
                }
                Superregion sr = new Superregion(n.Name, reg);
                foreach (Region re in sr.Regions)
                    re.Superregion = sr;
            }



            //lp.UpdateProgressLabel("Loading map variables...", 95);
            if (GlobalVariables.UseMod[10] > 0)
                Reader = new StreamReader(GlobalVariables.pathtomod + "map\\default.map");
            else
                Reader = new StreamReader(GlobalVariables.pathtogame + "map\\default.map");
            bool addlines = false;
            int addlinesto = 0;
            string seas = "";
            string lakes = "";
            while (!Reader.EndOfStream)
            {
                string line = Reader.ReadLine();
                if (line.Contains("sea_starts"))
                {
                    addlines = true;
                    addlinesto = 0;
                }
                else if (line.Contains("lakes"))
                {
                    addlines = true;
                    addlinesto = 1;
                }

                if (addlines)
                {
                    if (addlinesto == 0)
                        seas += " " + line;
                    else if (addlinesto == 1)
                        lakes += " " + line;
                }

                if (line.Contains("}"))
                    addlines = false;
            }
            seas = seas.Split('{')[1].Split('}')[0];
            lakes = lakes.Split('{')[1].Split('}')[0];


            foreach (string sea in seas.Split(' '))
            {
                if (sea != "" && sea != " ")
                {
                    if (int.TryParse(sea, out int id))
                    {
                        try
                        {
                            GlobalVariables.Provinces[id - 1].Sea = true;
                        }
                        catch
                        {
                            MessageBox.Show("Sea Id: " + id);
                        }
                    }
                }
            }
            foreach (string lake in lakes.Split(' '))
            {
                if (lake != "" && lake != " ")
                {
                    if (int.TryParse(lake, out int id))
                    {
                        try
                        {
                            GlobalVariables.Provinces[id - 1].Lake = true;
                        }
                        catch
                        {
                            MessageBox.Show("Lake Id: " + id);
                        }
                    }
                }
            }

            Reader.Dispose();

            foreach (Province p in GlobalVariables.Provinces)
            {
                p.BorderPixels = GraphicsMethods.CreateBorders(p);
            }
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Development);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.TradeGood);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Religion);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Culture);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Political);
            MapManagement.CreateClickMask();
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Area);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Region);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.TradeNode);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.HRE);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Fort);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Continent);
            MapManagement.UpdateMap(GlobalVariables.Provinces, MapManagement.UpdateMapOptions.Superregion);
            foreach (TradeGood tg in GlobalVariables.TradeGoods)
            {
                if (!GlobalVariables.LatentTradeGoods.Contains(tg))
                    ModEditor.form.TradeGoodBox.Items.Add(tg.ReadableName);
                else
                    ModEditor.form.LatentTradeGoodBox.Items.Add(tg.ReadableName);
            }
            foreach (Religion r in Religion.Religions)
            {
                ModEditor.form.ReligionBox.Items.Add(r.ReadableName);
                ModEditor.form.CountryReligionBox.Items.Add(r.ReadableName);
            }
            foreach (Culture c in Culture.Cultures)
            {
                ModEditor.form.CultureBox.Items.Add(c.Name);
                ModEditor.form.CountryPrimaryCultureBox.Items.Add(c.Name);
            }
            foreach (Country c in GlobalVariables.Countries)
            {
                ModEditor.form.OwnerBox.Items.Add(c.FullName + ", " + c.Tag);
                ModEditor.form.CountryBox.Items.Add(c.FullName + ", " + c.Tag);
                ModEditor.form.AddCoreBox.Items.Add(c.FullName + ", " + c.Tag);
                ModEditor.form.ControllerBox.Items.Add(c.Tag);
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
            //lp.CloseForm();

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



            int[] ids = new int[5000];
            string[] tags = new string[5000];
            string[] cultu = new string[5000];
            string[] relig = new string[5000];
            int[] dev = new int[5000];
            string[] tradego = new string[5000];
            string[] terrains = new string[5000];
            string[] names = new string[5000];

            int nme = -1;
            foreach (Province p in GlobalVariables.Provinces)
            {
                if (p.OwnerCountry != null)
                {
                    nme++;
                    ids[nme] = p.ID;
                    /*
                    tags[nme] = p.OwnerCountry.Tag;
                    if(p.Culture != null)
                        cultu[nme] = p.Culture.Name;
                    if(p.Religion != null)
                        relig[nme] = p.Religion.ReadableName;
                    dev[nme] = p.Tax + p.Production + p.Manpower;
                    if(p.TradeGood != null)
                        tradego[nme] = p.TradeGood.ReadableName;
                        */
                }


            }
            /*
            File.WriteAllLines("owners.txt", tags);
            File.WriteAllLines("cultures.txt", cultu);
            File.WriteAllLines("religions.txt", relig);
            File.WriteAllLines("dev.txt", dev.Select(x => x.ToString()).ToArray());
            File.WriteAllLines("tradegoods.txt", tradego);
            */
            File.WriteAllLines("ids.txt", ids.Select(x => x.ToString()).ToArray());
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
    }
}
