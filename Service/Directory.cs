using Microsoft.EntityFrameworkCore;
using SocialMedia.Dbcontext;
using SocialMedia.Enum;
using SocialMedia.Interface;
using SocialMedia.Models.DbModels;
using SocialMedia.Models.Friends;
using System.Collections.Generic;
using System.Linq;

namespace SocialMedia.Service
{
    public class Directory : IDirectory
    {
        //注入DbContext
        private readonly MemberContext _context;
        public Directory(MemberContext context)
        {
            _context = context;
        }

        public FriendsResp GetFrinedList(int? id)
        {
            FriendsResp resp = new FriendsResp();

            var memberInfo = _context.Members.Include(mf => mf.MemberInfo)
                                      .Include(mi => mi.MemberInterests)
                                        .ThenInclude(i => i.Interest)
                                      .Include(mt => mt.PreferTypes)
                                        .ThenInclude(k => k.Personality)
                                      .Include(md=>md.Directory)
                                      .AsNoTracking();

            var correctId = memberInfo.Any(m => m.ID == id);

            if (!correctId)
            {
                resp.Code = (int)RespCode.FAIL;
                resp.Msg = "用戶不存在，請重新登入";
                return resp;
            }

            List<RespData> friendlist = GetFrinedList(memberInfo,id);
   
            //回到用戶列表
            resp.Code = (int)RespCode.SUCCESS;
            resp.Msg = "取得用戶成功";
            resp.data = friendlist;
            //回到個人頁面

            return resp;
        }


        //取得朋友資訊
        public List<RespData> GetFrinedList(IQueryable<Member> memberInfo,int? id)
        {
            //取得用戶資料
            var memInfo = memberInfo.FirstOrDefault(m => m.ID == id);
            //取得用戶朋友的memberid
            string[] frinedId = memInfo.Directory.ContactList.Split(',');
            //取得朋友列表       
            var friendInfos = memberInfo.Where(m => frinedId.Contains(m.ID.ToString())).Select(m => m);

            List<RespData> frinedlist = new List<RespData>();
            foreach (var item in friendInfos)
            {
                //var friendInfo = memberInfo.FirstOrDefault(m => m.ID == int.Parse(frinedId));
                frinedlist.Add(new RespData()
                {
                    Gender = item.MemberInfo.Gender,
                    Job = item.MemberInfo.Job,
                    State = item.MemberInfo.State,
                    Introduce = item.MemberInfo.Introduce,
                    Intersert = string.Join("、", item.MemberInterests.Where(mi => mi.MemberID == item.ID).Select(mi => mi.Interest.Name)),
                    PreferType = string.Join("、", item.PreferTypes.Where(mi => mi.MemberID == item.ID).Select(mi => mi.Personality.Kind)),
                });
            }

            return frinedlist;

        }

    }
}
