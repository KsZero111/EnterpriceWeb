using EnterpriceWeb.Mailutils;
using EnterpriceWeb.Models;
using EnterpriceWeb.Repository;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<IActionResult> Index()
        {
            int[] art_count_arr;
            string[] f_name;
            //list string name faculty

            int[] f_id = await _repoFaculty.GetIdAllFaculty();
            foreach (int i in f_id)
            {
                List<User> users = await _repoAccount.SearAllhUserById(i);
                foreach (User user in users)
                {
                    //int quantity_art=_repoArticle.SearhAllArticleCoordinator
                }
            }

            // get user trùng f_id=> get article trùng id=> get count
            //list so lượng article của mỗi faculty
            return View();
        }
    }
}
