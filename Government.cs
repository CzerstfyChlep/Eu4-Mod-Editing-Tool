using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public class Government
    {
        public string Type = "";
        public List<string> reforms = new List<string>();
        public Government(string type)
        {
            Type = type;
        }
    }
}
