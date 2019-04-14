using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSimulator {
    public class Randop : Operation {
        public Randop() : base("RAND", 1, "10") {
        }
        public override void Run(string[] Params) {
            
        }
    }
}
