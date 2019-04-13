using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSimulator {
    public static class Supervisor {
        /// <summary>
        /// 随机数 单个 
        /// 注意快速随机时因为种子原因会产生相同结果 如果需要多个应使用RandNums
        /// </summary>
        /// <param name="maxnum">最大值 左右闭区间</param>
        /// <returns></returns>
        public static int RandNum(int maxnum, int seed = 0) {
            if (seed == 0) {
                seed = int.Parse(DateTime.Now.ToString("HHmmssfff"));
            }
            Random r = new Random(seed);
            return r.Next(maxnum + 1);
        }
        /// <summary>
        /// 随机数 多个 返回int数组
        /// </summary>
        /// <param name="maxnum">最大值 左右闭区间</param>
        /// <param name="times">随机次数</param>
        /// <returns></returns>
        public static int[] RandNums(int maxnum, int times) {
            int[] a = new int[times];
            for (int i = 0; i < times; i++) {
                Random r = new Random(int.Parse(DateTime.Now.ToString("HHmmssfff")) + i);
                a[i] = r.Next(maxnum + 1);
            }
            return a;
        }
        /// <summary>
        /// 从随机池中拿取一个
        /// </summary>
        /// <param name="RarePool"></param>
        /// <returns></returns>
        public static RareEnum RandRare(RarePool RarePool, int seed = 0) {
            int tmp = 0;
            int i = 0;
            var target = RandNum(RarePool.RarePoolTotalValue, seed);
            for (i = 0; i < RarePool.length; i++) {
                if (tmp > target) {
                    break;
                }
                tmp += RarePool[i].Value;
            }
            return RarePool[i - 1];
        }
        /// <summary>
        /// 从稀有度池中选择指定数量的类型
        /// </summary>
        /// <param name="RarePool"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public static RareEnum[] RandRares(RarePool RarePool, int times) {
            RareEnum[] RE = new RareEnum[times];
            int[] a = new int[times];
            int rarev = 0;
            for (int o = 0; o < RarePool.length; o++) {
                rarev += RarePool[o].Value;
            }
            a = RandNums(rarev, times);
            int tmp = 0;
            int i = 0;
            for (int j = 0; j < times; j++) {
                tmp = 0;
                for (i = 0; i < RarePool.length; i++) {
                    if (tmp > a[j]) {
                        break;
                    }
                    tmp += RarePool[i].Value;
                }
                RE[j] = RarePool[i - 1];
            }
            return RE;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardpool"></param>
        /// <param name="EnumNum"></param>
        /// <returns></returns>
        public static Card RandCardFromPool(CardPool cardpool, int seed = 0, RareEnum[] EnumNum = null) {
            int tmp = 0;
            int i = 0;
            var target = RandNum(cardpool.CardPoolTotalValue, seed);
            for (i = 0; i < cardpool.length; i++) {
                if (tmp > target) {
                    break;
                }
                tmp += cardpool[i].Value;
            }
            if (EnumNum != null) {
                FindAndAddNum(EnumNum, cardpool[i - 1]);
            }
            return cardpool[i - 1];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardpool"></param>
        /// <param name="times"></param>
        /// <param name="EnumNum"></param>
        /// <returns></returns>
        public static Card[] RandCardsFromPool(CardPool cardpool, int times, RareEnum[] EnumNum = null) {
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
                if (EnumNum != null) {
                    FindAndAddNum(EnumNum, cardpool[i - 1]);
                }
                Cards[j] = cardpool[i - 1];
            }
            return Cards;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RarePool"></param>
        /// <param name="times"></param>
        /// <param name="EnumNum"></param>
        /// <param name="cardpools"></param>
        /// <returns></returns>
        public static Card[] RandCardsFromPools(RarePool RarePool, int times, RareEnum[] EnumNum = null, params CardPool[] cardpools) {
            var Cards = new Card[times];
            var seed = int.Parse(DateTime.Now.ToString("HHmmssfff"));
            //随机卡牌稀有度
            var rareans = RandRares(RarePool, times);
            for (int i = 0; i < times; i++) {
                for (int j = 0; j < cardpools.Length; j++) {
                    if (rareans[i].Name == cardpools[j].CardPoolName) {
                        Cards[i] = RandCardFromPool(cardpools[j], seed);
                        seed = seed + i + j;
                    }
                }
                if (EnumNum != null) {
                    FindAndAddNum(EnumNum, Cards[i]);
                }
            }
            return Cards;
        }
        /// <summary>
        /// 统计数组初始化
        /// </summary>
        /// <param name="RarePool">稀有度列表</param>
        /// <returns></returns>
        public static RareEnum[] RarePoolToRareNum(List<RareEnum> RarePool) {
            var rarelist = new RareEnum[RarePool.Count];
            for (int i = 0; i < RarePool.Count; i++) {
                rarelist[i] = new RareEnum(RarePool[i].Name, 0);
            }
            return rarelist;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RarePool"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static RareEnum[] FindAllAndAddNum(List<RareEnum> RarePool, Card[] c) {
            var rarelist = RarePoolToRareNum(RarePool);
            for (int i = 0; i < c.Length; i++) {
                for (int j = 0; j < rarelist.Length; j++) {
                    if (rarelist[j].Name == c[i].RareDegree) {
                        rarelist[j].Value++;
                        break;
                    }
                }
            }
            return rarelist;
        }
        /// <summary>
        /// 添加对应稀有度数量
        /// </summary>
        /// <param name="EnumNum"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static RareEnum[] FindAndAddNum(RareEnum[] EnumNum, Card c) {
            for (int i = 0; i < EnumNum.Length; i++) {
                if (EnumNum[i].Name == c.RareDegree) {
                    EnumNum[i].Value++;
                    break;
                }
            }
            return EnumNum;
        }



    }
}
