using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mentor.Models;
/**
 * Author: matti
 */
namespace Mentor.ViewModels
{
    public class WishListViewModel
    {
        public User CurrentUser { get; set; }
        public ICollection<Interest> UndefinedInterests { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
        public string Interest { get; set; }
        public int AcceptCriteria { get; set; }
    }
}