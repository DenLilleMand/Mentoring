using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mentor.hubs;

namespace Mentor.Models.Repositories.ConcreteImplementation
{
    public enum Repository
    {
        Mentor,Mentee
    }


    public class InMemoryProgramChatRepository
    {
        private readonly Dictionary<int, UserAsJson> _connectedUsers;


        public Boolean ContainsUser(int userId)
        {
            lock (_connectedUsers)
            {
                return _connectedUsers.ContainsKey(userId);
            }
        }

        public ICollection<UserAsJson> Users()
        {
            return _connectedUsers.Values.ToList();
        }

        public InMemoryProgramChatRepository()
        {
            _connectedUsers = new Dictionary<int,UserAsJson>();
        }

        public void Add(UserAsJson user)
        {
            lock (_connectedUsers)
            {
                _connectedUsers.Add(user.Id,user);
            }
        }

        public void Remove(UserAsJson user)
        {
            lock (_connectedUsers)
            {
                _connectedUsers.Remove(user.Id);
            }
        }
    }
}