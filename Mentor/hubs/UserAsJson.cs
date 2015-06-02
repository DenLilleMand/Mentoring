using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mentor.Models;
/**
 * Author: matti
 */
namespace Mentor.hubs
{
    public class UserAsJson
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Picture { get; set; }

        public string FullName { get { return FirstName + " " + LastName;  } }

        public UserAsJson(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Picture = user.Picture;
        }
    }
}