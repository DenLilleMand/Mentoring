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
    public class MyProgramsViewModel
    {
        public User CurrentUser { get; set; }
        public ICollection<Program> ProgramSurgestions { get; set; } 
    }
}