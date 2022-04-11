using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public class Religion
    {
        public Color Color;
        public int Icon = 0;
        public string Name = "";
        public string ReadableName = "";
        public ReligionGroup Group;
        public Religion()
        {
            Religions.Add(this);            
        }

        public static List<Religion> Religions = new List<Religion>();

        public static Religion NoReligion = new Religion()
        {
            Color = Color.White,
            Name = "NoReligion"
        };
    }

    public class ReligionGroup
    {
        public string Name = "";
        public List<Religion> Religions = new List<Religion>();
    }
}
