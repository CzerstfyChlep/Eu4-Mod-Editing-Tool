using System.Collections.Generic;
using System.Drawing;

namespace Eu4ModEditor
{
    public class CultureGroup
    {
        /// <summary>
        /// List of all cultures in this group.
        /// </summary>
        public List<Culture> Cultures = new List<Culture>();
        /// <summary>
        /// Currently not editable name.
        /// </summary>
        public string Name = "";
        /// <summary>
        /// Returns the name of a culture group.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }


        //TODO
        //implement that
        public string graphical_culture = "";
        public List<string> male_names = new List<string>();
        public List<string> female_names = new List<string>();
        public List<string> dynasty_names = new List<string>();
    }

    public class Culture
    {
        //TODO
        //implement that
        public string graphical_culture = "";
        public List<string> male_names = new List<string>();
        public List<string> female_names = new List<string>();
        public List<string> dynasty_names = new List<string>();
        //to that

        /// <summary>
        /// Culture group a culture belongs in. Should be only in one!
        /// </summary>
        public CultureGroup Group;
        //TODO 
        //change it to country OR string if not found.
        /// <summary>
        /// Primary tag of a culture.
        /// </summary>
        public string PrimaryTag = "";
        /// <summary>
        /// Name of a culture.
        /// </summary>
        public string Name = "";
        /// <summary>
        /// Colour of a culture. Generated internally.
        /// </summary>
        public Color Color;
        /// <summary>
        /// File a culture is saved in and reads from.
        /// </summary>
        public NodeFile NodeFile;
        /// <summary>
        /// Adds a culture to it's internal list of Cultures. Generates a colour.
        /// </summary>
        public Culture()
        {
            Cultures.Add(this);
            Color = GenerateNiceColor();
        }
        /// <summary>
        /// Adds a culture to it's internal list of Cultures. Generates a colour.
        /// </summary>
        /// <param name="c"></param>
        public Culture(Color c)
        {
            Cultures.Add(this);
            Color = c;
        }
        /// <summary>
        /// All cultures in the editor
        /// </summary>
        public static List<Culture> Cultures = new List<Culture>();

        /// <summary>
        /// Replacement for null.
        /// </summary>
        public static Culture NoCulture = new Culture(Color.White)
        {
            Name = "",
        };
        /// <summary>
        /// Generates a nice colour for your eyes.
        /// </summary>
        /// <returns></returns>
        public static Color GenerateNiceColor()
        {           
            int r, g, b;
            do
            {
                r = GlobalVariables.GlobalRandom.Next(0, 256);
                g = GlobalVariables.GlobalRandom.Next(0, 256);
                b = GlobalVariables.GlobalRandom.Next(0, 256);
            } while (r + g + b < 100 && r + g + b > 240 * 3);
            return Color.FromArgb(r, g, b);
        }
        /// <summary>
        /// Returns the name of a culture.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
