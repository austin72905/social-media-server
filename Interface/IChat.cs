using SocialMedia.Models.Message;
using SocialMedia.Models.SetCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Interface
{
    public interface IChat
    {
        public (ChatResp chatSpeakerdata, ChatMsgLastData forSpeaker, ChatMsgLastData forReciever) SaveChatMsg(string userid, string recieveid, string input);

        public void UpdateToRead(string userid, string recieveid);

        public void SaveMsgData(string userid, string recieveid,string input);

        public ChatMemResp GetChatMemList();
    }
}
