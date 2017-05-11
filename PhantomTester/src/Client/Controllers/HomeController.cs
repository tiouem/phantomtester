using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Client.Data;
using Client.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Client.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;

  

        public HomeController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail()
        {
            //var model = new UserDetailVM();
            UserDetailVM model = null;
            ClaimsPrincipal currentUser = this.User;
            if(_signInManager.IsSignedIn(User))
            {
                var userId = _userManager.GetUserId(currentUser);
                var username = _userManager.GetUserName(currentUser);

                var token = _context.Tokens.Include(subscription => subscription.Subscription).FirstOrDefault(o => o.UserId.Equals(userId));

                var usage = token.Usages;
                var tokenGuid = token.GuidToken;

                var sName = token.Subscription.Name;
                var sLimit = token.Subscription.Limit;
                model = new UserDetailVM()
                {
                    Usename = username,
                    TokenGuid = tokenGuid.ToString(),
                    TokenUsage = usage,
                    SubscriptionName = sName,
                    SubscriptionLimit = sLimit
                };             
            }
            else
            {
               return RedirectToAction("Index");
            }
     


            return View(model);
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
