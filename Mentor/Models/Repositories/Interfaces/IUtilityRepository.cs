using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/**
 * Author: matti
 */
namespace Mentor.Models.Repositories.Interfaces
{
    public interface IUtilityRepository
    {
        string Search(string search);
    }
}