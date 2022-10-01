using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System;

namespace Eu4ModEditor
{

    public class ProvinceDateEntry
    {
        /// <summary>
        /// All entry types that can be used in dates.
        /// </summary>
        public enum EntryType { CoresAdd, CoresRemove, ClaimsAdd, ClaimsRemove, Owner, Controller,
            Tax, Production, Manpower, Capital, CenterOfTrade, HRE, BuildingAdd, BuildingRemove,
            TradeGood, LatentTradeGood, Religion, Culture, DiscoveredByAdd, DiscoveredByRemove, Revolt
        }

        public class Entry
        {
            public EntryType Type;
            public object Value = null;
            public Entry(EntryType type, object value)
            {
                Type = type;
                Value = value;
            }
        }

        public List<Entry> Entries = new List<Entry>();

        public DateTime Date;

        public ProvinceDateEntry(DateTime date)
        {
            Date = date;
        }
    }



    public class Province
    {
        public int ID = 1;
        public int R = 0;
        public int G = 0;
        public int B = 0;
        public string DefinitionName = "";

        public List<Province> BorderingProvinces = new List<Province>();

        public Dictionary<string, object> Variables = new Dictionary<string, object>();

        private List<string> Cores
        {
            get
            {             
                List<string> toreturn = Variables["Cores"] as List<string>;
                foreach (ProvinceDateEntry pde in DateEntries)
                {
                    if(DateTime.Compare(pde.Date, GlobalVariables.StartDate) <= 0)
                    {
                        foreach(ProvinceDateEntry.Entry entry in pde.Entries)
                        {
                            if(entry.Type == ProvinceDateEntry.EntryType.CoresAdd)
                            {
                                toreturn.Add(entry.Value as string);
                            }
                            else if (entry.Type == ProvinceDateEntry.EntryType.CoresRemove)
                            {
                                toreturn.Remove(entry.Value as string);
                            }
                        }
                    }
                }
                return toreturn;             
                //return Variables["Cores"] as List<string>;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "Cores", Variables["Cores"], value as List<string>));
                Variables["Cores"] = value;
            }
        }
        public string[] GetCores()
        {
            return Cores.ToArray();
        }
        public void AddCore(string TAG, bool noChange = false)
        {
            if (!Cores.Contains(TAG))
            {
                /*if (DateTime.Compare(GlobalVariables.CurrentDate, GlobalVariables.StartDate) == 0)
                {*/
                    Cores.Add(TAG);
                    if (!noChange)
                        GlobalVariables.Changes.Add(new VariableChange(this, "Core", null, TAG));
                /*}
                else
                {
                    ProvinceDateEntry pde = DateEntries.Find(x => DateTime.Compare(x.Date, GlobalVariables.CurrentDate) == 0);
                    if (pde == null)
                    {
                        pde = new ProvinceDateEntry(GlobalVariables.CurrentDate);
                        AddDateEntry(pde);
                    }
                    ProvinceDateEntry.Entry entry = pde.Entries.Find(x => x.Type == ProvinceDateEntry.EntryType.CoresAdd && (string)x.Value == TAG);
                    if (entry == null)
                    {
                        entry = new ProvinceDateEntry.Entry(ProvinceDateEntry.EntryType.CoresAdd, TAG);
                        pde.Entries.Add(entry);
                    }
                }*/
            }
        }
        public void RemoveCore(string TAG, bool noChange = false)
        {
            if (Cores.Contains(TAG))
            {
                /*if (DateTime.Compare(GlobalVariables.CurrentDate, GlobalVariables.StartDate) == 0)
                { */
                    Cores.Remove(TAG);
                    if (!noChange)
                        GlobalVariables.Changes.Add(new VariableChange(this, "Core", TAG, null));
                /*}
                else
                {
                    ProvinceDateEntry pde = DateEntries.Find(x => DateTime.Compare(x.Date, GlobalVariables.CurrentDate) == 0);
                    if (pde == null)
                    {
                        pde = new ProvinceDateEntry(GlobalVariables.CurrentDate);
                        AddDateEntry(pde);
                    }
                    ProvinceDateEntry.Entry entry = pde.Entries.Find(x => x.Type == ProvinceDateEntry.EntryType.CoresAdd && (string)x.Value == TAG);
                    if (entry == null)
                    {
                        entry = new ProvinceDateEntry.Entry(ProvinceDateEntry.EntryType.CoresAdd, TAG);
                        pde.Entries.Add(entry);
                    }
                }
                */
            }
        }

