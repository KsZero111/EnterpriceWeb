﻿using EnterpriceWeb.Mailutils;
using EnterpriceWeb.Models;
using EnterpriceWeb.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.Extensions.Hosting;
using MySqlX.XDevAPI;
using NuGet.Protocol;
using System;
using System.Numerics;
using System.Text;

namespace EnterpriceWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbConText _dbContext;
        private readonly RepoAccount _repoAccount;
        private ISession Session;
        private RepoFaculty _repoFaculty;
        private SendMailSystem mailSystem;
        public AccountController(AppDbConText dbContext, IHttpContextAccessor httpContextAccessor, IEmailSender emailSender, IWebHostEnvironment hostEnvironment)
        {
            _dbContext = dbContext;
            _repoAccount = new RepoAccount(dbContext);
            _repoFaculty = new RepoFaculty(dbContext);
            Session = httpContextAccessor.HttpContext.Session;
            mailSystem = new SendMailSystem(emailSender, hostEnvironment);
        }
        //Register Account
        public async Task<ActionResult> Register()
        {
            List<Faculty> list_Faculty = await _repoFaculty.SearhAllFaculty();
            ViewBag.ListFaculty = list_Faculty;
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([FromForm] User _user, IFormFile image)
        {
            List<Faculty> list_Faculty = await _repoFaculty.SearhAllFaculty();
            ViewBag.ListFaculty = list_Faculty;
            if (image != null)
            {
                string type = Path.GetFileName(image.FileName);
                type = type.Substring(type.LastIndexOf("."));
                if (type == ".png" || type == ".jpg" || type == ".csv")
                {
                    var user = await _repoAccount.Register(_user);
                    if (user == null)
                    {
                        //_user.us_password = MD5(_user.us_password);
                      await  HandleRegister(_user, image);
                        ViewBag.LoginSuccess = "Register successfull";
                        return View();
                    }
                    else
                    {
                        TempData.Clear();
                        TempData["erorr"] = "Email already exists";
                        return View();
                    }
                }
                else
                {
                    TempData.Clear();
                    TempData["erorr"] = "Please choose avatar is .jpg or .png";
                    return View();
                }
            }
            else
            {
                return View();
            }

        }
        private async Task HandleRegister(User newUser, IFormFile avatar)
        {
            try
            {

                if (avatar != null)
                {
                    string filename = await SupportFile.Instance.SaveFileAsync(avatar, "image/User");
                    newUser.us_image = filename;
                    _dbContext.users.Add(newUser);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(string gmail)
        {
            if (gmail != null)
            {
                User user1 = await _repoAccount.SearhUserBymail(gmail);
                if (user1 != null)
                {
                    string newpassword = await mailSystem.SendgmailForgetPassword(gmail);
                    changesPassword(newpassword, user1);
                    ViewBag.forgot = "success";
                    return View();
                }
                else
                {
                    ViewBag.forgot = "fail";
                    return View();
                }
            }
            return View();
        }

        private void changesPassword(string newpass, User user)
        {
            //newpass=MD5(newpass);
            user.us_password = newpass;
            _dbContext.Update(user);
            _dbContext.SaveChanges();
        }
        public IActionResult ChangesPassword(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangesPassword(int id, string password, string C_password)
        {
            int user_id = (int)Session.GetInt32("User_id");
            string role = Session.GetString("role");
            if ((user_id == id || role == "admin") && password == C_password)
            {
                User user = await _repoAccount.SearhUserById(id);
                changesPassword(password, user);
                if (role == "admin")
                {
                    return View("AccountManagement");
                }
                else
                {
                    return RedirectToAction("IndexProfile", "Profile");
                }
            }
            return RedirectToAction("NotFound", "Home");
        }
        public string MD5(string s)
        {
            using var provider = System.Security.Cryptography.MD5.Create();
            StringBuilder builder = new StringBuilder();

            foreach (byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(s)))
                builder.Append(b.ToString("x2").ToLower());

            return builder.ToString();
        }
        public async Task<ActionResult> AccountManagement()
        {
            int user_id;
            string role;
            List<User> list_user;
            try
            {
                user_id = (int)Session.GetInt32("User_id");
                role = Session.GetString("role");
            }
            catch (Exception)
            {
                return View("Error");
            }
            if (role != null && role.Equals("admin"))
            {
                list_user = await _repoAccount.SearhAllUser();
                return View(list_user);
            }
            else
            {
                return View("Error");
            }


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
                //password = MD5(password);
                var data = _repoAccount.login(gmail, password);
                if (data.Count() != 0)
                {
                    HttpContext.Session.SetString("gmail", data.First().us_gmail);
                    HttpContext.Session.SetInt32("User_id", data.First().us_id);
                    HttpContext.Session.SetString("role", data.First().us_role);
                    if (HttpContext.Session.GetString("role").Equals("admin"))
                    {
                        return RedirectToAction("Index", "Admin");

                    }
                    else
                    {
                        return RedirectToAction("IndexMagazine", "Magazine");
                    }
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
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> Index()
        {
            List<User> user = await _repoAccount.SearhAllUser();
            return View(user);
        }

        //Delete Account
        public async Task<IActionResult> DeleteAccount(int id)
        {
            User user = await _repoAccount.SearhUserById(id);
            if (user != null)
            {
                HandlderDeleteAccount(user);
                return RedirectToAction("AccountManagement", "Admin");
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