using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using activity_belt_demo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace activity_belt_demo.Controllers
{
    public class ActivityController : Controller
    {
        private dbContext _context;
 
        public ActivityController(dbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Dashboard")]
        public IActionResult ShowDashboard()
        {
            if (HttpContext.Session.GetInt32("logged_id") == null) {
                return RedirectToAction("LoginReg", "LoginReg");
            }
            ViewBag.LoggedUser = _context.Users.SingleOrDefault(user => (user.UserID == HttpContext.Session.GetInt32("logged_id")));
            // List<ActivityPlan> allofthem = _context.ActivityPlans.Include(a => a.User).Where(a => a.StartDate > DateTime.Now).OrderBy(a => a.StartDate).ToList();
            ViewBag.AllActivities = _context.ActivityPlans.Include(a => a.User).Include(a => a.AttendingUsers).Where(a => a.StartDate > DateTime.Now).OrderBy(a => a.StartDate).ToList();
            // var stuff = _context.ActivityPlans.Include(a => a.User).Include(a => a.AttendingUsers).ThenInclude(uap => uap.User).ToList();
            return View("Dashboard");
        }

        [HttpGet]
        [Route("ActivityForm")]
        public IActionResult ActivityForm()
        {
            if (HttpContext.Session.GetInt32("logged_id") == null) {
                return RedirectToAction("LoginReg", "LoginReg");
            }
            return View("ActivityForm");
        }

        [HttpPost]
        public IActionResult CreateActivityPlan(ActivityPlanViewModel AnActivityPlan)
        {
            if(ModelState.IsValid) 
            {
                ActivityPlan AddActivityPlan = new ActivityPlan 
                {
                    Title = AnActivityPlan.Title,
                    StartDate = AnActivityPlan.StartDate,
                    Duration = AnActivityPlan.Duration,
                    DurationType = AnActivityPlan.DurationType,
                    Description = AnActivityPlan.Description,
                    UserID = (int)HttpContext.Session.GetInt32("logged_id")
                };
                _context.ActivityPlans.Add(AddActivityPlan);
                _context.SaveChanges();
                return RedirectToAction("ShowDashboard");
            }
            return View("ActivityForm");
            
        }

        [HttpGet]
        [Route("DeleteActivity/{ActivityPlanID}")]
        public IActionResult DeleteActivty(int ActivityPlanID)
        {
            ActivityPlan deleteMe = _context.ActivityPlans.SingleOrDefault(a => a.ActivityPlanID == ActivityPlanID);
            _context.Remove(deleteMe);
            _context.SaveChanges();
            return RedirectToAction("ShowDashboard");
        }

        [HttpGet]
        [Route("AddUserActivityPlan/{ActivityID}")]
        public IActionResult AddUserActivityPlan(int ActivityID)
        {
            UserActivityPlan addMe = new UserActivityPlan
            {
                UserID = (int)HttpContext.Session.GetInt32("logged_id"),
                ActivityPlanID = ActivityID
            };
            _context.UserActivityPlans.Add(addMe);
            _context.SaveChanges();
            return RedirectToAction("ShowDashboard");
        }

        [HttpGet]
        [Route("DeleteUserActivityPlan/{ActivityPlanID}")]
        public IActionResult DeleteUserActivityPlan(int ActivityPlanID)
        {
            UserActivityPlan deleteMe = _context.UserActivityPlans.SingleOrDefault(uap => uap.UserID == (int)HttpContext.Session.GetInt32("logged_id") && uap.ActivityPlanID == ActivityPlanID);
            _context.UserActivityPlans.Remove(deleteMe);
            _context.SaveChanges();
            return RedirectToAction("ShowDashboard");
        }

        [HttpGet]
        [Route("ShowActivityPlan/{ActivityPlanID}")]
        public IActionResult ShowActivityPlan(int ActivityPlanID)
        {
            if (HttpContext.Session.GetInt32("logged_id") == null) {
                return RedirectToAction("LoginReg", "LoginReg");
            }
            ViewBag.LoggedUser = _context.Users.SingleOrDefault(user => (user.UserID == HttpContext.Session.GetInt32("logged_id")));
            ViewBag.ActivityPlan = _context.ActivityPlans.Include(a => a.User).Include(a => a.AttendingUsers).ThenInclude(uap => uap.User).SingleOrDefault(a => a.ActivityPlanID == ActivityPlanID);
            return View("ShowActivityPlan");
        }

        [HttpPost]
        [Route("Logout")]
        public IActionResult Logout() {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginReg", "LoginReg");
        }
    }

}
