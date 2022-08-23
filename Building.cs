using System.Collections.Generic;

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
