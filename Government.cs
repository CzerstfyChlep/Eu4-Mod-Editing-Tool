using System;
using System.Collections.Generic;
using System.Drawing;

namespace Eu4ModEditor
{
    public class Government
    {
        public string Type = "";
        public List<string> reforms = new List<string>();
        public Color Color;
        public Government(string type)
        {
            Type = type;
        }
        public override string ToString()
        {
            return Type;
        }
    }
}
