using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mentor.Models;

namespace Mentor.hubs
{
    public class ProgramMessageAsJson
    {
        public ProgramMessageAsJson(ProgramMessage programMessage)
        {
            FullName = programMessage.User.FirstName + " " + programMessage.User.LastName;
            Date = programMessage.Date.ToString();
            Message = programMessage.Message;
        }

        public string FullName { get; set; }
        public string Date { get; set; }
        public string Message { get; set; }
    }
}