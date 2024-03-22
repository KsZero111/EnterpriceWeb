using EnterpriceWeb.Mailutils;
using EnterpriceWeb.Models;
using EnterpriceWeb.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
namespace EnterpriceWeb.Controllers
{
    public class AdminController : Controller
    {
        private AppDbConText _dbContext;
        private RepoArticle _repoArticle;
        private RepoFeedBack _repoFeedBack;
        private RepoArticle_file _repoArticle_File;
        private RepoFaculty _repoFaculty;


        private ISession session;
        private RepoAccount _repoAccount;
        private RepoMagazine _repoMagazine;

        private SendMailSystem mailSystem;
        public AdminController(AppDbConText dbContext, IHttpContextAccessor httpContextAccessor, IEmailSender emailSender, IWebHostEnvironment hostEnvironment)

        {
            _dbContext = dbContext;
            _repoArticle = new RepoArticle(dbContext);
            _repoMagazine = new RepoMagazine(dbContext);
            _repoArticle_File = new RepoArticle_file(dbContext);
            _repoFeedBack = new RepoFeedBack(dbContext);
            _repoAccount = new RepoAccount(dbContext);
            _repoFaculty = new RepoFaculty(dbContext);


            session = httpContextAccessor.HttpContext.Session;
            mailSystem = new SendMailSystem(emailSender, hostEnvironment);
        }




        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int user_id;
            string role;

            List<Article> list_Article = new List<Article>();
            //check null
            try
            {
                user_id = (int)session.GetInt32("User_id");
                role = session.GetString("role");
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Home");
            }
            ViewBag.role = role;

            int art_Acccept = await _repoArticle.SearhAllArticle_Accept_DashboardAsync();
            int art_Inprocessing = await _repoArticle.SearhAllArticle_Inprocessing_DashboardAsync();
            int art_Refuse = await _repoArticle.SearhAllArticle_Refuse_DashboardAsync();
            int art_total = await _repoArticle.SearhAllArticle();
          
            ViewBag.art_acccept = art_Acccept;
            ViewBag.art_percent_acccept = percent(art_Acccept,art_total);


            ViewBag.art_inprocessing = art_Inprocessing;
            ViewBag.art_percent_inprocessing = percent(art_Inprocessing, art_total);

            ViewBag.art_refuse = art_Refuse;
            ViewBag.art_percent_refuse = percent(art_Refuse, art_total);

            ViewBag.art_total = art_total;


            return View();
        }
        public int percent(int number, int total)
        {
            if (total <= 0)
            {
                return 0;
            }
            return (int)(((double)number * 100) / total);
        }

        [HttpGet]
        public async Task<IActionResult> Data()
        {
            List<int> art_total_list = new List<int>();
            List<string> f_name_total_list = new List<string>();

            //list string name faculty

            int[] f_id = await _repoFaculty.GetIdAllFaculty();
            foreach (int i in f_id)
            {
                f_name_total_list.Add((await _repoFaculty.SearhFacultyById(i)).f_name);
                int total_Article_fromFaculty = 0;
                List<User> users_list = await _repoAccount.SearAllhUserById(i);
                if (users_list != null)
                {
                    int total_Article_File_from_User = 0;
                    foreach (User user in users_list)
                    {
                        List<Article> csjkdnajck = await _repoArticle.SearhAllArticleDashboard(user.us_id);
                       int getall_article_byuser = (await _repoArticle.SearhAllArticleDashboard(user.us_id)).Count();
                        total_Article_File_from_User += getall_article_byuser;
                    }
                    total_Article_fromFaculty += total_Article_File_from_User;
                }
                art_total_list.Add(total_Article_fromFaculty);
            }
            ViewBag.art_file_total_arr = art_total_list;
            ViewBag.f_name_total_arr = f_name_total_list;
            return Ok(JsonSerializer.Serialize(new
            {
                art_file_total_arr = ViewBag.art_file_total_arr
                ,
                f_name_total_arr = ViewBag.f_name_total_arr
            }));
        }

    }
}
