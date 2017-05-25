using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Client.Data;
using Client.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using NuGet.Protocol.Core.v3;

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
            if (_signInManager.IsSignedIn(User))
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

        [HttpPost]
        public IActionResult TestSuite(string token)
        {
            ViewData["Token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<string> ExecuteRequest([FromBody] JObject request)
        {
            var masterUrl = "http://ptmaster.azurewebsites.net/main";
            using (var client = new HttpClient())
            {
                var token = request["Token"].Value<string>();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(0, 0, 30);
                client.DefaultRequestHeaders.Add("pttoken", token);
                var response = await client.PostAsJsonAsync(masterUrl, request);
                return await response.Content.ReadAsStringAsync();
            }
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
