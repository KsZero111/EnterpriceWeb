using EnterpriceWeb.Models;
using EnterpriceWeb.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriceWeb.Controllers
{
    public class MagazineController:Controller
    {
        private AppDbConText _dbContext;
        private RepoMagazine _repoMagazine;
        public MagazineController(AppDbConText dbContext)
        {
            _dbContext = dbContext;
            _repoMagazine = new RepoMagazine(dbContext);
        }



        [HttpGet]
        public async Task<IActionResult> IndexMagazine()
        {
            List<Magazine> list_magazine = await _repoMagazine.SearchAllMagazine();
            return View(list_magazine);
        }


        [HttpGet]
        public IActionResult CreateMagazine()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMagazine([FromForm] Magazine magazine)
        {
            Magazine searchTitle = await _repoMagazine.SearchMagazineByTitle(magazine.magazine_title);
            if (searchTitle == null)
            {
                HandleCreateMagazine(magazine);
            }
            else
            {
                return RedirectToAction("CreateFaculty");
            }
            return RedirectToAction("IndexFaculty");
        }

        private void HandleCreateMagazine(Magazine ip_magazine)
        {
            Magazine magazine = new Magazine();
            //magazine.magazine_title = ip_magazine.magazine_title;
            //DateTime.Today.ToString()

            //_dbContext.Add(magazine);
            //_dbContext.SaveChanges();
        }

        //[HttpGet]
        //public IActionResult UpdateFaculty()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> UpdateFaculty(string ip_name, int id)
        //{
        //    Faculty oldFaculty = await _repoFaculty.SearhFacultyById(id);
        //    if (oldFaculty != null)
        //    {
        //        HandleUpdateFactulty(oldFaculty, ip_name);
        //    }
        //    else
        //    {
        //        return RedirectToAction("UpdateFaculty");
        //    }

        //    return RedirectToAction("IndexFaculty");
        //}

        //private void HandleUpdateFactulty(Faculty faculty, string ip_name)
        //{
        //    faculty.f_name = ip_name;
        //    _dbContext.Update(faculty);
        //    _dbContext.SaveChanges();
        //}

        //[HttpPost]
        //public async Task<IActionResult> DeleteFaculty(int facultyId)
        //{
        //    Faculty faculty = await _repoFaculty.SearhFacultyById(facultyId);
        //    if (faculty != null)
        //    {
        //        HandleDeleteFactulty(faculty);
        //    }
        //    else
        //    {
        //        return RedirectToAction("IndexFaculty");
        //    }
        //    return RedirectToAction("IndexFaculty");
        //}

        //private void HandleDeleteFactulty(Faculty faculty)
        //{
        //    faculty.f_status = "0";
        //    _dbContext.Update(faculty);
        //    _dbContext.SaveChanges();
        //}
    }
}

