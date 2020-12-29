using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fudge_it.Models
{
    public class Account
    {
       public int id { get; set; }
       public string name { get; set; }
       public int userId { get; set; }
       public double balance { get; set; }
       public string type { get; set; }
       public int hhId { get; set; }
    }
}
