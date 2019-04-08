using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace CardSimulator {
    partial class Program {
        static string loadFloder = ConfigurationManager.AppSettings["Floder"];
        static List<RareEnum> RarePool = new List<RareEnum>();
        static int RarePoolTotalValue = 0;
        static void SaveRareBasicInfo() {
            RarePool.Add(new RareEnum("N", 43));
            RarePool.Add(new RareEnum("R", 35));
            RarePool.Add(new RareEnum("SR", 12));
            RarePool.Add(new RareEnum("SSR", 7));
            RarePool.Add(new RareEnum("UR", 3));
            using (StreamWriter sw = new StreamWriter(loadFloder + "RarePool.xml", false, Encoding.Unicode)) {
                sw.WriteLine(XmlSerialize(RarePool));
                sw.Flush();
                sw.Close();
            }
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

                CardPool cp = new CardPool("R", new Card("爱菜美佳", "R", 5, "1.jpg"), new Card("暗黑大石", "R", 9, "2.jpg"));
                sw.WriteLine(XmlSerialize(cp));
                sw.Flush();
                sw.Close();
            }
        }
    }
}
