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
    public class Login : ILogin
    {
        //注入DbContext
        private readonly MemberContext _context;
        public Login(MemberContext context)
        {
            _context = context;
        }

        public ResponseModel CheckUserExisted(LoginReq req)
        {

            LoginResp resp = new LoginResp();
            var memberInfo = _context.Members.Include(mf => mf.MemberInfo)
                                      .Include(mp => mp.Password).AsNoTracking();
            //用 == 會忽略大小寫
            var memberExisted = memberInfo.Any(m => string.Equals(m.Name,req.username,StringComparison.Ordinal));
            
            //沒有這個帳戶
            if(!memberExisted)
            {
                resp.Code = (int)RespCode.FAIL;
                resp.Msg = "用戶不存在，請先註冊";
                return resp;
            }

            var member = memberInfo.FirstOrDefault(m => m.Name == req.username);

            //密碼錯誤
            if (req.password!= member.Password.Code)
            {
                resp.Code = (int)RespCode.FAIL;
                resp.Msg = "密碼錯誤";
            }
            else
            {
                resp.Code = (int)RespCode.SUCCESS;
                resp.Msg = "登入成功";
                Data da = new Data() 
                {
                    Gender= member.MemberInfo.Gender,
                    MemberID= member.ID,
                    IsRegist = false,
                };          
                resp.data = da;
            }
            return resp;
        }
    }
}
