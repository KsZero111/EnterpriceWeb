using EnterpriceWeb.Models;
using EnterpriceWeb.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriceWeb.Controllers
{
    public class MagazineController:Controller
    {
        private AppDbConText _dbContext;
        private RepoMagazine _repoMagazine;
        private ISession session;
        public MagazineController(AppDbConText dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _repoMagazine = new RepoMagazine(dbContext);
            session = httpContextAccessor.HttpContext.Session;
        }
        [HttpGet]
        public async Task<IActionResult> IndexMagazine()
        {
            int id =(int)session.GetInt32("User_id");
            string role = session.GetString("role");
            if(id!=null)
            {
                List<Magazine> list_magazine = await _repoMagazine.SearchAllMagazine();
                ViewBag.role = role;
                return View(list_magazine);
            }
            else
            {
                return View("error");
            }
           
        }


        [HttpGet]
        public IActionResult CreateMagazine()
        {
            if (TempData["Error"] != null)  ViewBag.Eror= TempData["Error"].ToString();
            int id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            if (id!=null && role=="admin")
            {
                return View();
            }
            else
            {
                return View("error");
            }
           
        }

        [HttpPost]
        public async Task<IActionResult> CreateMagazine([FromForm] Magazine magazine)
        {
            int id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            Magazine searchTitle = await _repoMagazine.SearchMagazineByTitle(magazine.magazine_title);
            if (id != null && role == "admin"&& searchTitle==null)
            {
                HandleCreateMagazine(magazine);
                RedirectToAction("IndexMagazine", "Magazine");
            }
            else
            {
                TempData["Error"] = "The title is already used, please put another one";
                return RedirectToAction("CreateMagazine");
            }
            return RedirectToAction("error");
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
        public async Task <IActionResult> UpdateMagazine(int id)
        {
            int user_id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            Magazine magazine= await _repoMagazine.SearchMagazineById(id);
            if (id != null && role == "admin" && magazine != null)
            {
                return View(magazine);
            }
            else
            {
                return View("error");
            }
            return View("error");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMagazine(int id,[FromForm] Magazine magazine)
        {
            int user_id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            Magazine oldMezine = await _repoMagazine.SearchMagazineById(id);
            if (user_id!=null && role=="admin" && oldMezine != null)
            {
                HandleUpdateMagazine(magazine,oldMezine);
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
        public async Task<IActionResult> DeleteMagazine(int id)
        {
            int user_id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            Magazine magazine = await _repoMagazine.SearchMagazineById(id);
            if (user_id!=null && role=="admin" && magazine!=null)
            {
                HandleDeleteMagazine(magazine);
            }
            else
            {
                return RedirectToAction("IndexMagazine");
            }
            return RedirectToAction("erorr");
        }

        private void HandleDeleteMagazine(Magazine magazine)
        {
            magazine.magazine_deleted = "1";
            _dbContext.Update(magazine);
            _dbContext.SaveChanges();
        }
    }
}

