using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TechProduct.Models;

namespace TechProduct.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DBaseTechProducts _dBaseTechProducts;
       
     
                 
        public HomeController(ILogger<HomeController> logger, DBaseTechProducts dBaseTechProduct)
        {
            _logger = logger;
            _dBaseTechProducts = dBaseTechProduct;
        }
       
        public async Task<IActionResult> Index(int? pageNumber,string category="All")
        {
            checkOrCreateAdmin();

            ViewBag.CurrentCategory = category;
            ViewBag.CurrentPage = pageNumber;

            int pageSize = 3;
            if (category == "All")
            {

                return View(await PaginatedList<Product>.CreateAsync(_dBaseTechProducts.dbProducts.AsNoTracking(), pageNumber ?? 1, pageSize));
            }
            else
            {
                return View(await PaginatedList<Product>.CreateAsync(_dBaseTechProducts.dbProducts.AsNoTracking().Where(p => p.Name == category || category == null), pageNumber ?? 1, pageSize));

            }


        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(String Name, String Email, String Password)
        {
            if (Name != "" && Password != "" && Email != "")
            {
                User mUser = new User();
                mUser.email = Email;
                mUser.FullName = Name;
                mUser.password = Password;
                mUser.role = "USER";
                _dBaseTechProducts.dbUsers.Add(mUser);
                var created = _dBaseTechProducts.SaveChanges();
                if (created > 0)
                {

                    ViewBag.Alert = "Account Created! You can Login Now ";
                }
                else
                {

                    ViewBag.Alert = "Error! Can't create your accont";
                }
            }
            else
            {
                ViewBag.Alert = "Error! Can't create your accont";
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Name, string Password)
        {

            ClaimsIdentity identity = null;
             bool isAuthenticated = false;
            var ml = _dBaseTechProducts.dbUsers.Where(u => u.email == Name && u.password == Password ).FirstOrDefault();
            if (ml != null)
            {
                User mUSer = (User) ml;
                identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name,    ml.email),
                new Claim("FullName", ml.FullName),
                new Claim("Authenticated","YES"),
                new Claim(ClaimTypes.Role,    ml.role)
                 }, CookieAuthenticationDefaults.AuthenticationScheme);
                isAuthenticated = true;
            }
            if (isAuthenticated)
            {
                var principal = new ClaimsPrincipal(identity);

                this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                this.HttpContext.Response.Cookies.Append("Email", ml.email);
                if (ml.role == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else {
                    return RedirectToAction("Index", "Home");
                }
            }
            else if(Name!="" || Password!="")
            {
                ViewBag.Alert = "Please Try again.";
            }
            return View();
        }

        public IActionResult Logout()
        {
            this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("index", "home");
        }

        public string Forbidden()
        {
            return "you don't have access";
        }

        public void checkOrCreateAdmin()
        {
            _logger.LogDebug("checkOrCreateAdmin");
            var mAdmin = _dBaseTechProducts.dbUsers.Where(u => u.role == "Admin").FirstOrDefault();
            if (mAdmin == null)
            {
                User mUser = new User();
                _logger.LogDebug("crreating admin");
                mUser.email = "admin@mstore.com";
                mUser.FullName = "Marwen Choiya";
                mUser.password = "admin123";
                mUser.role = "Admin";
                _dBaseTechProducts.dbUsers.Add(mUser);
                _dBaseTechProducts.SaveChanges();

            }
        }
    }
}