        private List<string> Claims
        {
            get
            {
                return Variables["Claims"] as List<string>;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "Claims", Variables["Claims"], value as List<string>));
                Variables["Claims"] = value;
            }
        }
        public string[] GetClaims()
        {
            return Claims.ToArray();
        }
        public void AddClaim(string TAG, bool noChange = false)
        {
            if (!Claims.Contains(TAG))
            {
                Claims.Add(TAG);
                if (!noChange)
                    GlobalVariables.Changes.Add(new VariableChange(this, "Claims", null, TAG));
            }
        }
        public void RemoveClaim(string TAG, bool noChange = false)
        {
            if (Claims.Contains(TAG))
            {
                Claims.Remove(TAG);
                if (!noChange)
                    GlobalVariables.Changes.Add(new VariableChange(this, "Claims", TAG, null));
            }
        }

        public List<ProvinceDateEntry> DateEntries = new List<ProvinceDateEntry>();

        public void AddDateEntry(ProvinceDateEntry entry)
        {
            DateEntries.Add(entry);
            DateEntries = DateEntries.OrderByDescending(x => x.Date).ToList();
        }


        public Country OwnerCountry
        {
            get
            {
                return Variables["OwnerCountry"] as Country;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "OwnerCountry", Variables["OwnerCountry"], value as Country));
                Variables["OwnerCountry"] = value;
            }
        }
        public Country Controller
        {
            get
            {
                return Variables["Controller"] as Country;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "Controller", Variables["Controller"], value as Country));
                Variables["Controller"] = value;
            }
        }
        public int Tax
        {
            get
            {
                return (int)Variables["Tax"];
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "Tax", Variables["Tax"], (int)value));
                Variables["Tax"] = value;
            }
        }
        public int Production
        {
            get
            {
                return (int)Variables["Production"];
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "Production", Variables["Production"], value));
                Variables["Production"] = value;
            }
        }
        public int Manpower
        {
            get
            {
                return (int)Variables["Manpower"];
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "Manpower", Variables["Manpower"], value));
                Variables["Manpower"] = value;
            }
        }
        public string Capital
        {
            get
            {
                return Variables["Capital"] as string;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "Capital", Variables["Capital"], value));
                Variables["Capital"] = value;
            }
        }
        public int CenterOfTrade
        {
            get
            {
                return (int)Variables["CenterOfTrade"];
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "CenterOfTrade", Variables["CenterOfTrade"], value));
                Variables["CenterOfTrade"] = value;
            }
        }
        public bool HRE
        {
            get
            {
                return (bool)Variables["HRE"];
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "HRE", Variables["HRE"], value));
                Variables["HRE"] = value;
            }
        }
        public bool Fort
        {
            get
            {
                return (bool)Variables["Fort"];
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "Fort", Variables["Fort"], value));
                Variables["Fort"] = value;
                if (value)
                {
                    Building fort = GlobalVariables.Buildings.Find(x => x.Name == "fort_15th");
                    if (fort != null)
                        AddBuilding(GlobalVariables.Buildings.Find(x => x.Name == "fort_15th"), true);
                }
                else
                {
                    Building fort = GlobalVariables.Buildings.Find(x => x.Name == "fort_15th");
                    if (fort != null)
                        RemoveBuilding(GlobalVariables.Buildings.Find(x => x.Name == "fort_15th"), true);
                }
            }
        }
        public bool City
        {
            get
            {
                return (bool)Variables["City"];
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "City", Variables["City"], value));
                Variables["City"] = value;
            }
        }
        private List<Building> Buildings
        {
            get
            {
                return Variables["Buildings"] as List<Building>;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "Buildings", Variables["Buildings"], value as List<Building>));
                Variables["Buildings"] = value;
            }
        }
        public void AddBuilding(Building Building, bool noChange = false)
        {
            if (!Buildings.Contains(Building))
            {
                Buildings.Add(Building);
                if (!noChange)
                    GlobalVariables.Changes.Add(new VariableChange(this, "Buildings", null, Building));
                if (Building.Name == "fort_15th")
                    Variables["Fort"] = true;
            }
        }
        public void RemoveBuilding(Building Building, bool noChange = false)
        {
            if (Buildings.Contains(Building))
            {
                Buildings.Remove(Building);
                if (!noChange)
                    GlobalVariables.Changes.Add(new VariableChange(this, "Buildings", Building, null));
                if (Building.Name == "fort_15th")
                    Variables["Fort"] = false;
            }
        }
        public Building[] GetBuildings()
        {
            return Buildings.ToArray();
        }

        public TradeGood TradeGood
        {
            get
            {
                return Variables["TradeGood"] as TradeGood;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "TradeGood", Variables["TradeGood"], value));
                Variables["TradeGood"] = value;
            }
        }
        public TradeGood LatentTradeGood
        {
            get
            {
                return Variables["LatentTradeGood"] as TradeGood;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "LatentTradeGood", Variables["LatentTradeGood"], value));
                Variables["LatentTradeGood"] = value;
            }
        }
        public Tradenode TradeNode
        {
            get
            {
                return Variables["TradeNode"] as Tradenode;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "TradeNode", Variables["TradeNode"], value));
                Variables["TradeNode"] = value;
            }
        }

        public Religion Religion
        {
            get
            {
                return Variables["Religion"] as Religion;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "Religion", Variables["Religion"], value));
                Variables["Religion"] = value;
            }
        }

        public Culture Culture
        {
            get
            {
                return Variables["Culture"] as Culture;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "Culture", Variables["Culture"], value));
                Variables["Culture"] = value;
            }
        }

        public bool Sea;
        public bool Lake;
        public bool Wasteland;

        public Point Pixel = new Point();
        public List<Point> Pixels = new List<Point>();
        public List<Point> BorderPixels = new List<Point>();
        public List<Point> NonBorderPixels = new List<Point>();
        public Rectangle ContainingRectangle;
        public Point Center = new Point();
        public Color c;

        public Color MainColor;
        public Color MainStripes;
        public Color VerticalStripes;

        public Color OldMainColor;
        public Color OldMainStripes;
        public Color OldVerticalStripes;

        public NodeFile HistoryFile;

        public TradeCompany TradeCompany
        {
            get
            {
                return Variables["TradeCompany"] as TradeCompany;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "TradeCompany", Variables["TradeCompany"], value.ToString()));
                Variables["TradeCompany"] = value;
            }
        }

        public Area Area
        {
            get
            {
                return Variables["Area"] as Area;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "Area", Variables["Area"], value));
                if (Variables["Area"] != null)
                    (Variables["Area"] as Area).Provinces.Remove(this);
                Variables["Area"] = value;
                if (value != null)
                    value.Provinces.Add(this);
            }
        }

        public Continent Continent
        {
            get
            {
                return Variables["Continent"] as Continent;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "Continent", Variables["Continent"], value));
                if (Variables["Continent"] != null)
                    (Variables["Continent"] as Continent).Provinces.Remove(this);
                Variables["Continent"] = value;
                if (value != null)
                    value.Provinces.Add(this);
            }
        }

        public int Climate
        {
            get
            {
                return (int)Variables["Climate"];
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.Climate))
                        GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Climate));
                Variables["Climate"] = value;
            }
        }
        public int Winter
        {
            get
            {
                return (int)Variables["Winter"];
            }
            set
            {
                Variables["Winter"] = value;
                if (GlobalVariables.FullyLoaded)
                    if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.Climate))
                        GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Climate));
            }
        }
        public int Monsoon
        {
            get
            {
                return (int)Variables["Monsoon"];
            }
            set
            {
                Variables["Monsoon"] = value;
                if (GlobalVariables.FullyLoaded)
                    if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.Climate))
                        GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Climate));
            }
        }
        public int Impassable
        {
            get
            {
                return (int)Variables["Impassable"];
            }
            set
            {
                Variables["Impassable"] = value;
                if (GlobalVariables.FullyLoaded)
                    if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.Climate))
                        GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Climate));
            }
        }

        public int Terrain
        {
            get
            {
                return (int)Variables["Terrain"];
            }
            set
            {
                Variables["Terrain"] = value;
                if (GlobalVariables.FullyLoaded)
                    if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.Terrain))
                        GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Terrain));
            }
        }

        private List<string> DiscoveredBy
        {
            get
            {
                return Variables["DiscoveredBy"] as List<string>;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "DiscoveredBy", Variables["DiscoveredBy"], value as List<string>));
                Variables["DiscoveredBy"] = value;
            }
        }
        public void AddDiscoveredBy(string Tech, bool noChange = false)
        {
            if (!DiscoveredBy.Contains(Tech))
            {
                DiscoveredBy.Add(Tech);
                if (!noChange)
                    GlobalVariables.Changes.Add(new VariableChange(this, "DiscoveredBy", null, Tech));
            }
        }
        public void RemoveDiscoveredBy(string Tech, bool noChange = false)
        {
            if (DiscoveredBy.Contains(Tech))
            {
                DiscoveredBy.Remove(Tech);
                if (!noChange)
                    GlobalVariables.Changes.Add(new VariableChange(this, "DiscoveredBy", Tech, null));
            }
        }
        public string[] GetDiscoveredBy()
        {
            return DiscoveredBy.ToArray();
        }

        public Province(int ID, int R, int G, int B, string DefinitionName = "")
        {
            this.ID = ID;
            this.R = R;
            this.G = G;
            this.B = B;
            this.DefinitionName = DefinitionName;
            c = Color.FromArgb(R, G, B);
            Variables.Add("Cores", new List<string>());
            Variables.Add("Claims", new List<string>());
            Variables.Add("OwnerCountry", null);
            Variables.Add("Controller", null);
            Variables.Add("Tax", 0);
            Variables.Add("Production", 0);
            Variables.Add("Manpower", 0);
            Variables.Add("Capital", "");
            Variables.Add("CenterOfTrade", 0);
            Variables.Add("HRE", false);
            Variables.Add("Fort", false);
            Variables.Add("TradeGood", TradeGood.nothing);
            Variables.Add("LatentTradeGood", null);
            Variables.Add("TradeNode", null);
            Variables.Add("Religion", Religion.NoReligion);
            Variables.Add("Culture", null);
            Variables.Add("Area", null);
            Variables.Add("Continent", null);
            Variables.Add("DiscoveredBy", new List<string>());
            Variables.Add("City", false);
            Variables.Add("Buildings", new List<Building>());
            Variables.Add("TradeCompany", null);
            Variables.Add("Winter", 0);
            Variables.Add("Climate", 0);
            Variables.Add("Terrain", null);
            Variables.Add("Impassable", 0);
            Variables.Add("Monsoon", 0);
        }
    }
}
