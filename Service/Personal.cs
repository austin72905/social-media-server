using Microsoft.EntityFrameworkCore;
using SocialMedia.Dbcontext;
using SocialMedia.Enum;
using SocialMedia.Interface;
using SocialMedia.Models.DbModels;
using SocialMedia.Models.Personal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Service
{
    public class Personal: DBfactory,IPersonal
    {
        //注入DbContext
        //private readonly MemberContext _context;
        public Personal(MemberContext context):base(context)
        {
            //_context = context;
        }

        /// <summary>
        /// 實作獲取個人資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PersonalResp GetPersonalInfo(int id)
        {
            PersonalResp resp = new PersonalResp();
            //var memberInfo = base.GetMemberListInstance();
            
            //var correctId = DBfactory.CheckMemberId(memberInfo, id);
            //if (!correctId)
            //{
            //    resp.code = (int)RespCode.FAIL;
            //    resp.msg = "用戶不存在，請重新登入";
            //    return resp;
            //}

            var memInfo = base.GetMemberInstance(id);
            if ( memInfo == null)
            {
                resp.code = (int)RespCode.FAIL;
                resp.msg = "用戶不存在，請重新登入";
                return resp;
            }


            //返回個人訊息
            PersonalData da = new PersonalData()
            {
                memberID =memInfo.ID,
                username= memInfo.Name.ToString(),
                nickname=memInfo.MemberInfo.NickName,
                gender =memInfo.Gender,
                job =memInfo.MemberInfo.Job,
                state=memInfo.MemberInfo.State,
                introduce=memInfo.MemberInfo.Introduce,
                interest = string.Join("、", memInfo.MemberInterests.Where(mi => mi.MemberID == memInfo.ID).Select(mi => mi.Interest.Name)),
                preferType = string.Join("、", memInfo.PreferTypes.Where(mi => mi.MemberID == memInfo.ID).Select(mi => mi.Personality.Kind)),
            };
            
            resp.code = (int)RespCode.SUCCESS;
            resp.msg = "取得用戶成功";
            resp.data = da;
            


            return resp;
        }

        /// <summary>
        /// 修改用戶資料
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public PersonalResp UpdatePersonalInfo(PersonalReq req)
        {
            PersonalResp resp = new PersonalResp();

            var memberListInfo = base.GetMemberListInstance();

            //驗證id
            var correctId = DBfactory.CheckMemberId(memberListInfo, req.memberid);
            if (!correctId)
            {
                resp.code = (int)RespCode.FAIL;
                resp.msg = "用戶不存在，請重新登入";
                return resp;
            }

            //修改資料
            base.SaveMemberInfoData(req);
            //修改prefertype 跟 interest
            base.SavePreferType(req);
            base.SaveMemberInterest(req);

            var memInfo = memberListInfo.FirstOrDefault(m => m.ID == req.memberid);

            //返回個人訊息
            PersonalData da = new PersonalData()
            {
                memberID = memInfo.ID,
                username = memInfo.Name.ToString(),
                nickname = memInfo.MemberInfo.NickName,
                gender = memInfo.Gender,
                job = memInfo.MemberInfo.Job,
                state = memInfo.MemberInfo.State,
                introduce = memInfo.MemberInfo.Introduce,
                interest = string.Join("、", memInfo.MemberInterests.Where(mi => mi.MemberID == memInfo.ID).Select(mi => mi.Interest.Name)),
                preferType = string.Join("、", memInfo.PreferTypes.Where(mi => mi.MemberID == memInfo.ID).Select(mi => mi.Personality.Kind)),
            };

            resp.code = (int)RespCode.SUCCESS;
            resp.msg = "取得用戶成功";
            resp.data = da;


            return resp;
        }

        /// <summary>
        /// 實作獲取興趣、類型取項
        /// </summary>
        /// <returns></returns>
        public SOResp GetSelectOption()
        {
            SOResp resp = new SOResp();
            //取得選項
            var types = base.GetPersonalInstance();
            var interests = base.GetInterestInstance();

            List<string> typelist = new List<string>();
            List<string> interlist = new List<string>();

            foreach (var t in types)
            {
                typelist.Add(t.Kind);
            }
            foreach (var i in interests)
            {
                interlist.Add(i.Name);
            }

            SOData dalists = new SOData() 
            {
                interests= interlist,
                preferTypes = typelist
            };
            
            //返回訊息
            resp.code = (int)RespCode.SUCCESS;
            resp.msg = "取得選項成功";
            resp.data = dalists;

            return resp;
        }

        


    }
}
