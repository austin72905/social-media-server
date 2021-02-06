using Microsoft.EntityFrameworkCore;
using SocialMedia.Dbcontext;
using SocialMedia.Interface.Repository;
using SocialMedia.Models.DbModels;
using SocialMedia.Models.Personal;
using System.Collections.Generic;
using System.Linq;
using static SocialMedia.Common.DbUtil;

namespace SocialMedia.Service.Repository
{
    public class PersonalRepository : DBfactory, IPersonalRepository
    {
        private readonly MemberContext _context;
        public PersonalRepository(MemberContext context) : base(context)
        {
            _context = context;
        }

        public virtual IQueryable<Personality> GetPersonalInstance()
        {
            var types = from type in _context.Personalitys
                        select type;
            return types;
        }

        //修改偏好類型
        public void SavePreferType(PersonalReq req)
        {
            //取得實體
            var memInfo = _context.PreferTypes.Include(pp => pp.Personality)
                .Where(p => p.MemberID == req.memberid);

            //personality 字典
            Dictionary<int, string> personalDic = PreferTypeDic(_context.Personalitys);

            //取得要刪除、新增的項目
            var result = GetAddDelTest(req.data.preferType, memInfo.Select(p => p.Personality.Kind).ToList(), personalDic);


            //添加
            foreach (var i in result.addList)
            {
                var prefer = SetInstance.SetPreferData(req, i);
                _context.Add(prefer);
            }
            //刪除
            foreach (var i in result.delList)
            {
                var delItem = memInfo.Where(x => x.PersonalityID == i).FirstOrDefault();
                _context.PreferTypes.Remove(delItem);
            }

            _context.SaveChanges();

        }

        //返回 preferType personality字典
        public Dictionary<int, string> PreferTypeDic(IQueryable<Personality> preferType)
        {
            var preferTypeDic = new Dictionary<int, string>();
            foreach (var item in preferType)
            {
                preferTypeDic.Add(item.ID, item.Kind);
            }
            return preferTypeDic;
        }

        


    }
}
