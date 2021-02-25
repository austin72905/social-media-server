using Microsoft.EntityFrameworkCore;
using SocialMedia.Dbcontext;
using SocialMedia.Models;
using SocialMedia.Models.DbModels;
using SocialMedia.Models.Friends;
using SocialMedia.Models.Message;
using SocialMedia.Models.Personal;
using SocialMedia.Models.SetCache;
using SocialMedia.Models.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Service
{
    public abstract class DBfactory
    {
        //注入DbContext
        private readonly MemberContext _context;
        public DBfactory(MemberContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 檢查ID
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public  static bool CheckMemberId(IQueryable<Member> context,int? id) 
        {
            var correctId = context.Any(m => m.ID == id);
            
            return correctId;
        }

        /// <summary>
        /// 檢查ID (task 使用的版本)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool CheckMemberId(IList<Member> context, int? id)
        {
            var correctId = context.Any(m => m.ID == id);

            return correctId;
        }


        /// <summary>
        /// 個人訊息
        /// </summary>
        /// <param name="memberInfo"></param>
        public virtual void SaveMemberInfoData(Models.DbModels.MemberInfo memberInfo)
        {
            _context.Add(memberInfo);
            _context.SaveChanges();
        }

        /// <summary>
        /// 密碼
        /// </summary>
        /// <param name="password"></param>
        public virtual void SavePasswordData(Password password)
        {
            _context.Add(password);
            _context.SaveChanges();
        }

        /// <summary>
        /// 好友列表
        /// </summary>
        /// <param name="directory"></param>
        public virtual void SaveDirectoryData(Models.DbModels.Directory directory)
        {

        }

        /// <summary>
        /// 興趣
        /// </summary>
        /// <param name="memberInterests"></param>
        public virtual void SaveMemberInterestData(List<MemberInterest> memberInterests)
        {
            foreach (var item in memberInterests)
            {
                _context.Add(item);
            }
            _context.SaveChanges();
        }

       


        /// <summary>
        /// 獲取用戶列表實體
        /// </summary>
        /// <returns></returns>
        public  virtual IQueryable<Member> GetMemberListInstance()
        {
            var memberList = _context.Members.Include(mf => mf.MemberInfo)
                                    .Include(mi => mi.MemberInterests)
                                      .ThenInclude(i => i.Interest)
                                    .Include(mt => mt.PreferTypes)
                                      .ThenInclude(k => k.Personality)
                                    .Include(md => md.Directory)
                                    .AsNoTracking();
            return memberList;
        }

        //取得要傳給聊天室的資料
        public virtual ChatResp GetMsgToChat(string userid)
        {
            var speakerData = GetMemberListInstance().FirstOrDefault(m => m.ID == Convert.ToInt32(userid));
            
            
            var speakerChatData = new ChatResp() 
            {
                memberid = speakerData.ID,
                username =speakerData.MemberInfo.NickName,
                gender =speakerData.Gender
            };

            return speakerChatData;
        }

        //取得要傳給message組件的資料
        public virtual (ChatMsgLastData forSpeaker, ChatMsgLastData forReciever) GetLastMsgData(string userid, string recieveid, string input) 
        {

            var speakerData =GetMemberListInstance().FirstOrDefault(m => m.ID == Convert.ToInt32(userid));
            
            var recieverData= GetMemberListInstance().FirstOrDefault(m => m.ID == Convert.ToInt32(recieveid));
            
            
            var forSpeaker = new ChatMsgLastData() 
            {
                memberid = speakerData.ID,
                gender = recieverData.Gender,
                username = speakerData.Name,
                text = input,
                //是對哪個用戶的msg
                chatname = recieverData.MemberInfo.NickName,
                //傳這個是要讓點訊息時可以到該聊天室
                chatid = recieverData.ID.ToString(),
            };

            var forReciever = new ChatMsgLastData()
            {
                memberid = speakerData.ID,
                gender = speakerData.Gender,
                username = speakerData.MemberInfo.NickName,
                text = input,
                //是對哪個用戶的msg
                chatname = speakerData.MemberInfo.NickName,
                //傳這個是要讓點訊息時可以到該聊天室
                chatid = speakerData.ID.ToString(),
                unreadcount = 1
            };

            return (forSpeaker, forReciever);
        }

        //新增興趣
        public object AddInterest(Interest interest)
        {
            _context.Add(interest);
            _context.SaveChanges();


            return "新增inter成功";
        }
        //新增類型
        public object AddPersonality(Personality personality)
        {
            _context.Add(personality);
            _context.SaveChanges();


            return "新增personality成功";
        }
  
    }
}
