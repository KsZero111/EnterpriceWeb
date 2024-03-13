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
            List<int> art_count_list = new List<int>();
            List<string> f_name_list = new List<string>();

            //list string name faculty

            int[] f_id = await _repoFaculty.GetIdAllFaculty();
            foreach (int i in f_id)
            {
                int count = 0;
                List<User> users_list = await _repoAccount.SearAllhUserById(i);
                if (users_list != null)
                {
                    f_name_list.Add(users_list[0].faculty.f_name);
                    int articles_file;
                    foreach (User user in users_list)
                    {
                        List<Article> articles = await _repoArticle.SearhAllArticleDashboard(user.us_id);
                        if (articles != null)
                        {
                            articles_file = (await _repoArticle_File.SearhAllArticleFileById(i)).Count();
                            art_count_list.Add(articles_file);
                            count += articles_file;
                        }
                    }
                }
                art_count_list.Add(count);
            }

            int[] art_count_arr = art_count_list.ToArray();
            string[] f_name_arr = f_name_list.ToArray();
            //get name
            // get user trùng f_id=> get article trùng id=> get count
            //list so lượng article của mỗi faculty
            return View();
        }
    }
}
