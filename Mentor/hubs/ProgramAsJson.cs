using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mentor.Models;

namespace Mentor.hubs
{
    public class ProgramAsJson
    {
        public ProgramAsJson(Program program)
        {
            Id = program.Id;
            Name = program.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}