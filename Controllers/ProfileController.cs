﻿using EnterpriceWeb.Models;
using EnterpriceWeb.Repository;
using EnterpriceWeb;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EnterpriceWeb.Controllers
{
    public class ProfileController : Controller
    {
        private AppDbConText _dbContext;
        private RepoAccount _repoAccount;
        private RepoFaculty _repoFaculty;
        private ISession Session;
        public ProfileController(AppDbConText dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _repoAccount = new RepoAccount(dbContext);
            _repoFaculty = new RepoFaculty(dbContext);
            Session = httpContextAccessor.HttpContext.Session;
        }
        [HttpGet]
        public async Task<IActionResult> IndexProfile()
        {
            string role;
            int user_id;
            try
            {
                role = Session.GetString("role");
                user_id = (int)Session.GetInt32("User_id");
            }
            catch (Exception)
            {
                throw;
            }


            if (user_id != 0)
            {
                User user = await _repoAccount.SearhUserById(user_id);
                Faculty faculty = await _repoFaculty.SearhFacultyById(user.f_id);
                ViewBag.faculty = faculty;

                if (role != null && role.Equals("admin") || role.Equals("guest"))
                {
                    ViewBag.layout = "_LayoutAdmin";
                }
                else ViewBag.layout = "_Layout";
                ViewBag.role=role;
                return View(user);
            }
            return RedirectToAction("NotFound", "Home");
        }


        [HttpGet]
        public async Task<IActionResult> UpdateProfile(int id)
        {

            int user_id = (int)Session.GetInt32("User_id");
            string role = Session.GetString("role");
            if (((user_id != null) && (user_id == id)) || role == "admin")
            {
                User user = await _repoAccount.SearhUserById(id);
                List<Faculty> list_Faculty = await _repoFaculty.SearhAllFaculty();
                ViewBag.ListFaculty = list_Faculty;
                return View(user);
            }
            return RedirectToAction("NotFound", "Home");

        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile([FromForm] User user, int id, IFormFile avatar)
        {
            User oldUser = await _repoAccount.SearhUserById(id);
            int user_id = (int)Session.GetInt32("User_id");
            if (oldUser != null && user_id != null)
            {
                await HandleUpdateProfile(oldUser, user, avatar);
            }
            else
            {
                return RedirectToAction("NotFound", "Home");
            }
            return RedirectToAction("AccountManagement", "Account");
        }
        private async Task HandleUpdateProfile(User oldUser, User newUser, IFormFile avatar)
        {
            try
            {
                oldUser.us_name = newUser.us_name;
                oldUser.f_id = newUser.f_id;
                oldUser.us_phone = newUser.us_phone;
                oldUser.us_gender = newUser.us_gender;
                oldUser.us_gmail = newUser.us_gmail;
                oldUser.us_start_year = newUser.us_start_year;
                oldUser.us_end_year = newUser.us_end_year;
                oldUser.us_role = newUser.us_role;
                if (avatar != null)
                {
                    SupportFile.Instance.DeleteFileAsync(oldUser.us_image, "image/User");
                    string filename = await SupportFile.Instance.SaveFileAsync(avatar, "image/User");
                    oldUser.us_image = filename;
                }


                _dbContext.Update(oldUser);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
