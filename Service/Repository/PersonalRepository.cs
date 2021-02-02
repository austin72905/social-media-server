using Microsoft.EntityFrameworkCore;
using SocialMedia.Dbcontext;
using SocialMedia.Interface.Repository;
using SocialMedia.Models.DbModels;
using SocialMedia.Models.Personal;
using System.Collections.Generic;
using System.Linq;

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
