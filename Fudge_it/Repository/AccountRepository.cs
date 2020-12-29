using Fudge_it.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Fudge_it.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public AccountRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Account> GetAllAccountsByUserId(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT id, [name], userId, balance, type, hhId FROM Account
                        WHERE userId = @id;
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Account> accounts = new List<Account>();
                    while (reader.Read())
                    {
                        Account account  = new Account
                        {
                            id = reader.GetInt32(reader.GetOrdinal("id")),
                            name = reader.GetString(reader.GetOrdinal("name")),
                            userId = reader.GetInt32(reader.GetOrdinal("userId")),
                            balance = reader.GetDouble(reader.GetOrdinal("balance")),
                            type = reader.GetString(reader.GetOrdinal("type")),
                            hhId = reader.GetInt32(reader.GetOrdinal("hhId"))
                        };
                        accounts.Add(account);
                    }

                    reader.Close();

                    return accounts;
                }
            }
        }

        public Account GetAccountById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT id, [name], userId, balance, type, hhId FROM Account
                        WHERE id = @id;
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Account account = new Account
                        {
                            id = reader.GetInt32(reader.GetOrdinal("id")),
                            name = reader.GetString(reader.GetOrdinal("name")),
                            userId = reader.GetInt32(reader.GetOrdinal("userId")),
                            balance = reader.GetDouble(reader.GetOrdinal("balance")),
                            type = reader.GetString(reader.GetOrdinal("type")),
                            hhId = reader.GetInt32(reader.GetOrdinal("hhId"))
                        };

                        reader.Close();
                        return account;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }
        public void AddAccount(Account account) 
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Account ([name], userId, balance, type, hhId) 
                                        VALUES (@name, @userId, @balance, @type, @hhId);
                    ";

                    cmd.Parameters.AddWithValue("@name", account.name);
                    cmd.Parameters.AddWithValue("@userId", account.userId);
                    cmd.Parameters.AddWithValue("@balance", account.balance);
                    cmd.Parameters.AddWithValue("@type", account.type);
                    cmd.Parameters.AddWithValue("@hhId", account.hhId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateAccount(Account account) 
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Account
                                        SET 
                                        [name] = @name, 
                                        userId = @userId, 
                                        balance = @balance, 
                                        type = @type, 
                                        hhId = @hhId,
                                        WHERE id = @id;

                    ";
                    cmd.Parameters.AddWithValue("@name", account.name);
                    cmd.Parameters.AddWithValue("@userId", account.userId);
                    cmd.Parameters.AddWithValue("@balance", account.balance);
                    cmd.Parameters.AddWithValue("@type", account.type);
                    cmd.Parameters.AddWithValue("@hhId", account.hhId);
                    cmd.Parameters.AddWithValue("@id", account.id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void DeleteAccount(int accountId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Account WHERE id = @id";

                    cmd.Parameters.AddWithValue("@id", accountId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
