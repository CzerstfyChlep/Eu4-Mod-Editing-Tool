using System.Collections.Generic;
using System.Drawing;
using System;

namespace Eu4ModEditor
{
    public class Country
    {
        /// <summary>
        /// The tag currently in editor
        /// </summary>
        public string Tag = "";
        /// <summary>
        /// The tag last saved in country's files
        /// </summary>
        public string OriginalTag = "";
        /// <summary>
        /// The name currently in editor
        /// </summary>
        public string FullName = "";
        /// <summary>
        /// The name last saved in country's files
        /// </summary>
        public string OriginalFullName = "";

        /// <summary>
        /// Colour of a country set in common/countries
        /// </summary>
        public Color Color = Color.Black;
        /// <summary>
        /// All provinces belonging to a country
        /// </summary>
        public List<Province> Provinces = new List<Province>();

        public List<Province> GetProvincesInCurrentDate()
        {          
            if (DateTime.Compare(GlobalVariables.CurrentDate, GlobalVariables.StartDate) == 0)
                return Provinces;
            else
            {
                List<Province> ToReturn = new List<Province>();
                foreach (Province p in Provinces)
                {
                    if (p.OwnerCountry == this)
                        ToReturn.Add(p);
                }
                return ToReturn;
            }
        }

        /// <summary>
        /// Capital province of a country
        /// </summary>
        public Province Capital;

        /// <summary>
        /// Government of a country. Uses internal variables.
        /// </summary>
        public Government Government
        {
            get
            {
                return Variables[Variable.Government] as Government;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new CountryVariableChange(this, Variable.Government, Variables[Variable.Government], value));
                Variables[Variable.Government] = value;
            }
        }
        /// <summary>
        /// First government reform of a country. Uses internal variables.
        /// </summary>
        public string GovernmentReform
        {
            get
            {
                return Variables[Variable.GovernmentReform] as string;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new CountryVariableChange(this, Variable.GovernmentReform, Variables[Variable.GovernmentReform], value));
                Variables[Variable.GovernmentReform] = value;
            }
        }
        /// <summary>
        /// Government rank of a country. Uses internal variables.
        /// </summary>
        public int GovernmentRank
        {
            get
            {
                return (int)Variables[Variable.GovernmentRank];
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new CountryVariableChange(this, Variable.GovernmentRank, Variables[Variable.GovernmentRank], value));
                Variables[Variable.GovernmentRank] = value;
            }
        }

        /// <summary>
        /// Primary culture of a country. Uses internal variables.
        /// </summary>
        public Culture PrimaryCulture
        {
            get
            {
                return Variables[Variable.PrimaryCulture] as Culture;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new CountryVariableChange(this, Variable.PrimaryCulture, Variables[Variable.PrimaryCulture], value));
                Variables[Variable.PrimaryCulture] = value;
            }
        }
        /// <summary>
        /// Technology group of a country. Uses internal variables.
        /// </summary>
        public string TechnologyGroup
        {
            get
            {
                return Variables[Variable.TechnologyGroup] as string;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new CountryVariableChange(this, Variable.TechnologyGroup, Variables[Variable.TechnologyGroup], value));
                Variables[Variable.TechnologyGroup] = value;
            }
        }
        /// <summary>
        /// Religion of a country. Uses internal variables.
        /// </summary>
        public Religion Religion
        {
            get
            {
                return Variables[Variable.Religion] as Religion;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new CountryVariableChange(this, Variable.Religion, Variables[Variable.Religion], value));
                Variables[Variable.Religion] = value;
            }
        }

        /// <summary>
        /// History file currently used to read from and save to.
        /// </summary>
        public NodeFile HistoryFile;
        /// <summary>
        /// Common file currently used to read from and save to.
        /// </summary>
        public NodeFile CommonFile;
        /// <summary>
        /// Tags file that has this country's tag in it.
        /// </summary>
        public NodeFile CountryTagsFile;

        /// <summary>
        /// List of monarch names with chances and regal numbers.
        /// </summary>
        public List<MonarchName> MonarchNames = new List<MonarchName>();
        /// <summary>
        /// Simple list of leader names.
        /// </summary>
        public List<string> LeaderNames = new List<string>();
        /// <summary>
        /// Simple list of ship names.
        /// </summary>
        public List<string> ShipNames = new List<string>();
        /// <summary>
        /// Simple list of army names.
        /// </summary>
        public List<string> ArmyNames = new List<string>();
        /// <summary>
        /// Simple list of fleet names.
        /// </summary>
        public List<string> FleetNames = new List<string>();

        /// <summary>
        /// OBSOLETE!!!
        /// </summary>
        public int CapitalID
        {
            get
            {
                return (int)Variables[Variable.CapitalID];
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new CountryVariableChange(this, Variable.CapitalID, Variables[Variable.CapitalID], value));
                Variables[Variable.CapitalID] = value;
            }
        }


        /// <summary>
        /// Graphical culture of country. Uses internal variables. Currently only static set.
        /// </summary>
        public string GraphicalCulture
        {
            get
            {
                return (string)Variables[Variable.GraphicalCulture];
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new CountryVariableChange(this, Variable.GraphicalCulture, Variables[Variable.GraphicalCulture], value));
                Variables[Variable.GraphicalCulture] = value;
            }
        }

        //TODO
        //Either remove or fully implement NoCountry
        /// <summary>
        /// A replacement of null. Currently not fully implemented
        /// </summary>
        public static Country NoCountry = new Country();

        /// <summary>
        /// Returns the name of a country
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return FullName;
        }

        /// <summary>
        /// Internal variables.
        /// </summary>
        public Dictionary<Variable, object> Variables = new Dictionary<Variable, object>();

        public enum Variable
        {
            CapitalID = 100, Religion, TechnologyGroup, PrimaryCulture, Government, GovernmentReform, GovernmentRank, GraphicalCulture
        }

        public Country()
        {
            Variables.Add(Variable.CapitalID, 0);
            Variables.Add(Variable.Religion, Religion.NoReligion);
            Variables.Add(Variable.TechnologyGroup, "");
            Variables.Add(Variable.PrimaryCulture, Culture.NoCulture);
            Variables.Add(Variable.Government, "");
            Variables.Add(Variable.GovernmentReform, "");
            Variables.Add(Variable.GovernmentRank, 1);
            Variables.Add(Variable.GraphicalCulture, "westerngfx");
        }


        /// <summary>
        /// Returns total development of all provinces in it (dynamic).
        /// </summary>
        public int TotalDevelopment
        {
            get
            {
                int x = 0;
                foreach (Province province in Provinces)
                    x += province.Tax + province.Production + province.Manpower;
                return x;
            }
        }
    }

    public class MonarchName
    {
        public string Name = "";
        public int Chance = 0;
        public MonarchName(string name, int chance)
        {
            Name = name;
            Chance = chance;
        }
    }
}
