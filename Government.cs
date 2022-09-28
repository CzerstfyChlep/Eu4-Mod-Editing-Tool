using System;
using System.Collections.Generic;
using System.Drawing;

namespace Eu4ModEditor
{
    public class Government
    {
        /// <summary>
        /// Type of government (monarchy, republic, theocracy, tribal, native).
        /// </summary>
        public string Type = "";
        /// <summary>
        /// List of all first reforms availbable to this government.
        /// </summary>
        public List<string> reforms = new List<string>();
        /// <summary>
        /// Color of a government read from files.
        /// </summary>
        public Color Color;
        public Government(string type)
        {
            Type = type;
        }
        /// <summary>
        /// Returns the name of a government.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Type;
        }
    }
}
