using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mentor.Models.Repositories.AbstractInterfaces;
using Mentor.Models.Repositories.Interfaces;
/**
 * Author: matti
 */
namespace Mentor.Models.Repositories.ConcreteImplementation
{
    public class InterestRepository : AbstractIRepository<Interest>, IInterestRepository
    {
        public InterestRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
               
        }
    }
}