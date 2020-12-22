using Fudge_it.Models;
using System.Collections.Generic;

namespace Fudge_it.Repositories
{
    public interface IExpenseRepository
    {
        List<Expense> GetAllExpensesByUserId(int id);

        Expense GetExpenseById(int id);
    }
}