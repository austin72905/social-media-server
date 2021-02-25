using SocialMedia.Models.Friends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Interface
{
    public interface IDirectory
    {
        Task<FriendResp> GetFrinedList(int id);

        Task<FriendResp> AddFrined(FriendReq req);

        Task<FriendResp> DeleteFrined(FriendReq req);
    }
}
