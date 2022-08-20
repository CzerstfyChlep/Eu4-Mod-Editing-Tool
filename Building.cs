using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public class Building
    {
        public string Name = "";
        public string File = "";
        public bool GameFile = false;
        public override string ToString()
        {
            return Name;
        }
    }
}
