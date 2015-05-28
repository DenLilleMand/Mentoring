using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Mentor.Models.Repositories.AbstractInterfaces;
using Mentor.Models.Repositories.Interfaces;

namespace Mentor.Models.Repositories.ConcreteImplementation
{
    //Burde implementere sit eget interface, der arver fra det andet. 
    public class ProgramRepository : AbstractIRepository<Program>, IProgramRepository 
    {
        public ProgramRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) {}
        
        private User CurrentUser { get; set; }
        public IEnumerable<Program> Search(string search)
        {
            return ApplicationDbContext.Set<Program>().Where(n => n.Name.Contains(search));
        }

        public ICollection<ProgramMessage> GetProgramMessages(int? id)
        {
            return ApplicationDbContext.Set<ProgramMessage>().Where(pm => pm.ProgramId == id).ToList();
        }

        public void SaveProgramChatMessage(ProgramMessage message, int programId, int userId)
        {
            message.User = ApplicationDbContext.Set<User>().Find(userId);
            message.Program = ApplicationDbContext.Set<Program>().Find(programId);
            ApplicationDbContext.Set<ProgramMessage>().Add(message);
            ApplicationDbContext.Entry(message).State = EntityState.Added;
            ApplicationDbContext.SaveChanges();
        }

        public IEnumerable<Program> RetrieveAllPrograms()
        {
            return ApplicationDbContext.Set<Program>();
        }



    }
}