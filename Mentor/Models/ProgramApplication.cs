using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mentor.Models.Repositories.Interfaces;
/**
 * Author: matti
 */
namespace Mentor.Models
{
    public enum Role
    {
        Mentor,Mentee
    }

    public class ProgramApplication : IContextEntity
    {
        public int Id { get; set; }

        public int SenderId { get; set; }
        public int ReceivingProgramId { get; set; }

        public string ReasonMessage { get; set; }
        public string QualificationsMessage { get; set; }
        public Role Role { get; set;}

        public bool Read { get; set; }
        public bool Response { get; set; }
        public string ResponseMessage { get; set; }


        public virtual Program ReceiveingProgram { get; set; }
        public virtual User SendingUser { get; set; }
    }
}