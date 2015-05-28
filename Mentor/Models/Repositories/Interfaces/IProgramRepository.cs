﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.Models.Repositories.Interfaces
{
    public interface IProgramRepository : IRepository<Program>
    {
        IEnumerable<Program> Search(string search);
        ICollection<ProgramMessage> GetProgramMessages(int? id);
        void SaveProgramChatMessage(ProgramMessage message, int programId, int userId);

        IEnumerable<Program> RetrieveAllPrograms();


    }
}