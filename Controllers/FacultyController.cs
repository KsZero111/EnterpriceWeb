using EnterpriceWeb.Models;
using EnterpriceWeb.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EnterpriceWeb.Controllers
{
    public class FacultyController : Controller
    {
        private AppDbConText _dbContext;
        private RepoFaculty _repoFaculty;
        public FacultyController(AppDbConText dbContext)
        {
            _dbContext = dbContext;
            _repoFaculty = new RepoFaculty(dbContext);
        }



        [HttpGet]
        public async Task<IActionResult> IndexFaculty()
        {
            List<Faculty> list_faculty = await _repoFaculty.SearhAllFaculty();
            return View(list_faculty);
        }


        [HttpGet]
        public IActionResult CreateFaculty()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFaculty(string ip_name)
        {
            Faculty searchName = await _repoFaculty.SearhFacultyByName(ip_name);
            if (searchName == null)
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
        public IActionResult UpdateFaculty()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFaculty(string ip_name, int id)
        {
            Faculty oldFaculty = await _repoFaculty.SearhFacultyById(id);
            if (oldFaculty != null)
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
            Faculty faculty = await _repoFaculty.SearhFacultyById(facultyId);
            if (faculty != null)
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
