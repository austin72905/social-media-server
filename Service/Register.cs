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
    public class Register : IRegister
    {
        //注入DbContext
        private readonly MemberContext _context;
        public Register(MemberContext context)
        {
            _context = context;
        }

        public ResponseModel CheckUserExisted(RegisReq req) 
        {
            RegistResp resp = new RegistResp();
            
            var memberInfo = _context.Members.Include(mp=>mp.Password).AsNoTracking();

            //用 == 會忽略大小寫
            var memberExisted = memberInfo.Any(m => string.Equals(m.Name, req.username, StringComparison.Ordinal));
            

            if (memberExisted)
            {
                resp.Code = (int)RespCode.FAIL;
                resp.Msg = "用戶已存在";
                return resp;
            }

            //之後寫一個存入資料庫的方法
            var psdSaveData = new Password()
            {
                Code = req.password,
            };

            var memberSaveData = new Member()
            {
                Name = req.username,
                Password = psdSaveData,
            };
            _context.Add(memberSaveData);
            _context.SaveChanges();

            



            var member = _context.Members.Include(mf => mf.MemberInfo)
                                      .Include(mp => mp.Password)
                                      .AsNoTracking()
                                      .FirstOrDefault(m => string.Equals(m.Name, req.username, StringComparison.Ordinal));


            resp.Code = (int)RespCode.SUCCESS;
            resp.Msg = "註冊成功";
            Data da = new Data() 
            { 
                Gender= req.gender,
                MemberID= member.ID,
                IsRegist=true,
            };            
            resp.data = da;

            return resp;
        }
    }
}
