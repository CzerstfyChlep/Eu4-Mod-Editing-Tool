using System.Collections.Generic;

namespace Eu4ModEditor
{
    public class Building
    {
        //TODO
        //Add Cost, Time, Modifier, AllowInGoldProvinces
        //InfluencingFort, GovernmentSpecific, OnMap, OnePerCountry
        //Add on_built, on_destroyed, on_obsolete, build_trigger,
        //keep trigger and ai_will_do only to pass them

        public string Name = "";
        public NodeFile NodeFile;
        
        public override string ToString()
        {
            return Name;
        }
    }
}
