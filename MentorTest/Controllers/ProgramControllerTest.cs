using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Mentor.customAttributes;
using Mentor.hubs;
using Mentor.Models;
using Mentor.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MentorTest.Controllers
{
    [TestClass]
    public class ProgramControllerTest
    {
        [TestMethod]
        public void Index(int? id)
        {
            
        }

        [TestMethod]
        public void MyPrograms()
        {
 
        }

        [TestMethod]
        public void CreateProgram()
        {

        }

       [TestMethod]
        public void CreateProgram(CreateProgramViewModel createProgramViewModel)
        {
            
        }
        [TestMethod]
       public void GetPicture(int id)
        {
       
        }

        [TestMethod]
        public void ProgramApplication(ProgramViewModel model)
        {
            
        }

        [TestMethod]
        public void WishList()
        {
            
        }

        [TestMethod]
        public void CreateWish(WishListViewModel model)
        {
            
        }
    }
}
