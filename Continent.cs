using System.Collections.Generic;
using System.Drawing;

namespace Eu4ModEditor
{
    public class Continent
    {
        public Continent(string name)
        {
            Name = name;
            GlobalVariables.Continents.Add(this);
            Color = Color.FromArgb(GlobalVariables.GlobalRandom.Next(10, 245), GlobalVariables.GlobalRandom.Next(10, 245), GlobalVariables.GlobalRandom.Next(10, 245));
        }
        public Continent(string name, List<Province> provinces)
        {
            Name = name;
            Provinces.AddRange(provinces);
            Color = Color.FromArgb(GlobalVariables.GlobalRandom.Next(10, 245), GlobalVariables.GlobalRandom.Next(10, 245), GlobalVariables.GlobalRandom.Next(10, 245));
            GlobalVariables.Continents.Add(this);
        }
        public List<Province> Provinces = new List<Province>();
        public string Name = "";
        public string OriginalName = "";
        public Color Color;
        public override string ToString()
        {
            return Name;
        }
    }
}
