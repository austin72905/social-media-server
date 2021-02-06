using Microsoft.EntityFrameworkCore;
using SocialMedia.Dbcontext;
using SocialMedia.Interface.Repository;
using SocialMedia.Models.DbModels;
using SocialMedia.Models.Personal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SocialMedia.Common.DbUtil;

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

    }
}
