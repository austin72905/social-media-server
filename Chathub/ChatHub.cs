using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Dbcontext;
using SocialMedia.Interface;
using SocialMedia.Models.Message;
using SocialMedia.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SocialMedia.Chathub.ConnectionList;

namespace SocialMedia.Chathub
{
    public class ChatHub : Hub
    {
        //注入DbContext
        private readonly IChat _iChat;
        public ChatHub(IChat chat)
        {
            _iChat = chat;
        }


        //private IServiceProvider _serviceProvider;
        //public ChatHub(IServiceProvider serviceProvider)
        //{
        //    _serviceProvider = serviceProvider;
        //}


        public async Task SendAllMsg(string user,string input) 
        {
            var content = $"{user} 說了 {input}";

            await Clients.All.SendAsync("",content);
        }

        public async Task SendGroupMsg(string user, string input)
        {
            var content = $"{user} 說了 {input}";

            await Clients.Group("").SendAsync("", content);
        }

        public async Task SendOneMsg(string userid, string recieveid, string input)
        {
            var content = $"{userid} 說了 {input}";

            await Clients.Client(Context.ConnectionId).SendAsync("RecieveBothMsg", content);
        }

        //主要是這個方法
        //public async Task SendBothMsg(string userid, string recieveid, string input)
        //{
        //    //調用這個方法去實作
        //    //(ChatResp chatSpeakerdata, ChatMsgLastData forSpeaker, ChatMsgLastData forReciever) result ;
        //    //using (var scope = _serviceProvider.CreateScope())
        //    //{
        //    //    var msg = scope.ServiceProvider.GetRequiredService<Service.Message>();

        //    //    result = msg.SaveChatMsg(userid, recieveid, input);

        //    //}
        //    var result = _iChat.SaveChatMsg(userid, recieveid, input);

        //    //取得要傳送的id 列表
        //    //member 的 connectionid list
        //    var useridList = ConnectList[userid];
        //    //reciever 的 connectionid list
        //    var recieveidList = new List<string>();
        //    if (ConnectList.ContainsKey(recieveid))
        //    {
        //        //reciever 的 connectionid list

        //        recieveidList = ConnectList[recieveid];
        //    }

        //    var resultList = useridList.Concat(recieveidList).ToList();
        //    //傳到聊天室的訊息
        //    await Clients.Clients(resultList).SendAsync("RecieveBothMsg",new ChatResp(), input);//result.chatSpeakerdata

        //    //傳訊息到message 組件 (最後訊息)
        //    await Clients.Clients(useridList).SendAsync("SendLastMsg",new ChatMsgLastData() ); //result.forSpeaker
        //    await Clients.Clients(recieveidList).SendAsync("SendLastMsg",new ChatMsgLastData() );//result.forReciever

        //    //修改未讀總數
        //    //改變footer 未讀總數
        //    //讓接收者知道是誰傳的
        //    await Clients.Clients(recieveidList).SendAsync("ChangeTotal", userid, 1);

        //}

        //連線時觸發這個方法，把連線id 加入
        public  async Task AddConnectList(string userid, string recieveid)
        {

            var recieveidList = new List<string>();
            if (!ConnectList.ContainsKey(userid))
            {
                ConnectList.Add(userid, new List<string>() { Context.ConnectionId });
            }
            else
            {
                ConnectList[userid].Add(Context.ConnectionId);
            }

            //如果他還沒上線過，就傳空的connectionID
            if (ConnectList.ContainsKey(recieveid))
            {
                //reciever 的 connectionid list

                recieveidList = ConnectList[recieveid];
            }

            //member 的 connectionid list
            var useridList = ConnectList[userid];

            var resultList = useridList.Concat(recieveidList).ToList();

            await Clients.Clients(resultList).SendAsync("IntoChat", "已進入聊天室" + "自己ID是: " + Context.ConnectionId);


        }

        //讀訊息，把資料庫的未讀修改
        public async Task ReadMsg(string userid, string recieveid)
        {
            //把數據庫的屬性改成unread=false
            //using (var scope = _serviceProvider.CreateScope())
            //{
            //    var msg = scope.ServiceProvider.GetRequiredService<Service.Message>();

            //    msg.UpdateToRead(userid, recieveid);

            //}
            //_iChat.UpdateToRead(userid, recieveid);
            //member 的 connectionid list
            var useridList = ConnectList[userid];
            //改變footer 未讀總數
            await Clients.Clients(useridList).SendAsync("ChangeTotal", 0, 0);
        }

        //解除連線時
        public override  Task OnDisconnectedAsync(Exception exception)
        {
            foreach (var connectList in ConnectList)
            {
                //斷線把連線ID刪掉
                connectList.Value.Remove(Context.ConnectionId);
            }
            return  base.OnDisconnectedAsync(exception);
        }
    }
}
