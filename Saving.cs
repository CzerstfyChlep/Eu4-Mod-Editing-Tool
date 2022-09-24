using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Eu4ModEditor
{
    public static class Saving
    {
        //DOESN'T SUPPORT ALL OBJECTS TO SAVE TYPES!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public static void WriteToNodeFile(NodeFile nf, object objectToSave, int option = 0)
        {
            if(objectToSave is Province)
            {
                Province province = (Province)objectToSave;
                if (nf == null)
                    return;
                if (nf.ReadOnly)
                {
                    if (!Directory.Exists(GlobalVariables.pathtomod + "history\\"))
                        Directory.CreateDirectory(GlobalVariables.pathtomod + "history\\");
                    if (!Directory.Exists(GlobalVariables.pathtomod + "history\\provinces\\"))
                        Directory.CreateDirectory(GlobalVariables.pathtomod + "history\\provinces\\");
                    province.HistoryFile = new NodeFile(GlobalVariables.pathtomod + "history\\provinces\\" + province.HistoryFile.Path.Split('\\').Last());
                    if (province.HistoryFile.LastStatus.HasError)
                    {
                        GlobalVariables.MainForm.ShowMessageBox($"File '{province.HistoryFile.Path}' has an error in line {province.HistoryFile.LastStatus.LineError}");
                        return;
                    }
                }
                nf.MainNode.Variables.RemoveAll(x => x.Name == "add_core" || x.Name == "discovered_by" || GlobalVariables.Buildings.Any(y => y.Name == x.Name) || x.Name == "add_claim");
                nf.MainNode.ItemOrder.RemoveAll(x => x.Name == "add_core" || x.Name == "discovered_by" || GlobalVariables.Buildings.Any(y => y.Name == x.Name) || x.Name == "add_claim");
                foreach (string tag in province.GetCores())
                {
                    nf.MainNode.AddVariable(new Variable("add_core", tag));
                }

                foreach (string tag in province.GetClaims())
                {
                    nf.MainNode.AddVariable(new Variable("add_claim", tag));
                }

                foreach (string techgroup in province.GetDiscoveredBy())
                {
                    nf.MainNode.AddVariable(new Variable("discovered_by", techgroup));
                }
                foreach (Building bl in province.GetBuildings())
                {
                    nf.MainNode.AddVariable(new Variable(bl.Name, "yes"));
                }

                if (province.OwnerCountry != Country.NoCountry && province.OwnerCountry != null)
                {
                    nf.MainNode.ChangeVariable("owner", province.OwnerCountry.Tag, true);
                }
                else
                {
                    nf.MainNode.Variables.RemoveAll(x => x.Name == "owner" && x.Value != "---");
                    nf.MainNode.ItemOrder.RemoveAll(x => x.Name == "owner" && x is Variable && (x as Variable).Value != "---");
                }

                if (province.Tax > 0)
                    nf.MainNode.ChangeVariable("base_tax", province.Tax.ToString(), true);
                if (province.Manpower > 0)
                    nf.MainNode.ChangeVariable("base_manpower", province.Manpower.ToString(), true);
                if (province.Production > 0)
                    nf.MainNode.ChangeVariable("base_production", province.Production.ToString(), true);
                if (province.Culture != Culture.NoCulture && province.Culture != null)
                {
                    nf.MainNode.ChangeVariable("culture", province.Culture.Name, true);
                }
                else
                {
                    nf.MainNode.Variables.RemoveAll(x => x.Name == "culture");
                    nf.MainNode.ItemOrder.RemoveAll(x => x.Name == "culture");
                }
                if (province.TradeGood != null && province.TradeGood != TradeGood.nothing)
                    nf.MainNode.ChangeVariable("trade_goods", province.TradeGood.Name, true);
                if (province.Religion != null && province.Religion != Religion.NoReligion)
                    nf.MainNode.ChangeVariable("religion", province.Religion.Name, true);
                else
                {
                    nf.MainNode.Variables.RemoveAll(x => x.Name == "religion");
                    nf.MainNode.ItemOrder.RemoveAll(x => x.Name == "religion");
                }
                if (province.HRE)
                    nf.MainNode.ChangeVariable("hre", "yes", true);
                else
                {
                    nf.MainNode.Variables.RemoveAll(x => x.Name == "hre");
                    nf.MainNode.ItemOrder.RemoveAll(x => x.Name == "hre");
                }

                if (province.Controller != Country.NoCountry && province.Controller != null)
                    nf.MainNode.ChangeVariable("controller", province.Controller.Tag, true);
                else if (province.OwnerCountry != Country.NoCountry && province.OwnerCountry != null)
                    nf.MainNode.ChangeVariable("controller", province.OwnerCountry.Tag, true);
                else
                {
                    nf.MainNode.Variables.RemoveAll(x => x.Name == "controller" && x.Value != "---");
                    nf.MainNode.ItemOrder.RemoveAll(x => x.Name == "controller" && x is Variable && (x as Variable).Value != "---");
                }
                if (province.Fort)
                    nf.MainNode.ChangeVariable("fort_15th", "yes", true);
                else
                {
                    nf.MainNode.Variables.RemoveAll(x => x.Name == "fort_15th");
                    nf.MainNode.ItemOrder.RemoveAll(x => x.Name == "fort_15th");
                }
                if (province.CenterOfTrade > 0)
                    nf.MainNode.ChangeVariable("center_of_trade", province.CenterOfTrade + "", true);
                else
                {
                    nf.MainNode.Variables.RemoveAll(x => x.Name == "center_of_trade");
                    nf.MainNode.ItemOrder.RemoveAll(x => x.Name == "center_of_trade");
                }

                if (!province.Sea && !province.Lake && !province.Wasteland)
                {
                    if (province.City)
                        nf.MainNode.ChangeVariable("is_city", "yes", true);
                    else
                        nf.MainNode.ChangeVariable("is_city", "no", true);
                }

                Node n = nf.MainNode.Nodes.Find(x => x.Name == "latent_trade_goods");
                if (province.LatentTradeGood != null)
                {
                    if (n != null)
                    {
                        n.RemoveAllPureValues();
                        n.AddPureValue(province.LatentTradeGood.Name);
                    }
                    else
                    {
                        n = nf.MainNode.AddNode("latent_trade_goods");
                        n.AddPureValue(province.LatentTradeGood.Name);
                    }
                }
                else
                {
                    if (n != null)
                    {
                        nf.MainNode.Nodes.Remove(n);
                        nf.MainNode.ItemOrder.Remove(n);
                    }
                }
            }

            else if (objectToSave is Country)
            {
                Country country = (Country)objectToSave;
                if (option == 0)
                {
                    
                    if (nf.ReadOnly)
                    {
                        if (!Directory.Exists(GlobalVariables.pathtomod + "history\\"))
                            Directory.CreateDirectory(GlobalVariables.pathtomod + "history\\");
                        if (!Directory.Exists(GlobalVariables.pathtomod + "history\\countries\\"))
                            Directory.CreateDirectory(GlobalVariables.pathtomod + "history\\countries\\");
                        country.HistoryFile = new NodeFile(GlobalVariables.pathtomod + "history\\countries\\" + country.HistoryFile.Path.Split('\\').Last());

                        if (country.HistoryFile.LastStatus.HasError)
                        {
                            GlobalVariables.MainForm.ShowMessageBox($"File '{country.HistoryFile.Path}' has an error in line {country.HistoryFile.LastStatus.LineError}");
                            return;
                        }
                    }

                    if (country.Religion != null && country.Religion != Religion.NoReligion)
                    {
                        Variable religion = nf.MainNode.Variables.Find(x => x.Name == "religion");
                        if (religion != null)
                        {
                            religion.Value = country.Religion.Name;
                        }
                        else
                        {
                            religion = new Variable("religion", country.Religion.Name);
                            nf.MainNode.AddVariable(religion);
                        }
                    }
                    if (country.Capital != null)
                    {
                        Variable capital = nf.MainNode.Variables.Find(x => x.Name == "capital");
                        if (capital != null)
                        {
                            capital.Value = country.CapitalID.ToString();
                        }
                        else
                        {
                            capital = new Variable("capital", country.CapitalID.ToString());
                            nf.MainNode.AddVariable(capital);
                        }
                    }

                    if (country.TechnologyGroup != "")
                    {
                        Variable techgroup = nf.MainNode.Variables.Find(x => x.Name == "technology_group");
                        if (techgroup != null)
                        {
                            techgroup.Value = country.TechnologyGroup;
                        }
                        else
                        {
                            techgroup = new Variable("technology_group", country.TechnologyGroup);
                            nf.MainNode.AddVariable(techgroup);
                        }
                    }

                    if (country.PrimaryCulture != Culture.NoCulture && country.PrimaryCulture != null)
                    {
                        Variable primculture = nf.MainNode.Variables.Find(x => x.Name == "primary_culture");
                        if (primculture != null)
                        {
                            primculture.Value = country.PrimaryCulture.ToString();
                        }
                        else
                        {
                            primculture = new Variable("primary_culture", country.PrimaryCulture.ToString());
                            nf.MainNode.AddVariable(primculture);
                        }
                    }
                    else
                    {
                        Variable primculture = nf.MainNode.Variables.Find(x => x.Name == "primary_culture");
                        if (primculture != null)
                        {
                            nf.MainNode.Variables.Remove(primculture);
                            nf.MainNode.ItemOrder.Remove(primculture);
                        }
                    }

                    if (country.Government != null)
                    {
                        Variable government = nf.MainNode.Variables.Find(x => x.Name == "government");
                        if (government != null)
                        {
                            government.Value = country.Government.Type;
                        }
                        else
                        {
                            government = new Variable("government", country.Government.Type);
                            nf.MainNode.AddVariable(government);
                        }
                        List<string> startingreforms = new List<string>();
                        GlobalVariables.Governments.ForEach(x => startingreforms.AddRange(x.reforms));
                        Variable reform = nf.MainNode.Variables.Find(x => x.Name == "add_government_reform" && startingreforms.Contains(x.Value));
                        if (reform != null)
                        {
                            reform.Value = country.GovernmentReform;
                        }
                        else
                        {
                            reform = new Variable("add_government_reform", country.GovernmentReform);
                            nf.MainNode.AddVariable(reform);
                        }
                    }

                    Variable governmentrank = nf.MainNode.Variables.Find(x => x.Name == "government_rank");
                    if (governmentrank != null)
                    {
                        governmentrank.Value = country.GovernmentRank.ToString();
                    }
                    else
                    {
                        governmentrank = new Variable("government_rank", country.GovernmentRank.ToString());
                        nf.MainNode.AddVariable(governmentrank);
                    }

                }
                else if(option == 1)
                {
                    if (nf.ReadOnly)
                    {
                        if (!Directory.Exists(GlobalVariables.pathtomod + "common\\"))
                            Directory.CreateDirectory(GlobalVariables.pathtomod + "common\\");
                        if (!Directory.Exists(GlobalVariables.pathtomod + "common\\countries\\"))
                            Directory.CreateDirectory(GlobalVariables.pathtomod + "common\\countries\\");
                        country.CommonFile = new NodeFile(GlobalVariables.pathtomod + "common\\countries\\" + country.CommonFile.Path.Split('\\').Last());
                        if (country.CommonFile.LastStatus.HasError)
                            GlobalVariables.MainForm.ShowMessageBox($"File '{country.CommonFile.Path}' has an error in line {country.CommonFile.LastStatus.LineError}");
                    }


                    Variable graphicalculture = nf.MainNode.Variables.Find(x => x.Name == "graphical_culture");
                    if (graphicalculture != null)
                    {
                        graphicalculture.Value = country.GraphicalCulture;
                    }
                    else
                    {
                        graphicalculture = new Variable("graphical_culture", country.GraphicalCulture);
                        nf.MainNode.AddVariable(graphicalculture);
                    }
                }
            }

            else if(objectToSave is SpecialSavingObject)
            {
                switch((objectToSave as SpecialSavingObject).Type)
                {
                    case SpecialSavingObject.SavingType.Area:
                        foreach (Area a in GlobalVariables.Areas)
                        {
                            Node n = nf.MainNode.Nodes.Find(x => x.Name == a.Name);
                            if (n != null)
                            {
                                n.RemoveAllPureValues();
                                foreach (Province p in a.Provinces)
                                    n.AddPureValue(p.ID.ToString());
                            }
                            else
                            {
                                n = nf.MainNode.Nodes.Find(x => x.Name == a.OriginalName);
                                if (n != null)
                                {
                                    n.Name = a.Name;
                                    n.RemoveAllPureValues();
                                    a.OriginalName = a.Name;
                                    foreach (Province p in a.Provinces)
                                        n.AddPureValue(p.ID.ToString());
                                }
                                else
                                {
                                    n = new Node(a.Name);
                                    a.OriginalName = a.Name;
                                    foreach (Province p in a.Provinces)
                                        n.AddPureValue(p.ID.ToString());
                                    nf.MainNode.AddNode(n);
                                }
                            }
                        }
                        nf.MainNode.Nodes.Where(x => !GlobalVariables.Areas.Any(y => y.Name == x.Name)).ToList().ForEach(x => nf.MainNode.RemoveNode(x));
                        break;
                    case SpecialSavingObject.SavingType.Region:
                        foreach (Region r in GlobalVariables.Regions)
                        {
                            Node pn = nf.MainNode.Nodes.Find(x => x.Name == r.Name);
                            if (pn != null)
                            {
                                Node n = pn.Nodes.Find(x => x.Name == "areas");
                                if (n == null)
                                    n = pn.AddNode("areas");
                                n.RemoveAllPureValues();
                                foreach (Area a in r.Areas)
                                    n.AddPureValue(a.Name);
                            }
                            else
                            {
                                pn = nf.MainNode.Nodes.Find(x => x.Name == r.OriginalName);
                                if (pn != null)
                                {
                                    pn.Name = r.Name;
                                    Node n = pn.Nodes.Find(x => x.Name == "areas");
                                    if (n == null)
                                        n = pn.AddNode("areas");
                                    n.RemoveAllPureValues();
                                    r.OriginalName = r.Name;
                                    foreach (Area a in r.Areas)
                                        n.AddPureValue(a.Name);
                                }
                                else
                                {
                                    pn = new Node(r.Name);
                                    r.OriginalName = r.Name;
                                    Node n = pn.AddNode("areas");
                                    foreach (Area a in r.Areas)
                                        n.AddPureValue(a.Name);
                                    nf.MainNode.AddNode(pn);
                                }
                            }
                        }
                        nf.MainNode.Nodes.Where(x => !GlobalVariables.Regions.Any(y => y.Name == x.Name)).ToList().ForEach(x => nf.MainNode.RemoveNode(x));
                        break;
                    case SpecialSavingObject.SavingType.Continent:
                        foreach (Continent c in GlobalVariables.Continents)
                        {
                            Node n = nf.MainNode.Nodes.Find(x => x.Name == c.Name);
                            if (n != null)
                            {
                                n.RemoveAllPureValues();
                                foreach (Province p in c.Provinces)
                                    n.AddPureValue(p.ID.ToString());
                            }
                            else
                            {
                                n = nf.MainNode.Nodes.Find(x => x.Name == c.OriginalName);
                                if (n != null)
                                {
                                    n.Name = c.Name;
                                    n.RemoveAllPureValues();
                                    c.OriginalName = c.Name;
                                    foreach (Province p in c.Provinces)
                                        n.AddPureValue(p.ID.ToString());
                                }
                                else
                                {
                                    n = new Node(c.Name);
                                    c.OriginalName = c.Name;
                                    foreach (Province p in c.Provinces)
                                        n.AddPureValue(p.ID.ToString());
                                    nf.MainNode.AddNode(n);
                                }
                            }
                        }
                        nf.MainNode.Nodes.Where(x => !GlobalVariables.Continents.Any(y => y.Name == x.Name)).ToList().ForEach(x => nf.MainNode.RemoveNode(x));
                        break;
                    case SpecialSavingObject.SavingType.Superregion:
                        foreach (Superregion sr in GlobalVariables.Superregions)
                        {
                            Node n = nf.MainNode.Nodes.Find(x => x.Name == sr.Name);
                            if (n != null)
                            {
                                n.RemoveAllPureValues();
                                foreach (Region a in sr.Regions)
                                    n.AddPureValue(a.Name);
                            }
                            else
                            {
                                n = nf.MainNode.Nodes.Find(x => x.Name == sr.OriginalName);
                                if (n != null)
                                {
                                    n.Name = sr.Name;
                                    n.RemoveAllPureValues();
                                    sr.OriginalName = sr.Name;
                                    foreach (Region a in sr.Regions)
                                        n.AddPureValue(a.Name);
                                }
                                else
                                {
                                    n = new Node(sr.Name);
                                    sr.OriginalName = sr.Name;
                                    foreach (Region a in sr.Regions)
                                        n.AddPureValue(a.Name);
                                    nf.MainNode.AddNode(n);
                                }
                            }
                        }
                        nf.MainNode.Nodes.Where(x => !GlobalVariables.Superregions.Any(y => y.Name == x.Name)).ToList().ForEach(x => nf.MainNode.RemoveNode(x));
                        break;
                    case SpecialSavingObject.SavingType.Climate:
                        {
                            Node tropical = nf.MainNode.Nodes.Find(x => x.Name == "tropical");
                            if (tropical == null)
                                tropical = nf.MainNode.AddNode("tropical");
                            Node arid = nf.MainNode.Nodes.Find(x => x.Name == "arid");
                            if (arid == null)
                                arid = nf.MainNode.AddNode("arid");
                            Node arctic = nf.MainNode.Nodes.Find(x => x.Name == "arctic");
                            if (arctic == null)
                                arctic = nf.MainNode.AddNode("arctic");
                            Node mild_winter = nf.MainNode.Nodes.Find(x => x.Name == "mild_winter");
                            if (mild_winter == null)
                                mild_winter = nf.MainNode.AddNode("mild_winter");
                            Node normal_winter = nf.MainNode.Nodes.Find(x => x.Name == "normal_winter");
                            if (normal_winter == null)
                                normal_winter = nf.MainNode.AddNode("normal_winter");
                            Node severe_winter = nf.MainNode.Nodes.Find(x => x.Name == "severe_winter");
                            if (severe_winter == null)
                                severe_winter = nf.MainNode.AddNode("severe_winter");
                            Node impassable = nf.MainNode.Nodes.Find(x => x.Name == "impassable");
                            if (impassable == null)
                                impassable = nf.MainNode.AddNode("impassable");
                            Node mild_monsoon = nf.MainNode.Nodes.Find(x => x.Name == "mild_monsoon");
                            if (mild_monsoon == null)
                                mild_monsoon = nf.MainNode.AddNode("mild_monsoon");
                            Node normal_monsoon = nf.MainNode.Nodes.Find(x => x.Name == "normal_monsoon");
                            if (normal_monsoon == null)
                                normal_monsoon = nf.MainNode.AddNode("normal_monsoon");
                            Node severe_monsoon = nf.MainNode.Nodes.Find(x => x.Name == "severe_monsoon");
                            if (severe_monsoon == null)
                                severe_monsoon = nf.MainNode.AddNode("severe_monsoon");
                            foreach (Province p in GlobalVariables.Provinces)
                            {
                                switch (p.Winter)
                                {
                                    case 0:
                                        mild_winter.RemovePureValue(p.ID.ToString());
                                        normal_winter.RemovePureValue(p.ID.ToString());
                                        severe_winter.RemovePureValue(p.ID.ToString());
                                        break;
                                    case 1:
                                        mild_winter.AddPureValue(p.ID.ToString(), checkexists: true);
                                        normal_winter.RemovePureValue(p.ID.ToString());
                                        severe_winter.RemovePureValue(p.ID.ToString());
                                        break;
                                    case 2:
                                        mild_winter.RemovePureValue(p.ID.ToString());
                                        normal_winter.AddPureValue(p.ID.ToString(), checkexists: true);
                                        severe_winter.RemovePureValue(p.ID.ToString());
                                        break;
                                    case 3:
                                        mild_winter.RemovePureValue(p.ID.ToString());
                                        normal_winter.RemovePureValue(p.ID.ToString());
                                        severe_winter.AddPureValue(p.ID.ToString(), checkexists: true);
                                        break;
                                }
                                switch (p.Monsoon)
                                {
                                    case 0:
                                        mild_monsoon.RemovePureValue(p.ID.ToString());
                                        normal_monsoon.RemovePureValue(p.ID.ToString());
                                        severe_monsoon.RemovePureValue(p.ID.ToString());
                                        break;
                                    case 1:
                                        mild_monsoon.AddPureValue(p.ID.ToString(), checkexists: true);
                                        normal_monsoon.RemovePureValue(p.ID.ToString());
                                        severe_monsoon.RemovePureValue(p.ID.ToString());
                                        break;
                                    case 2:
                                        mild_monsoon.RemovePureValue(p.ID.ToString());
                                        normal_monsoon.AddPureValue(p.ID.ToString(), checkexists: true);
                                        severe_monsoon.RemovePureValue(p.ID.ToString());
                                        break;
                                    case 3:
                                        mild_monsoon.RemovePureValue(p.ID.ToString());
                                        normal_monsoon.RemovePureValue(p.ID.ToString());
                                        severe_monsoon.AddPureValue(p.ID.ToString(), checkexists: true);
                                        break;
                                }
                                switch (p.Climate)
                                {
                                    case 0:
                                        tropical.RemovePureValue(p.ID.ToString());
                                        arid.RemovePureValue(p.ID.ToString());
                                        arctic.RemovePureValue(p.ID.ToString());
                                        break;
                                    case 1:
                                        tropical.AddPureValue(p.ID.ToString(), checkexists: true);
                                        arid.RemovePureValue(p.ID.ToString());
                                        arctic.RemovePureValue(p.ID.ToString());
                                        break;
                                    case 2:
                                        tropical.RemovePureValue(p.ID.ToString());
                                        arid.AddPureValue(p.ID.ToString(), checkexists: true);
                                        arctic.RemovePureValue(p.ID.ToString());
                                        break;
                                    case 3:
                                        tropical.RemovePureValue(p.ID.ToString());
                                        arid.RemovePureValue(p.ID.ToString());
                                        arctic.AddPureValue(p.ID.ToString(), checkexists: true);
                                        break;
                                }
                                switch (p.Impassable)
                                {
                                    case 0:
                                        impassable.RemovePureValue(p.ID.ToString());
                                        break;
                                    case 1:
                                        impassable.AddPureValue(p.ID.ToString(), checkexists: true);
                                        break;
                                }
                            }
                        }
                        break;
                }
            }

        }

        public static void SaveObject(object toSave)
        {
            if(toSave != null)
            {
                if (toSave.GetType() == typeof(Province))
                {
                    if (GlobalVariables.ReadOnly[8] && !GlobalVariables.CreateNewFilesReadOnly)
                        return;
                    WriteToNodeFile(((Province)toSave).HistoryFile, toSave);
                    ((Province)toSave).HistoryFile.SaveFile(((Province)toSave).HistoryFile.Path);
                }

                else if (toSave.GetType() == typeof(Country))
                {
                    if (GlobalVariables.ReadOnly[11] && !GlobalVariables.CreateNewFilesReadOnly)
                        return;
                    Country country = (Country)toSave;
                    WriteToNodeFile(country.HistoryFile, country, 0);
                    WriteToNodeFile(country.CommonFile, country, 1);
                    country.HistoryFile.SaveFile(country.HistoryFile.Path);
                    country.CommonFile.SaveFile(country.CommonFile.Path);
                }

                else if (toSave.GetType() == typeof(SpecialSavingObject))
                {
                    SpecialSavingObject specialobject = (SpecialSavingObject)toSave;
                    switch (specialobject.Type)
                    {
                        case SpecialSavingObject.SavingType.Area:
                            {
                                if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.area] && !GlobalVariables.CreateNewFilesReadOnly)
                                    return;
                                if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.area] && GlobalVariables.CreateNewFilesReadOnly && File.Exists(GlobalVariables.pathtomod + "map\\area.txt"))
                                    return;
                                if (!Directory.Exists(GlobalVariables.pathtomod + "map\\"))
                                    Directory.CreateDirectory(GlobalVariables.pathtomod + "map\\");

                                NodeFile nf = new NodeFile(GlobalVariables.pathtomod + "map\\area.txt");

                                if (nf.LastStatus.HasError)
                                {
                                    GlobalVariables.MainForm.ShowMessageBox($"File '{nf.Path}' has an error in line {nf.LastStatus.LineError}. Nothing will be saved!");
                                    return;
                                }
                                WriteToNodeFile(nf, toSave);
                                nf.SaveFile(GlobalVariables.pathtomod + "map\\area.txt");
                            }
                            break;
                        case SpecialSavingObject.SavingType.Region:
                            {
                                if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.region] && !GlobalVariables.CreateNewFilesReadOnly)
                                    return;
                                if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.region] && GlobalVariables.CreateNewFilesReadOnly && File.Exists(GlobalVariables.pathtomod + "map\\region.txt"))
                                    return;
                                if (!Directory.Exists(GlobalVariables.pathtomod + "map\\"))
                                    Directory.CreateDirectory(GlobalVariables.pathtomod + "map\\");

                                NodeFile nf = new NodeFile(GlobalVariables.pathtomod + "map\\region.txt");
                                if (nf.LastStatus.HasError)
                                {
                                    GlobalVariables.MainForm.ShowMessageBox($"File '{nf.Path}' has an error in line {nf.LastStatus.LineError}. Nothing will be saved!");
                                    return;
                                }
                                WriteToNodeFile(nf, toSave);
                                nf.SaveFile(GlobalVariables.pathtomod + "map\\region.txt");
                            }
                            break;
                        case SpecialSavingObject.SavingType.Continent:
                            {
                                if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.continent] && !GlobalVariables.CreateNewFilesReadOnly)
                                    return;
                                if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.continent] && GlobalVariables.CreateNewFilesReadOnly && File.Exists(GlobalVariables.pathtomod + "map\\continent.txt"))
                                    return;
                                if (!Directory.Exists(GlobalVariables.pathtomod + "map\\"))
                                    Directory.CreateDirectory(GlobalVariables.pathtomod + "map\\");

                                NodeFile nf = new NodeFile(GlobalVariables.pathtomod + "map\\continent.txt");
                                if (nf.LastStatus.HasError)
                                {
                                    GlobalVariables.MainForm.ShowMessageBox($"File '{nf.Path}' has an error in line {nf.LastStatus.LineError}. Nothing will be saved!");
                                    return;
                                }
                                WriteToNodeFile(nf, toSave);
                                nf.SaveFile(GlobalVariables.pathtomod + "map\\continent.txt");
                            }
                            break;
                        case SpecialSavingObject.SavingType.Superregion:
                            {
                                if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.superregion] && !GlobalVariables.CreateNewFilesReadOnly)
                                    return;
                                if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.superregion] && GlobalVariables.CreateNewFilesReadOnly && File.Exists(GlobalVariables.pathtomod + "map\\superregion.txt"))
                                    return;
                                if (!Directory.Exists(GlobalVariables.pathtomod + "map\\"))
                                    Directory.CreateDirectory(GlobalVariables.pathtomod + "map\\");

                                NodeFile nf = new NodeFile(GlobalVariables.pathtomod + "map\\superregion.txt");
                                if (nf.LastStatus.HasError)
                                {
                                    GlobalVariables.MainForm.ShowMessageBox($"File '{nf.Path}' has an error in line {nf.LastStatus.LineError}. Nothing will be saved!");
                                    return;
                                }
                                WriteToNodeFile(nf, toSave);
                                nf.SaveFile(GlobalVariables.pathtomod + "map\\superregion.txt");
                            }
                            break;
                        case SpecialSavingObject.SavingType.TradeCompany:
                            {
                                if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.tradecompanies] && !GlobalVariables.CreateNewFilesReadOnly)
                                    return;
                                foreach (TradeCompany tc in GlobalVariables.TradeCompanies)
                                {
                                    if (!tc.MadeChanges)
                                        continue;
                                    tc.MadeChanges = false;
                                    NodeFile toSaveTo = DetermineSaveLocation(GlobalVariables.ModNodeFileTypes.TradeCompanies, tc.NodeFile);

                                    if (toSaveTo == null)
                                        continue;
                                    tc.NodeFile = toSaveTo;
                                    Node n = tc.NodeFile.MainNode.Nodes.Find(x => x.Name == tc.Name);
                                    if (n == null)
                                    {
                                        n = new Node(tc.Name, toSaveTo.MainNode);
                                        tc.NodeFile.MainNode.AddNode(n);
                                        n.Parent = tc.NodeFile.MainNode;

                                        Node color = new Node("color", n)
                                        {
                                            PureValues = new List<PureValue>() { new PureValue(tc.Color.R.ToString()), new PureValue(tc.Color.G.ToString()), new PureValue(tc.Color.B.ToString()) }
                                        };
                                        n.AddNode(color);
                                        Node provinces = new Node("provinces", n);
                                        n.AddNode(provinces);
                                        foreach (Province p in tc.Provinces)
                                            provinces.AddPureValue(p.ID.ToString());
                                        foreach (string name in tc.Names)
                                        {
                                            Node nm = new Node("names", n);
                                            n.AddNode(nm);
                                            nm.ChangeVariable("name", name, true);
                                        }

                                    }
                                    else
                                    {
                                        Node colornode = n.Nodes.Find(x => x.Name == "color");
                                        if (colornode == null)
                                            colornode = n.AddNode("color");
                                        colornode.RemoveAllPureValues();
                                        colornode.AddPureValue(tc.Color.R.ToString());
                                        colornode.AddPureValue(tc.Color.G.ToString());
                                        colornode.AddPureValue(tc.Color.B.ToString());                                                                                     
                                        Node pnode = n.Nodes.Find(x => x.Name == "provinces");
                                        if (pnode == null)
                                            pnode = n.AddNode("provinces");
                                        pnode.RemoveAllPureValues();
                                        foreach (Province p in tc.Provinces)
                                            pnode.AddPureValue(p.ID.ToString());
                                        int N = 0;
                                        List<Node> ToRemove = new List<Node>();
                                        foreach (Node namenode in n.Nodes.FindAll(x => x.Name == "names"))
                                        {
                                            if (N >= tc.Names.Count)
                                            {
                                                ToRemove.Add(namenode);
                                                continue;
                                            }
                                            namenode.ChangeVariable("name", tc.Names[N]);
                                            N++;
                                        }
                                        if (N < tc.Names.Count)
                                        {
                                            for (; N < tc.Names.Count; N++)
                                            {
                                                Node nm = new Node("names", n);
                                                n.AddNode(nm);
                                                nm.ChangeVariable("name", tc.Names[N], true);
                                            }
                                        }
                                        n.Nodes.RemoveAll(x => ToRemove.Contains(x));
                                        n.ItemOrder.RemoveAll(x => ToRemove.Contains(x));
                                    }
                                    toSaveTo.SaveFile(toSaveTo.Path);
                                }
                            }
                            break;
                        case SpecialSavingObject.SavingType.TagFile:
                            {
                                if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.countrytags] && !GlobalVariables.CreateNewFilesReadOnly)
                                    return;
                                List<Country> newSaves = new List<Country>();

                                //search for the countries with changed tags
                                foreach(Country c in GlobalVariables.Countries)
                                {
                                    if (c.FullName != c.OriginalFullName)
                                        newSaves.Add(c);
                                    if (c.Tag != c.OriginalTag)
                                        newSaves.Add(c);
                                }

                                //if there are any tags which are using the gamefile


                                foreach(Country c in newSaves)
                                {
                                    if (c.CountryTagsFile == null && GlobalVariables.ModCountryTagsFiles.Any())
                                        c.CountryTagsFile = GlobalVariables.ModCountryTagsFiles[0];
                                    else if(c.CountryTagsFile == null && GlobalVariables.GameCountryTagsFile != null)
                                        c.CountryTagsFile = GlobalVariables.GameCountryTagsFile;
                                    else if(c.CountryTagsFile == null)
                                    {
                                        c.CountryTagsFile = new NodeFile(GlobalVariables.pathtomod + "common\\country_tags\\00_modeditor_tags.txt");
                                        if (c.CountryTagsFile.LastStatus.HasError)
                                        {
                                            GlobalVariables.MainForm.ShowMessageBox($"File '{c.CountryTagsFile.Path}' has an error in line {c.CountryTagsFile.LastStatus.LineError}. Nothing will be saved!");
                                            return;
                                        }

                                        c.CountryTagsFile.SaveFile();
                                        GlobalVariables.ModCountryTagsFiles.Add(c.CountryTagsFile);
                                    }

                                }

                                if(newSaves.Any(x=>x.CountryTagsFile.FileName == "00_countries.txt" && x.CountryTagsFile == GlobalVariables.GameCountryTagsFile))
                                {
                                    //create a mod replacement file
                                    NodeFile modTagsReplacement = new NodeFile(GlobalVariables.GameCountryTagsFile.Path);
                                    modTagsReplacement.Path = GlobalVariables.pathtomod + "common\\country_tags\\00_countries.txt";
                                    modTagsReplacement.SaveFile();
                                    GlobalVariables.ModCountryTagsFiles.Add(modTagsReplacement);
                                    //and set it for all countries
                                    foreach(Country c in GlobalVariables.Countries)
                                    {
                                        if (c.CountryTagsFile == GlobalVariables.GameCountryTagsFile)
                                            c.CountryTagsFile = modTagsReplacement;
                                    }
                                    foreach (Country c in GlobalVariables.RemovedCountries)
                                    {
                                        if (c.CountryTagsFile == GlobalVariables.GameCountryTagsFile)
                                            c.CountryTagsFile = modTagsReplacement;
                                    }

                                }
                                //save everything
                                foreach (Country c in newSaves)
                                {
                                    Variable v = c.CountryTagsFile.MainNode.Variables.Find(x => x.Name == c.OriginalTag);
                                    if (v == null)
                                        c.CountryTagsFile.MainNode.AddVariable(c.Tag, $"\"countries/{c.FullName}\"");
                                    else {
                                        v.Name = c.Tag;
                                        v.Value = $"\"countries/{c.FullName}.txt\"";
                                    }
                                    c.OriginalTag = c.Tag;
                                    c.OriginalFullName = c.FullName;
                                }
                                //remove deleted countries
                                foreach (Country c in GlobalVariables.RemovedCountries)
                                {
                                    Variable v = c.CountryTagsFile.MainNode.Variables.Find(x => x.Name == c.OriginalTag);
                                    if(v != null)
                                        c.CountryTagsFile.MainNode.RemoveVariable(v);
                                }                                

                                foreach (NodeFile nf in GlobalVariables.ModCountryTagsFiles)
                                {
                                    nf.SaveFile();
                                }
                            }
                            break;
                        case SpecialSavingObject.SavingType.Climate:
                            {
                                if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.climate] && !GlobalVariables.CreateNewFilesReadOnly)
                                    return;
                                if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.climate] && GlobalVariables.CreateNewFilesReadOnly && File.Exists(GlobalVariables.pathtomod + "map\\climate.txt"))
                                    return;
                                if (!Directory.Exists(GlobalVariables.pathtomod + "map\\"))
                                    Directory.CreateDirectory(GlobalVariables.pathtomod + "map\\");

                                NodeFile nf = new NodeFile(GlobalVariables.pathtomod + "map\\climate.txt");
                                if (nf.LastStatus.HasError)
                                {
                                    GlobalVariables.MainForm.ShowMessageBox($"File '{nf.Path}' has an error in line {nf.LastStatus.LineError}. Nothing will be saved!");
                                    return;
                                }
                                WriteToNodeFile(nf, toSave);                      
                                nf.SaveFile(GlobalVariables.pathtomod + "map\\climate.txt");
                            }
                            break;
                    }                
                }
            }
        }
        public static List<NodeFile> GetModNodeFile(GlobalVariables.ModNodeFileTypes type)
        {
            switch ((int)type)
            {
                case 0:
                    return GlobalVariables.ModTradeGoodsFiles;
                case 1:
                    return GlobalVariables.ModPricesFiles;
                case 2:
                    return GlobalVariables.ModCulturesFiles;
                case 3:
                    return GlobalVariables.ModReligionsFiles;
                case 4:
                    return GlobalVariables.ModTradeNodesFiles;
                case 5:
                    return GlobalVariables.ModTradeCompanyFiles;
                case 6:
                    return GlobalVariables.ModCountryTagsFiles;
                case 7:
                    return GlobalVariables.ModGovernmentsFiles;
                default:
                    return null;
            }
        }
        public static string GetModNodeFileName(GlobalVariables.ModNodeFileTypes type)
        {
            switch ((int)type)
            {
                case 0:
                    return "";
                case 1:
                    return "";
                case 2:
                    return "";
                case 3:
                    return "";
                case 4:
                    return "";
                case 5:
                    for (int a = 0; a < 99; a++)
                    {
                        if (File.Exists(GlobalVariables.pathtomod + $"\\common\\trade_companies\\{a.ToString("00")}_modeditor_trade_companies.txt"))
                            continue;
                        else
                            return $"\\common\\trade_companies\\{a.ToString("00")}_modeditor_trade_companies.txt";
                    }
                    return "\\common\\trade_companies\\99_modeditor_trade_companies.txt";
                case 6:
                    for (int a = 0; a < 99; a++)
                    {
                        if (File.Exists(GlobalVariables.pathtomod + $"\\common\\country_tags\\{a.ToString("00")}_modeditor_countries.txt"))
                            continue;
                        else
                            return $"\\common\\country_tags\\{a.ToString("00")}_modeditor_countries.txt";
                    }
                    return "\\common\\country_tags\\99_modeditor_countries.txt";
                case 7:
                    return "";
                default:
                    return "";
            }
        }
        public static NodeFile DetermineSaveLocation(GlobalVariables.ModNodeFileTypes type, NodeFile Parent)
        {
            NodeFile toSaveTo = null;
            if (Parent != null) //file found
            {
                //file is readonly but there is permission to create new files
                if (Parent.ReadOnly && GlobalVariables.CreateNewFilesReadOnly)
                {
                    toSaveTo = GetModNodeFile(type).Find(x => x.CreatedByEditor);
                    if (toSaveTo == null)
                    {
                        toSaveTo = new NodeFile(GlobalVariables.pathtomod + GetModNodeFileName(type));
                        toSaveTo.CreatedByEditor = true;
                        GetModNodeFile(type).Add(toSaveTo);
                    }
                }
                //file is readonly but no permission to save over
                else if (Parent.ReadOnly && !GlobalVariables.CreateNewFilesReadOnly)
                    return null;
                //file isn't readonly
                else
                    toSaveTo = Parent;

            }
            else //file wasn't found (new object)
            {
                //file will be saved to a new file
                if (GlobalVariables.NewObjectsNewFiles)
                {
                    toSaveTo = GetModNodeFile(type).Find(x => x.CreatedByEditor);
                    if (toSaveTo == null)
                    {
                        toSaveTo = new NodeFile(GlobalVariables.pathtomod + GetModNodeFileName(type));
                        toSaveTo.CreatedByEditor = true;
                        GetModNodeFile(type).Add(toSaveTo);
                    }


                }
                //editor will look for some already exisitng file (if it fails it won't save)
                else
                {
                    toSaveTo = GetModNodeFile(type).First(x => !x.ReadOnly);
                }
            }
            return toSaveTo;
        }


        public static void LoadObject(object toLoad)
        {
            if (toLoad != null)
            {
                if (toLoad.GetType() == typeof(Province))
                {
                    Province province = (Province)toLoad;
                    NodeFile nf = province.HistoryFile;


                    List<VariableChange> toremove = new List<VariableChange>();
                    foreach (VariableChange vc in GlobalVariables.Changes)
                    {
                        if (vc.Object == province)
                            toremove.Add(vc);
                    }
                    GlobalVariables.Changes.RemoveAll(x => toremove.Contains(x));
                    GlobalVariables.Saves.Remove(province);


                    if (nf == null)
                        return;
                    if (!nf.MainNode.Variables.Any())
                        GlobalVariables.TotalUsableProvinces--;

                    (province.Variables["Cores"] as List<string>).Clear();
                    (province.Variables["Claims"] as List<string>).Clear();
                    (province.Variables["DiscoveredBy"] as List<string>).Clear();
                    (province.Variables["Buildings"] as List<Building>).Clear();
                    if (province.OwnerCountry != null)
                        province.OwnerCountry.Provinces.Remove(province);
                    if (province.TradeGood != null)
                    {
                        province.TradeGood.TotalProvinces--;
                        province.TradeGood.TotalDev -= province.Tax + province.Production + province.Manpower;
                    }
                    if (province.LatentTradeGood != null)
                    {
                        province.LatentTradeGood.TotalProvinces--;
                        province.LatentTradeGood.TotalDev -= province.Tax + province.Production + province.Manpower;
                    }
                    province.Variables["LatentTradeGood"] = null;
                    province.Variables["TradeGood"] = null;
                    province.Variables["OwnerCountry"] = null;

                    foreach (Variable v in nf.MainNode.Variables)
                    {
                        Building bl = GlobalVariables.Buildings.Find(x => x.Name == v.Name);
                        if (bl != null && v.Value == "yes")
                        {
                            (province.Variables["Buildings"]as List<Building>).Add(bl);
                        }
                        switch (v.Name)
                        {
                            case "add_core":
                                (province.Variables["Cores"] as List<string>).Add(v.Value);
                                break;
                            case "add_claim":
                                (province.Variables["Claims"] as List<string>).Add(v.Value);
                                break;
                            case "owner":
                                Country c = GlobalVariables.Countries.Find(x => x.Tag == v.Value.ToUpper());
                                if (c != null)
                                {
                                    province.Variables["OwnerCountry"] = c;
                                    c.Provinces.Add(province);
                                }
                                break;
                            case "controller":
                                province.Variables["Controlller"] = v.Value;
                                break;
                            case "culture":
                                province.Variables["Culture"] = Culture.Cultures.Find(x => x.Name == v.Value);
                                break;
                            case "religion":
                                province.Variables["Religion"] = Religion.Religions.Find(x => x.Name == v.Value);
                                break;
                            case "hre":
                                if (v.Value == "yes")
                                    province.Variables["HRE"] = true;
                                else
                                    province.Variables["HRE"] = false;
                                break;
                            case "fort_15th":
                                if (v.Value == "yes")
                                    province.Variables["Fort"] = true;
                                else
                                    province.Variables["Fort"] = false;
                                break;
                            case "base_tax":
                                province.Variables["Tax"] = int.Parse(v.Value);
                                break;
                            case "base_production":
                                province.Variables["Production"] = int.Parse(v.Value);
                                break;
                            case "base_manpower":
                                province.Variables["Manpower"] = int.Parse(v.Value);
                                break;
                            case "trade_goods":
                                province.Variables["TradeGood"] = GlobalVariables.TradeGoods.Find(x => x.Name == v.Value);
                                if (province.TradeGood != null)
                                {
                                    province.TradeGood.TotalProvinces++;
                                }
                                break;
                            case "capital":
                                province.Variables["Capital"] = v.Value.Replace("\"", "");
                                break;
                            case "center_of_trade":
                                province.Variables["CenterOfTrade"] = int.Parse(v.Value);
                                break;
                            case "discovered_by":
                                (province.Variables["DiscoveredBy"] as List<string>).Add(v.Value);
                                break;
                            case "is_city":
                                if (v.Value == "yes")
                                    province.Variables["City"] = true;
                                else
                                    province.Variables["City"] = false;
                                break;
                        }
                    }

                    if (province.TradeGood != null)
                    {
                        province.TradeGood.TotalDev += province.Tax + province.Production + province.Manpower;
                    }

                    Node n = nf.MainNode.Nodes.Find(x => x.Name == "latent_trade_goods");
                    if (n != null)
                    {
                        if (n.PureValues.Any())
                        {
                            province.Variables["LatentTradeGood"] = GlobalVariables.TradeGoods.Find(x => x.Name == n.PureValues[0].Name.Trim());
                            if (province.LatentTradeGood != null)
                            {
                                province.LatentTradeGood.TotalProvinces++;
                                province.TradeGood.TotalDev += province.Tax + province.Production + province.Manpower;
                            }
                        }
                    }


                    MapManagement.ReloadProvince(new List<Province> { province });
                }

                else if (toLoad.GetType() == typeof(Country) && false)
                {
                    if (GlobalVariables.ReadOnly[6])
                        return;
                    Country country = (Country)toLoad;
                    NodeFile nf = country.HistoryFile;
                    if (country.Religion != null)
                    {
                        Variable religion = nf.MainNode.Variables.Find(x => x.Name == "religion");
                        if (religion != null)
                        {
                            religion.Value = country.Religion.Name;
                        }
                        else
                        {
                            religion = new Variable("religion", country.Religion.Name);
                            nf.MainNode.AddVariable(religion);
                        }
                    }
                    if (country.Capital != null)
                    {
                        Variable capital = nf.MainNode.Variables.Find(x => x.Name == "capital");
                        if (capital != null)
                        {
                            capital.Value = country.CapitalID.ToString();
                        }
                        else
                        {
                            capital = new Variable("capital", country.CapitalID.ToString());
                            nf.MainNode.AddVariable(capital);
                        }
                    }
                }
            }
        }

        public class SpecialSavingObject
        {
            public enum SavingType { Area, Region, Continent, Superregion, TradeCompany, TagFile, Climate, Terrain }
            public SavingType Type;
            public SpecialSavingObject(SavingType sv)
            {
                Type = sv;
            }
        }
    }
}
