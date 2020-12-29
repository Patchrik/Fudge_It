using Fudge_it.Models;
using Fudge_it.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fudge_it.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly IExpenseRepository _expenseRepo;
        private readonly IAccountRepository _accountRepo;

        public AccountController(IExpenseRepository expenseRepo, IUserRepository userRepo, IAccountRepository accountRepo)
        {
            _userRepo = userRepo;
            _expenseRepo = expenseRepo;
            _accountRepo = accountRepo;
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

        // GET: AccountController
        public ActionResult Accounts()
        {
            User user = _userRepo.GetUserById(GetCurrentUserId());
            List<Account> accounts = _accountRepo.GetAllAccountsByUserId(user.id);
            return View(accounts);
        }

        // GET: AccountController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Account account)
        {
            try
            {
                account.userId = GetCurrentUserId();
                account.hhId = GetCurrentUserHHid();
                _accountRepo.AddAccount(account);
                return RedirectToAction("Accounts");
            }
            catch
            {
                return View(account);
            }
        }

        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountController/Edit/5
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

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountController/Delete/5
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
