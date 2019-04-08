using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardSimulator {
    public partial class Program {
        static void Main(string[] args) {


            //List<string> mc = new List<string>();
            //mc.Add("1");
            //mc.Add("2");
            ////Console.WriteLine();

            //string a = XmlSerialize(mc);
            //var tmp = DESerializer<List<string>>(a);
            Load();
            //SaveCardPool("R");
            //CardPool cp = LoadCardPool("R");

            //foreach (var item in Supervisor.RandRares(RarePool, RarePoolTotalValue, 10)) {
            //    Console.WriteLine(item.Name);
            //}
            //Console.WriteLine( Supervisor.RandCard(LoadCardPool("R")).Name);
            var i = LoadCardPool("R");
            foreach (var item in Supervisor.RandCards(i, 10)) {
                Console.WriteLine(item.Name);
            }
            


            Console.ReadLine();
        }
        static void Load() {
            RarePool = LoadClassFromXml<List<RareEnum>>("RarePool.xml");
            foreach (var item in RarePool) {
                RarePoolTotalValue += item.Value;
            }
        }
        /// <summary>
        /// Xml序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">序列化实例</param>
        /// <returns></returns>
        public static string XmlSerialize<T>(T obj) {
            using (StringWriter sw = new StringWriter()) {
                Type t = obj.GetType();
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(sw, obj);
                sw.Close();
                return sw.ToString();
            }
        }
        /// <summary>
        /// Xml反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strXML">反序列化字符串</param>
        /// <returns></returns>
        public static T DESerializer<T>(string strXML) where T : class {
            try {
                using (StringReader sr = new StringReader(strXML)) {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(sr) as T;
                }
            } catch (Exception ex) {
                return null;
            }
        }
    }
}
