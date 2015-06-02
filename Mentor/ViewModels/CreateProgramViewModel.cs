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
    public class CreateProgramViewModel
    {
        public User CurrentUser { get; set; }
        public Program ProgramToBeCreated { get; set; }
        public string VisibilityHolder { get; set; }
        public string InterestId { get; set; }
        public ICollection<Interest> UndefinedInterests { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}