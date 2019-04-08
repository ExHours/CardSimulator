using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSimulator {
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class Card {
        public string Name { get; set; }
        public string RareDegree { get; set; }
        public int Value { get; set; }
        public string imagename { get; set; }

        public Card() { Value = 0; }
        public Card(string Name, string RareDegree, int value, string imagename) {
            this.Name = Name;
            this.RareDegree = RareDegree;
            this.Value = value;
            this.imagename = imagename;
        }
    }
}
