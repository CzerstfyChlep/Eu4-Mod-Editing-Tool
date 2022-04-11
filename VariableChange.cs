using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public class VariableChange
    {
        public object Object;
        public string VariableName = "";
        public object PreviousValue;
        public object CurrentValue;
        public VariableChange(object obj, string name, object prev, object cur)
        {
            Object = obj;
            VariableName = name;
            PreviousValue = prev;
            CurrentValue = cur;
        }
    }
}
