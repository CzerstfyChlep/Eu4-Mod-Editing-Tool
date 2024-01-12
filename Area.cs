using System.Collections.Generic;
using System.Drawing;

namespace Eu4ModEditor
{
    public class Area
    {
        //TODO
        //Areas can have defined colour!
        //Colours should be made "static" (not changing between reloads)

        /// <summary>
        /// Adds the area to GlobalVariables.Areas and generates random colour
        /// </summary>
        /// <param name="name"></param>
        public Area(string name)
        {
            Name = name;
            Areas.Add(this);
            Color = Color.FromArgb(GlobalVariables.GlobalRandom.Next(10, 245), GlobalVariables.GlobalRandom.Next(10, 245), GlobalVariables.GlobalRandom.Next(10, 245));
        }
        /// <summary>
        /// Adds the area to GlobalVariables.Areas but DOESN'T set provinces' area!
        /// </summary>
        /// <param name="name"></param>
        /// <param name="provinces"></param>
        public Area(string name, List<Province> provinces)
        {
            Name = name;
            Provinces.AddRange(provinces);
            Color = Color.FromArgb(GlobalVariables.GlobalRandom.Next(10, 245), GlobalVariables.GlobalRandom.Next(10, 245), GlobalVariables.GlobalRandom.Next(10, 245));
            Areas.Add(this);
        }
        public List<Province> Provinces = new List<Province>();
        public string Name = "";
        public string OriginalName = "";
        public Color Color;
        public Region Region = Region.NoRegion;
        public override string ToString()
        {
            return Name;
        }

        public static List<Area> Areas = new List<Area>();
        public static Area NoArea = new Area("");
    }
}
