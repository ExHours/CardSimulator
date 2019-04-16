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
        public static string commandline = "";
        static int historyIndex = 0;
        static List<string> commandlineList = new List<string>();
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
            //StreamWriter sw = new StreamWriter("XX.xml");
            //Console.SetOut(sw);

            //foreach (var item in RandomAllCards(10)) {
            //    Console.WriteLine(item.Name);
            //}   

            while (true) {
                var a = Console.ReadKey(true);
                var b = new byte[1] { (byte)a.KeyChar };
                if (a.Key == ConsoleKey.Tab) {
                    var clsp = proload(commandline);
                    if (clsp.Length == 0 || clsp[0] == "") {
                        //空
                    } else {
                        //信息检索代码
                        var ans = Ol.Search(clsp);
                        if (ans.Count == 1) {
                            BackSpace(commandline.Length);
                            commandline += ans[0];
                            Console.Write(ans[0]);
                        } else if (ans.Count > 1) {
                            commandlineList.Add(commandline);
                            historyIndex = commandlineList.Count - 1;
                            byte couter = 0;
                            Console.WriteLine();
                            for (int i = 0; i < ans.Count; i++) {
                                if (couter <= 4) {
                                    Console.Write(ans[i] + "\t\t");
                                    couter++;
                                } else {
                                    couter = 0;
                                    Console.WriteLine();
                                }
                            }
                            Console.WriteLine();
                            Console.Write(commandline);
                        } else {
                            //空
                        }
                    }
                } else if (a.Key == ConsoleKey.Enter) {
                    //命令执行代码
                    var clsp = proload(commandline);
                    string[] clsp2 = new string[clsp.Length - 1];
                    for (int i = 1; i < clsp.Length; i++) {
                        clsp2[i - 1] = clsp[i];
                    }
                    commandlineList.Add(commandline);
                    historyIndex = commandlineList.Count - 1;
                    var tmp = Ol.RunOp(clsp[0], clsp2);
                    Console.WriteLine("\r\n" + tmp + "\r\n");
                    commandline = "";
                } else if (a.Key == ConsoleKey.UpArrow) {
                    //Console.WriteLine("up");
                    if (commandlineList.Count != 0) {
                        BackSpace(commandline.Length);
                        Console.Write(commandlineList[historyIndex]);
                        commandline = commandlineList[historyIndex];
                        if (historyIndex != 0) {
                            historyIndex--;
                        }
                    }
                } else if (a.Key == ConsoleKey.DownArrow) {
                    //Console.WriteLine("down");
                    if (commandlineList.Count != 0 && historyIndex != commandlineList.Count - 1) {
                        historyIndex++;
                        BackSpace(commandline.Length);
                        Console.Write(commandlineList[historyIndex]);
                        commandline = commandlineList[historyIndex];
                    }
                } else if (a.Key == ConsoleKey.LeftArrow) {
                    if (Console.CursorLeft != 0) {
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    }
                } else if (a.Key == ConsoleKey.RightArrow) {
                    if (Console.CursorLeft < commandline.Length) {
                        Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                    }
                } else if (a.Key == ConsoleKey.Home) {
                    Console.SetCursorPosition(0, Console.CursorTop);
                } else if (a.Key == ConsoleKey.End) {
                    if (Console.CursorLeft != 0) {
                        Console.SetCursorPosition(commandline.Length, Console.CursorTop);
                    }
                } else if (a.Key == ConsoleKey.Backspace) {
                    if (commandline != "") {
                        BackSpace();
                    }
                } else {
                    var tmp = Encoding.ASCII.GetString(b);
                    if (tmp[0] != '\0') {
                        if (commandline.Length == Console.CursorLeft) {
                            commandline += tmp;
                            RefreshCommandLine();

                        } else {
                            commandline = commandline.Insert(Console.CursorLeft, tmp);
                            RefreshCommandLine();
                        }
                    }
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
