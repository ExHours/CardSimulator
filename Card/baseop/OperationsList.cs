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
            opList.Add(new Randop2());
        }
        public List<string> Search(string[] comarr) {
            List<string> ans = new List<string>();
            //一个参数 搜索关键字
            if (comarr.Length == 1) {
                foreach (var item in opList) {
                    if (item.Name.ToUpper().IndexOf(comarr[0].ToUpper()) != -1) {
                        ans.Add(item.Name);
                        //Program.BackSpace(Program.commandline.Length);
                        //Program.commandline += item.Name;
                        //Console.Write(item.Name);
                    }
                }
            }
            //多个参数搜索参数内容
            return ans;
        }
        public string RunOp(string opName, string[] Params) {
            foreach (var item in opList) {
                if (item.Name.ToUpper() == opName.ToUpper()) {
                    return item.Run(Params);
                }
            }
            return "";
        }
        List<Operation> opList = new List<Operation>();
        List<string> drList = new List<string>();
    }
}
