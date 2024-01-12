using System.Collections.Generic;
using System.Drawing;

namespace Eu4ModEditor
{
    public class Continent
    {
        /// <summary>
        /// Adds to GlobalVariables.Continents and generates a random colour
        /// </summary>
        /// <param name="name"></param>
        public Continent(string name)
        {
            Name = name;
            Continents.Add(this);
            Color = Color.FromArgb(GlobalVariables.GlobalRandom.Next(10, 245), GlobalVariables.GlobalRandom.Next(10, 245), GlobalVariables.GlobalRandom.Next(10, 245));
        }
        /// <summary>
        /// Adds to GlobalVariables.Continents and generates a random colour but DOESN'T set provinces' continent
        /// </summary>
        /// <param name="name"></param>
        /// <param name="provinces"></param>
        public Continent(string name, List<Province> provinces)
        {
            Name = name;
            Provinces.AddRange(provinces);
            Color = Color.FromArgb(GlobalVariables.GlobalRandom.Next(10, 245), GlobalVariables.GlobalRandom.Next(10, 245), GlobalVariables.GlobalRandom.Next(10, 245));
            Continents.Add(this);
        }
        public List<Province> Provinces = new List<Province>();
        public string Name = "";
        public string OriginalName = "";
        public Color Color;
        public override string ToString()
        {
            return Name;
        }

        public static List<Continent> Continents = new List<Continent>();
        public static Continent NoContinent = new Continent("");
    }
}
