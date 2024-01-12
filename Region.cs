using System.Collections.Generic;
using System.Drawing;

namespace Eu4ModEditor
{
    public class Region
    {
        public Region(string name)
        {
            Name = name;
            Region.Regions.Add(this);
            Color = Color.FromArgb(GlobalVariables.GlobalRandom.Next(10, 245), GlobalVariables.GlobalRandom.Next(10, 245), GlobalVariables.GlobalRandom.Next(10, 245));
        }
        public Region(string name, List<Area> areas)
        {
            Name = name;
            Areas.AddRange(areas);
            Color = Color.FromArgb(GlobalVariables.GlobalRandom.Next(10, 245), GlobalVariables.GlobalRandom.Next(10, 245), GlobalVariables.GlobalRandom.Next(10, 245));
            Region.Regions.Add(this);
        }
        public List<Area> Areas = new List<Area>();
        public Superregion Superregion = Superregion.NoSuperregion;



        public string Name = "";
        public string OriginalName = "";
        public Color Color;
        public override string ToString()
        {
            return Name;
        }

        public static List<Region> Regions = new List<Region>();
        public static Region NoRegion = new Region("");
    }
}
