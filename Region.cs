using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public class Region
    {
        public Region(string name)
        {
            Name = name;
            GlobalVariables.Regions.Add(this);
            Color = Color.FromArgb(GlobalVariables.GlobalRandom.Next(10, 245), GlobalVariables.GlobalRandom.Next(10, 245), GlobalVariables.GlobalRandom.Next(10, 245));
        }
        public Region(string name, List<Area> areas)
        {
            Name = name;
            Areas.AddRange(areas);
            Color = Color.FromArgb(GlobalVariables.GlobalRandom.Next(10, 245), GlobalVariables.GlobalRandom.Next(10, 245), GlobalVariables.GlobalRandom.Next(10, 245));
            GlobalVariables.Regions.Add(this);
        }
        public List<Area> Areas = new List<Area>();
        public string Name = "";
        public Color Color;
    }
}
