using EnterpriceWeb.Models;
using EnterpriceWeb.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using System.Text.Json;

namespace EnterpriceWeb.Controllers
{
    public class FacultyController : Controller
    {
        private AppDbConText _dbContext;
        private RepoFaculty _repoFaculty;
        private ISession session;
        public FacultyController(AppDbConText dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _repoFaculty = new RepoFaculty(dbContext);
            session = httpContextAccessor.HttpContext.Session;
        }

        [HttpGet]
        public async Task<IActionResult> IndexFaculty()
        {
            int user_id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");

            List<Faculty> faculties = await _repoFaculty.SearhAllFaculty();
            if (user_id != null && role == "admin" && faculties.Count() > 0)
            {
                List<Faculty> list_faculty = await _repoFaculty.SearhAllFaculty();
                return View(list_faculty);
            }
            else
            {
                return View("error");
            }
        }


        [HttpGet]
        public IActionResult CreateFaculty()
        {
            int user_id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            if (user_id != null && role == "admin")
            {
                return View();
            }
            else
            {
                return View("erorr");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateFaculty(string ip_name)
        {
            int user_id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            Faculty searchName = await _repoFaculty.SearhFacultyByName(ip_name);
            if (searchName == null && user_id != null && role == "admin")
            {
                HandleCreateFactulty(ip_name);
            }
            else
            {
                return RedirectToAction("CreateFaculty");
            }
            return RedirectToAction("IndexFaculty");
        }

        private void HandleCreateFactulty(string ip_name)
        {
            Faculty faculty = new Faculty();
            faculty.f_name = ip_name;
            _dbContext.Add(faculty);
            _dbContext.SaveChanges();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateFaculty(int id)
        {
            int user_id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            Faculty faculty= await _repoFaculty.SearhFacultyById(id);
            if (faculty != null && user_id != null && role == "admin")
            {
                return View(faculty);
            }
            else
            {
                return View("error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFaculty(string ip_name, int id)
        {
            int user_id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            Faculty oldFaculty = await _repoFaculty.SearhFacultyById(id);
            if (oldFaculty != null && user_id != null && role == "admin")
            {
                HandleUpdateFactulty(oldFaculty, ip_name);
            }
            else
            {
                return RedirectToAction("UpdateFaculty");
            }

            return RedirectToAction("IndexFaculty");
        }

        private void HandleUpdateFactulty(Faculty faculty, string ip_name)
        {
            faculty.f_name = ip_name;
            _dbContext.Update(faculty);
            _dbContext.SaveChanges();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFaculty(int facultyId)
        {
            int user_id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            Faculty faculty = await _repoFaculty.SearhFacultyById(facultyId);
            if (faculty != null && user_id != null && role == "admin")
            {
                HandleDeleteFactulty(faculty);
            }
            else
            {
                return RedirectToAction("IndexFaculty");
            }
            return RedirectToAction("IndexFaculty");
        }

        private void HandleDeleteFactulty(Faculty faculty)
        {
            faculty.f_status = "0";
            _dbContext.Update(faculty);
            _dbContext.SaveChanges();
        }



    }
}
