using System.Collections.Generic;

namespace Eu4ModEditor
{
    public class Building
    {
        public string Name = "";
        public NodeFile NodeFile;
        
        public override string ToString()
        {
            return Name;
        }
    }
}
