using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Chathub
{
    public class ChatHub : Hub
    {
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

        public async Task SendOneMsg(string user, string input)
        {
            var content = $"{user} 說了 {input}";

            await Clients.Client(Context.ConnectionId).SendAsync("", content);
        }
    }
}
