using System.Collections.Generic;


namespace Eu4ModEditor
{
    /// <summary>
    /// Not fully implemented yet.
    /// </summary>
    public class CountryModifier
    {
        public string Name = "";
        public string Type = "";
        public int Value = 0;
        public double MinValue = 0;
        public double MaxValue = 1;
        public bool Integer = false;
        public CountryModifier(string name, string type, int value, double minvalue, double maxvalue, bool integer)
        {
            Name = name;
            Type = type;
            Value = value;
            MinValue = minvalue;
            MaxValue = maxvalue;
            Integer = integer;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
