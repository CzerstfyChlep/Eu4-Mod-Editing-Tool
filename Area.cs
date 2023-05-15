﻿using System.Collections.Generic;
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
            GlobalVariables.Areas.Add(this);
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
            GlobalVariables.Areas.Add(this);
        }
        public List<Province> Provinces = new List<Province>();
        public string Name = "";
        public string OriginalName = "";
        public Color Color;
        public Region Region;
        public override string ToString()
        {
            return Name;
        }

        public static bool IsValid(string Name)
        {
            return GlobalVariables.Areas.Exists(x=>x.Name == Name);
        }
    }
}
