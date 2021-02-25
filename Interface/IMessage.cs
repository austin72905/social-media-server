using SocialMedia.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Interface
{
    public interface IMessage
    {
        public Task<MsgResp> GetMsg(string memberid, string recieveid);

        public Task<MsgLastResp> GetAllLastMsg(string memberid);

        public Task<MsgCountResp> GetUnreadMsg(string memberid);
        public Task UpdateToRead(string userid, string recieveid);
        //public (ChatResp chatSpeakerdata, ChatMsgLastData forSpeaker, ChatMsgLastData forReciever) SaveChatMsg(string userid, string recieveid, string input);
    }
}
