using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSimulator {
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class RarePool {
        public int RarePoolTotalValue = 0;
        public List<RareEnum> list { get; set; }

        public void AddRareItem(RareEnum Re) {
            list.Add(Re);
        }
        public int length {
            get {
                return list.Count;
            }
        }
        public RarePool() {
            list = new List<RareEnum>();
        }
        public RarePool(string CardPoolName, params RareEnum[] Re) {
            list = new List<RareEnum>();
            for (int i = 0; i < Re.Length; i++) {
                list.Add(new RareEnum(Re[i].Name, Re[i].Value));
            }
        }

        public RareEnum this[int index] {
            get {
                return list[index];
            }
            set {
                list[index] = value;
            }
        }
    }
}
