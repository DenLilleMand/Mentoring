using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mentor.Models.Repositories.Interfaces;

namespace Mentor.Models
{
    public class UserMessage : IContextEntity
    {
        public int Id { get; set; }
    }
}