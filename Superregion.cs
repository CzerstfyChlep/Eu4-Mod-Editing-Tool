﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public class Superregion
    {
        public string Name = "";
        public List<Region> Regions = new List<Region>();
        public Color Color;
        public Superregion(string name, List<Region> list)
        {
            GlobalVariables.Superregions.Add(this);
            Name = name;
            Regions.AddRange(list);
            Color = AdditionalElements.GenerateColor(GlobalVariables.GlobalRandom);
        }
        public Superregion(string name)
        {
            GlobalVariables.Superregions.Add(this);
            Name = name;
            Color = AdditionalElements.GenerateColor(GlobalVariables.GlobalRandom);
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
