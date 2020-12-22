﻿using Fudge_it.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Fudge_it.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public ExpenseRepository(IConfiguration config)
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

        public List<Expense> GetAllExpensesByUserId(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT id, [name], amount, userId, recurring, hhId, hhExpense FROM Expense
                        WHERE userId = @id;
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Expense> expenses = new List<Expense>();
                    while (reader.Read())
                    {
                        Expense expense = new Expense
                        {
                            id = reader.GetInt32(reader.GetOrdinal("id")),
                            name = reader.GetString(reader.GetOrdinal("name")),
                            amount = reader.GetDouble(reader.GetOrdinal("amount")),
                            userId = reader.GetInt32(reader.GetOrdinal("userId")),
                            recurring = reader.GetBoolean(reader.GetOrdinal("recurring")),
                            hhId = reader.GetInt32(reader.GetOrdinal("hhId")),
                            hhExpense = reader.GetBoolean(reader.GetOrdinal("hhExpense"))
                        };

                        expenses.Add(expense);
                    }

                    reader.Close();

                    return expenses;
                }
            }
        }

        public Expense GetExpenseById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT id, [name], amount, userId, recurring, hhId, hhExpense FROM Expense
                        WHERE id = @id;
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Expense expense = new Expense
                        {
                            id = reader.GetInt32(reader.GetOrdinal("id")),
                            name = reader.GetString(reader.GetOrdinal("name")),
                            amount = reader.GetDouble(reader.GetOrdinal("amount")),
                            userId = reader.GetInt32(reader.GetOrdinal("userId")),
                            recurring = reader.GetBoolean(reader.GetOrdinal("recurring")),
                            hhId = reader.GetInt32(reader.GetOrdinal("hhId")),
                            hhExpense = reader.GetBoolean(reader.GetOrdinal("hhExpense"))
                        };

                        reader.Close();
                        return expense;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }
    }
}
