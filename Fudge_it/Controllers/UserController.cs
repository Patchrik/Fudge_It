using Fudge_it.Models;
using Fudge_it.Models.ViewModels;
using Fudge_it.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fudge_it.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly IExpenseRepository _expenseRepo;

        public UserController(IUserRepository userRepo, IExpenseRepository expenseRepo)
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


        // GET: UserController
        public ActionResult Dashboard()
        {
            User user = _userRepo.GetUserById(GetCurrentUserId());
            List<Expense> expenses = _expenseRepo.GetAllExpensesByUserId(user.id);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                DashboardViewModel vm = new DashboardViewModel()
                {
                    User = user,
                    Expenses = expenses
                };
                return View(vm);
            }
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Expense expense)
        {
            try
            {
                expense.userId = GetCurrentUserId();
                expense.hhId = GetCurrentUserHHid();
                _expenseRepo.AddExpense(expense);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(expense);
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
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

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
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

        public ActionResult Login() {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel viewModel) 
        {
            User user = _userRepo.GetUserByEmail(viewModel.email);

            if (user == null)
            {
                return Unauthorized();
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.UserData, user.hhId.ToString()),
                new Claim(ClaimTypes.Email, user.email),
                new Claim(ClaimTypes.Role, "User"),
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
            //this is just a placeholder at the moment we will need to redirect the logged in user to their dash board from the login screen.
            return RedirectToAction("Dashboard");
        }
    }
}
