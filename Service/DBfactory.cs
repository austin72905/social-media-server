using Microsoft.EntityFrameworkCore;
using SocialMedia.Dbcontext;
using SocialMedia.Models;
using SocialMedia.Models.DbModels;
using SocialMedia.Models.Friends;
using SocialMedia.Models.Personal;
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
        public virtual Member SetMember(RegisReq req)
        {
            var password = SetPassword(req);
            //var directory = SetDirectory(req);
            //不用建立memberInfo 他會幫你建立 除了 memberid 其他都是null

            var member = new Member()
            {
                Name= req.username,
                Password = password,
                //Directory= directory

            };

            return member;
        }

        /// <summary>
        /// 提供password 實體
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public virtual Password SetPassword(RegisReq req)
        {
            var password = new Password()
            {
                Code = req.password,
            };

            return password;
        }

        /// <summary>
        /// 提供 memberInfo 實體
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public virtual Models.DbModels.MemberInfo SetMemberInfo(SetReq req)
        {
            var memberInfo = new Models.DbModels.MemberInfo()
            {
                MemberID = req.memberid,
                //Gender = req.data.Gender,
                NickName=req.data.NickName,
                Job = req.data.Job,
                State = req.data.State,
                Introduce = req.data.Introduce,
            };

            return memberInfo;
        }

        //第一次建立
        public virtual Models.DbModels.MemberInfo SetMemberInfo(RegisReq req)
        {
            var memberInfo = new Models.DbModels.MemberInfo()
            {
                
            };
            return memberInfo;
        }


        public virtual  Models.DbModels.Directory SetDirectory(RegisReq req)
        {

            var directory = new Models.DbModels.Directory()
            {
                ContactList="",
            };
            return directory;

        }


        public virtual Models.DbModels.PreferType SetPreferData(PersonalReq req , int personalityid)
        {
            var prefer = new Models.DbModels.PreferType
            {
                MemberID = req.memberid,
                PersonalityID = personalityid
            };
            return prefer;


        }

        
        public virtual Models.DbModels.MemberInterest MemberInterestData(PersonalReq req, int interestid)
        {
            var inter = new Models.DbModels.MemberInterest
            {
                MemberID = req.memberid,
                InterestID = interestid
            };
            return inter;
        }

        /// <summary>
        /// 用戶基本資訊
        /// </summary>
        /// <param name="req"></param>
        public virtual void SaveMemberData(RegisReq req)
        {
            //註冊的時候把資料都建立起來
            var member = SetMember(req);
            SaveMemberData(member);
        }


        //這個好像用不到
        /// <summary>
        /// 個人訊息
        /// </summary>
        /// <param name="memberInfo"></param>
        public virtual void SaveMemberInfoData(SetReq req)
        {
            var memberInfo = SetMemberInfo(req);
            SaveMemberInfoData(memberInfo);
        }


        //修改個人資料
        public virtual void SaveMemberInfoData(PersonalReq req)
        {
            var memberlist = _context.Members.Include(mf =>mf.MemberInfo );
            var memberInfo = GetMemberInstance(memberlist, req.memberid);
            //修改值
            memberInfo.MemberInfo.NickName = req.data.nickname;
            memberInfo.MemberInfo.Job = req.data.job;
            memberInfo.MemberInfo.State = req.data.state;
            memberInfo.MemberInfo.Introduce = req.data.introduce;
            _context.SaveChanges();
        }


        //修改偏好類型
        public virtual void SavePreferType(PersonalReq req)
        {
            //取得實體
            var preferType = _context.PreferTypes.Include(pp => pp.Personality);

            //列出用戶的偏好類型
            var memInfo = preferType.Where(p => p.MemberID == req.memberid);

            //personality 字典
            var preferTypeDic = PreferTypeDic(preferType);

            //取得現有的idlist 與 輸入prefertype 的idlist
            List<int> inputIdList = GetIdList(req.data.preferType, preferTypeDic);
            List<int> NowIdList = GetIdList(memInfo.ToList(), preferTypeDic);

            //列出req 的preferType
            //var plist2 = new List<int>();
            //foreach (var i in req.data.preferType)
            //{
            //    foreach(var x in preferTypeDic)
            //    {
            //        if(x.Value == i)
            //        {
            //            //[1,2,6]
            //            plist2.Add(x.Key);
            //        }
            //    }
            //}

            //列出目前有的preferType
            //var plist3 = new List<int>();
            //foreach (var i in preferlist)
            //{
            //    foreach (var x in preferTypeDic)
            //    {
            //        if (x.Value == i.Personality.Kind)
            //        {
            //            //[1,3,5]
            //            plist3.Add(x.Key);
            //        }
            //    }
            //}

            //篩選出 要刪除的 跟要新增的(取差集)

            var addList = inputIdList.Except(NowIdList);
            var delList = NowIdList.Except(inputIdList);


            //要新增的
            //var list1 = new List<int>();
            

            //foreach(var i in inputIdList)
            //{
            //    if (!inputIdList.Contains(i))
            //    {
            //        list1.Add(i);
            //    }
            //}

            ////要刪除的
            //var list2 = new List<int>();
            //foreach (var i in NowIdList)
            //{
            //    if (!NowIdList.Contains(i))
            //    {
            //        list2.Add(i);
            //    }
            //}

            //添加
            foreach(var i in addList)
            {
                var prefer =SetPreferData(req,i);
                _context.Add(prefer);
            }
            //刪除
            foreach (var i in delList)
            {
                var delItem = memInfo.Where(x =>x.PersonalityID == i).FirstOrDefault();
                _context.PreferTypes.Remove(delItem);
            }

            _context.SaveChanges();

        }

        //修改興趣
        public virtual void SaveMemberInterest(PersonalReq req)
        {
            //取得實體
            var MemberInterest = _context.MemberInterests.Include(mi => mi.Interest);
            //列出用戶的興趣
            var memInfo = MemberInterest.Where(p => p.MemberID == req.memberid);
            //interest 字典
            var MemberItDic = MemberInterestDic(MemberInterest);
            //取得現有的idlist 與 輸入prefertype 的idlist
            List<int> inputIdList = GetIdList(req.data.interest, MemberItDic);
            List<int> NowIdList = GetIdList(memInfo.ToList(), MemberItDic);

            //篩選出 要刪除的 跟要新增的(取差集)

            var addList = inputIdList.Except(NowIdList);
            var delList = NowIdList.Except(inputIdList);

            //修改資料庫
            //添加
            foreach (var i in addList)
            {
                var inter = MemberInterestData(req, i);
                _context.Add(inter);
            }
            //刪除
            foreach (var i in delList)
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
        public virtual void SaveDirectoryData(FriendReq req,bool isAdd =true)
        {
            var memberlist = _context.Members.Include(md => md.Directory);
            var memberInfo = GetMemberInstance(memberlist, req.memberid);

            //取得用戶朋友的memberid
            string frinedId = memberInfo.Directory.ContactList;
            if (isAdd)
            {
                frinedId += $"、{req.friendid}";
            }
            else
            {
                string[] frinedList = frinedId.Split('、');
                frinedId = string.Join("、", frinedList.Where(i=> i != req.friendid.ToString()).Select(m=>m));
            }
            
            //更新值
            memberInfo.Directory.ContactList = frinedId;
            _context.SaveChanges();
        }


        public virtual Member GetMemberInstance(RegisReq req)
        {
            var member = _context.Members.Include(mf => mf.MemberInfo)
                                      .Include(mp => mp.Password)
                                      .AsNoTracking()
                                      .FirstOrDefault(m => string.Equals(m.Name, req.username, StringComparison.Ordinal));
            return member;
        }

        public virtual Member GetMemberInstance(LoginReq req)
        {
            var member = _context.Members.Include(mf => mf.MemberInfo)
                                      .Include(mp => mp.Password)
                                      .AsNoTracking()
                                      .FirstOrDefault(m => m.Name == req.username);
            return member;
        }

        

        /// <summary>
        /// 取得member實體
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Member GetMemberInstance(IQueryable<Member> memberInfo,int id)
        {
            var member = memberInfo.FirstOrDefault(m => m.ID == id);
            return member;
        }




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
            var memInfo = GetMemberInstance(memberInfo, id);
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
                    gender = item.Gender,
                    job = item.MemberInfo.Job,
                    state = item.MemberInfo.State,
                    introduce = item.MemberInfo.Introduce,
                    //intersert = string.Join("、", item.MemberInterests.Where(mi => mi.MemberID == item.ID).Select(mi => mi.Interest.Name)),
                    //preferType = string.Join("、", item.PreferTypes.Where(mi => mi.MemberID == item.ID).Select(mi => mi.Personality.Kind)),
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
        public Dictionary<int,string> PreferTypeDic(IQueryable<PreferType> preferType)
        {
            var preferTypeDic = new Dictionary<int, string>();
            foreach (var item in preferType)
            {
                preferTypeDic.Add(item.Personality.ID, item.Personality.Kind);
            }
            return preferTypeDic;
        }

        //返回 interest personality字典
        public Dictionary<int, string> MemberInterestDic(IQueryable<MemberInterest> memberInterest)
        {
            var preferTypeDic = new Dictionary<int, string>();
            foreach (var item in memberInterest)
            {
                preferTypeDic.Add(item.Interest.ID, item.Interest.Name);
            }
            return preferTypeDic;
        }

        //返回 memberInterest、 prefertype 這種 多對多關連資料表 對應的id
        public List<int> GetIdList<T>(IList<T> inputList, Dictionary<int, string> preferTypeDic) 
        {
            var idList = new List<int>();
            foreach (var i in inputList)
            {
                foreach (var x in preferTypeDic)
                {
                    if (x.Value == i.ToString())
                    {
                        //[1,2,6]
                        idList.Add(x.Key);
                    }
                }
            }

            return idList;
        }

    }
}
