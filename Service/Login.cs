﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SocialMedia.Dbcontext;
using SocialMedia.Enum;
using SocialMedia.Interface;
using SocialMedia.Models;
using SocialMedia.Models.Login;
using SocialMedia.Service.Repository;
using System;
using System.Linq;

namespace SocialMedia.Service
{
    public class Login : PasswordRepository, ILogin
    {
        //注入DbContext
        //private readonly MemberContext _context;
        public Login(MemberContext context):base(context)
        {
            //_context = context;
        }

        public LoginResp CheckUserExisted(LoginReq req)
        {

            LoginResp resp = new LoginResp();
            //var memberInfo = _context.Members.Include(mf => mf.MemberInfo)
            //                          .Include(mp => mp.Password).AsNoTracking();
            ////用 == 會忽略大小寫
            
            var member = base.GetMemberInstance(req);
            //沒有這個帳戶
            if (member == null)
            {
                resp.code = (int)RespCode.FAIL;
                resp.msg = "用戶不存在，請先註冊";
                return resp;
            }

            //密碼錯誤
            if (req.password!= member.Password.Code)
            {
                resp.code = (int)RespCode.FAIL;
                resp.msg = "密碼錯誤";
            }
            else
            {
                resp.code = (int)RespCode.SUCCESS;
                resp.msg = "登入成功";
                LoginData da = new LoginData() 
                {
                    gender= member.Gender,
                    memberID= member.ID,
                    isRegist = false,
                };          
                resp.data = da;
            }
            return resp;
        }
    }
}
