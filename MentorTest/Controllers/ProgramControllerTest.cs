using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Mentor.customAttributes;
using Mentor.Controllers;
using Mentor.hubs;
using Mentor.Models;
using Mentor.Models.Repositories.Interfaces;
using Mentor.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
 /**
  * matti
  */
namespace MentorTest.Controllers
{
    [TestClass]
    public class ProgramControllerTest
    {
        [TestMethod]
        public void Index()
        {
            var factory = new MockRepository(MockBehavior.Loose) { DefaultValue = DefaultValue.Mock };
            // Create a mock using the factory settings
            var mockedProgramRepository = factory.Create<IProgramRepository>();
            var mockedUserRepository = factory.Create<IUserRepository>();
            var mockedInterestRepository = factory.Create<IInterestRepository>();
            var controller = new ProgramsController(mockedProgramRepository.Object, mockedUserRepository.Object, mockedInterestRepository.Object);
            var result = controller.Index(1) as ViewResult;
            Assert.AreEqual("Index", result.ViewName);

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
