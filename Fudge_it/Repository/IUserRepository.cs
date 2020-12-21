using Fudge_it.Models;
using System.Collections.Generic;

namespace Fudge_it.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetUserById(int id);

        User GetUserByEmail(string email);
    }
}