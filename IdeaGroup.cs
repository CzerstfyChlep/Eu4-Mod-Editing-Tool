﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public class IdeaGroup
    {
        public List<string> allowedTags = new List<string>();
        public List<Modifier>[] Slots = new List<Modifier>[7];
        public Modifier Ambition;
        public Modifier[] Traditions = new Modifier[2];
    }

    public class Modifier
    {
        string Name = "";
        double Value = 0;
        public Modifier(string name, double val)
        {
            Name = name;
            Value = val;
        }
    }
}
