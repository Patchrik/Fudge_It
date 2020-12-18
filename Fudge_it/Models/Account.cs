using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fudge_it.Models
{
    public class Account
    {
        int id { get; set; }
        string name { get; set; }
        int userId { get; set; }
        double balance { get; set; }
        string type { get; set; }
        int hhId { get; set; }
    }
}
