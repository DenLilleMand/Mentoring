using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Mentor.Models;
using Microsoft.AspNet.SignalR;
/**
 * Author: matti
 */
namespace Mentor.hubs
{
    public class WishListHub : Hub
    {
        private readonly WishListInMemoryRepository _wishListInMemoryRepository = WishListInMemoryRepository.GetInstance();

        //vote
        public void Vote( WishListProgramJson program, int userId, Boolean vote)
        {
            WishListProgramJson wishProgram =_wishListInMemoryRepository.Vote(program, userId,vote);
            if (wishProgram != null)
            {
                Clients.All.vote(wishProgram);
                Debug.WriteLine("a vote went through just fine.");  
            }
            else
            {
                Debug.WriteLine("alot of things might've went wrong, a common issue is that people duplicate their upvote or downvote. " +
                                "which doesnt make sense because u can only vote once.");    
            }
            
            
        }

        public ICollection<WishListProgramJson> RetrieveAllWishListPrograms()
        {
            Debug.WriteLine("All wishes was retrieved.");
            return _wishListInMemoryRepository.GetAllWishListPrograms();
        } 
        
    }
}