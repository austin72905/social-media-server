using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SocialMedia.Common;
using SocialMedia.Dbcontext;
using SocialMedia.Enum;
using SocialMedia.Interface;
using SocialMedia.MiddleWares;
using SocialMedia.Models;
using SocialMedia.Models.Login;
using SocialMedia.Service.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Service
{
  
    public class Login : PasswordRepository, ILogin
    {
        //注入DbContext
        //private readonly MemberContext _context;

        private readonly ILogMan _logMan;
        public Login(MemberContext context, ILogMan logMan) :base(context)
        {
            //_context = context;
            _logMan = logMan;
        }

        public async Task<LoginResp> CheckUserExisted(LoginReq req)
        {

            LoginResp resp = new LoginResp();
            //var memberInfo = _context.Members.Include(mf => mf.MemberInfo)
            //                          .Include(mp => mp.Password).AsNoTracking();
            ////用 == 會忽略大小寫
            ///
            try
            {
                var member = await base.GetMemberInstance(req);
                //沒有這個帳戶
                if (member == null)
                {
                    resp.code = (int)RespCode.FAIL;
                    resp.msg = "用戶不存在，請先註冊";
                    return resp;
                }

                //密碼錯誤
                if (req.password != member.Password.Code)
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
                        username = member.Name,
                        gender = member.Gender,
                        memberID = member.ID,
                        isRegist = false,
                    };
                    //加入token
                    string sign = VerifySign.GenerateSign(da.username);
                    resp.token = sign;
                    resp.data = da;
                }
                return resp;

            }
            catch(Exception ex)
            {
                resp.code= (int)RespCode.FAIL;
                resp.msg = "系統內部異常";
                _logMan.Appendline($"System Error : {ex.Message}");
                return resp;
            }
            
            
        }
    }
}
