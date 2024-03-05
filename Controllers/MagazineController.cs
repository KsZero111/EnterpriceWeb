using EnterpriceWeb.Models;
using EnterpriceWeb.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriceWeb.Controllers
{
    public class MagazineController : Controller
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
                RedirectToAction("IndexMagazine", "Magazine");
            }
            else
            {
                TempData["erorr"] = "Magazine title is exists";
                return RedirectToAction("CreateMagazine");
            }
            return RedirectToAction("IndexMagazine");
        }

        private void HandleCreateMagazine(Magazine ip_magazine)
        {
            Magazine magazine = new Magazine();
            magazine.magazine_title = ip_magazine.magazine_title;
            magazine.magazine_closure_date = ip_magazine.magazine_closure_date;
            magazine.magazine_final_closure_date = ip_magazine.magazine_final_closure_date;
            magazine.magazine_academic_year = ip_magazine.magazine_academic_year;
            _dbContext.Add(magazine);
            _dbContext.SaveChanges();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateMagazine(int id)
        {
            Magazine magazine = await _repoMagazine.SearchMagazineById(id);
            return View(magazine);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMagazine(int id, [FromForm] Magazine magazine)
        {
            Magazine oldMezine = await _repoMagazine.SearchMagazineById(id);
            if (oldMezine != null)
            {
                HandleUpdateMagazine(magazine, oldMezine);
                return RedirectToAction("IndexMagazine");
            }
            else
            {
                return RedirectToAction("~/404");
            }
        }

        private void HandleUpdateMagazine(Magazine magazine, Magazine oldmagazine)
        {
            oldmagazine.magazine_title = magazine.magazine_title;
            oldmagazine.magazine_closure_date = magazine.magazine_closure_date;
            oldmagazine.magazine_final_closure_date = magazine.magazine_final_closure_date;
            oldmagazine.magazine_academic_year = magazine.magazine_academic_year;
            _dbContext.Update(oldmagazine);
            _dbContext.SaveChanges();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMagazine(int facultyId)
        {
            Magazine magazine = await _repoMagazine.SearchMagazineById(facultyId);
            if (magazine != null)
            {
                HandleDeleteMagazine(magazine);
            }
            else
            {
                return RedirectToAction("IndexMagazine");
            }
            return RedirectToAction("IndexMagazine");
        }

        private void HandleDeleteMagazine(Magazine magazine)
        {
            magazine.magazine_deleted = "1";
            _dbContext.Update(magazine);
            _dbContext.SaveChanges();
        }
    }
}

