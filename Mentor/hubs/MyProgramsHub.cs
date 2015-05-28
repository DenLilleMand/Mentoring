using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mentor.Models;
using Mentor.Models.Repositories.ConcreteImplementation;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using System.Diagnostics;

namespace Mentor.hubs
{
    public class MyProgramsHub : Hub
    {
            private readonly ProgramRepository _programRepository;
            private readonly UserRepository _userRepository;

            public MyProgramsHub(ProgramRepository programRepository, UserRepository userRepository)
            {
                _userRepository = userRepository;
                _programRepository = programRepository;
            }



            public ICollection<ProgramAsJson> RetrieveAllMentorPrograms(string userId)
            {
                Debug.WriteLine("RetrieveAllMentorPrograms was called with id:" + userId);
                List<ProgramAsJson> programAsJsonList = new List<ProgramAsJson>();
                _userRepository.Read(Int32.Parse(Context.User.Identity.GetUserId())).MentorPrograms.ForEach(
                    delegate(Program p)
                    {
                        Debug.WriteLine("mentor program:" + p.Name);
                        programAsJsonList.Add(new ProgramAsJson(p));
                    });
                return programAsJsonList;
            }

            public ICollection<ProgramAsJson> RetrieveAllMenteePrograms(string userId)
            {
                Debug.WriteLine("RetrieveAllMenteePrograms was called with id:" + userId);
                List<ProgramAsJson> programAsJsonList = new List<ProgramAsJson>();
                _userRepository.Read(Int32.Parse(Context.User.Identity.GetUserId())).MenteePrograms.ForEach(
                    delegate(Program p)
                    {
                        Debug.WriteLine("Mentee program:" + p.Name);
                        programAsJsonList.Add(new ProgramAsJson(p));
                    });
                return programAsJsonList;
            }

            public ICollection<ProgramAsJson> RetrieveAllSurgestedPrograms(string userId)
            {
                Debug.WriteLine("RetrieveAllSurgestedPrograms was called with id:" + userId);
                List<ProgramAsJson> programAsJsonList = new List<ProgramAsJson>();
                 _programRepository.RetrieveAllPrograms().ToList().ForEach(
                    delegate(Program p)
                    {
                        Debug.WriteLine("every program:" + p.Name);
                        programAsJsonList.Add(new ProgramAsJson(p));
                    });
                return programAsJsonList; ;
            } 

    }
}