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
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Data()
        {
            List<int> art_file_total_list = new List<int>();
            List<string> f_name_total_list = new List<string>();

            //list string name faculty

            int[] f_id = await _repoFaculty.GetIdAllFaculty();
            foreach (int i in f_id)
            {
                f_name_total_list.Add((await _repoFaculty.SearhFacultyById(i)).f_name);
                int total_Article_File_fromFaculty = 0;
                List<User> users_list = await _repoAccount.SearAllhUserById(i);
                if (users_list != null)
                {
                    //if (users_list.Count > 0) f_name_total_list.Add(users_list[0].faculty.f_name);

                    int total_Article_File_from_User = 0;
                    foreach (User user in users_list)
                    {
                        List<Article> articles = await _repoArticle.SearhAllArticleDashboard(user.us_id);
                        if (articles != null)
                        {
                            int total_Article_File_from_Article = 0;
                            foreach (Article article in articles)
                            {
                                int soluong_article_file = (await _repoArticle_File.SearhAllArticleFileById(article.article_id)).Count();
                                total_Article_File_from_Article += soluong_article_file;
                            }
                            total_Article_File_from_User += total_Article_File_from_Article;
                        }
                    }
                    total_Article_File_fromFaculty += total_Article_File_from_User;
                }
                art_file_total_list.Add(total_Article_File_fromFaculty);
            }
            ViewBag.art_file_total_arr = art_file_total_list;
            ViewBag.f_name_total_arr = f_name_total_list;
            return Ok(JsonSerializer.Serialize(new
            {
                art_file_total_arr = ViewBag.art_file_total_arr
                ,
                f_name_total_arr = ViewBag.f_name_total_arr
            }));
        }
        [HttpGet]
        public async Task<IActionResult> DataArticle()
        {
            int art_Acccept = await _repoArticle.SearhAllArticle_Accept_DashboardAsync();
            int art_Inprocessing = await _repoArticle.SearhAllArticle_Inprocessing_DashboardAsync();
            int art_Refuse = await _repoArticle.SearhAllArticle_Refuse_DashboardAsync();
            ViewBag.art_Acccept = art_Acccept;
            ViewBag.art_Inprocessing = art_Inprocessing;
            ViewBag.art_Refuse = art_Refuse;
            return View();
        }
    }
}
