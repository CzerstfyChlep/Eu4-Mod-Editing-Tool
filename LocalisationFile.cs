using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public class LocalisationFile
    {
        public OrderedDictionary Localisation = new OrderedDictionary();
        public string Filename = "";
        public bool Changed = false;

        public bool GetLocalised(string key, out string output)
        {
            if(Localisation.Contains(key))
            {
                output = (string)Localisation[key];
                return true;
            }
            output = "";
            return false;
        }

        public bool TryChange(string key, string value)
        {
            if(!Localisation.Contains(key))
            {
                return false;
            }
            if ((string)Localisation[key] != value)
            {
                Localisation[key] = value;
                Changed = true;
            }
            return true;
        }

        public void AddNew(string key, string value)
        {
            Localisation.Add(key, value);
            Changed = true;
        }
    }
}
