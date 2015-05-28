using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using Mentor.Models;
using Mentor.Models.Repositories.ConcreteImplementation;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Transports;
using Newtonsoft.Json;
using System.Diagnostics;
using WebGrease.Css.Extensions;

namespace Mentor.hubs
{
        public class ProgramChatHub : Hub
        {
            private readonly ProgramRepository _programRepository;
            private readonly UserRepository _userRepository;
            private readonly ConnectionMapping _connectionMapping;


            #region public
            public ProgramChatHub(ProgramRepository programRepository, UserRepository userRepository)
            {
                _userRepository = userRepository;
                _programRepository = programRepository;
                _connectionMapping = ConnectionMapping.GetInstance(_programRepository, userRepository);
            }

            public void Send(string message,string programId, string userId)
            {
                ProgramMessage programMessage = new ProgramMessage()
                {
                    Message = message
                };
                _programRepository.SaveProgramChatMessage(programMessage,Int32.Parse(programId),Int32.Parse(userId));
                ProgramMessageAsJson messageAsJson = new ProgramMessageAsJson(programMessage);
                Clients.All.onMessageReceived(JsonConvert.SerializeObject(messageAsJson));
            }

            public override Task OnDisconnected(bool notCalled)
            {
                    int userId = Int32.Parse( Context.User.Identity.GetUserId());
                    User user = _userRepository.Read(userId);
                    bool stillHasConnections = _connectionMapping.Remove(user,Context.ConnectionId);
                    if (!stillHasConnections)
                    {
                        Debug.WriteLine("OnDisconnected was called, and we assume that the user has no more connections");
                        return Clients.All.leaves(new UserAsJson(user));
                    }
                    return base.OnDisconnected(notCalled);
            }

            public void Joined(string userIdString)
            {
               /* System.Diagnostics.Debug.WriteLine("userid:" + Clients.Caller.userId);*/
                User user = _userRepository.Read(Int32.Parse(userIdString));
                //This basically just have to return true, if its the first connection this user creates. 
                //Right now, if the same user creates several connections he wont see himself on other than the firstConnection, we should fix that aswell.
                if (_connectionMapping.Add(user, Context.ConnectionId))
                {
                    Debug.WriteLine("Joined is called, and we set the mapping.");
                    Clients.All.joins(new UserAsJson(user));    
                }
            }

            public ICollection<ProgramMessageAsJson> RetrieveProgramMessagesAsJson(int programId)
            {
                List<ProgramMessageAsJson> programMessageAsJsons = new List<ProgramMessageAsJson>();
                _programRepository.GetProgramMessages(programId).ToList().ForEach(delegate(ProgramMessage pm)
                {
                    programMessageAsJsons.Add(new ProgramMessageAsJson(pm));
                });
                return programMessageAsJsons;
            }

            public ICollection<UserAsJson> GetConnectedMentors(string programId)
            {
                return _connectionMapping.GetConnectedMentors(programId);
            }

            public ICollection<UserAsJson> GetConnectedMentees(string programId)
            {
                return _connectionMapping.GetConnectedMentees(programId);
            }





            #endregion


            #region private


            #endregion


        }
}