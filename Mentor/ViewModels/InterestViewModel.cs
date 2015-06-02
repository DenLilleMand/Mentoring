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
    public class InterestViewModel
    {
        public User CurrentUser { get; set; }
        public string[] Interests { get; set; }
        public string RedirectUrl { get; set; }
    }
}