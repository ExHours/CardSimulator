using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace CardSimulator {
    partial class Program {
        static void SaveRareBasicInfo() {
            RarePool.RarePoolTotalValue = 100;
            RarePool.AddRareItem(new RareEnum("N", 30));
            RarePool.AddRareItem(new RareEnum("R", 60));
            RarePool.AddRareItem(new RareEnum("SR", 9));
            RarePool.AddRareItem(new RareEnum("SSR", 1));
            using (StreamWriter sw = new StreamWriter(loadFloder + "RarePool.xml", false, Encoding.Unicode)) {
                sw.WriteLine(XmlSerialize(RarePool));
                sw.Flush();
                sw.Close();
            }
        }
        static Card[] RandomCards(int times = 10, params string[] rares) {
            var a = new RarePool();
            var cardpools = new CardPool[rares.Length];
            for (int i = 0; i < rares.Length; i++) {
                for (int j = 0; j < RarePool.length; j++) {
                    if (rares[i] == RarePool[j].Name) {
                        a.AddRareItem(new RareEnum(RarePool[j].Name, RarePool[j].Value));
                        cardpools[i] = LoadCardPool(RarePool[j].Name);
                    }
                }
            }
            return Supervisor.RandCardsFromPools(a, times, null, cardpools);
        }
        static Card[] RandomAllCards(int times = 10) {
            var cardpools = new CardPool[RarePool.length];
            for (int j = 0; j < RarePool.length; j++) {
                cardpools[j] = LoadCardPool(RarePool[j].Name);
            }
            return Supervisor.RandCardsFromPools(RarePool, times, null, cardpools);
        }
        static Card[] RandomAllCardsWithSafe(int times = 10) {
            var cardpools = new CardPool[RarePool.length];
            for (int j = 0; j < RarePool.length; j++) {
                cardpools[j] = LoadCardPool(RarePool[j].Name);
            }
            var rarenums = Supervisor.RarePoolToRareNum(RarePool);
            var cards = Supervisor.RandCardsFromPools(RarePool, times, rarenums, cardpools);
            int a = 0;
            for (int i = 0; i < rarenums.Length; i++) {
                if (rarenums[i].Name == "SR" || rarenums[i].Name == "SSR") {
                    a += rarenums[i].Value;
                }
            }
            if (a == 0) cards[times - 1] = RandomCards(1, "SR", "SSR")[0];
            return cards;
        }
        static Card[] RandomAllCardsWithSafe2(int times = 10) {
            var cardpools = new CardPool[RarePool.length];
            for (int j = 0; j < RarePool.length; j++) {
                cardpools[j] = LoadCardPool(RarePool[j].Name);
            }
            var rarenums = Supervisor.RarePoolToRareNum(RarePool);
            var cards = Supervisor.RandCardsFromPools(RarePool, times, rarenums, cardpools);
            int a = 0;
            for (int i = 0; i < rarenums.Length; i++) {
                if (rarenums[i].Name == "SR" || rarenums[i].Name == "SSR") {
                    a += rarenums[i].Value;
                }
            }
            if (a == 0) cards[times - 1] = RandomCards(1, "SR", "SSR")[0];
            if (a > 4) {
                for (int i = 0; i < times; i++) {
                    if (cards[i].RareDegree == "SR" || cards[i].RareDegree == "SSR") {
                        cards[i] = RandomCards(1, "N", "R")[0];
                        a--;
                        if (a <= 4) {
                            break;
                        }
                    }
                }
            }
            return cards;
        }
        static T LoadClassFromXml<T>(string loadpath) where T : class {
            using (StreamReader sr = new StreamReader(loadFloder + loadpath, Encoding.Unicode)) {
                var tmp = DESerializer<T>(sr.ReadToEnd());
                sr.Close();
                return tmp;
            }
        }
        static CardPool LoadCardPool(string loadpath) {
            using (StreamReader sr = new StreamReader(loadFloder + loadpath + ".xml", Encoding.Unicode)) {
                var tmp = DESerializer<CardPool>(sr.ReadToEnd());
                var totalvalue = 0;
                foreach (var item in tmp.list) {
                    totalvalue += item.Value;
                    if (item.Value == 0) {
                        item.Value = 5;
                    }
                }
                tmp.CardPoolTotalValue = totalvalue;
                sr.Close(); return tmp;
            }
        }
        static void SaveCardPool(string filename) {
            using (StreamWriter sw = new StreamWriter(loadFloder + filename + ".xml", false, Encoding.Unicode)) {
                CardPool cp = new CardPool("N", new Card("1", "稀有1", "SR", 5, "1.jpg"), new Card("2", "稀有2", "SR", 9, "2.jpg"));
                sw.WriteLine(XmlSerialize(cp));
                sw.Flush();
                sw.Close();
            }
        }
        static void BackSpace(int nums=1) {
            for (int i = 0; i < nums; i++) {
                commandline = commandline.Remove(commandline.Length - 1, 1);
                Console.Write("\b\0\b");
            }
        }
    }
}
