using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public class TerrainType
    {
        string _name;
        Color _color;
        
        public string GetName() { return _name; }
        public Color GetColor() { return _color; }

        public void ReadFromNode(in Node n)
        {
            _name = n.Name.ToLower();
            Node colorNode;
            if(n.TryGetNode("color", out colorNode))
            {
                colorNode.TryGetColorFromNode(out _color);
            }
            
        }

    }
}
