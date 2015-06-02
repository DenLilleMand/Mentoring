using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/**
 * Author: matti
 */
namespace Mentor.Models
{
    public class WishListProgramJson {
       
        public int Id { get; set; }
        public string Name { get; set; }
        public string Creator { get; set; }
        public int CreatorId { get; set; }
        public string Interest { get; set; }
        public string Reason { get; set; }
        public int AcceptCriteria { get; set; }
        public int Votes { get; set; }
        public bool Voted { get; set; }
        public string Description { get; set; }
        
    }
}