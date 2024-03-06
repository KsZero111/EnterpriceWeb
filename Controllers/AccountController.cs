
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
        public ActionResult Register([FromForm]User _user)
        {
            //if (ModelState.IsValid)
            {
                var user =_repoAccount.Register(_user);
                _user.us_role = "student";  
                
                if (user==null)
                {
                    _dbContext.users.Add(_user);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Login");
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
                        return RedirectToAction("Index","Admin");
                    }
                    else if(HttpContext.Session.GetString("role").Equals("coordinator"))
                    {
                        
                        return RedirectToAction("Index","Coordinator");
                    }
                    else
                    {
                        return View("Index","Student");

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

        //Logout Account
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Index()
        {
            List<User> user = await _repoAccount.SearhAllUser();
            return View(user);
        }

        //Provide Permissions Account
          

        //Delete Account
        public async Task<IActionResult> DeleteAccount(int id)
        {
            User user = await _repoAccount.SearhUserById(id);
            if (user != null)
            {
                HandlderDeleteAccount(user);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View();
            }
        }
        
        private void HandlderDeleteAccount(User user)
        {
            user.us_role = "0";
            _dbContext.Update(user);
            _dbContext.SaveChanges();
        }
        
        
    }
}
