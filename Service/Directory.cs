using Microsoft.EntityFrameworkCore;
using SocialMedia.Dbcontext;
using SocialMedia.Enum;
using SocialMedia.Interface;
using SocialMedia.Models.DbModels;
using SocialMedia.Models.Friends;
using SocialMedia.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialMedia.Service
{
    public class Directory : DirectoryRepository, IDirectory
    {
        //注入DbContext
        //private readonly MemberContext _context;
        public Directory(MemberContext context):base(context)
        {
            //_context = context;
        }

        /// <summary>
        /// 實作取得好友列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FriendResp GetFrinedList(int id)
        {
            FriendResp resp = new FriendResp();

            var memberInfo = base.GetMemberListInstance();
            
            var correctId = CheckMemberId(memberInfo, id);
            
            if (!correctId)
            {
                resp.code = (int)RespCode.FAIL;
                resp.msg = "用戶不存在，請重新登入";
                return resp;
            }

            List<FriendData> friendlist = base.GetFrinedList(memberInfo,id);
   
            //回到用戶列表
            resp.code = (int)RespCode.SUCCESS;
            resp.msg = "取得用戶成功";
            resp.data = friendlist;
            //回到個人頁面

            return resp;
        }

        /// <summary>
        /// 實作新增好友
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public FriendResp AddFrined(FriendReq req)
        {
            FriendResp resp = new FriendResp();

            var memberInfo = base.GetMemberListInstance();
            var correctId = CheckMemberId(memberInfo, Convert.ToInt32(req.memberid));        
            if (!correctId)
            {
                resp.code = (int)RespCode.FAIL;
                resp.msg = "用戶不存在，請重新登入";
                return resp;
            }

            //儲存朋友
            base.SaveDirectoryData(req);

            List<FriendData> friendlist = base.GetFrinedList(memberInfo,Convert.ToInt32(req.memberid));
            //回到用戶列表
            resp.code = (int)RespCode.SUCCESS;
            resp.msg = "取得用戶成功";
            resp.data = friendlist;

            return resp;

        }

        /// <summary>
        /// 實作刪除好友
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>

        public FriendResp DeleteFrined(FriendReq req)
        {
            FriendResp resp = new FriendResp();
            var memberInfo = base.GetMemberListInstance();
            var correctId = CheckMemberId(memberInfo, Convert.ToInt32(req.memberid));
            if (!correctId)
            {
                resp.code = (int)RespCode.FAIL;
                resp.msg = "用戶不存在，請重新登入";
                return resp;
            }
            //刪除朋友
            base.SaveDirectoryData(req,false);

            List<FriendData> friendlist = base.GetFrinedList(memberInfo, Convert.ToInt32(req.memberid));
            //回到用戶列表
            resp.code = (int)RespCode.SUCCESS;
            resp.msg = "取得用戶成功";
            resp.data = friendlist;

            return resp;

        }

    }
}
