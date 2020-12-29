using Fudge_it.Models;
using System.Collections.Generic;

namespace Fudge_it.Repositories
{
    public interface IAccountRepository
    {
        List<Account> GetAllAccountsByUserId(int id);

        Account GetAccountById(int id);

        void AddAccount(Account account);

        void UpdateAccount(Account account);

        void DeleteAccount(int accountId);
    }
}