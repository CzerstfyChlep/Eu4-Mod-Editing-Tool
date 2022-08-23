using System.Collections.Generic;
using System.Drawing;

namespace Eu4ModEditor
{
    public class CultureGroup
    {
        public List<Culture> Cultures = new List<Culture>();
        public string Name = "";
        public override string ToString()
        {
            return Name;
        }
    }

    public class Culture
    {

        public CultureGroup Group;
        public string PrimaryTag = "";
        public string Name = "";
        public Color Color;
        public Culture()
        {
            Cultures.Add(this);
            Color = GenerateNiceColor();
        }
        public Culture(Color c)
        {
            Cultures.Add(this);
            Color = c;
        }
        public static List<Culture> Cultures = new List<Culture>();

        public static Culture NoCulture = new Culture(Color.White)
        {
            Name = "NoCulture",
        };

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
        public override string ToString()
        {
            return Name;
        }
    }
}
