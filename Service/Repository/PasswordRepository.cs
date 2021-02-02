using Microsoft.EntityFrameworkCore;
using SocialMedia.Dbcontext;
using SocialMedia.Models;
using SocialMedia.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Service.Repository
{
    public class PasswordRepository : DBfactory
    {
        private readonly MemberContext _context;
        public PasswordRepository(MemberContext context) : base(context)
        {
            _context = context;
        }

        public Member GetMemberInstance(BasicReq req)
        {
            var member = _context.Members.Include(mf => mf.MemberInfo)
                                      .Include(mp => mp.Password)
                                      .AsNoTracking()
                                      .FirstOrDefault(m => string.Equals(m.Name, req.username, StringComparison.Ordinal));
            return member;
        }

        /// <summary>
        /// 檢查用戶名是否存在
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public bool CheckMemberExisted(RegisReq req)
        {
            var memberInfo = _context.Members.Include(mp => mp.Password).AsNoTracking();

            //用 == 會忽略大小寫
            var memberExisted = memberInfo.Any(m => string.Equals(m.Name, req.username, StringComparison.Ordinal));

            return memberExisted;
        }

        /// <summary>
        /// 檢查用戶名是否存在
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public bool CheckMemberExisted(LoginReq req)
        {
            var memberInfo = _context.Members.Include(mf => mf.MemberInfo)
                                      .Include(mp => mp.Password).AsNoTracking();
            //用 == 會忽略大小寫
            var memberExisted = memberInfo.Any(m => string.Equals(m.Name, req.username, StringComparison.Ordinal));

            return memberExisted;
        }

        /// <summary>
        /// 用戶基本資訊
        /// </summary>
        /// <param name="member"></param>
        public virtual void SaveMemberData(Member member)
        {
            _context.Add(member);
            _context.SaveChanges();
        }

        public virtual void SaveMemberData(RegisReq req)
        {
            //註冊的時候把資料都建立起來
            var member = SetInstance.SetMember(req);
            SaveMemberData(member);
        }

    }
}
