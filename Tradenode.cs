using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public class Tradenode
    {
        public Province Location;
        public Color Color;
        public bool Inland;
        public List<Destination> Destination = new List<Destination>();
        public List<Province> Provinces = new List<Province>();
        public List<Tradenode> Incoming = new List<Tradenode>();
        public string Name;
        public bool Endnode;
        public NodeFile NodeFile;
        public override string ToString()
        {
            return Name;
        }
    }
    public class Destination
    {
        public Tradenode TradeNode;
        public List<string> Path = new List<string>();
        public List<string> Control = new List<string>();
    }
}
