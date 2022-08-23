using System.Collections.Generic;
using System.Drawing;

namespace Eu4ModEditor
{
    public class Area
    {
        public Area(string name)
        {
            Name = name;
            GlobalVariables.Areas.Add(this);
            Color = Color.FromArgb(GlobalVariables.GlobalRandom.Next(10, 245), GlobalVariables.GlobalRandom.Next(10, 245), GlobalVariables.GlobalRandom.Next(10, 245));
        }
        public Area(string name, List<Province> provinces)
        {
            Name = name;
            Provinces.AddRange(provinces);
            Color = Color.FromArgb(GlobalVariables.GlobalRandom.Next(10, 245), GlobalVariables.GlobalRandom.Next(10, 245), GlobalVariables.GlobalRandom.Next(10, 245));
            GlobalVariables.Areas.Add(this);
        }
        public List<Province> Provinces = new List<Province>();
        public string Name = "";
        public Color Color;
        public Region Region;
        public override string ToString()
        {
            return Name;
        }
    }
}
