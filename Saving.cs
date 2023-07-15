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

            /// <summary>
            /// Accepts only Province, Country, Area, Region, Superregion, Continent and Climate
            /// </summary>
            /// <param name="nf"></param>
            /// <param name="objectToSave"></param>
            /// <param name="option"></param>
        public static void WriteToNodeFile(NodeFile nf, object objectToSave, int option = 0)
        {
            DateTime temp = GlobalVariables.CurrentDate;
            GlobalVariables.CurrentDate = new DateTime(1, 1, 1);
            if (objectToSave is Province)
            {                
                Province province = (Province)objectToSave;
                if (nf == null)
                    return;

                //REVOLT

                //CORES
                {
                    Variable[] add_cores = nf.MainNode.Variables.Where(x => x.Name == "add_core").ToArray();
                    List<Variable> ToRemove = new List<Variable>();

                    List<string> newCores = province.GetCores().ToList();
                    ToRemove.AddRange(add_cores);

                    foreach (Variable core_var in add_cores)
                    {
                        newCores.RemoveAll(x => x.ToLower() == core_var.Value.ToLower());
                    }
                    foreach (string core in province.GetCores())
                    {
                        ToRemove.RemoveAll(x => x.Value.ToLower() == core.ToLower());
                    }

                    foreach (Variable v in ToRemove)
                        nf.MainNode.RemoveVariable(v);
                    foreach (string s in newCores)
                        nf.MainNode.AddVariable("add_core", s.ToUpper());
                }
                //DISCOVERED BY
                {
                    Variable[] add_disc = nf.MainNode.Variables.Where(x => x.Name == "discovered_by").ToArray();
                    List<Variable> RemDisc = new List<Variable>();

                    List<string> newDisc = province.GetDiscoveredBy().ToList();
                    RemDisc.AddRange(add_disc);

                    foreach (Variable disc_var in add_disc)
                    {
                        newDisc.RemoveAll(x => x.ToLower() == disc_var.Value.ToLower());
                    }
                    foreach (string disc in province.GetDiscoveredBy())
                    {
                        RemDisc.RemoveAll(x => x.Value.ToLower() == disc.ToLower());
                    }

                    foreach (Variable v in RemDisc)
                        nf.MainNode.RemoveVariable(v);
                    foreach (string s in newDisc)
                        nf.MainNode.AddVariable("discovered_by", s);
                }
                //BUILDINGS
                {
                    Variable[] add_buildings = nf.MainNode.Variables.Where(x => GlobalVariables.Buildings.Any(y => y.Name.ToLower() == x.Name.ToLower())).ToArray();
                    List<Variable> ToRemove = new List<Variable>();

                    List<string> newBuildings = province.GetBuildings().ToList().ConvertAll(x => x.Name);
                    ToRemove.AddRange(add_buildings);

                    foreach (Variable building_add in add_buildings)
                    {
                        newBuildings.RemoveAll(x => x.ToLower() == building_add.Value.ToLower());
                    }
                    foreach (Building building in province.GetBuildings())
                    {
                        ToRemove.RemoveAll(x => x.Value.ToLower() == building.Name.ToLower());
                    }

                    foreach (Variable v in ToRemove)
                        nf.MainNode.RemoveVariable(v);
                    foreach (string s in newBuildings)
                        nf.MainNode.AddVariable(s, "yes");
                }
                //CLAIMS
                {
                    Variable[] add_claims = nf.MainNode.Variables.Where(x => x.Name == "add_claim").ToArray();
                    List<Variable> ToRemove = new List<Variable>();

                    List<string> newClaims = province.GetClaims().ToList();
                    ToRemove.AddRange(add_claims);

                    foreach (Variable claim_var in add_claims)
                    {
                        newClaims.RemoveAll(x => x.ToLower() == claim_var.Value.ToLower());
                    }
                    foreach (string claim in province.GetClaims())
                    {
                        ToRemove.RemoveAll(x => x.Value.ToLower() == claim.ToLower());
                    }

                    foreach (Variable v in ToRemove)
                        nf.MainNode.RemoveVariable(v);
                    foreach (string s in newClaims)
                        nf.MainNode.AddVariable("add_claim", s.ToUpper());
                }
                //DATE NODES
                {
                    Node[] dateNodes = nf.MainNode.Nodes.Where(x => x.Name.Count(y=>(y=='.')) >= 2).ToArray();
                    List<Node> ToRemove = new List<Node>();
                    List<ProvinceDateEntry> newdateNodes = province.DateEntries.ToList();
                    ToRemove.AddRange(dateNodes);

                    foreach (Node date_node in dateNodes)
                    {
                        //TODO 
                        //ACOUNT FOR 01 dates
                        newdateNodes.RemoveAll(x => x.Date.Year+"."+x.Date.Month+"."+x.Date.Day == date_node.Name);
                    }
                    foreach (ProvinceDateEntry dateentry in province.DateEntries)
                    {
                        var l = ToRemove.Where(x => x.Name == dateentry.Date.Year + "." + dateentry.Date.Month + "." + dateentry.Date.Day);                       
                        if (l.Any())
                        {
                            nf.MainNode.ReplaceNode(l.First(), ConvertDateEntryIntoNode(dateentry));
                            //GlobalVariables.MainForm.ShowMessageBox("Worked!");
                        }
                        ToRemove.RemoveAll(x => l.Contains(x));
                    }

                    foreach (Node nd in ToRemove)
                        nf.MainNode.RemoveNode(nd);
                    foreach (ProvinceDateEntry pde in newdateNodes)
                        nf.MainNode.AddNode(ConvertDateEntryIntoNode(pde));
                }

                /*
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
                */
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
                else
                {
                    Variable vra = nf.MainNode.Variables.Find(x => x.Name == "trade_goods");
                    if(vra!= null) 
                        nf.MainNode.RemoveVariable(vra);
                }
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
            GlobalVariables.CurrentDate = temp;

        }

        public static Node ConvertDateEntryIntoNode(ProvinceDateEntry PDE)
        {
            Node n = new Node(PDE.Date.Year + "." + PDE.Date.Month + "." + PDE.Date.Day);         
            if (!PDE.Entries.Any())
                return null;

            foreach (ProvinceDateEntry.Entry en in PDE.Entries)
            {
                switch (en.Type)
                {
                    case Province.Variable.Revolt:
                        Node revolt = new Node("revolt");
                        revolt.InLine = true;
                        ProvinceDateEntry.Revolt revoltEntry = (ProvinceDateEntry.Revolt)en.Value;
                        if(revoltEntry.Type != "")
                            revolt.AddVariable("type", revoltEntry.Type);
                        if (revoltEntry.Name != "")
                            revolt.AddVariable("name", revoltEntry.Name).QuotedValue = true;
                        revolt.AddVariable("size", revoltEntry.Size.ToString());
                        if(revoltEntry.Leader != "")
                            revolt.AddVariable("leader", revoltEntry.Leader).QuotedValue = true;
                        n.AddNode(revolt);
                        break;
                    case Province.Variable.AddLocalAutonomy:
                        n.AddVariable("add_local_autonomy", en.Value.ToString());
                        break;
                    case Province.Variable.AddPermanentProvinceModifier:
                        Node modifier = new Node("add_permanent_province_modifier");
                        modifier.AddVariable("name", en.Value.ToString());
                        modifier.AddVariable("duration", "-1");
                        n.AddNode(modifier);
                        break;
                    case Province.Variable.AddProvinceTriggeredModifier:
                        n.AddVariable("add_province_triggered_modifier", en.Value.ToString());
                        break;
                    case Province.Variable.AddToTradeCompany:
                        n.AddVariable("add_to_trade_company", en.Value.ToString());
                        break;
                    case Province.Variable.AddTradeCompanyInvestement:
                        throw new NotImplementedException();
                    case Province.Variable.BuildingAdd:
                        n.AddVariable(en.Value.ToString(), "yes");
                        break;
                    case Province.Variable.BuildingRemove:
                        n.AddVariable(en.Value.ToString(), "no");
                        break;
                    case Province.Variable.Capital:
                        n.AddVariable(new Variable("capital", en.Value.ToString(), true));
                        break;
                    case Province.Variable.CenterOfTrade:
                        n.AddVariable("center_of_trade", en.Value.ToString());
                        break;
                    case Province.Variable.City:
                        n.AddVariable("city", ((bool)en.Value) ? "yes" : "no");
                        break;
                    case Province.Variable.ClaimsAdd:
                        n.AddVariable("add_claim", en.Value.ToString());
                        break;
                    case Province.Variable.ClaimsRemove:
                        n.AddVariable("remove_claim", en.Value.ToString());
                        break;
                    case Province.Variable.Controller:
                        n.AddVariable("controller", ((Country)en.Value).Tag);
                        break;
                    case Province.Variable.CoresAdd:
                        n.AddVariable("add_core", en.Value.ToString());
                        break;
                    case Province.Variable.CoresRemove:
                        n.AddVariable("remove_core", en.Value.ToString());
                        break;
                    case Province.Variable.Culture:
                        n.AddVariable("culture", en.Value.ToString());
                        break;
                    case Province.Variable.DiscoveredByAdd:
                        n.AddVariable("discovered_by", en.Value.ToString());
                        break;
                    case Province.Variable.DiscoveredByRemove:
                        break;
                    case Province.Variable.ExtraCost:
                        n.AddVariable("extra_cost", en.Value.ToString());
                        break;
                    case Province.Variable.HRE:
                        n.AddVariable("hre", ((bool)en.Value) ? "yes" : "no");
                        break;
                    case Province.Variable.LatentTradeGood:
                        n.AddNode("latent_trade_goods").AddPureValue(en.Value.ToString());
                        break;
                    case Province.Variable.Manpower:
                        n.AddVariable("base_manpower", en.Value.ToString());
                        break;
                    case Province.Variable.NativeFerocity:
                        n.AddVariable("native_ferocity", en.Value.ToString());
                        break;
                    case Province.Variable.NativeHostileness:
                        n.AddVariable("native_hostileness", en.Value.ToString());
                        break;
                    case Province.Variable.NativeSize:
                        n.AddVariable("native_size", en.Value.ToString());
                        break;
                    case Province.Variable.OwnerCountry:
                        n.AddVariable("owner", ((Country)en.Value).Tag);
                        break;
                    case Province.Variable.Production:
                        n.AddVariable("base_production", en.Value.ToString());
                        break;
                    case Province.Variable.ReformationCenter:
                        n.AddVariable("reformation_center", en.Value.ToString());
                        break;
                    case Province.Variable.Religion:
                        n.AddVariable("religion", en.Value.ToString());
                        break;
                    case Province.Variable.RemoveProvinceModifier:
                        n.AddVariable("remove_province_modifier", en.Value.ToString());
                        break;
                    case Province.Variable.SeatInParliament:
                        n.AddVariable("seat_in_parliament", ((bool)en.Value) ? "yes" : "no");
                        break;
                    case Province.Variable.Tax:
                        n.AddVariable("base_tax", en.Value.ToString());
                        break;
                    case Province.Variable.TradeGood:
                        n.AddVariable("trade_goods", en.Value.ToString());
                        break;
                    case Province.Variable.TribalOwner:
                        n.AddVariable("tribal_owner", ((Country)en.Value).Tag);
                        break;
                    case Province.Variable.Unrest:
                        n.AddVariable("unrest", en.Value.ToString());
                        break;
                }
            }
            return n;
          
        }

        public static void SaveObject(object toSave, string altPath = "def_DIR", string staticText = "def_TEX", string staticText2 = "def_TEX")
        {
            if(altPath != "def_DIR" && altPath != "")
            {
                if (altPath.Last() != '\\')
                    altPath += @"\";
            }


            if (toSave != null)
            {
                if (toSave.GetType() == typeof(Province))
                {
                    if (altPath == "def_DIR")
                    {
                        if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.historyProvinces] && !GlobalVariables.CreateNewFilesReadOnly)
                            return;
                        Province province = (Province)toSave;
                        if (province.HistoryFile.ReadOnly)
                        {
                            if (!Directory.Exists(GlobalVariables.pathtomod + "history\\"))
                                Directory.CreateDirectory(GlobalVariables.pathtomod + "history\\");
                            if (!Directory.Exists(GlobalVariables.pathtomod + "history\\provinces\\"))
                                Directory.CreateDirectory(GlobalVariables.pathtomod + "history\\provinces\\");

                            province.HistoryFile = new NodeFile(province.HistoryFile.Path);
                            province.HistoryFile.Path = GlobalVariables.pathtomod + "history\\provinces\\" + province.HistoryFile.Path.Split('\\').Last();
                            //PREV
                            //province.HistoryFile = new NodeFile(GlobalVariables.pathtomod + "history\\provinces\\" + province.HistoryFile.Path.Split('\\').Last());                     
                            if (province.HistoryFile.LastStatus.HasError)
                            {
                                GlobalVariables.MainForm.ShowMessageBox($"File '{province.HistoryFile.Path}' has an error in line {province.HistoryFile.LastStatus.LineError}");
                                return;
                            }
                        }
                        if (staticText == "def_TEX")
                        {
                            WriteToNodeFile(province.HistoryFile, toSave);
                            province.HistoryFile.SaveFile(province.HistoryFile.Path);
                        }
                        else
                        {
                            File.WriteAllText(province.HistoryFile.Path, staticText);
                        }
                    }
                    else
                    {
                        Province province = (Province)toSave;
                        if (!Directory.Exists(altPath + "history\\"))
                            Directory.CreateDirectory(altPath + "history\\");
                        if (!Directory.Exists(altPath + "history\\provinces\\"))
                            Directory.CreateDirectory(altPath + "history\\provinces\\");

                        NodeFile nf = new NodeFile(province.HistoryFile.Path);
                        nf.Path = altPath + "history\\provinces\\" + province.HistoryFile.Path.Split('\\').Last();
                        if (province.HistoryFile.LastStatus.HasError)
                        {
                            GlobalVariables.MainForm.ShowMessageBox($"File '{province.HistoryFile.Path}' has an error in line {province.HistoryFile.LastStatus.LineError}");
                            return;
                        }
                        if (staticText == "def_TEX")
                        {
                            WriteToNodeFile(nf, toSave);
                            nf.SaveFile();
                        }
                        else
                        {
                            File.WriteAllText(altPath + "history\\provinces\\" + province.HistoryFile.Path.Split('\\').Last(), staticText);
                        }
                    }
                }

                else if (toSave.GetType() == typeof(Country))
                {

                    if (altPath == "def_DIR")
                    {
                        bool countryComPriv = true;
                        bool countryHisPriv = true;
                        if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.historyCountries] && !GlobalVariables.CreateNewFilesReadOnly)
                            countryHisPriv = false;
                        if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.commonCountries] && !GlobalVariables.CreateNewFilesReadOnly)
                            countryComPriv = false;
                        Country country = (Country)toSave;
                        if (countryHisPriv)
                        {
                            if (country.HistoryFile.ReadOnly)
                            {
                                if (!Directory.Exists(GlobalVariables.pathtomod + "history\\"))
                                    Directory.CreateDirectory(GlobalVariables.pathtomod + "history\\");
                                if (!Directory.Exists(GlobalVariables.pathtomod + "history\\countries\\"))
                                    Directory.CreateDirectory(GlobalVariables.pathtomod + "history\\countries\\");

                                country.HistoryFile = new NodeFile(country.HistoryFile.Path);
                                country.HistoryFile.Path = GlobalVariables.pathtomod + "history\\countries\\" + country.HistoryFile.Path.Split('\\').Last();


                                //PREV
                                //country.HistoryFile = new NodeFile(GlobalVariables.pathtomod + "history\\countries\\" + country.HistoryFile.Path.Split('\\').Last());

                                if (country.HistoryFile.LastStatus.HasError)
                                {
                                    GlobalVariables.MainForm.ShowMessageBox($"File '{country.HistoryFile.Path}' has an error in line {country.HistoryFile.LastStatus.LineError}");
                                    return;
                                }
                            }
                            if (staticText == "def_TEX")
                            {
                                WriteToNodeFile(country.HistoryFile, country, 0);
                                country.HistoryFile.SaveFile(country.HistoryFile.Path);
                            }
                            else
                            {
                                File.WriteAllText(country.HistoryFile.Path, staticText);
                            }
                        }
                        if (countryComPriv)
                        {
                            if (country.CommonFile.ReadOnly)
                            {
                                if (!Directory.Exists(GlobalVariables.pathtomod + "common\\"))
                                    Directory.CreateDirectory(GlobalVariables.pathtomod + "common\\");
                                if (!Directory.Exists(GlobalVariables.pathtomod + "common\\countries\\"))
                                    Directory.CreateDirectory(GlobalVariables.pathtomod + "common\\countries\\");

                                country.CommonFile = new NodeFile(country.CommonFile.Path);
                                country.CommonFile.Path = GlobalVariables.pathtomod + "common\\countries\\" + country.HistoryFile.Path.Split('\\').Last();

                                //PREV
                                //country.CommonFile = new NodeFile(GlobalVariables.pathtomod + "common\\countries\\" + country.CommonFile.Path.Split('\\').Last());
                                if (country.CommonFile.LastStatus.HasError)
                                    GlobalVariables.MainForm.ShowMessageBox($"File '{country.CommonFile.Path}' has an error in line {country.CommonFile.LastStatus.LineError}");
                            }
                            if (staticText2 == "def_TEX")
                            {
                                WriteToNodeFile(country.CommonFile, country, 1);
                                country.CommonFile.SaveFile(country.CommonFile.Path);
                            }
                            else
                            {
                                File.WriteAllText(country.CommonFile.Path, staticText2);
                            }
                        }
                    }
                    else
                    {
                        Country country = (Country)toSave;
                        if (!Directory.Exists(altPath + "history\\"))
                            Directory.CreateDirectory(altPath + "history\\");
                        if (!Directory.Exists(altPath + "history\\countries\\"))
                            Directory.CreateDirectory(altPath + "history\\countries\\");

                        NodeFile his = new NodeFile(country.HistoryFile.Path);
                        his.Path = altPath + "history\\countries\\" + country.HistoryFile.Path.Split('\\').Last();
                        if (country.HistoryFile.LastStatus.HasError)
                        {
                            GlobalVariables.MainForm.ShowMessageBox($"File '{country.HistoryFile.Path}' has an error in line {country.HistoryFile.LastStatus.LineError}");
                            return;
                        }
                        if (staticText == "def_TEX")
                        {
                            WriteToNodeFile(his, country, 0);
                            his.SaveFile();
                        }
                        else
                        {
                            File.WriteAllText(altPath + "history\\countries\\" + country.HistoryFile.Path.Split('\\').Last(), staticText);
                        }
                        if (!Directory.Exists(altPath + "common\\"))
                            Directory.CreateDirectory(altPath + "common\\");
                        if (!Directory.Exists(altPath + "common\\countries\\"))
                            Directory.CreateDirectory(altPath + "common\\countries\\");

                        NodeFile com = new NodeFile(country.CommonFile.Path);
                        com.Path = altPath + "common\\countries\\" + country.HistoryFile.Path.Split('\\').Last();

                        if (country.CommonFile.LastStatus.HasError)
                            GlobalVariables.MainForm.ShowMessageBox($"File '{country.CommonFile.Path}' has an error in line {country.CommonFile.LastStatus.LineError}");

                        if (staticText2 == "def_TEX")
                        {
                            WriteToNodeFile(com, country, 1);
                            com.SaveFile();
                        }
                        else
                        {
                            File.WriteAllText(altPath + "common\\countries\\" + country.HistoryFile.Path.Split('\\').Last(), staticText2);
                        }

                    }
                
                }
                else if (toSave.GetType() == typeof(SpecialSavingObject))
                {
                    SpecialSavingObject specialobject = (SpecialSavingObject)toSave;
                    switch (specialobject.Type)
                    {
                        case SpecialSavingObject.SavingType.Area:
                            {
                                string path = GlobalVariables.pathtomod;
                                if (altPath != "def_DIR")
                                {
                                    path = altPath;
                                }
                                else
                                {
                                    if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.area] && !GlobalVariables.CreateNewFilesReadOnly)
                                        return;
                                    if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.area] && GlobalVariables.CreateNewFilesReadOnly && File.Exists(GlobalVariables.pathtomod + "map\\area.txt"))
                                        return;
                                }

                                if (!Directory.Exists(path + "map\\"))
                                    Directory.CreateDirectory(path + "map\\");

                                NodeFile nf = new NodeFile(path + "map\\area.txt");

                                if (nf.LastStatus.HasError)
                                {
                                    GlobalVariables.MainForm.ShowMessageBox($"File '{nf.Path}' has an error in line {nf.LastStatus.LineError}. Nothing will be saved!");
                                    return;
                                }
                                if (staticText == "def_TEX")
                                {
                                    WriteToNodeFile(nf, toSave);
                                    nf.SaveFile(path + "map\\area.txt");
                                }
                                else
                                {
                                    File.WriteAllText(path + "map\\area.txt", staticText);
                                }

                            }
                            break;
                        case SpecialSavingObject.SavingType.Region:
                            {
                                string path = GlobalVariables.pathtomod;
                                if (altPath != "def_DIR")
                                {
                                    path = altPath;
                                }
                                else
                                {
                                    if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.region] && !GlobalVariables.CreateNewFilesReadOnly)
                                        return;
                                    if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.region] && GlobalVariables.CreateNewFilesReadOnly && File.Exists(GlobalVariables.pathtomod + "map\\region.txt"))
                                        return;
                                }
                                if (!Directory.Exists(path+ "map\\"))
                                    Directory.CreateDirectory(path+ "map\\");
                                NodeFile nf = new NodeFile(path + "map\\region.txt");
                                if (nf.LastStatus.HasError)
                                {
                                    GlobalVariables.MainForm.ShowMessageBox($"File '{nf.Path}' has an error in line {nf.LastStatus.LineError}. Nothing will be saved!");
                                    return;
                                }
                                if (staticText == "def_TEX")
                                {
                                    WriteToNodeFile(nf, toSave);
                                    nf.SaveFile(path + "map\\region.txt");
                                }
                                else
                                {
                                    File.WriteAllText(path + "map\\region.txt", staticText);
                                }
                            }
                            break;
                        case SpecialSavingObject.SavingType.Continent:
                            {
                                string path = GlobalVariables.pathtomod;
                                if (altPath != "def_DIR")
                                {
                                    path = altPath;
                                }
                                else
                                {
                                    if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.continent] && !GlobalVariables.CreateNewFilesReadOnly)
                                        return;
                                    if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.continent] && GlobalVariables.CreateNewFilesReadOnly && File.Exists(GlobalVariables.pathtomod + "map\\continent.txt"))
                                        return;
                                }
                                if (!Directory.Exists(path + "map\\"))
                                    Directory.CreateDirectory(path + "map\\");

                                NodeFile nf = new NodeFile(path + "map\\continent.txt");
                                if (nf.LastStatus.HasError)
                                {
                                    GlobalVariables.MainForm.ShowMessageBox($"File '{nf.Path}' has an error in line {nf.LastStatus.LineError}. Nothing will be saved!");
                                    return;
                                }
                                if (staticText == "def_TEX")
                                {
                                    WriteToNodeFile(nf, toSave);
                                    nf.SaveFile(path + "map\\continent.txt");
                                }
                                else
                                {
                                    File.WriteAllText(path + "map\\continent.txt", staticText);
                                }
                            }
                            break;
                        case SpecialSavingObject.SavingType.Superregion:
                            {
                                string path = GlobalVariables.pathtomod;
                                if (altPath != "def_DIR")
                                {
                                    path = altPath;
                                }
                                else
                                {
                                    if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.superregion] && !GlobalVariables.CreateNewFilesReadOnly)
                                        return;
                                    if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.superregion] && GlobalVariables.CreateNewFilesReadOnly && File.Exists(GlobalVariables.pathtomod + "map\\superregion.txt"))
                                        return;
                                }
                                if (!Directory.Exists(path + "map\\"))
                                    Directory.CreateDirectory(path + "map\\");

                                NodeFile nf = new NodeFile(path + "map\\superregion.txt");
                                if (nf.LastStatus.HasError)
                                {
                                    GlobalVariables.MainForm.ShowMessageBox($"File '{nf.Path}' has an error in line {nf.LastStatus.LineError}. Nothing will be saved!");
                                    return;
                                }
                                if (staticText == "def_TEX")
                                {
                                    WriteToNodeFile(nf, toSave);
                                    nf.SaveFile(path + "map\\superregion.txt");
                                }
                                else
                                {
                                    File.WriteAllText(path + "map\\superregion.txt", staticText);
                                }
                            }
                            break;
                        case SpecialSavingObject.SavingType.TradeCompany:
                            {
                                string path = GlobalVariables.pathtomod;
                                bool normal = true;
                                if (altPath != "def_DIR")
                                {
                                    path = altPath;
                                    normal = false;
                                }
                                else
                                {
                                    if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.tradecompanies] && !GlobalVariables.CreateNewFilesReadOnly)
                                        return;
                                }
                                foreach (TradeCompany tc in GlobalVariables.TradeCompanies)
                                {                                   
                                    if (!tc.MadeChanges)
                                        continue;
                                    NodeFile toSaveTo;
                                    if (normal)
                                    {
                                        tc.MadeChanges = false;
                                        toSaveTo = DetermineSaveLocation(GlobalVariables.ModNodeFileTypes.TradeCompanies, tc.NodeFile);
                                    }
                                    else
                                    {
                                        if (!Directory.Exists(path + @"common\"))
                                            Directory.CreateDirectory(path + @"common\");
                                        if(!Directory.Exists(path + @"common\trade_companies\"))
                                            Directory.CreateDirectory(path + @"common\trade_companies\");
                                        toSaveTo = new NodeFile(path + @"common\trade_companies\00_trade_companies.txt");
                                    }
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
                                string path = GlobalVariables.pathtomod;
                                bool normal = true;
                                if (altPath != "def_DIR")
                                {
                                    path = altPath;
                                    normal = false;
                                }
                                else
                                {
                                    if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.countrytags] && !GlobalVariables.CreateNewFilesReadOnly)
                                        return;
                                }
                                List<Country> newSaves = new List<Country>();

                                //search for the countries with changed tags
                                foreach (Country c in GlobalVariables.Countries)
                                {
                                    if (c.FullName != c.OriginalFullName)
                                        newSaves.Add(c);
                                    if (c.Tag != c.OriginalTag)
                                        newSaves.Add(c);
                                }

                                //if there are any tags which are using the gamefile

                                if (normal)
                                {
                                    foreach (Country c in newSaves)
                                    {
                                        if (c.CountryTagsFile == null && GlobalVariables.ModCountryTagsFiles.Any())
                                            c.CountryTagsFile = GlobalVariables.ModCountryTagsFiles[0];
                                        else if (c.CountryTagsFile == null && GlobalVariables.GameCountryTagsFile != null)
                                            c.CountryTagsFile = GlobalVariables.GameCountryTagsFile;
                                        else if (c.CountryTagsFile == null)
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


                                    if (newSaves.Any(x => x.CountryTagsFile.FileName == "00_countries.txt" && x.CountryTagsFile == GlobalVariables.GameCountryTagsFile))
                                    {
                                        //create a mod replacement file
                                        NodeFile modTagsReplacement = new NodeFile(GlobalVariables.GameCountryTagsFile.Path);
                                        modTagsReplacement.Path = GlobalVariables.pathtomod + "common\\country_tags\\00_countries.txt";
                                        modTagsReplacement.SaveFile();
                                        GlobalVariables.ModCountryTagsFiles.Add(modTagsReplacement);
                                        //and set it for all countries
                                        foreach (Country c in GlobalVariables.Countries)
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
                                    foreach (Country c in newSaves)
                                    {
                                        Variable v = c.CountryTagsFile.MainNode.Variables.Find(x => x.Name == c.OriginalTag);
                                        if (v == null)
                                            c.CountryTagsFile.MainNode.AddVariable(c.Tag, $"\"countries/{c.FullName}\"");
                                        else
                                        {
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
                                        if (v != null)
                                            c.CountryTagsFile.MainNode.RemoveVariable(v);
                                    }

                                    foreach (NodeFile nf in GlobalVariables.ModCountryTagsFiles)
                                    {
                                        nf.SaveFile();
                                    }
                                }
                                else
                                {
                                    if (!Directory.Exists(path + @"common\"))
                                        Directory.CreateDirectory(path + @"common\");
                                    if (!Directory.Exists(path + @"common\tags\"))
                                        Directory.CreateDirectory(path + @"common\country_tags\");
                                    
                                    NodeFile tagsfile = new NodeFile(path + @"common\trade_companies\00_countries.txt");
                                    foreach (Country c in newSaves)
                                    {
                                        Variable v = tagsfile.MainNode.Variables.Find(x => x.Name == c.OriginalTag);
                                        if(v == null)
                                            tagsfile.MainNode.AddVariable(c.Tag, $"\"countries/{c.FullName}\"");
                                        else
                                        {
                                            v.Name = c.Tag;
                                            v.Value = $"\"countries/{c.FullName}.txt\"";
                                        }
                                    }
                                    foreach (Country c in GlobalVariables.RemovedCountries)
                                    {
                                        Variable v = tagsfile.MainNode.Variables.Find(x => x.Name == c.OriginalTag);
                                        if (v != null)
                                          tagsfile.MainNode.RemoveVariable(v);
                                    }
                                    tagsfile.SaveFile();

                                }
                                //save everything
                                
                            }
                            break;
                        case SpecialSavingObject.SavingType.Climate:
                            {
                                string path = GlobalVariables.pathtomod;
                                if (altPath != "def_DIR")
                                {
                                    path = altPath;
                                }
                                else
                                {
                                    if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.climate] && !GlobalVariables.CreateNewFilesReadOnly)
                                        return;
                                    if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.climate] && GlobalVariables.CreateNewFilesReadOnly && File.Exists(GlobalVariables.pathtomod + "map\\climate.txt"))
                                        return;
                                }

                                if (!Directory.Exists(path + "map\\"))
                                    Directory.CreateDirectory(path + "map\\");

                                NodeFile nf = new NodeFile(path + "map\\climate.txt");
                                if (nf.LastStatus.HasError)
                                {
                                    GlobalVariables.MainForm.ShowMessageBox($"File '{nf.Path}' has an error in line {nf.LastStatus.LineError}. Nothing will be saved!");
                                    return;
                                }
                                if (staticText == "def_TEX")
                                {
                                    WriteToNodeFile(nf, toSave);
                                    nf.SaveFile(path + "map\\climate.txt");
                                }
                                else
                                {
                                    File.WriteAllText(path + "map\\climate.txt", staticText);
                                }
                            }
                            break;
                        case SpecialSavingObject.SavingType.TradeNode:
                            {
                                string Fpath = GlobalVariables.pathtomod;
                                if (altPath != "def_DIR")
                                {
                                    Fpath = altPath;
                                }
                                else
                                {
                                    if (GlobalVariables.ReadOnly[(int)GlobalVariables.LoadFilesOrder.tradenodes] && !GlobalVariables.CreateNewFilesReadOnly)
                                        return;
                                }


                                if (!Directory.Exists(Fpath + @"common\"))
                                    Directory.CreateDirectory(Fpath + @"common\");
                                if (!Directory.Exists(Fpath + @"common\tradenodes\"))
                                    Directory.CreateDirectory(Fpath + @"common\tradenodes\");

                                NodeFile nf = new NodeFile();
                                List<Tradenode> left = new List<Tradenode>();
                                left.AddRange(GlobalVariables.TradeNodes);
                                List<Tradenode> done = new List<Tradenode>();
                                do
                                {
                                    foreach (Tradenode tn in left)
                                    {
                                        if (tn.Incoming.Any(x => !done.Contains(x)))
                                            continue;
                                        Node n = new Node(tn.Name);
                                        if (tn.Location != null)
                                            n.AddVariable("location", tn.Location.ID + "");
                                        else if (tn.Provinces.Any())
                                            n.AddVariable("location", tn.Provinces[0].ID + "");
                                        if (tn.Inland)
                                            n.AddVariable("inland", "yes");
                                        if (!tn.Destination.Any())
                                            n.AddVariable("end", "yes");
                                        Node cl = new Node("color");
                                        cl.AddPureValue(tn.Color.R + "");
                                        cl.AddPureValue(tn.Color.G + "");
                                        cl.AddPureValue(tn.Color.B + "");

                                        n.AddNode(cl);
                                        foreach (Destination ds in tn.Destination)
                                        {
                                            Node des = new Node("outgoing");
                                            des.AddVariable("name", "\"" + ds.TradeNode.Name + "\"");
                                            Node path = new Node("path");
                                            foreach (string s in ds.Path)
                                                path.AddPureValue(s);
                                            des.AddNode(path);
                                            Node control = new Node("control");
                                            foreach (string s in ds.Control)
                                                control.AddPureValue(s);
                                            des.AddNode(control);
                                            n.AddNode(des);
                                        }
                                        Node members = new Node("members");
                                        tn.Provinces.ForEach(x => members.AddPureValue(x.ID + ""));
                                        n.AddNode(members);
                                        nf.MainNode.AddNode(n);
                                        done.Add(tn);
                                    }
                                    left.RemoveAll(x => done.Contains(x));
                                } while (left.Any());
                                nf.SaveFile(Fpath + "common\\tradenodes\\00_tradenodes.txt");
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

                    (province.Variables[Province.Variable.Cores] as List<string>).Clear();
                    (province.Variables[Province.Variable.Claims] as List<string>).Clear();
                    (province.Variables[Province.Variable.DiscoveredBy] as List<string>).Clear();
                    (province.Variables[Province.Variable.Buildings] as List<Building>).Clear();
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
                    province.Variables[Province.Variable.LatentTradeGood] = null;
                    province.Variables[Province.Variable.TradeGood] = null;
                    province.Variables[Province.Variable.OwnerCountry] = null;

                    foreach (Variable v in nf.MainNode.Variables)
                    {
                        Building bl = GlobalVariables.Buildings.Find(x => x.Name == v.Name);
                        if (bl != null && v.Value == "yes")
                        {
                            (province.Variables[Province.Variable.Buildings] as List<Building>).Add(bl);
                        }
                        switch (v.Name)
                        {
                            case "add_core":
                                (province.Variables[Province.Variable.Cores] as List<string>).Add(v.Value);
                                break;
                            case "add_claim":
                                (province.Variables[Province.Variable.Claims] as List<string>).Add(v.Value);
                                break;
                            case "owner":
                                {
                                    Country c = GlobalVariables.Countries.Find(x => x.Tag == v.Value.ToUpper());
                                    if (c != null)
                                    {
                                        province.Variables[Province.Variable.OwnerCountry] = c;
                                        c.Provinces.Add(province);
                                    }
                                }
                                break;
                            case "controller":
                                {
                                    Country c = GlobalVariables.Countries.Find(x => x.Tag == v.Value.ToUpper());
                                    if (c != null)
                                    {
                                        province.Variables[Province.Variable.Controller] = c;
                                    }
                                }
                                break;
                            case "culture":
                                province.Variables[Province.Variable.Culture] = Culture.Cultures.Find(x => x.Name == v.Value);
                                break;
                            case "religion":
                                province.Variables[Province.Variable.Religion] = Religion.Religions.Find(x => x.Name == v.Value);
                                break;
                            case "hre":
                                province.Variables[Province.Variable.HRE] = v.Value == "yes" ? true : false;
                                break;
                            case "base_tax":
                                province.Variables[Province.Variable.Tax] = int.Parse(v.Value);
                                break;
                            case "base_production":
                                province.Variables[Province.Variable.Production] = int.Parse(v.Value);
                                break;
                            case "base_manpower":
                                province.Variables[Province.Variable.Manpower] = int.Parse(v.Value);
                                break;
                            case "trade_goods":
                                province.Variables[Province.Variable.TradeGood] = GlobalVariables.TradeGoods.Find(x => x.Name == v.Value);
                                if (province.TradeGood != null)
                                {
                                    province.TradeGood.TotalProvinces++;
                                }
                                break;
                            case "capital":
                                province.Variables[Province.Variable.Capital] = v.Value.Replace("\"", "");
                                break;
                            case "center_of_trade":
                                province.Variables[Province.Variable.CenterOfTrade] = int.Parse(v.Value);
                                break;
                            case "discovered_by":
                                (province.Variables[Province.Variable.DiscoveredBy] as List<string>).Add(v.Value);
                                break;
                            case "is_city":
                                province.Variables[Province.Variable.City] = v.Value == "yes" ? true : false;
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
                            province.Variables[Province.Variable.LatentTradeGood] = GlobalVariables.TradeGoods.Find(x => x.Name == n.PureValues[0].Name.Trim());
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
            public enum SavingType { Area, Region, Continent, Superregion, TradeCompany, TagFile, Climate, Terrain, TradeNode }
            public SavingType Type;
            public SpecialSavingObject(SavingType sv)
            {
                Type = sv;
            }
        }
    }
}
