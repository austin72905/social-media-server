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
using System.Threading.Tasks;

namespace SocialMedia.Service
{
    public class Directory : DirectoryRepository, IDirectory
    {
        //注入DbContext
        //private readonly MemberContext _context;
        private readonly IErrorHandler _errorHandler;
        public Directory(MemberContext context, IErrorHandler errorHandler) :base(context)
        {
            //_context = context;
            _errorHandler = errorHandler;
        }

        /// <summary>
        /// 實作取得好友列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<FriendResp> GetFrinedList(int id)
        {
            FriendResp resp = new FriendResp();
            try
            {
                var memberInfo = await base.GetMemberListInstance().ToListAsync();

                var correctId = CheckMemberId(memberInfo, id);

                if (!correctId)
                {
                    resp.code = (int)RespCode.FAIL;
                    resp.msg = "用戶不存在，請重新登入";
                    return resp;
                }

                List<FriendData> friendlist = base.GetFrinedList(memberInfo, id);

                //回到用戶列表
                resp.code = (int)RespCode.SUCCESS;
                resp.msg = "取得用戶成功";
                resp.data = friendlist;
                //回到個人頁面

                return resp;
            }
            catch (Exception ex)
            {
                
                return _errorHandler.SysError(resp,ex.Message);
            }
            
        }

        /// <summary>
        /// 實作新增好友
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<FriendResp> AddFrined(FriendReq req)
        {
            FriendResp resp = new FriendResp();
            try
            {
                var memberInfo = base.GetMemberListInstance();
                var correctId = CheckMemberId(memberInfo, Convert.ToInt32(req.memberid));
                if (!correctId)
                {
                    resp.code = (int)RespCode.FAIL;
                    resp.msg = "用戶不存在，請重新登入";
                    return resp;
                }

                //儲存朋友
                await base.SaveDirectoryData(req);

                List<FriendData> friendlist = base.GetFrinedList(memberInfo, Convert.ToInt32(req.memberid));
                //回到用戶列表
                resp.code = (int)RespCode.SUCCESS;
                resp.msg = "取得用戶成功";
                resp.data = friendlist;

                return resp;
            }
            catch (Exception ex)
            {
                
                return _errorHandler.SysError(resp,ex.Message);
            }
            

        }

        /// <summary>
        /// 實作刪除好友
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>

        public async Task<FriendResp> DeleteFrined(FriendReq req)
        {
            FriendResp resp = new FriendResp();
            try
            {
                var memberInfo = base.GetMemberListInstance();
                var correctId = CheckMemberId(memberInfo, Convert.ToInt32(req.memberid));
                if (!correctId)
                {
                    resp.code = (int)RespCode.FAIL;
                    resp.msg = "用戶不存在，請重新登入";
                    return resp;
                }
                //刪除朋友
                await base.SaveDirectoryData(req, false);

                List<FriendData> friendlist = base.GetFrinedList(memberInfo, Convert.ToInt32(req.memberid));
                //回到用戶列表
                resp.code = (int)RespCode.SUCCESS;
                resp.msg = "取得用戶成功";
                resp.data = friendlist;

                return resp;
            }
            catch (Exception ex)
            {
                
                return _errorHandler.SysError(resp,ex.Message);
            }
            

        }

    }
}
