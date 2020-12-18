using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fudge_it.Models
{
    public class User
    {
        int id { get; set; }
        string firstName { get; set; }
        string lastName { get; set; }
        string email { get; set; }
        int hhId { get; set; }

        string fullName {
            get {
                return firstName + lastName;
            }
        }

    }
}
