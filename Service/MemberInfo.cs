using Microsoft.EntityFrameworkCore;
using SocialMedia.Dbcontext;
using SocialMedia.Enum;
using SocialMedia.Interface;
using SocialMedia.Models.DbModels;
using SocialMedia.Models.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Service
{
    public class MemberInfo :ISetting
    {
        //注入DbContext
        private readonly MemberContext _context;
        public MemberInfo(MemberContext context)
        {
            _context = context;
        }

        //修改成 儲存變更
        public SetResp GetMemberInfo(SetReq req) 
        {
            SetResp resp = new SetResp();

            var memberInfo = _context.Members.Include(mf => mf.MemberInfo)
                                      .Include(mi => mi.MemberInterests)
                                        .ThenInclude(i=>i.Interest)
                                      .Include(mt => mt.PreferTypes)
                                        .ThenInclude(k =>k.Personality)
                                      .AsNoTracking();

            var correctId = CheckId.CheckMemberId(memberInfo, req.memberid);
            //var correctId = memberInfo.Any(m=> m.ID ==req.memberid);

            if (!correctId)
            {
                resp.Code=(int)RespCode.FAIL;
                resp.Msg = "用戶不存在，請重新登入";
                return resp;
            }


            //存資料
            var saveData = new Models.DbModels.MemberInfo() 
            { 
                MemberID=req.memberid,
                Gender=req.data.Gender,
                Job=req.data.Job,
                State=req.data.State,
                Introduce=req.data.Introduce,

            };

            _context.Add(saveData);
            _context.SaveChanges();

            //
            var interdatalist = from inter in _context.Interests
                                select inter;

            List<string> interlist = new List<string>();

            foreach(var item in req.data.Interests)
            {
                interlist.Add(interdatalist.Where(m=>m.Name == item).Select(m=>m.ID).ToString());
            }

            var interSaveData = new MemberInterest()
            {
                MemberID=req.memberid,
                InterestID = 1,
            };

            var memberlist = new List<RespData>();
            foreach (var item in memberInfo)
            {
                memberlist.Add(new RespData()
                {
                    Gender =item.MemberInfo.Gender,
                    Job = item .MemberInfo.Job,
                    State =item.MemberInfo.State,
                    Introduce=item.MemberInfo.Introduce,
                    Intersert=string.Join("、",item.MemberInterests.Where(mi => mi.MemberID == item.ID).Select(mi =>mi.Interest.Name)),
                    PreferType = string.Join("、", item.PreferTypes.Where(mi => mi.MemberID == item.ID).Select(mi => mi.Personality.Kind)),
                });
            }
            
            //回到用戶列表
            resp.Code = (int)RespCode.SUCCESS;
            resp.Msg = "取得用戶成功";
            resp.data = memberlist;
            //回到個人頁面


            return resp;
        }


        //get 實作
        public SetResp GetMemberInfo(int? id)
        {
            SetResp resp = new SetResp();

            var memberInfo = _context.Members.Include(mf => mf.MemberInfo)
                                      .Include(mi => mi.MemberInterests)
                                        .ThenInclude(i => i.Interest)
                                      .Include(mt => mt.PreferTypes)
                                        .ThenInclude(k => k.Personality)
                                      .AsNoTracking();

            //var correctId = memberInfo.Any(m => m.ID == id);
            var correctId = CheckId.CheckMemberId(memberInfo, id);
            if (!correctId)
            {
                resp.Code = (int)RespCode.FAIL;
                resp.Msg = "用戶不存在，請重新登入";
                return resp;
            }

            
            var memInfo = memberInfo.FirstOrDefault(m => m.ID == id);
            var memberlist = new List<RespData>();

            memberlist.Add(new RespData()
            {
                MemberName =memInfo.Name,
                Gender = memInfo.MemberInfo.Gender,
                Job = memInfo.MemberInfo.Job,
                State = memInfo.MemberInfo.State,
                Introduce = memInfo.MemberInfo.Introduce,
                Intersert = string.Join("、", memInfo.MemberInterests.Where(mi => mi.MemberID == memInfo.ID).Select(mi => mi.Interest.Name)),
                PreferType = string.Join("、", memInfo.PreferTypes.Where(mi => mi.MemberID == memInfo.ID).Select(mi => mi.Personality.Kind)),
            });


            //回到用戶列表
            resp.Code = (int)RespCode.SUCCESS;
            resp.Msg = "取得用戶成功";
            resp.data = memberlist;
            //回到個人頁面


            return resp;
        }

        //取得用戶列表
        public SetResp GetMemberList(int? id)
        {
            SetResp resp = new SetResp();
            var memberInfo = _context.Members.Include(mf => mf.MemberInfo)
                                      .Include(mi => mi.MemberInterests)
                                        .ThenInclude(i => i.Interest)
                                      .Include(mt => mt.PreferTypes)
                                        .ThenInclude(k => k.Personality)
                                      .AsNoTracking();
            //var correctId = memberInfo.Any(m => m.ID == id);
            var correctId = CheckId.CheckMemberId(memberInfo, id);
            if (!correctId)
            {
                resp.Code = (int)RespCode.FAIL;
                resp.Msg = "用戶不存在，請重新登入";
                return resp;
            }
            var memberlist = new List<RespData>();
            foreach(var item in memberInfo)
            {
                memberlist.Add(new RespData()
                {
                    Gender = item.MemberInfo.Gender,
                    Job = item.MemberInfo.Job,
                    State = item.MemberInfo.State,
                    Introduce = item.MemberInfo.Introduce,
                    Intersert = string.Join("、", item.MemberInterests.Where(mi => mi.MemberID == item.ID).Select(mi => mi.Interest.Name)),
                    PreferType = string.Join("、", item.PreferTypes.Where(mi => mi.MemberID == item.ID).Select(mi => mi.Personality.Kind)),
                });
            }

            resp.Code = (int)RespCode.SUCCESS;
            resp.Msg = "取得用戶成功";
            resp.data = memberlist;

            return resp;
        }


        public TypeResp GetSelectOption()
        {
            TypeResp resp = new TypeResp();
            var types = from type in _context.Personalitys
                        select type;
            var interests = from interest in _context.Interests
                            select interest;
            List<string> typelist = new List<string>();
            List<string> interlist = new List<string>();
            foreach(var t in types)
            {
                typelist.Add(t.Kind);
            }
            foreach(var i in interests)
            {
                interlist.Add(i.Name);
            }

            OptionData opdata = new OptionData()
            {
                TypeData=typelist,
                Interestdata=interlist,
            };


            //返回訊息
            resp.Code = (int)RespCode.SUCCESS;
            resp.Msg = "取得選項成功";
            resp.OptionData = opdata;

            return resp;
        }
    }

    
}
