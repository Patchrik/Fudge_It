using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fudge_it.Models
{
    public class Expense
    {
       public int id { get; set; }
       public string name { get; set; }
       public double amount { get; set; }
       public int userId { get; set; }
       public bool recurring { get; set; }
       public int hhId { get; set; }
       public bool hhExpense { get; set; }
    }
}
