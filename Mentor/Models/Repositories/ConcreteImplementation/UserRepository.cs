using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
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
            return ApplicationDbContext.Users.Where(n => n.FirstName.Contains(search)
                || n.LastName.Contains(search));
        }

        public ArrayList compressedDataSearch(string input,int take,int skip)

        {
            var userList = new ArrayList();

            foreach (var item in Search(input).ToList().Skip(skip).Take(take))
            {
                byte[] yourBytes = item.Picture;
                var p = new { Id = item.Id, FirstName = item.FirstName,
                    LastName = item.LastName, Picture = yourBytes };

                userList.Add(p);
            }

            return userList;
        }

        public List<UserMessage> GetMessages(int? receiverId, int? senderId)
        {
            throw new NotImplementedException();
        }
    }
}