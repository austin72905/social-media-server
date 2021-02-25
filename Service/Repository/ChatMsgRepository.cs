using SocialMedia.Dbcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialMedia.Models.DbModels;
using SocialMedia.Models.Message;
using SocialMedia.Models.SetCache;
using Microsoft.EntityFrameworkCore;

namespace SocialMedia.Service.Repository
{
    public class ChatMsgRepository : DBfactory
    {
        private readonly MemberContext _context;
        public ChatMsgRepository(MemberContext context) : base(context)
        {
            _context = context;
        }

        //存放聊天訊息
        public  async Task SaveChatMsg(string userid, string recieveid, string input, bool unread = false)
        {
            //把原本最新的塗銷
            var nowNewest =await  _context.ChatMsgs.Where(m => m.MemberID == Convert.ToInt32(userid) && m.ChatID == Convert.ToInt32(recieveid) && m.Newest == true).FirstOrDefaultAsync();
            //要先判斷如果是第一次聊天要先加
            if (nowNewest != null)
            {
                nowNewest.Newest = false;
            }


            var newNewestRecieve = await _context.ChatMsgs.Where(m => m.MemberID == Convert.ToInt32(recieveid) && m.ChatID == Convert.ToInt32(userid) && m.Newest == true).FirstOrDefaultAsync();
            //要先判斷如果是第一次聊天要先加
            if (newNewestRecieve != null)
            {
                newNewestRecieve.Newest = false;
            }

            //發送者要存入的資料
            var message = new ChatMsg()
            {
                MemberID = Convert.ToInt32(userid),
                ChatID = Convert.ToInt32(recieveid),
                SpeakerID = Convert.ToInt32(userid),
                Text = input,
                Time = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),
                Newest = true

            };

            //接收者要存入的資料
            var recieverMsg = new ChatMsg()
            {
                MemberID = Convert.ToInt32(recieveid),
                ChatID = Convert.ToInt32(userid),
                SpeakerID = Convert.ToInt32(userid),
                Text = input,
                Time = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),
                Unread = true,
                Newest = true
            };

            _context.ChatMsgs.Add(message);
            _context.ChatMsgs.Add(recieverMsg);
            await _context.SaveChangesAsync();
        }

        protected async Task<Dictionary<string, int>> GetUnreadDic(string memberid)
        {
            var msgList =await  _context.ChatMsgs.Where(msg => msg.MemberID == Convert.ToInt32(memberid)).ToListAsync();

            var tempSet = new HashSet<string>();

            //取得每個聊天室id (hashset 會去到重複值)
            foreach (var item in msgList)
            {
                tempSet.Add(item.ChatID.ToString());
            }

            //最後要返回的未讀數量 (每個id 的未讀數量個別計算)
            var resultDic = new Dictionary<string, int>();

            foreach (var key in tempSet)
            {
                resultDic.Add(key, 0);
            }

            foreach (var item in msgList)
            {
                if (resultDic.ContainsKey(item.ChatID.ToString()) && item.Unread)
                {
                    resultDic[item.ChatID.ToString()] += 1;
                }
            }

            return resultDic;


        }

        //目前沒用
        public int GetUnreadCount(string memberid)
        {
            var msgList = _context.ChatMsgs.Where(msg => msg.MemberID == Convert.ToInt32(memberid));
            int unReadCount = 0;
            foreach (var msg in msgList)
            {
                if (msg.Unread == true)
                {
                    unReadCount += 1;
                }
            }

            return unReadCount;
        }

        public async Task<List<ChatMsgLastData>> GetAllLastMsgList(string memberid)
        {
            //之前寫的要再加一個時間戳，不然沒辦法排序
            //取得訊息符合memberid 的 newest == true 的
            //搞成list
            var memMsg =await  _context.ChatMsgs.Where(m => m.MemberID == Convert.ToInt32(memberid) && m.Newest == true).ToListAsync();

            var userData = await GetMemberListInstance().ToListAsync();

            //最後要回傳的list
            List<ChatMsgLastData> msgLastList = new List<ChatMsgLastData>();

            foreach (var item in memMsg)
            {
                int unreadCount = 0;
                Member chatUser = userData.Where(user => user.ID == item.ChatID).FirstOrDefault();
                //找到每個對應的訊息組
                var chatMsgSet =await  _context.ChatMsgs.Where(msg => msg.MemberID == Convert.ToInt32(memberid) && msg.ChatID == item.ChatID).ToListAsync();
                foreach (var msg in chatMsgSet)
                {
                    if (msg.Unread == true)
                    {
                        unreadCount += 1;
                    }
                }

                msgLastList.Add(new ChatMsgLastData
                {
                    username = chatUser.Name,
                    memberid = item.SpeakerID,
                    gender = chatUser.Gender,
                    text = item.Text,
                    chatname = chatUser.MemberInfo.NickName,
                    chatid = chatUser.ID.ToString(),
                    unreadcount = unreadCount

                });
            }

            return msgLastList;
        }

        protected async Task<List<ChatMsgData>> GetMsgList(string memberid, string recieveid)
        {
            var msgItem = _context.ChatMsgs.Where(m => m.MemberID == Convert.ToInt32(memberid) && m.ChatID == Convert.ToInt32(recieveid));//.OrderBy(m =>long.Parse( m.Time))
            var Msglist = new List<ChatMsgData>();

            //用戶及他正在聊天的用戶的基本資料
            //var userData = GetMemberListInstance().FirstOrDefault(m => m.ID == Convert.ToInt32(memberid));
            //var recieverData = GetMemberListInstance().FirstOrDefault(m => m.ID == Convert.ToInt32(recieveid));
            foreach (var item in msgItem)
            {
                var speaker =await  _context.Members.FirstOrDefaultAsync(m => m.ID == Convert.ToInt32(item.SpeakerID));
                //var gender = await _context.Members.FirstOrDefaultAsync(m => m.ID == Convert.ToInt32(item.SpeakerID));

                Msglist.Add(new ChatMsgData
                {
                    username = speaker.Name,
                    memberid = item.SpeakerID,
                    gender = speaker.Gender,
                    text = item.Text,
                    unread = item.Unread
                });
            }

            return Msglist;
        }

        //修改資料庫為已讀
        public async Task UpdateDBToRead(string userid, string recieveid)
        {
            var updateItem =await  _context.ChatMsgs.Where(m => m.MemberID == Convert.ToInt32(userid)).ToListAsync();

            foreach (var item in updateItem)
            {
                if (item.ChatID == Convert.ToInt32(recieveid))
                {
                    //修改成已讀
                    item.Unread = false;
                }

            }

            await _context.SaveChangesAsync();
        }

        //刷新chathub 緩存
        public async Task<List<MemberData>> GetMemListToChat()
        {
            var chatmemlist = new List<MemberData>();
            var memlist =await  _context.Members.Include(m => m.MemberInfo).ToListAsync();

            foreach (var item in memlist)
            {
                chatmemlist.Add(new MemberData
                {
                    username = item.Name,
                    memberid = item.ID,
                    gender = item.Gender,
                    nickname = item.MemberInfo.NickName
                });
            }

            return chatmemlist;
        }
    }
}
