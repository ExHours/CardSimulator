using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSimulator {
    //public enum RareEnum {
    //    N = 0,
    //    R = 1,
    //    SR = 2,
    //    SSR = 3,
    //    UR = 4
    //}
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class RareEnum {
        public string Name { get; set; }
        public int Value { get; set; }
        public RareEnum() {
        }
        public RareEnum(string name, int defaultValue) {
            Name = name;
            Value = defaultValue;
        }
    }


}
