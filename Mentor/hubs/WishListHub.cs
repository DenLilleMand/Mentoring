using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mentor.Models;
using Microsoft.AspNet.SignalR;

namespace Mentor.hubs
{
    public class WishListHub : Hub
    {
        private readonly WishListInMemoryRepository _wishListInMemoryRepository = WishListInMemoryRepository.GetInstance();


        //upvote
        public void Vote( WishListProgramJson program, int userId)
        {
            _wishListInMemoryRepository.Vote(program, userId);
        }

        public ICollection<WishListProgramJson> RetrieveAllWishListPrograms()
        {
            return _wishListInMemoryRepository.GetAllWishListPrograms();
        } 
        
    }
}