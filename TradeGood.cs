using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public class TradeGood
    {
        public int ID = 0;
        public string Name = "";
        public string ReadableName = "";
        public Color Color;

        public double Price = 1;
        public bool GoldLike = false;

        public int TotalProvinces = 0;
        public int TotalDev = 0;

        public static TradeGood nothing = new TradeGood()
        {
            Name = "nothing",
            ReadableName = "Nothing",
            Color = Color.DarkRed
        };

        public override string ToString()
        {
            return ReadableName;
        }
    }
}
