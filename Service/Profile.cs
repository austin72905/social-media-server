﻿using Microsoft.EntityFrameworkCore;
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
        public ProfileResp GetMemberDetail(int id,int userid)
        {
            ProfileResp resp = new ProfileResp();
            //var memberInfo = base.GetMemberListInstance();


            var memberInfo = base.GetMemberListInstance().FirstOrDefault(m => m.ID == id);
            //var correctId = CheckMemberId(memberInfo, id);
            if (memberInfo == null)
            {
                resp.code = (int)RespCode.FAIL;
                resp.msg = "用戶不存在，請重新登入";
                return resp;
            }

            //取得對應用戶的資料
            var QueryMemInfo = base.GetMemberListInstance().FirstOrDefault(m => m.ID == userid);
            
            //查詢的用戶不存在
            if(QueryMemInfo == null)
            {
                resp.code = (int)RespCode.FAIL;
                resp.msg = "此用戶不存在";
                return resp;
            }

            // 返回對應用戶資料
            ProfileData da = new ProfileData()
            {
                username = QueryMemInfo.ID.ToString(),
                nickname = QueryMemInfo.MemberInfo.NickName,
                gender = QueryMemInfo.Gender,
                job = QueryMemInfo.MemberInfo.Job,
                state = QueryMemInfo.MemberInfo.State,
                introduce = QueryMemInfo.MemberInfo.Introduce,
                interest = string.Join("、", QueryMemInfo.MemberInterests.Where(mi => mi.MemberID == QueryMemInfo.ID).Select(mi => mi.Interest.Name)),
                preferType = string.Join("、", QueryMemInfo.PreferTypes.Where(mi => mi.MemberID == QueryMemInfo.ID).Select(mi => mi.Personality.Kind)),
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
