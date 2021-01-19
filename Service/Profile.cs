using Microsoft.EntityFrameworkCore;
using SocialMedia.Dbcontext;
using SocialMedia.Enum;
using SocialMedia.Interface;
using SocialMedia.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Service
{
    public class Profile: DBfactory,IProfile
    {
        //注入DbContext
        //private readonly MemberContext _context;
        public Profile(MemberContext context):base(context)
        {
           // _context = context;
        }

        /// <summary>
        /// 實作取得用戶詳細資料
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public ProfileResp GetMemberDetail(int id,string username)
        {
            ProfileResp resp = new ProfileResp();
            var memberInfo = base.GetMemberListInstance();
            
            var correctId = CheckMemberId(memberInfo, id);
            if (!correctId)
            {
                resp.code = (int)RespCode.FAIL;
                resp.msg = "用戶不存在，請重新登入";
                return resp;
            }

            //取得對應用戶的資料
            var memInfo = base.GetMemberInstance(memberInfo,Convert.ToInt32(username));
            

            // 返回對應用戶資料
            ProfileData da = new ProfileData()
            {
                username = memInfo.ID.ToString(),
                nickname = memInfo.MemberInfo.NickName,
                gender = memInfo.Gender,
                job = memInfo.MemberInfo.Job,
                state = memInfo.MemberInfo.State,
                introduce = memInfo.MemberInfo.Introduce,
                interest = string.Join("、", memInfo.MemberInterests.Where(mi => mi.MemberID == memInfo.ID).Select(mi => mi.Interest.Name)),
                preferType = string.Join("、", memInfo.PreferTypes.Where(mi => mi.MemberID == memInfo.ID).Select(mi => mi.Personality.Kind)),
            };
            //回到用戶列表
            resp.code = (int)RespCode.SUCCESS;
            resp.msg = "取得用戶成功";
            resp.data = da;
            //回到個人頁面


            return resp;
        }
    }
}
