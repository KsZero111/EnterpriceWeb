
using EnterpriceWeb.Models;
using EnterpriceWeb.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;

namespace EnterpriceWeb.Controllers
{
    public class AccountController:Controller
    {
        private readonly AppDbConText _dbContext;
        private readonly RepoAccount _repoAccount;
        public AccountController(AppDbConText dbContext)
        {
            _dbContext = dbContext;
            _repoAccount = new RepoAccount(dbContext);
            
        }
        //Register Account
        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                if (_repoAccount.Register(_user).Equals(null))
                {
                    _dbContext.users.Add(_user);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            return View();
        }

        //Login Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string gmail, string password)
        {
            if (ModelState.IsValid)
            {
                var data = _repoAccount.login(gmail,password);
                if (data.Count()!=0)
                {
                    HttpContext.Session.SetString("gmail", data.First().us_gmail);
                    HttpContext.Session.SetInt32("User_id", data.First().us_id);
                    HttpContext.Session.SetString("role", data.First().us_role);
                    if (HttpContext.Session.GetString("role").Equals("admin"))
                    {
                        return RedirectToAction("Admin");
                    }
                    else if(HttpContext.Session.GetString("role").Equals("coordinator"))
                    {
                        TempData["role"] = HttpContext.Session.GetInt32("User_id");
                        return RedirectToAction("Coordinator"+HttpContext.Session.GetInt32("User_id"));
                    }
                    else
                    {
                        return View("Student"+HttpContext.Session.GetInt32("User_id"));

                    }
                    return RedirectToAction("~/Home/Private");
                }
                else
                {
                    TempData["erorr"] = "Login failed";
                    return View();
                }
            }
            return View();
        }
        public ActionResult Admin()
        {
            return View();
        }
        public ActionResult Coordinator()
        {
            return View();
        }
        public ActionResult Student()
        {
            return View();
        }

        //Logout Account
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }


        //Provide Permissions Account
          

        //Delete Account
    }
}
