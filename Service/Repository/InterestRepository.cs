using Microsoft.EntityFrameworkCore;
using SocialMedia.Dbcontext;
using SocialMedia.Interface.Repository;
using SocialMedia.Models.DbModels;
using SocialMedia.Models.Personal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Service.Repository
{
    public class InterestRepository:DBfactory, IInterestRepository
    {
        private readonly MemberContext _context;
        public InterestRepository(MemberContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Interest> GetInterestInstance()
        {
            var interests = from interest in _context.Interests
                            select interest;
            return interests;
        }

        //修改興趣
        public void SaveMemberInterest(PersonalReq req)
        {
            //取得實體
            var memInfo = _context.MemberInterests.Include(mi => mi.Interest)
                .Where(p => p.MemberID == req.memberid);

            //interest 字典
            Dictionary<int, string> MemberItDic = MemberInterestDic(_context.Interests);

            //取得要刪除、新增的項目
            var result = GetAddDelTest(req.data.interest, memInfo.Select(p => p.Interest.Name).ToList(), MemberItDic);


            //修改資料庫
            //添加
            foreach (var i in result.addList)
            {
                var inter = SetInstance.MemberInterestData(req, i);
                _context.Add(inter);
            }
            //刪除
            foreach (var i in result.delList)
            {
                var delItem = memInfo.Where(x => x.InterestID == i).FirstOrDefault();
                _context.MemberInterests.Remove(delItem);
            }

            _context.SaveChanges();

        }

        //返回 interest personality字典
        public Dictionary<int, string> MemberInterestDic(IQueryable<Interest> memberInterest)
        {
            var preferTypeDic = new Dictionary<int, string>();
            foreach (var item in memberInterest)
            {
                preferTypeDic.Add(item.ID, item.Name);
            }
            return preferTypeDic;
        }

        //返回 memberInterest、 prefertype 這種 多對多關連資料表 對應的id
        //可以返回兩個值
        public (IEnumerable<int> addList, IEnumerable<int> delList) GetAddDelTest<T>(IList<T> inputList, IList<T> NowList, Dictionary<int, string> Dic)
        {
            //類似tuple 的東西
            //可以存多個值
            //取得對應的id 值
            var tuple = (GetIdList(inputList, Dic), GetIdList(NowList, Dic));

            //取差集
            return GetTwoExcept(tuple.Item1, tuple.Item2);

        }

        //返回 memberInterest、 prefertype 這種 多對多關連資料表 對應的id
        public List<int> GetIdList<T>(IList<T> inputList, Dictionary<int, string> Dic)
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
        public (IEnumerable<int> addList, IEnumerable<int> delList) GetTwoExcept(List<int> inputIdList, List<int> NowIdList)
        {

            return (inputIdList.Except(NowIdList), NowIdList.Except(inputIdList));
        }
    }
}
