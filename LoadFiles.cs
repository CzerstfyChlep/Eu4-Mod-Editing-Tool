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
    public static partial class LoadFilesClass
    {
        public static NodeFile ReadOneFile(string path, LoadingProgress progress)
        {
            return null;
        }
        public static NodeFile[] ReadFiles(string path, LoadingProgress progress)
        {
            List<NodeFile> files = new List<NodeFile>();
            if (!Directory.Exists(path))
                progress.ReportError($"Error: Directory '{path}' doesn't exist!");
            else
            {
                foreach (string file in Directory.GetFiles(path))
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
                                files.Add(nf);
                            }
                        }
                    }
                }
            }
            return files.ToArray();
        }


        public static async void LoadFilesWork(LoadingProgress progress)
        {
            try
            {

                /*NodeFile nodef = new NodeFile();
                nodef.ReadFile("test.txt", true);
                nodef.SaveFile("testout.txt");
                */


                //BackgroundWorker bw = (BackgroundWorker)sender;
                List<NodeFile> tradegoodsfiles = new List<NodeFile>();
                List<NodeFile> tradegoodspricesfiles = new List<NodeFile>();
                List<NodeFile> culturesfiles = new List<NodeFile>();
                List<NodeFile> religionsfiles = new List<NodeFile>();
                List<NodeFile> governmentsfiles = new List<NodeFile>();
                List<NodeFile> ideafiles = new List<NodeFile>();
                Dictionary<string, string> NameToTag = new Dictionary<string, string>();
                Dictionary<string, NodeFile> NameToFile = new Dictionary<string, NodeFile>();
                List<NodeFile> countrytagsfiles = new List<NodeFile>();
                List<NodeFile> buildingsfiles = new List<NodeFile>();
                List<NodeFile> CountryCommonFiles = new List<NodeFile>();
                NodeFile technology = new NodeFile();
                NodeFile areas = new NodeFile();
                NodeFile climate = new NodeFile();
                NodeFile regions = new NodeFile();
                NodeFile continents = new NodeFile();
                List<NodeFile> tradenodesfiles = new List<NodeFile>();
                List<NodeFile> tradecompanyfiles = new List<NodeFile>();
                NodeFile Superregions = new NodeFile();

                GlobalVariables.Countries.Add(Country.NoCountry);
                Task llocalisation = new Task(() =>
                {
                    LoadLocalisation(progress);
                });
                llocalisation.Start();
                progress.UpdateProgress(22, 0);

                Task ldefinition = new Task(() =>
                {
                    LoadDefinition(progress);
                });
                ldefinition.Start();
                progress.UpdateProgress(0, 0);

                Task ltradegoods = new Task(() =>
                {
                    LoadTradeGoods(progress, tradegoodsfiles);
                });
                ltradegoods.Start();
                progress.UpdateProgress(1, 0);
            
                Task lcultures = new Task(() =>
                {
                    LoadCultures(progress, culturesfiles);

                });
                lcultures.Start();
                progress.UpdateProgress(4, 0);

                Task lrebelTypes = new Task(() => {
                    LoadRebels(progress);
                });
                lrebelTypes.Start();

                Task lreligions = new Task(() =>
                {
                    LoadReligions(progress, religionsfiles);
                });
                lreligions.Start();
                progress.UpdateProgress(5, 0);

                Task lgovernments = new Task(() =>
                {
                    LoadGovernments(progress, governmentsfiles);
                });
                lgovernments.Start();
                progress.UpdateProgress(6, 0);

                Task lideas = new Task(() =>
                {
                    LoadIdeas(progress, ideafiles);
                });
                lideas.Start();

                Task ltechnology = new Task(() =>
                {
                    LoadTechGroups(progress, technology);
                });
                ltechnology.Start();
                progress.UpdateProgress(7, 0);

                Task ltags = new Task(() =>
                {
                    LoadTags(progress, countrytagsfiles, NameToTag, NameToFile);   
                });
                ltags.Start();
                progress.UpdateProgress(8, 0);

                Task lbuildings = new Task(() =>
                {
                    LoadBuildings(progress, buildingsfiles);
                });
                lbuildings.Start();
                progress.UpdateProgress(20, 0);

                //TODO no progress
                await lrebelTypes;
                await lideas;

                await ldefinition;
                if (ldefinition.IsFaulted)
                    progress.UpdateProgress(0, 1);
                else if (ldefinition.IsCompleted)
                    progress.UpdateProgress(0, 2);

                Task larea = new Task(() =>
                {
                    LoadAreas(progress, areas);
                });
                larea.Start();
                progress.UpdateProgress(12, 0);

                //DONE
                Task lcontinent = new Task(() =>
                {
                    LoadContinents(progress, continents);
                });
                lcontinent.Start();
                progress.UpdateProgress(14, 0);

                //DONE
                Task ltradenodes = new Task(() =>
                {
                    LoadTradenodes(progress, tradenodesfiles);
                });
                ltradenodes.Start();
                progress.UpdateProgress(15, 0);

                //DONE
                Task ldefaultmap = new Task(() =>
                {
                    LoadDefault(progress);
                });
                ldefaultmap.Start();
                progress.UpdateProgress(17, 0);

                //DONE
                Task lmap = new Task(() =>
                {
                    LoadMap(progress);  
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
                    LoadTradeCompanies(progress, tradecompanyfiles);
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
                    LoadClimate(progress, climate);
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
                    LoadCommonCountries(progress, CountryCommonFiles, NameToTag, NameToFile);
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
                    LoadHistoryCountries(progress);
                });
                lcountries.Start();
                progress.UpdateProgress(9, 0);

                //DONE
                Task lprovhistory = new Task(() =>
                {
                    LoadProvinces(progress);
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
                    LoadRegions(progress, regions);
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
                    LoadSuperregions(progress, Superregions);
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
                        if (province.OwnerCountry != null && DateTime.Compare(GlobalVariables.StartDate, date)==0)
                            province.OwnerCountry.Provinces.Remove(province);
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
