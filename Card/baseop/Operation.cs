using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSimulator {
    public abstract class Operation {
        public Operation() { }
        public Operation(string name, int parmsnums, string serachpos) {
            Name = name;
            ParmsNums = parmsnums;
            SearchPos = serachpos;
        }
        public string Name { get; }
        public int ParmsNums { get; }
        public string SearchPos { get; }
        public abstract string Run(string[] Params);

    }
}
