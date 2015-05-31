using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.Models.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> Search(string search);
        List<UserMessage> GetMessages(int? receiverId, int? senderId);
    }
}
