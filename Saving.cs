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
        
        public static void SaveObject(object toSave)
        {
            if(toSave != null)
            {
                if (toSave.GetType() == typeof(Province))
                {
                    if (GlobalVariables.ReadOnly[8])
                        return;
                    Province province = (Province)toSave;

                    NodeFile nf = new NodeFile(province.HistoryFile);
                    if (nf == null)
                        return;
                    if (province.HistoryFileGame)
                    {
                        province.HistoryFileGame = false;
                        if(!Directory.Exists(GlobalVariables.pathtomod + "history\\"))
                            Directory.CreateDirectory(GlobalVariables.pathtomod + "history\\");
                        if (!Directory.Exists(GlobalVariables.pathtomod + "history\\provinces\\"))
                            Directory.CreateDirectory(GlobalVariables.pathtomod + "history\\provinces\\");
                        province.HistoryFile = GlobalVariables.pathtomod + "history\\provinces\\" + province.HistoryFile.Split('\\').Last();
                    }                                               
                    nf.MainNode.Variables.RemoveAll(x => x.Name == "add_core" || x.Name == "discovered_by" || GlobalVariables.Buildings.Any(y=>y.Name == x.Name) || x.Name == "add_claim");
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
                    foreach(Building bl in province.GetBuildings())
                    {
                        nf.MainNode.AddVariable(new Variable(bl.Name, "yes"));
                    }

                    if (province.OwnerCountry != Country.NoCountry && province.OwnerCountry != null)
                    {
                        nf.MainNode.ChangeVariable("owner", province.OwnerCountry.Tag, true);
                    }
                    else
                    {
                        nf.MainNode.Variables.RemoveAll(x => x.Name == "owner");
                        nf.MainNode.ItemOrder.RemoveAll(x => x.Name == "owner");
                    }

                    if(province.Tax > 0)
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
                    if(province.TradeGood != null && province.TradeGood != TradeGood.nothing)
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
                        nf.MainNode.Variables.RemoveAll(x => x.Name == "controller");
                        nf.MainNode.ItemOrder.RemoveAll(x => x.Name == "controller");
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
                            n.PureValues.Clear();
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



                    nf.SaveFile(province.HistoryFile);
                }

                if (toSave.GetType() == typeof(Country))
                {
                    if (GlobalVariables.ReadOnly[6])
                        return;
                    Country country = (Country)toSave;
                    NodeFile nf = new NodeFile(country.HistoryFile);

                    if (country.HistoryFileGame)
                    {
                        country.HistoryFileGame = false;
                        if (!Directory.Exists(GlobalVariables.pathtomod + "history\\"))
                            Directory.CreateDirectory(GlobalVariables.pathtomod + "history\\");
                        if (!Directory.Exists(GlobalVariables.pathtomod + "history\\countries\\"))
                            Directory.CreateDirectory(GlobalVariables.pathtomod + "history\\countries\\");
                        country.HistoryFile = GlobalVariables.pathtomod + "history\\countries\\" + country.HistoryFile.Split('\\').Last();
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

                    NodeFile cnf = new NodeFile(country.CommonFile);

                    if (country.CommonFileGame)
                    {
                        country.CommonFileGame = false;
                        if (!Directory.Exists(GlobalVariables.pathtomod + "common\\"))
                            Directory.CreateDirectory(GlobalVariables.pathtomod + "common\\");
                        if (!Directory.Exists(GlobalVariables.pathtomod + "common\\countries\\"))
                            Directory.CreateDirectory(GlobalVariables.pathtomod + "common\\countries\\");
                        country.CommonFile = GlobalVariables.pathtomod + "common\\countries\\" + country.CommonFile.Split('\\').Last();
                    }

                    Variable graphicalculture = cnf.MainNode.Variables.Find(x => x.Name == "graphical_culture");
                    if (graphicalculture != null)
                    {
                        graphicalculture.Value = country.GraphicalCulture;
                    }
                    else
                    {
                        graphicalculture = new Variable("graphical_culture", country.GraphicalCulture);
                        nf.MainNode.AddVariable(graphicalculture);
                    }

                    cnf.SaveFile(country.CommonFile);
                }
            }
        }

        public static void SaveThingsToUpdate_OLD()
        {
            foreach (object obj in GlobalVariables.ToUpdate)
            {
                if (obj != null)
                {
                    if (obj.GetType() == typeof(Province))
                    {
                        if (GlobalVariables.ReadOnly[8])
                            continue;
                        Province province = (Province)obj;
                        NodeFile nf = new NodeFile(province.HistoryFile);

                        if (nf == null)
                            continue;

                        nf.MainNode.Variables.RemoveAll(x => x.Name == "add_core");
                        nf.MainNode.ItemOrder.RemoveAll(x => x.Name == "add_core");
                        foreach (string tag in province.GetCores())
                        {
                            nf.MainNode.AddVariable("add_core", tag);
                        }

                        if (province.OwnerCountry != null)
                        {
                            nf.MainNode.ChangeVariable("owner", province.OwnerCountry.Tag, true);
                        }
                        else
                        {
                            nf.MainNode.Variables.RemoveAll(x => x.Name == "owner");
                            nf.MainNode.ItemOrder.RemoveAll(x => x.Name == "owner");
                        }

                        nf.MainNode.ChangeVariable("base_tax", province.Tax.ToString(), true);
                        nf.MainNode.ChangeVariable("base_manpower", province.Manpower.ToString(), true);
                        nf.MainNode.ChangeVariable("base_production", province.Production.ToString(), true);
                        if (province.Culture != null)
                        {
                            nf.MainNode.ChangeVariable("culture", province.Culture.Name, true);
                        }
                        else
                        {
                            nf.MainNode.Variables.RemoveAll(x => x.Name == "culture");
                            nf.MainNode.ItemOrder.RemoveAll(x => x.Name == "culture");
                        }
                        nf.MainNode.ChangeVariable("trade_goods", province.TradeGood.Name, true);
                        if (province.Religion != null)
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

                        Node n = nf.MainNode.Nodes.Find(x => x.Name == "latent_trade_goods");
                        if (province.LatentTradeGood != null)
                        {
                            if (n != null)
                            {
                                n.PureValues.Clear();
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

                       

                        nf.SaveFile(province.HistoryFile);
                    }

                    if (obj.GetType() == typeof(Country))
                    {
                        if (GlobalVariables.ReadOnly[6])
                            return;
                        Country country = (Country)obj;
                        NodeFile nf = new NodeFile(country.HistoryFile);
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

                        

                        nf.SaveFile(country.HistoryFile);
                    }

                }
            }
            GlobalVariables.ToUpdate.Clear();
        }

        public static void LoadObject(object toLoad)
        {
            if (toLoad != null)
            {
                if (toLoad.GetType() == typeof(Province))
                {
                    Province province = (Province)toLoad;
                    NodeFile nf = new NodeFile(province.HistoryFile);


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
                    NodeFile nf = new NodeFile(country.HistoryFile);
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

                    

                    nf.SaveFile(country.HistoryFile);
                }
            }
        }
    }
}
