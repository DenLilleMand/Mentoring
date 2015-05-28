using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

using System.Collections.Generic;
using System.Linq;
using Mentor.Models;
using Mentor.Models.Repositories.ConcreteImplementation;
using Microsoft.AspNet.Identity;

namespace Mentor.hubs
{
    /**
     * Its pretty clear that when we use so much in-memory mapping, we're not making a scalable application because in-memory implementations
     * dont even scale beyond one server. I just found the in-memory implementations more interesting, because i havent worked with hashmaps 
     * since the 3rd semester exam. This class is pretty much just a mapping between a userId, and a list of string connection Ids. 
     * a user can have several browsers/ devices open when browsing our website, so thats the foundation for this implementation.
     * 
     * The alternatives to this implementation is listed here: http://www.asp.net/signalr/overview/guide-to-the-api/mapping-users-to-connections
     * and one of the  pros for our implementation, is that we can easily get a list of ALL connected users. so this user activity/chat 
     * implementation is enableing users to see who is online in their specific program, and at the same time in general see who is online. 
     */
    public class ConnectionMapping
    {
        private readonly Dictionary<int, HashSet<string>> _connections = new Dictionary<int, HashSet<string>>();
        private static ConnectionMapping _instance;
        private readonly ProgramRepository _programRepository;
        private readonly UserRepository _userRepository;
        private readonly Dictionary<int, InMemoryProgramChatRepository> _inMemoryProgramMentorChatRepositories;
        private readonly Dictionary<int, InMemoryProgramChatRepository> _inMemoryProgramMenteeChatRepositories;


        private ConnectionMapping(ProgramRepository programRepository,UserRepository userRepository)
        {
            if (_instance != null)
            {
                throw new AccessViolationException();
            }
            _programRepository = programRepository;
            _userRepository = userRepository;
            _inMemoryProgramMentorChatRepositories = new Dictionary<int, InMemoryProgramChatRepository>();
            _inMemoryProgramMenteeChatRepositories = new Dictionary<int, InMemoryProgramChatRepository>();

            _programRepository.RetrieveAllPrograms().ToList().ForEach(delegate(Program program)
            {
                _inMemoryProgramMentorChatRepositories.Add(program.Id, new InMemoryProgramChatRepository());
                _inMemoryProgramMenteeChatRepositories.Add(program.Id, new InMemoryProgramChatRepository());
            });
        }

        public static ConnectionMapping GetInstance(ProgramRepository programRepository,UserRepository userRepository)
        {
            if (_instance == null)
            {
                _instance = new ConnectionMapping(programRepository, userRepository);
            }
            return _instance;
        }

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public bool Add(User user, string connectionId)
        {
            lock (_connections)
            {
                int userId = user.Id;
                HashSet<string> connections;
                if (!_connections.TryGetValue(userId, out connections))
                {
                    connections = new HashSet<string>();
                    _connections.Add(userId, connections);
                    user.IsOnline = true;
                    _userRepository.Update(user);
                    AddUserTo(_inMemoryProgramMentorChatRepositories, user, user.MentorPrograms.ToList());
                    AddUserTo(_inMemoryProgramMenteeChatRepositories, user, user.MenteePrograms.ToList());
                    lock (connections)
                    {
                        connections.Add(connectionId);
                    }
                    return true;
                }

                lock (connections)
                {
                    connections.Add(connectionId);
                    Debug.WriteLine("We added another connection with key:" + userId + " and connectionId:" + connectionId);
                    _connections[userId].ToList().ForEach(delegate(string keuy2)
                    {
                        Debug.WriteLine("Current key:" + keuy2);
                    });
                    return false;
                }
            }
        }

        public void AddUserToProgram(string programId, bool asMentor)
        {
                User user = _userRepository.Read(Int32.Parse(HttpContext.Current.User.Identity.GetUserId()));
                if (asMentor)
                {
                    lock (_inMemoryProgramMentorChatRepositories)
                    {
                        _inMemoryProgramMentorChatRepositories.Add(Int32.Parse(programId), new InMemoryProgramChatRepository());       
                    }
                }
                else
                {
                    lock (_inMemoryProgramMenteeChatRepositories)
                    {
                        _inMemoryProgramMenteeChatRepositories.Add(Int32.Parse(programId), new InMemoryProgramChatRepository());        
                    }
                }
        }

        public ICollection<ProgramMessageAsJson> RetrieveProgramMessagesAsJson(string programId)
        {
            lock (_programRepository)
            {
                List<ProgramMessage> progreamMessageList = _programRepository.GetProgramMessages(Int32.Parse(programId)).ToList();
                List<ProgramMessageAsJson> programMessagesAsJsons = new List<ProgramMessageAsJson>();
                progreamMessageList.ForEach(delegate(ProgramMessage p)
                {
                    programMessagesAsJsons.Add(new ProgramMessageAsJson(p));
                });
                return programMessagesAsJsons;   
            }
        }

        public ICollection<UserAsJson> GetConnectedMentors(string programIdString)
        {
            lock (_inMemoryProgramMentorChatRepositories)
            {
                return _inMemoryProgramMentorChatRepositories[Int32.Parse(programIdString)].Users();    
            }
        }

        public ICollection<UserAsJson> GetConnectedMentees(string programIdString)
        {
            lock (_inMemoryProgramMenteeChatRepositories)
            {
                return _inMemoryProgramMenteeChatRepositories[Int32.Parse(programIdString)].Users();
            }
        }


        private void AddUserTo(Dictionary<int, InMemoryProgramChatRepository> dictionary, User user, List<Program> programList)
        {
            programList.ForEach(delegate(Program program)
            {
                if (dictionary.ContainsKey(program.Id))
                {
                    dictionary[program.Id].Add(new UserAsJson(user));
                }
            });
        }

        private void RemoveUserFrom(Dictionary<int, InMemoryProgramChatRepository> dictionary, User user, List<Program> programList)
        {
            programList.ForEach(delegate(Program program)
            {
                if (dictionary.ContainsKey(program.Id))
                {
                    dictionary[program.Id].Remove(new UserAsJson(user));
                }
            });
        }


        public IEnumerable<string> GetConnections(int key)
        {
            HashSet<string> connections;
            if (_connections.TryGetValue(key, out connections))
            {
                return connections;
            }
            return Enumerable.Empty<string>();
        }

        public bool Remove(User user, string connectionId)
        {
            lock (_connections)
            {
                //If there is no key, just return instantly, even though this would indicate a weird state in general. :/
                int userId = user.Id;
                HashSet<string> connections;
                if (!_connections.TryGetValue(userId, out connections))
                {
                    Debug.WriteLine("there is something wrong in the connectionMapping if this is being printed.");
                    _connections.Keys.ToList().ForEach(delegate(int keuy2)
                    {
                        Debug.WriteLine("key:" + keuy2);
                    });
                    return false;
                }

                lock (connections)
                {
                    connections.Remove(connectionId);
                    Debug.WriteLine("We just removed a connection with key:" + userId + " and connectionId:" + connectionId);
                    Debug.WriteLine("Amount of connections for user with key:" + userId + " is " + connections.Count);
                    if (connections.Count == 0)
                    {
                        user.IsOnline = false;
                        _userRepository.Update(user);
                        RemoveUserFrom(_inMemoryProgramMentorChatRepositories, user, user.MentorPrograms.ToList());
                        RemoveUserFrom(_inMemoryProgramMenteeChatRepositories, user, user.MenteePrograms.ToList());
                        _connections.Remove(userId);
                        return false;
                    }
                }
                return true;
            }
        }
    }
}