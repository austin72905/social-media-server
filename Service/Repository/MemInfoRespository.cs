using Microsoft.EntityFrameworkCore;
using SocialMedia.Dbcontext;
using SocialMedia.Interface.Repository;
using SocialMedia.Models.DbModels;
using SocialMedia.Models.Personal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Service.Repository
{
    public class MemInfoRespository: DBfactory
    {
        private readonly MemberContext _context;
        
        public MemInfoRespository(MemberContext context) : base(context)
        {
            _context = context;
            
        }
        //修改個人資料
        public void UpdateMemberInfoData(PersonalReq req)
        {
            var memberInfo = _context.Members.Include(mf => mf.MemberInfo).FirstOrDefault(m => m.ID == req.memberid);
            //var memberInfo = GetMemberInstance(memberlist, req.memberid);
            //修改值
            memberInfo.MemberInfo.NickName = req.data.nickname;
            memberInfo.MemberInfo.Job = req.data.job;
            memberInfo.MemberInfo.State = req.data.state;
            memberInfo.MemberInfo.Introduce = req.data.introduce;
            _context.SaveChanges();
        }

       
    }
}
