using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using Mentor.Models;
using Mentor.Models.Repositories.ConcreteImplementation;
using Mentor.Models.Repositories.Interfaces;
using Mentor.ViewModels;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
/**
 * Author: Martin
 */
namespace Mentor.Controllers
{
    public class UtilityController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IProgramRepository _programRepository;

        public UtilityController(IUserRepository userRepository, IProgramRepository programRepository)
        {
            _userRepository = userRepository;
            _programRepository = programRepository;
        }

        [HttpPost]
        public ActionResult GetSearchData(string input,int take, int skip)
        {
            var both = new ArrayList();
            both.Add(_programRepository.CompressedDataSearch(input,take,skip));
            both.Add(_userRepository.CompressedDataSearch(input,take,skip));
           
            var finishSearchList = JsonConvert.SerializeObject(both, Formatting.None,
                new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
            return Content(finishSearchList, "application/json");

        }

        [HttpGet]
        public ActionResult GetUserFirstName()
        {
            return Content(_userRepository.Read(Convert.ToInt32(User.Identity.GetUserId())).FirstName);
        }

        public ActionResult GetPersonNoti(int? id)
        {
            var finishSearchList = JsonConvert.SerializeObject(_userRepository.Read(Convert.ToInt32(User.Identity.GetUserId())).Notifications, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });

            return Content(finishSearchList,"application/json");
        }

      

    }
}