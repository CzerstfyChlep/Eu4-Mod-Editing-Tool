using System.Collections.Generic;
using System.Drawing;


namespace Eu4ModEditor
{
    public class Country
    {
        public string Tag = "";
        public string FullName = "";
        public Color Color = Color.Black;
        public List<Province> Provinces = new List<Province>();
        public Province Capital;

        public static Country NoCountry = new Country();

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
        public string HistoryFile;
        public bool HistoryFileGame = false;
        public string CommonFile;
        public bool CommonFileGame = false;
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

        public override string ToString()
        {
            if (FullName != "")
                return FullName + ", " + Tag;
            else
                return "";
        }

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
        }

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
}
