﻿using System;
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
        public Province.Variable ProvinceVariable;
        public object PreviousValue;
        public object CurrentValue;
        public NonVariableChange SpecialChange;

        public VariableChange(object obj, Province.Variable name, object prev, object cur)
        {
            Object = obj;
            ProvinceVariable = name;
            VariableName = ProvinceVariable.ToString();
            PreviousValue = prev;
            CurrentValue = cur;
        }
        public VariableChange(object obj, string name, object prev, object cur)
        {
            Object = obj;
            VariableName = name;
            PreviousValue = prev;
            CurrentValue = cur;
        }
        public VariableChange(NonVariableChange sp)
        {
            SpecialChange = sp;
        }

        public enum NonVariableChange { Area }
    }
}
