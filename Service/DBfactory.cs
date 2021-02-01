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
        /// 檢查用戶名是否存在
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public bool CheckMemberExisted(RegisReq req)
        {
            var memberInfo = _context.Members.Include(mp => mp.Password).AsNoTracking();

            //用 == 會忽略大小寫
            var memberExisted = memberInfo.Any(m => string.Equals(m.Name, req.username, StringComparison.Ordinal));

            return memberExisted;
        }

        /// <summary>
        /// 檢查用戶名是否存在
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public bool CheckMemberExisted(LoginReq req)
        {
            var memberInfo = _context.Members.Include(mf => mf.MemberInfo)
                                      .Include(mp => mp.Password).AsNoTracking();
            //用 == 會忽略大小寫
            var memberExisted = memberInfo.Any(m => string.Equals(m.Name, req.username, StringComparison.Ordinal));

            return memberExisted;
        }

        

        /// <summary>
        /// 用戶基本資訊
        /// </summary>
        /// <param name="member"></param>
        public virtual void SaveMemberData(Member member)
        {
            _context.Add(member);
            _context.SaveChanges();
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
        /// 偏好類型
        /// </summary>
        /// <param name="preferTypes"></param>
        public virtual void SavePreferTypeData(List<PreferType> preferTypes)
        {
            foreach (var item in preferTypes)
            {
                _context.Add(item);
            }
            _context.SaveChanges();
        }

        

        /// <summary>
        /// 提供member 實體
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        //private Member SetMember(RegisReq req)
        //{
        //    var password = SetPassword(req);
        //    var directory = SetDirectory(req);
        //    var memberInfo = SetMemberInfo(req);
            
        //    var member = new Member()
        //    {
        //        Name= req.username,
        //        Gender =req.gender,
        //        Password = password,
        //        Directory= directory,
        //        MemberInfo= memberInfo,

        //    };

        //    return member;
        //}

        /// <summary>
        /// 提供password 實體
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        //private  Password SetPassword(RegisReq req)
        //{
        //    var password = new Password()
        //    {
        //        Code = req.password,
        //    };

        //    return password;
        //}

        

        ////第一次建立
        //private  Models.DbModels.MemberInfo SetMemberInfo(RegisReq req)
        //{
        //    var memberInfo = new Models.DbModels.MemberInfo()
        //    {
                
        //    };
        //    return memberInfo;
        //}


        //private  Models.DbModels.Directory SetDirectory(RegisReq req)
        //{

        //    var directory = new Models.DbModels.Directory()
        //    {
        //        ContactList="",
        //    };
        //    return directory;

        //}


        //public virtual Models.DbModels.PreferType SetPreferData(PersonalReq req , int personalityid)
        //{
        //    var prefer = new Models.DbModels.PreferType
        //    {
        //        MemberID = req.memberid,
        //        PersonalityID = personalityid
        //    };
        //    return prefer;


        //}

        
        //public virtual Models.DbModels.MemberInterest MemberInterestData(PersonalReq req, int interestid)
        //{
        //    var inter = new Models.DbModels.MemberInterest
        //    {
        //        MemberID = req.memberid,
        //        InterestID = interestid
        //    };
        //    return inter;
        //}

        /// <summary>
        /// 用戶基本資訊
        /// </summary>
        /// <param name="req"></param>
        protected virtual void SaveMemberData(RegisReq req)
        {
            //註冊的時候把資料都建立起來
            var member = SetInstance.SetMember(req);
            SaveMemberData(member);
        }


        //修改個人資料
        protected virtual void SaveMemberInfoData(PersonalReq req)
        {
            var memberInfo = _context.Members.Include(mf =>mf.MemberInfo ).FirstOrDefault(m => m.ID == req.memberid) ;
            //var memberInfo = GetMemberInstance(memberlist, req.memberid);
            //修改值
            memberInfo.MemberInfo.NickName = req.data.nickname;
            memberInfo.MemberInfo.Job = req.data.job;
            memberInfo.MemberInfo.State = req.data.state;
            memberInfo.MemberInfo.Introduce = req.data.introduce;
            _context.SaveChanges();
        }


        //修改偏好類型
        protected virtual void SavePreferType(PersonalReq req)
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
                var prefer =SetInstance.SetPreferData(req,i);
                _context.Add(prefer);
            }
            //刪除
            foreach (var i in result.delList)
            {
                var delItem = memInfo.Where(x =>x.PersonalityID == i).FirstOrDefault();
                _context.PreferTypes.Remove(delItem);
            }

            _context.SaveChanges();

        }


        //修改興趣
        protected virtual void SaveMemberInterest(PersonalReq req)
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

        /// <summary>
        /// 新增好友
        /// </summary>
        /// <param name="req"></param>
        protected virtual void SaveDirectoryData(FriendReq req,bool isAdd =true)
        {
            var memberInfo = _context.Members.Include(md => md.Directory).FirstOrDefault(m => m.ID == req.memberid);

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
                frinedId = string.Join(",", frinedList.Where(i=> i != req.friendid.ToString()).Select(m=>m));
            }
            
            //更新值
            memberInfo.Directory.ContactList = frinedId;
            _context.SaveChanges();
        }


        //存放聊天訊息
        protected void SaveChatMsg(string userid, string recieveid, string input ,bool unread =false)
        {
            


            
            
            //把原本最新的塗銷
            var nowNewest=_context.ChatMsgs.Where(m => m.MemberID == Convert.ToInt32(userid) && m.ChatID == Convert.ToInt32(recieveid) && m.Newest==true).FirstOrDefault();
            //要先判斷如果是第一次聊天要先加
            if(nowNewest != null)
            {
                nowNewest.Newest = false;
            }

            
            var newNewestRecieve = _context.ChatMsgs.Where(m => m.MemberID == Convert.ToInt32(recieveid) && m.ChatID == Convert.ToInt32(userid) && m.Newest == true).FirstOrDefault();
            //要先判斷如果是第一次聊天要先加
            if (newNewestRecieve != null)
            {
                newNewestRecieve.Newest = false;
            }
            


            //發送者要存入的資料
            var message = new ChatMsg()
            {
                MemberID = Convert.ToInt32(userid),
                ChatID = Convert.ToInt32(recieveid),
                SpeakerID = Convert.ToInt32(userid),
                Text = input,
                Time = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),
                Newest =true

            };

            //接收者要存入的資料
            var recieverMsg = new ChatMsg()
            {
                MemberID = Convert.ToInt32(recieveid),
                ChatID = Convert.ToInt32(userid),
                SpeakerID = Convert.ToInt32(userid),
                Text = input,
                Time = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),
                Unread =true,
                Newest = true
            };

            _context.ChatMsgs.Add(message);
            _context.ChatMsgs.Add(recieverMsg);
            _context.SaveChanges();
        }

        /// <summary>
        /// 註冊時取得用戶實體
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        //public virtual Member GetMemberInstance(RegisReq req)
        //{
        //    var member = _context.Members.Include(mf => mf.MemberInfo)
        //                              .Include(mp => mp.Password)
        //                              .AsNoTracking()
        //                              .FirstOrDefault(m => string.Equals(m.Name, req.username, StringComparison.Ordinal));
        //    return member;
        //}

        /// <summary>
        /// 登入取得用戶實體
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        //public virtual Member GetMemberInstance(LoginReq req)
        //{
        //    var member = _context.Members.Include(mf => mf.MemberInfo)
        //                              .Include(mp => mp.Password)
        //                              .AsNoTracking()
        //                              .FirstOrDefault(m => string.Equals(m.Name, req.username, StringComparison.Ordinal));
        //    return member;
        //}

        public virtual Member GetMemberInstance(BasicReq req)
        {
            var member = _context.Members.Include(mf => mf.MemberInfo)
                                      .Include(mp => mp.Password)
                                      .AsNoTracking()
                                      .FirstOrDefault(m => string.Equals(m.Name, req.username, StringComparison.Ordinal));
            return member;
        }


        /// <summary>
        /// get 方法時 取得用戶實體
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public virtual Member GetMemberInstance(int id)
        //{
        //    var member = _context.Members.Include(mf => mf.MemberInfo)
        //                              .Include(mi => mi.MemberInterests)
        //                                .ThenInclude(i => i.Interest)
        //                              .Include(mt => mt.PreferTypes)
        //                                .ThenInclude(k => k.Personality)
        //                              .AsNoTracking()
        //                              .FirstOrDefault(m => m.ID == id);
        //    return member;
        //}


        /// <summary>
        /// 獲取用戶列表實體
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<Member> GetMemberListInstance()
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
                    memberID =item.ID,
                    gender = item.Gender,
                    job = item.MemberInfo.Job,
                    state = item.MemberInfo.State,
                    introduce = item.MemberInfo.Introduce,
                });
            }

            return frinedlist;

        }

        public virtual IQueryable<Personality> GetPersonalInstance()
        {
            var types = from type in _context.Personalitys
                        select type;
            return types;
        }

        public virtual IQueryable<Interest> GetInterestInstance()
        {
            var interests = from interest in _context.Interests
                            select interest;
            return interests;
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


        //刷新chathub 緩存
        public List<MemberData> GetMemListToChat()
        {
            var chatmemlist = new List<MemberData>();
            var memlist = _context.Members.Include(m => m.MemberInfo);

            foreach(var item in memlist)
            {
                chatmemlist.Add(new MemberData 
                {
                    username =item.Name,
                    memberid =item.ID,
                    gender =item.Gender,
                    nickname =item.MemberInfo.NickName
                });
            }

            return chatmemlist;
        }

        //修改資料庫為已讀
        public void UpdateDBToRead(string userid, string recieveid)
        {
            var updateItem = _context.ChatMsgs.Where(m => m.MemberID == Convert.ToInt32(userid));
        
            foreach(var item in updateItem)
            {
                if(item.ChatID == Convert.ToInt32(recieveid))
                {
                    //修改成已讀
                    item.Unread = false;
                }
                
            }

            _context.SaveChanges();
        }

        protected List<ChatMsgData> GetMsgList(string memberid,string recieveid)
        {
            var msgItem = _context.ChatMsgs.Where(m => m.MemberID == Convert.ToInt32(memberid) && m.ChatID == Convert.ToInt32(recieveid));//.OrderBy(m =>long.Parse( m.Time))
            var Msglist = new List<ChatMsgData>();

            //用戶及他正在聊天的用戶的基本資料
            //var userData = GetMemberListInstance().FirstOrDefault(m => m.ID == Convert.ToInt32(memberid));
            //var recieverData = GetMemberListInstance().FirstOrDefault(m => m.ID == Convert.ToInt32(recieveid));
            foreach (var item in msgItem)
            {
                string speakerName = _context.Members.FirstOrDefault(m => m.ID == Convert.ToInt32(item.SpeakerID)).Name;
                string gender = _context.Members.FirstOrDefault(m => m.ID == Convert.ToInt32(item.SpeakerID)).Gender;

                Msglist.Add(new ChatMsgData 
                {
                    username = speakerName,
                    memberid =item.SpeakerID,
                    gender= gender,
                    text=item.Text,
                    unread=item.Unread
                });
            }

            return Msglist;
        }

        protected List<ChatMsgLastData> GetAllLastMsgList(string memberid)
        {
            //之前寫的要再加一個時間戳，不然沒辦法排序
            //取得訊息符合memberid 的 newest == true 的
            //搞成list
            var memMsg = _context.ChatMsgs.Where(m => m.MemberID == Convert.ToInt32(memberid) && m.Newest == true);

            var userData = GetMemberListInstance();

            //最後要回傳的list
            List<ChatMsgLastData> msgLastList = new List<ChatMsgLastData>();
            
            foreach (var item in memMsg)
            {
                int unreadCount = 0;
                Member chatUser = userData.Where(user =>user.ID == item.ChatID).FirstOrDefault();
                //找到每個對應的訊息組
                var chatMsgSet = _context.ChatMsgs.Where(msg => msg.MemberID == Convert.ToInt32(memberid) && msg.ChatID == item.ChatID);
                foreach(var msg in chatMsgSet)
                {
                    if (msg.Unread == true)
                    {
                        unreadCount += 1;
                    }
                }

                msgLastList.Add(new ChatMsgLastData 
                { 
                    username = chatUser.Name,
                    memberid = item.SpeakerID,
                    gender =chatUser.Gender,
                    text =item.Text,
                    chatname = chatUser.MemberInfo.NickName,
                    chatid = chatUser.ID.ToString(),
                    unreadcount = unreadCount

                });
            }

            return msgLastList;
        }


        protected int GetUnreadCount(string memberid)
        {
            var msgList = _context.ChatMsgs.Where(msg => msg.MemberID == Convert.ToInt32(memberid));
            int unReadCount = 0;
            foreach(var msg in msgList)
            {
                if(msg.Unread == true)
                {
                    unReadCount += 1;
                }
            }

            return unReadCount;
        }

        protected Dictionary<string,int> GetUnreadDic(string memberid)
        {
            var msgList = _context.ChatMsgs.Where(msg => msg.MemberID == Convert.ToInt32(memberid));

            var tempSet = new HashSet<string>();

            //取得每個聊天室id (hashset 會去到重複值)
            foreach(var item in msgList)
            {
                tempSet.Add(item.ChatID.ToString());
            }

            //最後要返回的未讀數量 (每個id 的未讀數量個別計算)
            var resultDic = new Dictionary<string, int>();

            foreach(var key in tempSet)
            {
                resultDic.Add(key, 0);
            }

            foreach(var item in msgList)
            {
                if (resultDic.ContainsKey(item.ChatID.ToString()))
                {
                    resultDic[item.ChatID.ToString()] += 1;
                }
            }

            return resultDic;


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


        //返回 preferType personality字典
        public Dictionary<int,string> PreferTypeDic(IQueryable<Personality> preferType)
        {
            var preferTypeDic = new Dictionary<int, string>();
            foreach (var item in preferType)
            {
                preferTypeDic.Add(item.ID, item.Kind);
            }
            return preferTypeDic;
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

        //返回 memberInterest、 prefertype 這種 多對多關連資料表 對應的id
        //可以返回兩個值
        public (IEnumerable<int>addList, IEnumerable<int> delList) GetAddDelTest<T>(IList<T> inputList, IList<T> NowList, Dictionary<int, string> Dic)
        {
            //類似tuple 的東西
            //可以存多個值
            //取得對應的id 值
            var tuple = (GetIdList(inputList, Dic), GetIdList(NowList, Dic));

            //取差集
            return GetTwoExcept(tuple.Item1,tuple.Item2);

        }

        //取得差集
        public (IEnumerable<int> addList, IEnumerable<int> delList) GetTwoExcept(List<int> inputIdList, List<int> NowIdList)
        {
            
            return (inputIdList.Except(NowIdList), NowIdList.Except(inputIdList));
        }

        
    }
}
