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

namespace Mentor.Controllers
{
    public class UtilityController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly ProgramRepository _programRepository;

        public UtilityController( UserRepository userRepository, ProgramRepository programRepository)
        {
            _userRepository = userRepository;
            _programRepository = programRepository;
        }

        [HttpPost]
        public ActionResult GetSearchData(string input,int take, int skip)
        {
            var both = new ArrayList();
            both.Add(_programRepository.compressedDataSearch(input,take,skip));
            both.Add(_userRepository.compressedDataSearch(input,take,skip));
           
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