using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Common
{
    public class DbUtil
    {
        //返回 memberInterest、 prefertype 這種 多對多關連資料表 對應的id
        //可以返回兩個值
        public static (IEnumerable<int> addList, IEnumerable<int> delList) GetAddDelTest<T>(IList<T> inputList, IList<T> NowList, Dictionary<int, string> Dic)
        {
            //類似tuple 的東西
            //可以存多個值
            //取得對應的id 值
            var tuple = (GetIdList(inputList, Dic), GetIdList(NowList, Dic));

            //取差集
            return GetTwoExcept(tuple.Item1, tuple.Item2);

        }

        //返回 memberInterest、 prefertype 這種 多對多關連資料表 對應的id
        public static List<int> GetIdList<T>(IList<T> inputList, Dictionary<int, string> Dic)
        {
            var idList = new List<int>();
            foreach (var item in inputList)
            {
                foreach (var optionItem in Dic)
                {
                    if (optionItem.Value == item.ToString())
                    {
                        //[1,2,6]
                        idList.Add(optionItem.Key);
                    }
                }
            }

            return idList;
        }

        //取得差集
        public static (IEnumerable<int> addList, IEnumerable<int> delList) GetTwoExcept(List<int> inputIdList, List<int> NowIdList)
        {

            return (inputIdList.Except(NowIdList), NowIdList.Except(inputIdList));
        }
    }
}
