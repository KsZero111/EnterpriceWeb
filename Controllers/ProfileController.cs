using EnterpriceWeb.Models;
using EnterpriceWeb.Repository;
using EnterpriceWeb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;

namespace EnterpriceWeb.Controllers
{
    public class ProfileController : Controller
    {
        private AppDbConText _dbContext;
        private RepoAccount _repoAccount;
        private RepoFaculty _repoFaculty;
        public ProfileController(AppDbConText dbContext)
        {
            _dbContext = dbContext;
            _repoAccount = new RepoAccount(dbContext);
            _repoFaculty = new RepoFaculty(dbContext);
        }



        [HttpGet]
        public async Task<IActionResult> IndexProfile(int id)
        {
            User user = await _repoAccount.SearhUserById(id);
            Faculty faculty = await _repoFaculty.SearhFacultyById(user.faculty.f_id);
            ViewBag.faculty = faculty;
            return View(user);
        }


        [HttpGet]
        public async Task<IActionResult> UpdateProfile(int id)
        {
            User user = await _repoAccount.SearhUserById(id);
            List<Faculty> list_Faculty = await _repoFaculty.SearhAllFaculty();
            ViewBag.ListFaculty = list_Faculty;
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile([FromForm] User user, int id, IFormFile avatar)
        {
            User oldUser = await _repoAccount.SearhUserById(id);
            if (oldUser != null)
            {
                HandleUpdateProfile(oldUser, user, avatar);
            }
            else
            {
                return RedirectToAction("UpdateProfile");
            }

            return RedirectToAction("IndexProfile");
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
                if (oldUser.us_image != null && avatar != null)
                {
                    SupportFile.Instance.DeleteFileAsync(oldUser.us_image, "image/User");
                }
                string filename = await SupportFile.Instance.SaveFileAsync(avatar, "image/User");
                oldUser.us_image = filename;
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
