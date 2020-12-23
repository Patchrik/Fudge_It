using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fudge_it.Models.ViewModels
{
    public class DashboardViewModel
    {
        public User User { get; set; }
        public List<Expense> Expenses { get; set; }
    }
}
