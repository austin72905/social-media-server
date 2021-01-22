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

namespace SocialMedia.Service
{
    public class Register : DBfactory,IRegister
    {
        //注入DbContext
        //private readonly MemberContext _context;
        public Register(MemberContext context) : base(context)
        {
            //_context = context;
        }

        /// <summary>
        /// 實作註冊功能
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public RegistResp CheckUserExisted(RegisReq req) 
        {
            RegistResp resp = new RegistResp();

            //var memberInfo = _context.Members.Include(mp=>mp.Password).AsNoTracking();

            ////用 == 會忽略大小寫
            //var memberExisted = memberInfo.Any(m => string.Equals(m.Name, req.username, StringComparison.Ordinal));
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
            var member = base.GetMemberInstance(req);

            resp.code = (int)RespCode.SUCCESS;
            resp.msg = "註冊成功";
            RegistData da = new RegistData() 
            { 
                gender= member.Gender,
                memberID= member.ID,
                isRegist=true,
            };            
            resp.data = da;

            return resp;
        }
    }
}
