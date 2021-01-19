using SocialMedia.Models.Friends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Interface
{
    public interface IDirectory
    {
        FriendResp GetFrinedList(int id);

        FriendResp AddFrined(FriendReq req);

        FriendResp DeleteFrined(FriendReq req);
    }
}
