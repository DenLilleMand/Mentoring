using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Mentor.Models;
using Microsoft.AspNet.SignalR;

namespace Mentor.hubs
{
    public class WishListHub : Hub
    {
        private readonly WishListInMemoryRepository _wishListInMemoryRepository = WishListInMemoryRepository.GetInstance();

        //vote
        public void Vote( WishListProgramJson program, int userId, bool vote)
        {
            _wishListInMemoryRepository.Vote(program, userId);
            Debug.WriteLine("Vote was called with:" + program + " and voted:" + vote);
            Clients.All.Vote(program, vote);
        }

        public ICollection<WishListProgramJson> RetrieveAllWishListPrograms()
        {
            Debug.WriteLine("All wishes was retrieved.");
            return _wishListInMemoryRepository.GetAllWishListPrograms();
        } 
        
    }
}