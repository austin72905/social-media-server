﻿using SocialMedia.Dbcontext;
using SocialMedia.Enum;
using SocialMedia.Interface;
using SocialMedia.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Service
{
    public class Message : DBfactory,IMessage
    {
        public Message(MemberContext context) : base(context)
        {
            //_context = context;
        }

        //獲取聊天訊息
        public MsgResp GetMsg(string memberid,string recieveid) 
        {
            var resp = new MsgResp();

            var memberInfo = base.GetMemberListInstance();

            //檢查id
            var correctId = DBfactory.CheckMemberId(memberInfo, Convert.ToInt32(memberid));
            var correctRecieveId = DBfactory.CheckMemberId(memberInfo, Convert.ToInt32(recieveid));
            if (!correctId || !correctRecieveId)
            {
                resp.code = (int)RespCode.FAIL;
                resp.msg = "用戶不存在，請重新登入";
                resp.data = new List<ChatMsgData>();
                return resp;
            }

            var msgList = base.GetMsgList(memberid, recieveid);


            resp.code = (int)RespCode.SUCCESS;
            resp.msg = "獲取訊息成功";
            resp.data = msgList;



            return resp;
        }

        //要傳到message 組件的資訊
        //對所有對話紀錄的最後一筆對話
        public MsgLastResp GetAllLastMsg(string memberid) 
        {
            var resp = new MsgLastResp
            {
                code = 0,
                msg = "獲取訊息成功",

            };
            //之前寫的要再加一個時間戳，不然沒辦法排序
            return resp;

        }

        //實作存放聊天訊息
        public (ChatResp chatSpeakerdata, ChatMsgLastData forSpeaker, ChatMsgLastData forReciever) SaveChatMsg(string userid, string recieveid, string input)
        {
            //1. 先取得 message 實體 m==userid
            var chatSpeakerdata = GetMsgToChat(userid);
            //2. 新增訊息進去 新增一個 setChatMsg 方法 準備要存入的資料
            base.SaveChatMsg(userid, recieveid, input);
            //2. 取得 member 實體  UserName  跟 Gender 準備好， 之後要回傳訊息
            var speakerData = base.GetLastMsgData(userid, recieveid, input);


            return (chatSpeakerdata, speakerData.forSpeaker,speakerData.forReciever);
        }

        //實作已讀時修改資料庫
        public void UpdateToRead(string userid, string recieveid)
        {
            base.UpdateDBToRead(userid, recieveid);
        }

    }
}