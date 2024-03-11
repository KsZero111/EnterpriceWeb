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
            int user_id = (int)Session.GetInt32("User_id");
            if (user_id != 0)
            {
                User user = await _repoAccount.SearhUserById(user_id);
                Faculty faculty = await _repoFaculty.SearhFacultyById(user.f_id);
                ViewBag.faculty = faculty;
                return View(user);
            }
            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> IndexProfileUser(int id)
        {
            if (id != 0)
            {
                User user = await _repoAccount.SearhUserById(id);
                return View(user);
            }
            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfile(int id)
        {
            int user_id;
            User user;
            try
            {
                user_id = (int)Session.GetInt32("User_id");
                user = await _repoAccount.SearhUserById(id);
            }
            catch (Exception)
            {
                return View("Erorr");
            }

            List<Faculty> list_Faculty = await _repoFaculty.SearhAllFaculty();
            ViewBag.ListFaculty = list_Faculty;
            return View(user);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile([FromForm] User user, int id, IFormFile avatar)
        {
            int user_id;
            try
            {
                user_id = (int)Session.GetInt32("User_id");
            }
            catch (Exception)
            {
                return View("Error");
            }
            if (user_id.Equals("admin") && id != 0)
            {
                User oldUser = await _repoAccount.SearhUserById(id);
                await HandleUpdateProfile(oldUser, user, avatar);
            }
            else
            {
                return View("Error");
            }

            return RedirectToAction("IndexAccount", "Account");
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
