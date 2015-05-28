using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mentor.Models
{
    public class WishListProgramJson : IEquatable<WishListProgramJson> {
       
        public int Id { get; set; }
        public string Name { get; set; }
        public string Creator { get; set; }
        public string Interest { get; set; }
        public string Reason { get; set; }
        public int AcceptCriteria { get; set; }
        public int Votes { get; set; }
        public bool Voted { get; set; }
         
        public override int GetHashCode() {
            return Id;
        }

        public override bool Equals(object obj) {
            return Equals(obj as WishListProgramJson);
        }

        public bool Equals(WishListProgramJson obj) {
            return obj != null && obj.Id == Id;
        }
    }
}