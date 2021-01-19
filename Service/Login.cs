using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SocialMedia.Dbcontext;
using SocialMedia.Enum;
using SocialMedia.Interface;
using SocialMedia.Models;
using SocialMedia.Models.Login;
using System;
using System.Linq;

namespace SocialMedia.Service
{
    public class Login :DBfactory,ILogin
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
            //var memberExisted = memberInfo.Any(m => string.Equals(m.Name,req.username,StringComparison.Ordinal));
            var memberExisted = base.CheckMemberExisted(req);
            //沒有這個帳戶
            if (!memberExisted)
            {
                resp.code = (int)RespCode.FAIL;
                resp.msg = "用戶不存在，請先註冊";
                return resp;
            }

            //var member = memberInfo.FirstOrDefault(m => m.Name == req.username);
            var member = base.GetMemberInstance(req);
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
