using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSimulator {
    public class Randop : Operation {
        public Randop() : base("RandNum", 2, "100") {
        }
        public override string Run(string[] Params) {
            try {
                var rd = new Random();
                switch (Params.Length) {
                    case 0:
                        return rd.Next().ToString();
                    case 1:
                        return rd.Next(int.Parse(Params[0])).ToString();
                    case 2:
                        return rd.Next(int.Parse(Params[0]), int.Parse(Params[1])).ToString();
                    default:
                        throw new Exception();
                }
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
    public class Randop2 : Operation {
        public Randop2() : base("RandomAllCards", 1, "10") {
        }
        public override string Run(string[] Params) {
            try {
                Card[] Cards = null;
                switch (Params.Length) {
                    case 0: Cards = Program.RandomAllCards(); break;
                    case 1: Cards = Program.RandomAllCards(int.Parse(Params[0])); break;
                }
                Console.WriteLine();
                Console.WriteLine();
                foreach (var item in Cards) {
                    Console.WriteLine(item.Name);
                }
                return "";
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}
