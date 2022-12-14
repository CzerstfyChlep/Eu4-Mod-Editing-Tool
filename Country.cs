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
                return Variables["Government"] as Government;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "Government", Variables["Government"], value));
                Variables["Government"] = value;
            }
        }
        /// <summary>
        /// First government reform of a country. Uses internal variables.
        /// </summary>
        public string GovernmentReform
        {
            get
            {
                return Variables["GovernmentReform"] as string;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "GovernmentReform", Variables["GovernmentReform"], value));
                Variables["GovernmentReform"] = value;
            }
        }
        /// <summary>
        /// Government rank of a country. Uses internal variables.
        /// </summary>
        public int GovernmentRank
        {
            get
            {
                return (int)Variables["GovernmentRank"];
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "GovernmentRank", Variables["GovernmentRank"], value));
                Variables["GovernmentRank"] = value;
            }
        }

        /// <summary>
        /// Primary culture of a country. Uses internal variables.
        /// </summary>
        public Culture PrimaryCulture
        {
            get
            {
                return Variables["PrimaryCulture"] as Culture;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "PrimaryCulture", Variables["PrimaryCulture"], value));
                Variables["PrimaryCulture"] = value;
            }
        }
        /// <summary>
        /// Technology group of a country. Uses internal variables.
        /// </summary>
        public string TechnologyGroup
        {
            get
            {
                return Variables["TechnologyGroup"] as string;
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "TechnologyGroup", Variables["TechnologyGroup"], value));
                Variables["TechnologyGroup"] = value;
            }
        }
        /// <summary>
        /// Religion of a country. Uses internal variables.
        /// </summary>
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
                return (int)Variables["CapitalID"];
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "CapitalID", Variables["CapitalID"], value));
                Variables["CapitalID"] = value;
            }
        }


        /// <summary>
        /// Graphical culture of country. Uses internal variables. Currently only static set.
        /// </summary>
        public string GraphicalCulture
        {
            get
            {
                return (string)Variables["GraphicalCulture"];
            }
            set
            {
                if (GlobalVariables.FullyLoaded)
                    GlobalVariables.Changes.Add(new VariableChange(this, "GraphicalCulture", Variables["GraphicalCulture"], value));
                Variables["GraphicalCulture"] = value;
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
        public Dictionary<string, object> Variables = new Dictionary<string, object>();

        public Country()
        {
            Variables.Add("CapitalID", 0);
            Variables.Add("Religion", Religion.NoReligion);
            Variables.Add("TechnologyGroup", Religion.NoReligion);
            Variables.Add("PrimaryCulture", Culture.NoCulture);
            Variables.Add("Government", "");
            Variables.Add("GovernmentReform", "");
            Variables.Add("GovernmentRank", 1);
            Variables.Add("GraphicalCulture", "westerngfx");
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
