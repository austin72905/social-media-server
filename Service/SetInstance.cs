using SocialMedia.Models;
using SocialMedia.Models.DbModels;
using SocialMedia.Models.Personal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Service
{
    public static class SetInstance
    {
        /// <summary>
        /// 提供member 實體
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static Member SetMember(RegisReq req)
        {
            var password = SetPassword(req);
            var directory = SetDirectory(req);
            var memberInfo = SetMemberInfo(req);

            var member = new Member()
            {
                Name = req.username,
                Gender = req.gender,
                Password = password,
                Directory = directory,
                MemberInfo = memberInfo,

            };

            return member;
        }

        /// <summary>
        /// 提供password 實體
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static Password SetPassword(RegisReq req)
        {
            var password = new Password()
            {
                Code = req.password,
            };

            return password;
        }



        //第一次建立
        /// <summary>
        /// 提供要存在 MemberInfo 表的資料
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static Models.DbModels.MemberInfo SetMemberInfo(RegisReq req)
        {
            var memberInfo = new Models.DbModels.MemberInfo()
            {

            };
            return memberInfo;
        }

        /// <summary>
        /// 提供要存在 Directory 表的資料
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>

        public static Models.DbModels.Directory SetDirectory(RegisReq req)
        {

            var directory = new Models.DbModels.Directory()
            {
                ContactList = "",
            };
            return directory;

        }

        /// <summary>
        /// 提供要存在 PreferType 表的資料
        /// </summary>
        /// <param name="req"></param>
        /// <param name="personalityid"></param>
        /// <returns></returns>
        public static Models.DbModels.PreferType SetPreferData(PersonalReq req, int personalityid)
        {
            var prefer = new Models.DbModels.PreferType
            {
                MemberID = Convert.ToInt32(req.memberid),
                PersonalityID = personalityid
            };
            return prefer;


        }

        /// <summary>
        /// 提供要 存在 MemberInterest 表的資料
        /// </summary>
        /// <param name="req"></param>
        /// <param name="interestid"></param>
        /// <returns></returns>
        public static Models.DbModels.MemberInterest MemberInterestData(PersonalReq req, int interestid)
        {
            var inter = new Models.DbModels.MemberInterest
            {
                MemberID = Convert.ToInt32(req.memberid),
                InterestID = interestid
            };
            return inter;
        }
    }
}
