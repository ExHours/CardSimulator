using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSimulator {
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class CardPool {
        public string CardPoolName { get; set; }
        public int CardPoolTotalValue = 0;
        public List<Card> list { get; set; }
        public void AddCard(Card cd) {
            list.Add(cd);
        }
        public int length {
            get {
                return list.Count;
            }
        }
        public CardPool() {
            list = new List<Card>();
        }
        public CardPool(string CardPoolName, params Card[] Ca) {
            this.CardPoolName = CardPoolName;
            list = new List<Card>();
            for (int i = 0; i < Ca.Length; i++) {
                list.Add(new Card(Ca[i].ID, Ca[i].Name, Ca[i].RareDegree, Ca[i].Value, Ca[i].imagename));
            }
        }
        public Card this[int index] {
            get {
                return list[index];
            }
            set {
                list[index] = value;
            }
        }
    }
}
