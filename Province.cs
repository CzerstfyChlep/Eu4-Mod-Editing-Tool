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
        /// 

        public class Revolt
        {
            public string Type = "";
            public int Size = 1;
            public string Name = "";
            public string Leader = "";
        }

        public class Entry
        {
            public Province.Variable Type;
            public object Value = null;
            public ProvinceDateEntry Parent = null;
            public Entry(Province.Variable type, object value)
            {
                Type = type;
                Value = value;
            }
        }

        public Entry AddDateEntry(Province.Variable type, object value)
        {
            Entry e = new Entry(type,value);
            e.Parent = this;
            Entries.Add(e);
            return e;
        }


        public List<Entry> Entries = new List<Entry>();

        public bool TryGetValue(Province.Variable type, out object value)
        {
            Entry e = Entries.Find(x => x.Type == type);
            if(e == null)
            {
                value = null;
                return false;
            }
            else
            {
                value = e.Value;
                return true;
            }
        }

        public DateTime Date;

        private Province Province;
        public Province GetParentProvince()
        {
            return Province;
        }
        public void RemoveFromProvince()
        {
            Province.DateEntries.Remove(this);
        }

        public ProvinceDateEntry(DateTime date, Province p)
        {
            Date = date;
            Province = p;
            p.AddDateEntry(this);
        }

        public override string ToString()
        {
            return Date.ToString("yyyy.MM.dd");
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

        public Dictionary<Variable, object> Variables = new Dictionary<Variable, object>();

        public void AddRevolt(DateTime date,ProvinceDateEntry.Revolt revolt)
        {
            ProvinceDateEntry pde = DateEntries.Find(x => DateTime.Compare(x.Date, date) == 0);
            if (pde == null)
            {
                pde = new ProvinceDateEntry(date, this);
            }
            ProvinceDateEntry.Entry entry = pde.Entries.Find(x => x.Type == Variable.Revolt);
            if (entry == null)
            {
                entry = pde.AddDateEntry(Variable.Revolt, revolt);
            }
            else
            {
                entry.Value = revolt;
            }

        }

        private List<string> Cores
        {
            get
            {             
                List<string> toreturn = new List<string>(Variables[Variable.Cores] as List<string>);
                foreach (ProvinceDateEntry pde in DateEntries)
                {
                    if(DateTime.Compare(pde.Date, GlobalVariables.CurrentDate) <= 0)
                    {
                        foreach(ProvinceDateEntry.Entry entry in pde.Entries)
                        {
                            if(entry.Type == Variable.CoresAdd)
                            {
                                toreturn.Add(entry.Value as string);
                            }
                            else if (entry.Type == Variable.CoresRemove)
                            {
                                toreturn.Remove(entry.Value as string);
                            }
                        }
                    }
                }
                return toreturn;             
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "Cores", Variables[Variable.Cores], value as List<string>));
                Variables[Variable.Cores] = value;
            }
        }
        public string[] GetCores()
        {
            return Cores.ToArray();
        }
        public void AddCore(string TAG, DateTime date, bool noChange = false)
        {
            if (!Cores.Contains(TAG))
            {
                if (DateTime.Compare(date, GlobalVariables.StartDate) == 0)
                {
                    var b = GetCores().ToList();
                    b.Add(TAG);
                    Cores = b;
                    if (!noChange)
                        GlobalVariables.Changes.Add(new VariableChange(this, "Core", null, TAG));
                }
                else
                {
                    ProvinceDateEntry pde = DateEntries.Find(x => DateTime.Compare(x.Date, date) == 0);
                    if (pde == null)
                    {
                        pde = new ProvinceDateEntry(date, this);
                        //AddDateEntry(pde);
                    }
                    ProvinceDateEntry.Entry entry = pde.Entries.Find(x => x.Type == Variable.CoresAdd && (string)x.Value == TAG);
                    if (entry == null)
                    {
                        entry = pde.AddDateEntry(Variable.CoresAdd, TAG);
                    }
                }
            }
        }
        public void RemoveCore(string TAG, DateTime date, bool noChange = false)
        {
            if (Cores.Contains(TAG))
            {
                if (DateTime.Compare(date, GlobalVariables.StartDate) == 0)
                {
                    var b = GetCores().ToList();
                    b.Remove(TAG);
                    Cores = b;
                    if (!noChange)
                        GlobalVariables.Changes.Add(new VariableChange(this, "Core", TAG, null));
                }
                else
                {
                    ProvinceDateEntry pde = DateEntries.Find(x => DateTime.Compare(x.Date, date) == 0);
                    if (pde == null)
                    {
                        pde = new ProvinceDateEntry(date, this);
                        //AddDateEntry(pde);
                    }
                    ProvinceDateEntry.Entry entry = pde.Entries.Find(x => x.Type == Variable.CoresRemove && (string)x.Value == TAG);
                    if (entry == null)
                    {
                        entry = pde.AddDateEntry(Variable.CoresRemove, TAG);
                    }
                }
                
            }
        }

        private void SetValueWithDates<T>(object value, Variable type)
        {
            if (DateTime.Compare(GlobalVariables.CurrentDate, GlobalVariables.StartDate) == 0)
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, type.ToString(), Variables[type], (T)value));
                Variables[type] = value;
            }
            else
            {
                ProvinceDateEntry pde = DateEntries.Find(x => DateTime.Compare(x.Date, GlobalVariables.CurrentDate) == 0);
                if (pde == null)
                {
                    pde = new ProvinceDateEntry(GlobalVariables.CurrentDate, this);
                    //AddDateEntry(pde);
                }
                ProvinceDateEntry.Entry entry = pde.Entries.Find(x => x.Type == type);
                if (entry == null)
                {
                    entry = pde.AddDateEntry(type, value);
                }
                else
                    entry.Value = value;
            }
        }
        private T GetValueWithDates<T>(Variable type, DateTime date)
        {
            T Value = (T)Variables[type];
            if (!DateEntries.Any())
                return Value;
            else
            {
                foreach (ProvinceDateEntry pd in DateEntries)
                {
                    if (DateTime.Compare(date, pd.Date) >= 0)
                    {
                        if (pd.TryGetValue(type, out object value))
                        {
                            Value = (T)value;
                        }
                    }
                    else
                        continue;
                }
                return Value;
            }
        }

        private List<string> Claims
        {

            get
            {
                List<string> toreturn = new List<string>(Variables[Variable.Claims] as List<string>);
                foreach (ProvinceDateEntry pde in DateEntries)
                {
                    if (DateTime.Compare(pde.Date, GlobalVariables.CurrentDate) <= 0)
                    {
                        foreach (ProvinceDateEntry.Entry entry in pde.Entries)
                        {
                            if (entry.Type == Variable.ClaimsAdd)
                            {
                                toreturn.Add(entry.Value as string);
                            }
                            else if (entry.Type == Variable.ClaimsRemove)
                            {
                                toreturn.Remove(entry.Value as string);
                            }
                        }
                    }
                }
                return toreturn;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "Claims", Variables[Variable.Claims], value as List<string>));
                Variables[Variable.Claims] = value;
            }
        }
        public string[] GetClaims()
        {
            return Claims.ToArray();
        }
        public void AddClaim(string TAG, DateTime date, bool noChange = false)
        {
            if (!Claims.Contains(TAG))
            {
                if (DateTime.Compare(date, GlobalVariables.StartDate) == 0)
                {
                    var b = GetClaims().ToList();
                    b.Add(TAG);
                    Claims = b;
                    if (!noChange)
                        GlobalVariables.Changes.Add(new VariableChange(this, "Claims", null, TAG));
                }
                else
                {
                    ProvinceDateEntry pde = DateEntries.Find(x => DateTime.Compare(x.Date, date) == 0);
                    if (pde == null)
                    {
                        pde = new ProvinceDateEntry(date, this);
                        //AddDateEntry(pde);
                    }
                    ProvinceDateEntry.Entry entry = pde.Entries.Find(x => x.Type == Variable.ClaimsAdd && (string)x.Value == TAG);
                    if (entry == null)
                    {
                        pde.AddDateEntry(Variable.ClaimsAdd, TAG);
                    }
                }
            }
        }
        public void RemoveClaim(string TAG, DateTime date, bool noChange = false)
        {
            if (Claims.Contains(TAG))
            {
                if (DateTime.Compare(date, GlobalVariables.StartDate) == 0)
                {
                    var b = GetClaims().ToList();
                    b.Remove(TAG);
                    Cores = b;
                    if (!noChange)
                        GlobalVariables.Changes.Add(new VariableChange(this, "Claims", TAG, null));
                }
                else
                {
                    ProvinceDateEntry pde = DateEntries.Find(x => DateTime.Compare(x.Date, date) == 0);
                    if (pde == null)
                    {
                        pde = new ProvinceDateEntry(date, this);
                        //AddDateEntry(pde);
                    }
                    ProvinceDateEntry.Entry entry = pde.Entries.Find(x => x.Type == Variable.ClaimsRemove && (string)x.Value == TAG);
                    if (entry == null)
                    {
                        pde.AddDateEntry(Variable.ClaimsRemove, TAG);
                    }
                }

            }
        }

        public List<ProvinceDateEntry> DateEntries = new List<ProvinceDateEntry>();
        
        public void AddDateEntry(ProvinceDateEntry entry)
        {
            DateEntries.Add(entry);
            DateEntries.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
        }

        public Country OwnerCountry
        {
            get
            {
                return GetValueWithDates<Country>(Variable.OwnerCountry, GlobalVariables.CurrentDate);
            }
            set
            {
                SetValueWithDates<Country>(value, Variable.OwnerCountry);
            }
        }
        public Country Controller
        {
            get
            {
                return GetValueWithDates<Country>(Variable.Controller, GlobalVariables.CurrentDate);
            }
            set
            {
                SetValueWithDates<Country>(value, Variable.Controller);
            }
        }
        public int Tax
        {
            get
            {
                return GetValueWithDates<int>(Variable.Tax, GlobalVariables.CurrentDate);
            }
            set
            {
                SetValueWithDates<int>(value, Variable.Tax);
            }
        }
        public int Production
        {
            get
            {
                return GetValueWithDates<int>(Variable.Production, GlobalVariables.CurrentDate);
            }
            set
            {
                SetValueWithDates<int>(value, Variable.Production);
            }
        }
        public int Manpower
        {
            get
            {
                return GetValueWithDates<int>(Variable.Manpower, GlobalVariables.CurrentDate);
            }
            set
            {
                SetValueWithDates<int>(value, Variable.Manpower);
            }
        }
        public string Capital
        {
            get
            {
                return GetValueWithDates<string>(Variable.Capital, GlobalVariables.CurrentDate);
            }
            set
            {
                SetValueWithDates<string>(value, Variable.Capital);
            }
        }
        public int CenterOfTrade
        {
            get
            {
                return GetValueWithDates<int>(Variable.CenterOfTrade, GlobalVariables.CurrentDate);
            }
            set
            {
                SetValueWithDates<int>(value, Variable.CenterOfTrade);
            }
        }
        public bool HRE
        {
            get
            {
                return GetValueWithDates<bool>(Variable.HRE, GlobalVariables.CurrentDate);
            }
            set
            {
                SetValueWithDates<bool>(value, Variable.HRE);
            }
        }

        //TODO FORT WITH DATES

            /*
        public bool Fort
        {
            get
            {
                bool Value = (bool)Variables["Fort"];
                if (!DateEntries.Any())
                    return Value;
                else
                {
                    foreach (ProvinceDateEntry pd in DateEntries)
                    {
                        if (DateTime.Compare(GlobalVariables.CurrentDate, pd.Date) >= 0)
                        {
                            if (pd.TryGetValue(ProvinceDateEntry.EntryType.Fort, out object value))
                            {
                                Value = (bool)value;
                            }
                        }
                        else
                            continue;
                    }
                    return Value;
                }
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
                        AddBuilding(GlobalVariables.Buildings.Find(x => x.Name == "fort_15th"), GlobalVariables.CurrentDate ,true);
                }
                else
                {
                    Building fort = GlobalVariables.Buildings.Find(x => x.Name == "fort_15th");
                    if (fort != null)
                        RemoveBuilding(GlobalVariables.Buildings.Find(x => x.Name == "fort_15th"), GlobalVariables.CurrentDate, true);
                }
            }
        }
        */

        public bool City
        {
            get
            {
                return GetValueWithDates<bool>(Variable.City, GlobalVariables.CurrentDate);
            }
            set
            {
                SetValueWithDates<bool>(value, Variable.City);
            }
        }

        private List<Building> Buildings
        {
            get
            {
                //return Variables["Buildings"] as List<Building>;

                List<Building> Value = new List<Building>(((List<Building>)Variables[Variable.Buildings]).ToList());
                if (!DateEntries.Any())
                    return Value;
                else
                {
                    foreach (ProvinceDateEntry pd in DateEntries)
                    {
                        if (DateTime.Compare(GlobalVariables.CurrentDate, pd.Date) >= 0)
                        {
                            foreach(ProvinceDateEntry.Entry entry in pd.Entries.Where(x=>x.Type == Variable.BuildingAdd || x.Type == Variable.BuildingRemove))
                            {
                                if (entry.Type == Variable.BuildingRemove)
                                    Value.Remove((Building)entry.Value);
                                else
                                    Value.Add((Building)entry.Value);
                            }
                        }
                        else
                            continue;
                    }
                    return Value;
                }
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "Buildings", Variables[Variable.Buildings], value as List<Building>));
                Variables[Variable.Buildings] = value;
            }
        }
        public void AddBuilding(Building Building, DateTime date, bool noChange = false)
        {
            if (!Buildings.Contains(Building))
            {
                if (DateTime.Compare(date, GlobalVariables.StartDate) == 0)
                {
                    var b = GetBuildings().ToList();
                    b.Add(Building);
                    Buildings = b;
                    if (!noChange)
                        GlobalVariables.Changes.Add(new VariableChange(this, "Buildings", null, Building));
                }
                else
                {
                    ProvinceDateEntry pde = DateEntries.Find(x => DateTime.Compare(x.Date, date) == 0);
                    if (pde == null)
                    {
                        pde = new ProvinceDateEntry(date, this);
                        //AddDateEntry(pde);
                    }
                    ProvinceDateEntry.Entry entry = pde.Entries.Find(x => x.Type == Variable.BuildingAdd && (Building)x.Value == Building);
                    if (entry == null)
                    {
                        entry = pde.AddDateEntry(Variable.BuildingAdd, Building);
                    }
                }
            }
        }
        public void RemoveBuilding(Building Building, DateTime date, bool noChange = false)
        {
            if (Buildings.Contains(Building) || true)
            {
                if (DateTime.Compare(date, GlobalVariables.StartDate) == 0)
                {
                    var b = GetBuildings().ToList();
                    b.Remove(Building);
                    Buildings = b;
                    if (!noChange)
                        GlobalVariables.Changes.Add(new VariableChange(this, "Buildings", Building, null));
                    //if (Building.Name == "fort_15th")
                    //    Variables["Fort"] = false;
                }
                else
                {
                    ProvinceDateEntry pde = DateEntries.Find(x => DateTime.Compare(x.Date, date) == 0);
                    if (pde == null)
                    {
                        pde = new ProvinceDateEntry(date, this);
                        //AddDateEntry(pde);
                    }
                    ProvinceDateEntry.Entry entry = pde.Entries.Find(x => x.Type == Variable.BuildingRemove && (Building)x.Value == Building);
                    if (entry == null)
                    {
                        pde.AddDateEntry(Variable.BuildingRemove, Building);
                    }
                }

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
                return GetValueWithDates<TradeGood>(Variable.TradeGood, GlobalVariables.CurrentDate);
            }
            set
            {
                SetValueWithDates<TradeGood>(value, Variable.TradeGood);
            }
        }
        public TradeGood LatentTradeGood
        {
            get
            {
                return GetValueWithDates<TradeGood>(Variable.LatentTradeGood, GlobalVariables.CurrentDate);
            }
            set
            {
                SetValueWithDates<TradeGood>(value, Variable.LatentTradeGood);
            }
        }
        public Tradenode TradeNode
        {
            get
            {
                return Variables[Variable.TradeNode] as Tradenode;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "TradeNode", Variables[Variable.TradeNode], value));
                Variables[Variable.TradeNode] = value;
            }
        }

        public Religion Religion
        {
            get
            {
                return GetValueWithDates<Religion>(Variable.Religion, GlobalVariables.CurrentDate);
            }
            set
            {
                SetValueWithDates<Religion>(value, Variable.Religion);
            }
        }

        public Culture Culture
        {
            get
            {
                return GetValueWithDates<Culture>(Variable.Culture, GlobalVariables.CurrentDate);
            }
            set
            {
                SetValueWithDates<Culture>(value, Variable.Culture);
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
                return Variables[Variable.TradeCompany] as TradeCompany;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "TradeCompany", Variables[Variable.TradeCompany], value.ToString()));
                Variables[Variable.TradeCompany] = value;
            }
        }

        public Area Area
        {
            get
            {
                return Variables[Variable.Area] as Area;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "Area", Variables[Variable.Area], value));
                if (Variables[Variable.Area] != null)
                    (Variables[Variable.Area] as Area).Provinces.Remove(this);
                Variables[Variable.Area] = value;
                if (value != null)
                    value.Provinces.Add(this);
            }
        }

        public Continent Continent
        {
            get
            {
                return Variables[Variable.Continent] as Continent;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "Continent", Variables[Variable.Continent], value));
                if (Variables[Variable.Continent] != null)
                    (Variables[Variable.Continent] as Continent).Provinces.Remove(this);
                Variables[Variable.Continent] = value;
                if (value != null)
                    value.Provinces.Add(this);
            }
        }

        public int Climate
        {
            get
            {
                return (int)Variables[Variable.Climate];
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.Climate))
                        GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Climate));
                Variables[Variable.Climate] = value;
            }
        }
        public int Winter
        {
            get
            {
                return (int)Variables[Variable.Climate];
            }
            set
            {
                Variables[Variable.Climate] = value;
                if (GlobalVariables.FullyLoaded)
                    if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.Climate))
                        GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Climate));
            }
        }
        public int Monsoon
        {
            get
            {
                return (int)Variables[Variable.Monsoon];
            }
            set
            {
                Variables[Variable.Monsoon] = value;
                if (GlobalVariables.FullyLoaded)
                    if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.Climate))
                        GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Climate));
            }
        }
        public int Impassable
        {
            get
            {
                return (int)Variables[Variable.Impassable];
            }
            set
            {
                Variables[Variable.Impassable] = value;
                if (GlobalVariables.FullyLoaded)
                    if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.Climate))
                        GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Climate));
            }
        }

        public int Terrain
        {
            get
            {
                return (int)Variables[Variable.Terrain];
            }
            set
            {
                Variables[Variable.Terrain] = value;
                if (GlobalVariables.FullyLoaded)
                    if (!GlobalVariables.Saves.Any(x => x is Saving.SpecialSavingObject && ((Saving.SpecialSavingObject)x)?.Type == Saving.SpecialSavingObject.SavingType.Terrain))
                        GlobalVariables.Saves.Add(new Saving.SpecialSavingObject(Saving.SpecialSavingObject.SavingType.Terrain));
            }
        }


        private List<string> DiscoveredBy
        {

            get
            {
                List<string> toreturn = new List<string>(Variables[Variable.DiscoveredBy] as List<string>);
                foreach (ProvinceDateEntry pde in DateEntries)
                {
                    if (DateTime.Compare(pde.Date, GlobalVariables.CurrentDate) <= 0)
                    {
                        foreach (ProvinceDateEntry.Entry entry in pde.Entries)
                        {
                            if (entry.Type == Variable.DiscoveredByAdd)
                            {
                                toreturn.Add(entry.Value as string);
                            }
                            else if (entry.Type == Variable.DiscoveredByRemove)
                            {
                                toreturn.Remove(entry.Value as string);
                            }
                        }
                    }
                }
                return toreturn;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "DiscoveredBy", Variables[Variable.DiscoveredBy], value as List<string>));
                Variables[Variable.DiscoveredBy] = value;
            }
        }
        public void AddDiscoveredBy(string Tech, DateTime date, bool noChange = false)
        {
            if (!DiscoveredBy.Contains(Tech))
            {
                if (DateTime.Compare(date, GlobalVariables.StartDate) == 0)
                {
                    var b = GetDiscoveredBy().ToList();
                    b.Add(Tech);
                    DiscoveredBy = b;
                    if (!noChange)
                        GlobalVariables.Changes.Add(new VariableChange(this, "DiscoveredBy", null, Tech));
                }
                else
                {
                    ProvinceDateEntry pde = DateEntries.Find(x => DateTime.Compare(x.Date, date) == 0);
                    if (pde == null)
                    {
                        pde = new ProvinceDateEntry(date, this);
                        //AddDateEntry(pde);
                    }
                    ProvinceDateEntry.Entry entry = pde.Entries.Find(x => x.Type == Variable.DiscoveredByAdd && (string)x.Value == Tech);
                    if (entry == null)
                    {
                        pde.AddDateEntry(Variable.DiscoveredByAdd, Tech);
                    }
                }
            }
        }
        public void RemoveDiscoveredBy(string Tech, DateTime date, bool noChange = false)
        {
            if (DiscoveredBy.Contains(Tech))
            {
                if (DateTime.Compare(date, GlobalVariables.StartDate) == 0)
                {
                    var b = GetDiscoveredBy().ToList();
                    b.Remove(Tech);
                    DiscoveredBy = b;
                    if (!noChange)
                        GlobalVariables.Changes.Add(new VariableChange(this, "DiscoveredBy", Tech, null));
                }
                else
                {
                    ProvinceDateEntry pde = DateEntries.Find(x => DateTime.Compare(x.Date, date) == 0);
                    if (pde == null)
                    {
                        pde = new ProvinceDateEntry(date, this);
                        //AddDateEntry(pde);
                    }
                    ProvinceDateEntry.Entry entry = pde.Entries.Find(x => x.Type == Variable.DiscoveredByRemove && (string)x.Value == Tech);
                    if (entry == null)
                    {
                        entry = pde.AddDateEntry(Variable.DiscoveredByRemove, Tech);
                    }
                }

            }
        }
        public string[] GetDiscoveredBy()
        {
            return DiscoveredBy.ToArray();
        }

        public int FortLevel()
        {
            int FortLevel = 0;
            if(OwnerCountry != null)
            {
                if (OwnerCountry.Capital == this)
                    FortLevel += 1;
            }
            for(int a = 0; a < GlobalVariables.FortBuildings.Length; a++)
            {
                if (Buildings.Find(x => x.Name == GlobalVariables.FortBuildings[a].Item1) != null)
                    FortLevel += GlobalVariables.FortBuildings[a].Item2;
            }
           return FortLevel;
        }

        public enum Variable { Cores, CoresAdd, CoresRemove, Claims, ClaimsAdd, ClaimsRemove, OwnerCountry, Controller,
        Tax, Production, Manpower, Capital, CenterOfTrade, HRE, TradeGood, LatentTradeGood, TradeNode, Religion, Culture,
        Area, Continent, DiscoveredBy, DiscoveredByAdd, DiscoveredByRemove, City, Buildings, BuildingAdd, BuildingRemove, TradeCompany, Winter, Climate, Terrain,
        Impassable, Monsoon, Revolt, AddLocalAutonomy, Unrest, TribalOwner, NativeSize, NativeFerocity, NativeHostileness,
        ExtraCost, SeatInParliament, AddPermanentProvinceModifier, ReformationCenter, RemoveProvinceModifier, AddProvinceTriggeredModifier,
        AddToTradeCompany, AddTradeCompanyInvestement
        }

        public Province(int ID, int R, int G, int B, string DefinitionName = "")
        {
            this.ID = ID;
            this.R = R;
            this.G = G;
            this.B = B;
            this.DefinitionName = DefinitionName;
            c = Color.FromArgb(R, G, B);
            Variables.Add(Variable.Cores, new List<string>());
            Variables.Add(Variable.Claims, new List<string>());
            Variables.Add(Variable.OwnerCountry, null);
            Variables.Add(Variable.Controller, null);
            Variables.Add(Variable.Tax, 0);
            Variables.Add(Variable.Production, 0);
            Variables.Add(Variable.Manpower, 0);
            Variables.Add(Variable.Capital, "");
            Variables.Add(Variable.CenterOfTrade, 0);
            Variables.Add(Variable.HRE, false);
            Variables.Add(Variable.TradeGood, TradeGood.nothing);
            Variables.Add(Variable.LatentTradeGood, null);
            Variables.Add(Variable.TradeNode, null);
            Variables.Add(Variable.Religion, Religion.NoReligion);
            Variables.Add(Variable.Culture, Culture.NoCulture);
            Variables.Add(Variable.Area, null);
            Variables.Add(Variable.Continent, null);
            Variables.Add(Variable.DiscoveredBy, new List<string>());
            Variables.Add(Variable.City, false);
            Variables.Add(Variable.Buildings, new List<Building>());
            Variables.Add(Variable.TradeCompany, null);
            Variables.Add(Variable.Winter, 0);
            Variables.Add(Variable.Climate, 0);
            Variables.Add(Variable.Terrain, null);
            Variables.Add(Variable.Impassable, 0);
            Variables.Add(Variable.Monsoon, 0);
        }
    }
}
