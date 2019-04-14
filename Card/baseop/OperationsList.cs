using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSimulator {
    public class OperationsList {
        public int Length {
            get {
                return opList.Count + drList.Count;
            }
        }
        public OperationsList() {
            opList.Add(new Randop());
        }
        public List<string> Search(string commandline) {
            List<string> ans = new List<string>();
            //if (Isop) {
            //    foreach (var item in opList) {
            //        if (item.Name.IndexOf(opname) != -1) {
            //            ans.Add(item.Name);
            //        }
            //    }
            //} else {


            //}
            return ans;
        }
        public string RunOp(string opName, string[] Params) {
            opList[1].Run(Params);
            return "";
        }
        List<Operation> opList = new List<Operation>();
        List<string> drList = new List<string>();
    }
}
