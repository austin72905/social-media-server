using System.Text.Json.Serialization;
using SocialMedia.Dbcontext;
using SocialMedia.Interface;
using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using SocialMedia.Enum;
using SocialMedia.Models.Register;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Models.DbModels;
using SocialMedia.Service.Repository;
using SocialMedia.MiddleWares;

namespace SocialMedia.Service
{
    public class Register : PasswordRepository, IRegister
    {
        //注入DbContext
        //private readonly MemberContext _context;
        private readonly IErrorHandler _errorHandler;
        public Register(MemberContext context, IErrorHandler errorHandler) : base(context)
        {
            //_context = context;
            _errorHandler = errorHandler;
        }

        /// <summary>
        /// 實作註冊功能
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<RegistResp> CheckUserExisted(RegisReq req) 
        {
            RegistResp resp = new RegistResp();

            //var memberInfo = _context.Members.Include(mp=>mp.Password).AsNoTracking();

            ////用 == 會忽略大小寫
            //var memberExisted = memberInfo.Any(m => string.Equals(m.Name, req.username, StringComparison.Ordinal));
            try
            {
                var memberExisted = base.CheckMemberExisted(req);

                if (memberExisted)
                {
                    resp.code = (int)RespCode.FAIL;
                    resp.msg = "用戶已存在";
                    return resp;
                }


                //如果用戶不存在就新增
                base.SaveMemberData(req);

                //取得用戶實體
                var member = await base.GetMemberInstance(req);

                resp.code = (int)RespCode.SUCCESS;
                resp.msg = "註冊成功";
                RegistData da = new RegistData()
                {
                    username = member.Name,
                    gender = member.Gender,
                    memberID = member.ID,
                    isRegist = true,
                };
                resp.data = da;
                //加入token
                string sign = VerifySign.GenerateSign(da.username);
                resp.token = sign;

                return resp;
            }
            catch (Exception ex)
            {               
                return _errorHandler.SysError(resp, ex.Message);
            }
            
        }
    }
}
