using Microsoft.EntityFrameworkCore;
using SocialMedia.Dbcontext;
using SocialMedia.Models.DbModels;
using SocialMedia.Models.Friends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Service.Repository
{
    public class DirectoryRepository : DBfactory
    {
        private readonly MemberContext _context;
        public DirectoryRepository(MemberContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// 取得朋友資訊
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<FriendData> GetFrinedList(IQueryable<Member> memberInfo, int id)
        {
            //取得用戶資料
            var memInfo = memberInfo.FirstOrDefault(m => m.ID == id);
            //取得用戶朋友的memberid
            string[] frinedId = memInfo.Directory.ContactList.Split(',');
            //取得朋友列表       
            var friendInfos = memberInfo.Where(m => frinedId.Contains(m.ID.ToString())).Select(m => m);

            List<FriendData> frinedlist = new List<FriendData>();
            foreach (var item in friendInfos)
            {
                //var friendInfo = memberInfo.FirstOrDefault(m => m.ID == int.Parse(frinedId));
                frinedlist.Add(new FriendData()
                {
                    username=item.Name,
                    nickname=item.MemberInfo.NickName,
                    memberID = item.ID,
                    gender = item.Gender,
                    job = item.MemberInfo.Job,
                    state = item.MemberInfo.State,
                    introduce = item.MemberInfo.Introduce,
                });
            }

            return frinedlist;

        }

        /// <summary>
        /// 新增好友
        /// </summary>
        /// <param name="req"></param>
        public  void SaveDirectoryData(FriendReq req, bool isAdd = true)
        {
            var memberInfo = _context.Members.Include(md => md.Directory).FirstOrDefault(m => m.ID == Convert.ToInt32(req.memberid));

            //var memberInfo = GetMemberInstance(memberDirectorylist, req.memberid);

            //取得用戶朋友的memberid
            string frinedId = memberInfo.Directory.ContactList;
            if (isAdd)
            {
                frinedId += $",{req.friendid}";
            }
            else
            {
                string[] frinedList = frinedId.Split(',');
                frinedId = string.Join(",", frinedList.Where(i => i != req.friendid.ToString()).Select(m => m));
            }

            //更新值
            memberInfo.Directory.ContactList = frinedId;
            _context.SaveChanges();
        }
    }
}
