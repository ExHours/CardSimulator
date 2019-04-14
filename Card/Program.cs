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
        static string commandline = "";
        static OperationsList Ol = new OperationsList();
        static void Main(string[] args) {
            Load();
            //foreach (var item in Supervisor.RandCardsFromPool(i, 10)) {
            //    Console.WriteLine(item.Name);
            //}
            //var e1 = Supervisor.RandCardsFromPool(i, 10);
            //var tmp = Supervisor.FindAllAndAddNum(RarePool, e1);

            //var ans = RandomAllCardsWithSafe(10);

            //foreach (var item in ans) {
            //    Console.WriteLine(item.Name);
            //}

            while (true) {
                var a = Console.ReadKey(true);
                var b = new byte[1] { (byte)a.KeyChar };
                if (a.Key == ConsoleKey.Tab) {
                    var clsp = commandline.Trim().Split(' ');
                    if (clsp.Length == 0 || clsp.Length == 1) {
                        commandline += "\t";
                        Console.Write("\t");
                    } else {
                        //信息检索代码

                        Console.WriteLine(clsp[clsp.Length - 1]);
                    }
                } else if (a.Key == ConsoleKey.Enter) {
                    //命令执行代码
                    Console.WriteLine();
                    commandline = commandline.Trim();

                    Console.WriteLine(commandline);
                    commandline = "";
                } else if (a.Key == ConsoleKey.Backspace) {
                    if (commandline!="") {
                        
                    }
                } else {
                    var tmp = Encoding.ASCII.GetString(b);
                    commandline += tmp;
                    Console.Write(tmp);
                }
            }
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
