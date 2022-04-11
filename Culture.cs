using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public class CultureGroup
    {
        public List<Culture> Cultures = new List<Culture>();
        public string Name = "";
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
            Random ra = new Random();
            int r, g, b;
            do
            {
                r = ra.Next(0, 256);
                g = ra.Next(0, 256);
                b = ra.Next(0, 256);
            } while (r + g + b < 100 && r + g + b > 240 * 3);
            return Color.FromArgb(r, g, b);
        }
    }
}
