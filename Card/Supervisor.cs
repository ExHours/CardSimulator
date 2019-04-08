using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSimulator {
    public static class Supervisor {
        public static int RandNum(int maxnum) {
            Random r = new Random(int.Parse(DateTime.Now.ToString("HHmmssfff")));
            return r.Next(maxnum + 1);
        }
        public static int[] RandNums(int maxnum, int times) {
            int[] a = new int[times];
            for (int i = 0; i < times; i++) {
                Random r = new Random(int.Parse(DateTime.Now.ToString("HHmmssfff")) + i);
                a[i] = r.Next(maxnum + 1);
            }
            return a;
        }
        public static RareEnum RandRare(List<RareEnum> RarePool, int RarePoolTotalValue) {
            int tmp = 0;
            int i = 0;
            var target = RandNum(RarePoolTotalValue);
            for (i = 0; i < RarePool.Count; i++) {
                if (tmp > target) {
                    break;
                }
                tmp += RarePool[i].Value;
            }
            return RarePool[i - 1];
        }
        public static RareEnum[] RandRares(List<RareEnum> RarePool, int RarePoolTotalValue, int times) {
            RareEnum[] RE = new RareEnum[times];
            int[] a = new int[times];
            a = RandNums(RarePoolTotalValue, times);
            int tmp = 0;
            int i = 0;
            for (int j = 0; j < times; j++) {
                tmp = 0;
                for (i = 0; i < RarePool.Count; i++) {
                    if (tmp > a[j]) {
                        break;
                    }
                    tmp += RarePool[i].Value;
                }
                RE[j] = RarePool[i - 1];
            }
            return RE;
        }
        public static Card RandCard(CardPool cardpool, RareEnum[] EnumNum = null) {
            int tmp = 0;
            int i = 0;
            var target = RandNum(cardpool.CardPoolTotalValue);
            for (i = 0; i < cardpool.length; i++) {
                if (tmp > target) {
                    break;
                }
                tmp += cardpool[i].Value;
            }
            return cardpool[i - 1];
        }
        public static Card[] RandCards(CardPool cardpool, int times, RareEnum[] EnumNum = null) {
            var Cards = new Card[times];
            int[] a = new int[times];
            a = RandNums(cardpool.CardPoolTotalValue, times);
            int tmp = 0;
            int i = 0;
            for (int j = 0; j < times; j++) {
                tmp = 0;
                for (i = 0; i < cardpool.length; i++) {
                    if (tmp > a[j]) {
                        break;
                    }
                    tmp += cardpool[i].Value;
                }
                Cards[j] = cardpool[i - 1];
            }
            return Cards;
        }
        public static RareEnum[] FindAndAdd(RareEnum[] rarelist,Card[] c) {
            for (int i = 0; i < c.Length; i++) {
                if (c[i].RareDegree==) {

                }
            }


        }





    }
}
