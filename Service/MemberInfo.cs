using Microsoft.EntityFrameworkCore;
using SocialMedia.Dbcontext;
using SocialMedia.Enum;
using SocialMedia.Interface;
using SocialMedia.Models.DbModels;
using SocialMedia.Models.Memberinfo;
using SocialMedia.Models.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Service
{
    public class MemberInfo :DBfactory,IMemberinfo
    {
        //注入DbContext
        //private readonly MemberContext _context;

        private readonly ILogMan _logMan;
        private readonly IErrorHandler _errorHandler;
        public MemberInfo(MemberContext context, ILogMan logMan, IErrorHandler errorHandler) :base(context)
        {
            // _context = context;
            _logMan = logMan;
            _errorHandler = errorHandler;
        }

        /// <summary>
        /// 實作取得用戶列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MemberinfoResp> GetMemberList(int id)
        {
            MemberinfoResp resp = new MemberinfoResp();
            try
            {
                var memberInfo = await base.GetMemberListInstance().ToListAsync();

                var correctId = DBfactory.CheckMemberId(memberInfo, id);
                if (!correctId)
                {
                    resp.code = (int)RespCode.FAIL;
                    resp.msg = "用戶不存在，請重新登入";
                    return resp;
                }

                //要返回的列表
                var memberlist = new List<Models.Memberinfo.MemberinfoData>();
                foreach (var item in memberInfo)
                {
                    memberlist.Add(new Models.Memberinfo.MemberinfoData()
                    {
                        username = item.Name,
                        nickname = item.MemberInfo.NickName,
                        gender = item.Gender,
                        memberID = item.ID,
                        job = item.MemberInfo.Job,
                        state = item.MemberInfo.State,
                        introduce = item.MemberInfo.Introduce,
                        //intersert = string.Join("、", item.MemberInterests.Where(mi => mi.MemberID == item.ID).Select(mi => mi.Interest.Name)),
                        //preferType = string.Join("、", item.PreferTypes.Where(mi => mi.MemberID == item.ID).Select(mi => mi.Personality.Kind)),
                    });
                }

                resp.code = (int)RespCode.SUCCESS;
                resp.msg = "取得用戶成功";
                resp.data = memberlist;

                return resp;
            }
            catch (Exception ex)
            {               
                return _errorHandler.SysError(resp, ex.Message);
            }
            

            
        }

        

        //新增興趣
        public object AddInterest1(Interest interest)
        {
            var result = base.AddInterest(interest);
            //_context.Add(interest);
            //_context.SaveChanges();


            return result;
        }

        //新增類型
        public object AddPersonality1(Personality personality)
        {
            var result = base.AddPersonality(personality);
            //_context.Add(personality);
            //_context.SaveChanges();


            return result;
        }

       


        

    }

    
}
