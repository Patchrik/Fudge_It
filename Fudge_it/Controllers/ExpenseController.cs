using Fudge_it.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fudge_it.Models;
using System.Security.Claims;

namespace Fudge_it.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly IExpenseRepository _expenseRepo;
        public ExpenseController(IExpenseRepository expenseRepo, IUserRepository userRepo) 
        {
            _userRepo = userRepo;
            _expenseRepo = expenseRepo;
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }

        private int GetCurrentUserHHid()
        {
            string id = User.FindFirstValue(ClaimTypes.UserData);
            return int.Parse(id);
        }

        // GET: ExpenseController
        public ActionResult Expense()
        {
            User user = _userRepo.GetUserById(GetCurrentUserId());
            List<Expense> expenses = _expenseRepo.GetAllExpensesByUserId(user.id);
            return View(expenses);
        }

        // GET: ExpenseController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ExpenseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExpenseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ExpenseController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ExpenseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ExpenseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ExpenseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
