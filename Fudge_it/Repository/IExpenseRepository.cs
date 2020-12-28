using Fudge_it.Models;
using System.Collections.Generic;

namespace Fudge_it.Repositories
{
    public interface IExpenseRepository
    {
        List<Expense> GetAllExpensesByUserId(int id);

        Expense GetExpenseById(int id);

        void AddExpense(Expense expense);

        void UpdateExpense(Expense expense);

        void DeleteExpense(int expenseId);
    }
}