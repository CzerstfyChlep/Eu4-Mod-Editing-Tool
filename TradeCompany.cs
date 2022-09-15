using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Eu4ModEditor
{
    public class TradeCompany
    {
        public List<Province> Provinces = new List<Province>();
        public Color Color = new Color();
        public string Name = "";
        public List<string> Names = new List<string>();
        public bool MadeChanges = false;
        public NodeFile NodeFile;
        public TradeCompany()
        {
            Color = AdditionalElements.GenerateColor(GlobalVariables.GlobalRandom);
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
