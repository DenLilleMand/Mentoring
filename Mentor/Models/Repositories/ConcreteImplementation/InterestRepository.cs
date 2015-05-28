using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mentor.Models.Repositories.AbstractInterfaces;

namespace Mentor.Models.Repositories.ConcreteImplementation
{
    public class InterestRepository : AbstractIRepository<Interest>
    {
        public InterestRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            
        }



    }
}