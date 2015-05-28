using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Mentor.Models.Repositories.AbstractInterfaces;
using Mentor.Models.Repositories.Interfaces;

namespace Mentor.Models.Repositories.ConcreteImplementation
{
    public class UserRepository : AbstractIRepository<User>
    {
        public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }
       
        public IEnumerable<User> Search(string search)
        {
            return ApplicationDbContext.Users.Where(n => n.FirstName.Contains(search) || n.LastName.Contains(search));
        }

        public List<UserMessage> GetMessages(int? receiverId, int? senderId)
        {
            throw new NotImplementedException();
        }
    }
}