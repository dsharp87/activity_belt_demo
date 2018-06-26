using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using activity_belt_demo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace activity_belt_demo.Controllers
{
    public class LoginRegController : Controller
    {
        private dbContext _context;
 
        public LoginRegController(dbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("")]
        public IActionResult LoginReg()
        {
            if (TempData["error"] != null) {
                ViewBag.Error = TempData["error"];
            }
            return View("LoginReg");
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(UserViewModel FormUser) {
            if(ModelState.IsValid) {
                User NewUser = new User {
                    FirstName = FormUser.FirstName,
                    LastName = FormUser.LastName,
                    Email = FormUser.Email,
                    Password = FormUser.Password
                };
                User emailExistsQuery = _context.Users.SingleOrDefault(user => (user.Email == FormUser.Email));
                if(emailExistsQuery != null) {
                    ViewBag.ExistsError = "User with this email already exists, please choose a different email";
                    return View("LoginReg");
                } else {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    NewUser.Password = Hasher.HashPassword(NewUser, NewUser.Password);
                    _context.Add(NewUser);
                    _context.SaveChanges();
                    User results = _context.Users.SingleOrDefault(user => (user.Email == FormUser.Email));
                    int id = results.UserID;
                    HttpContext.Session.SetInt32("logged_id", id);
                    return RedirectToAction("ShowDashboard", "Activity");
                }
            }
            return View("LoginReg");
        }

        [HttpPost]
        [Route("attemptlogin")]
        public IActionResult Login(string email, string password) {
            User results = _context.Users.SingleOrDefault(user => (user.Email == email));
            if (results != null) {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                if(Hasher.VerifyHashedPassword(results, results.Password, password) != 0) {
                    int id = results.UserID;
                    HttpContext.Session.SetInt32("logged_id", id);
                    return RedirectToAction("ShowDashboard", "Activity");
                } else {
                    TempData["error"] = "login information inccorrect. Please try again.";
                    return RedirectToAction("LoginReg");
                }
               
            }
            TempData["error"] = "login information inccorrect. Please try again.";
            return RedirectToAction("LoginReg");
        }
    }
}
