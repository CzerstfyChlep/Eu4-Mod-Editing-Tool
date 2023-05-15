using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{

    public class ProvinceVariableChange : VariableChange
    {
        public Province.Variable ProvinceVariable;

        public ProvinceVariableChange(object obj, Province.Variable name, object prev, object cur) : base(obj, prev, cur)
        {
            ProvinceVariable = name;
        }

        public override int GetEnumInt()
        {
            return (int)ProvinceVariable;
        }
        public override string GetVariableName()
        {
            return ProvinceVariable.ToString();
        }

    }
    public class CountryVariableChange : VariableChange
    {
        public Country.Variable CountryVariable;
        public CountryVariableChange(object obj, Country.Variable name, object prev, object cur) : base(obj, prev, cur)
        {
            CountryVariable = name;
        }

        public override int GetEnumInt()
        {
            return (int)CountryVariable;
        }
        public override string GetVariableName()
        {
            return CountryVariable.ToString();
        }
    }


    public class VariableChange
    {
        public object Object;
        public object PreviousValue;
        public object CurrentValue;
        public NonVariableChange SpecialChange;

        public VariableChange(object obj, object prev, object cur)
        {
            Object = obj;
            PreviousValue = prev;
            CurrentValue = cur;
        }
        public VariableChange(object obj, NonVariableChange name, object prev, object cur)
        {
            Object = obj;
            PreviousValue = prev;
            CurrentValue = cur;
        }
        public VariableChange(NonVariableChange sp)
        {
            SpecialChange = sp;
        }

        public virtual int GetEnumInt()
        {
            return (int)SpecialChange;
        }

        public virtual string GetVariableName()
        {
            return SpecialChange.ToString();
        }

        public enum NonVariableChange { Area = 200, OwnerTag, ControllerTag, DiscoveredByTag, CoreTag, ClaimTag}
    }
}
