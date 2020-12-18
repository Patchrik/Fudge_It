using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fudge_it.Models
{
    public class Expense
    {
        int id { get; set; }
        string name { get; set; }
        double amount { get; set; }
        int userId { get; set; }
        int recurring { get; set; }
        int hhId { get; set; }
        int hhExpense { get; set; }
    }
}
