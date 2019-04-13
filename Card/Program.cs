using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardSimulator {
    public partial class Program {
        static string loadFloder = ConfigurationManager.AppSettings["Floder"];
        static RarePool RarePool = new RarePool();
        static void Main(string[] args) {
            Load();
            var i = LoadCardPool("R");
            var a = LoadCardPool("SR");
            //foreach (var item in Supervisor.RandCardsFromPool(i, 10)) {
            //    Console.WriteLine(item.Name);
            //}
            //var e1 = Supervisor.RandCardsFromPool(i, 10);
            //var tmp = Supervisor.FindAllAndAddNum(RarePool, e1);




            var ans = RandomAllCards();
            foreach (var item in ans) {
                Console.WriteLine(item.Name);
            }



            Console.ReadLine();
        }
        static void Load() {
            RarePool = LoadClassFromXml<RarePool>("RarePool.xml");
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
