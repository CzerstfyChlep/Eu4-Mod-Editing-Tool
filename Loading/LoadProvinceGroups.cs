using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public static partial class LoadFilesClass
    {
        public static void LoadAreas(LoadingProgress progress, NodeFile areas)
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
                                        if (p.Area != null)
                                        {
                                            progress.ReportError($"Alert: Province '{p.ID}' is in multiple areas, '{p.Area}' and '{n.Name}'! Using the second one!");
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
        }
        public static void LoadContinents(LoadingProgress progress, NodeFile continents)
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
                            if (int.TryParse(s.Name.Trim(), out id))
                            {
                                Province p = GlobalVariables.Provinces.Find(x => x.ID == id);
                                if (p == null)
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

        }
        public static void LoadTradeCompanies(LoadingProgress progress, List<NodeFile> tradecompanyfiles)
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


                        Node provinces = node.Nodes.Find(x => x.Name.ToLower() == "provinces");
                        if (provinces == null)
                        {
                            progress.ReportError($"Alert: Trade company '{tc.Name}' has no provinces specified!");
                        }
                        else
                        {
                            foreach (PureValue value in provinces.PureValues)
                            {
                                int id = 0;
                                if (!int.TryParse(value.Name, out id))
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
                                            progress.ReportError($"Alert: Province {p.ID} belongs to many trade companies! '{p.TradeCompany.Name}' and '{tc.Name}'. Using second!");
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
        }
        public static void LoadRegions(LoadingProgress progress, NodeFile regions)
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
                                    Area are = GlobalVariables.Areas.Find(x => x.Name.ToLower() == vr.Name.ToLower());
                                    if (are != null)
                                    {
                                        if (are.Region != null)
                                        {
                                            are.Region.Areas.Remove(are);
                                            progress.ReportError($"Alert: Area '{are}' is part of two regions. '{n.Name}' and '{are.Region}'. Picking second.");
                                        }
                                        ar.Add(are);
                                    }
                                    else
                                    {
                                        progress.ReportError($"Error: Region '{n.Name}' has unknown area '{vr.Name}'.");
                                    }
                                }
                            }
                            Region r = new Region(n.Name, ar);
                            r.OriginalName = n.Name;
                            foreach (Area are in r.Areas)
                                are.Region = r;
                        }
                        else
                        {
                            progress.ReportError($"Alert: Region '{n.Name}' has no areas.");
                        }
                    }
                }
            }
            catch
            {
                if (GlobalVariables.__DEBUG)
                    throw;
                progress.ReportError("Critical error: Unexpected issue with regions! Program will exit after continuing!");
            }
        }
        public static void LoadSuperregions(LoadingProgress progress, NodeFile Superregions)
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
                        if (s.Name.ToLower() == "restrict_charter")
                        {
                            res = true;
                            continue;
                        }
                        Region r = GlobalVariables.Regions.Find(x => x.Name.ToLower() == s.Name.ToLower());
                        if (r != null)
                        {
                            if (r.Superregion != null)
                            {
                                r.Superregion.Regions.Remove(r);
                                progress.ReportError($"Alert: Region '{r}' is part of two superregions. '{r.Name}' and '{r.Superregion}'. Picking second.");
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
        }
    }
}
