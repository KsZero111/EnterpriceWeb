using EnterpriceWeb.Models;
using EnterpriceWeb.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriceWeb.Controllers
{
    public class ArticleController : Controller
    {
        private AppDbConText _dbContext;
        private RepoArticle _repoArticle;
        private RepoFeedBack _repoFeedBack;
        private RepoArticle_file _repoArticle_File;
        private ISession session;
        private RepoMagazine _repoMagazine;
        public ArticleController(AppDbConText dbContext,IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _repoArticle = new RepoArticle(dbContext);
            _repoMagazine = new RepoMagazine(dbContext);
            _repoArticle_File = new RepoArticle_file(dbContext);
            _repoFeedBack= new RepoFeedBack(dbContext);
            session = httpContextAccessor.HttpContext.Session;
        }

        [HttpGet]
        public async Task<IActionResult> IndexArticle(int id)
        {
            int user_id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            List<Article> list_Article = await _repoArticle.SearhAllArticle(user_id, id);
            if (user_id!=null && role!="admin" && list_Article.Count()>0)
            {
                ViewBag.m_id = id;
                return View(list_Article);
            }
            else
            {
                return View("error");
            }
            
        }


        [HttpGet]
        public async Task<IActionResult> CreateArticle(int id)
        {
            int user_id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            Magazine Magazine = await _repoMagazine.SearchMagazineById(id);
            if(user_id!=null&& role=="student"&& Magazine == null)
            {
                ViewBag.Magazine = Magazine;
                return View();
            }
            else
            {
                return View("error");
            }
           
        }


        [HttpPost]
        public async Task<IActionResult> CreateArticle([FromForm] Article inputArticle, IFormFile avatarArticle)
        {
<<<<<<< Updated upstream
            await HandleCreateArticle(inputArticle.magazine_id, inputArticle.article_title, avatarArticle);
            return RedirectToAction("IndexArticle", "Article", new {id=inputArticle.magazine_id});
=======
            int user_id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            
            if (user_id != null && role == "student")
            {
                await HandleCreateArticle(inputArticle.magazine_id, inputArticle.article_title, avatarArticle);
                return RedirectToAction("IndexArticle", "Article", new { id = inputArticle.magazine_id });
            }
            else
            {
                return View("error");
            }
            
>>>>>>> Stashed changes
        }

        private async Task HandleCreateArticle(int magazine_id, string article_title, IFormFile avatarArticle)
        {
            Article article = new Article();
            //int id = (int)HttpContext.Session.GetInt32("User_id");
            //article.us_id = id;

            article.us_id = 1;
            article.magazine_id = magazine_id;
            article.article_title = article_title;
            string filename = await SupportFile.Instance.SaveFileAsync(avatarArticle, "image/Article");
            article.article_avatar = filename;
            article.article_submit_date = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt");
            _dbContext.Add(article);
            _dbContext.SaveChanges();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateArticle(int id)
        {
            int user_id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            Article article = await _repoArticle.SearhArticleById(id);
            if (user_id != null && role =="student"&&article.us_id==user_id)
            {
                return View(article);
            }
            else
            {
                return View("error");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> UpdateArticle([FromForm] Article article, IFormFile avatar)
        {
            int user_id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            Article oldArticle = await _repoArticle.SearhArticleById(article.article_id);
            if (user_id!=null&&role=="student"&&oldArticle != null)
            {
<<<<<<< Updated upstream
                await HandleUpdateProfile(oldArticle, article, avatar);
            }
            else
            {
                return RedirectToAction("UpdateProfile");
            }
            return RedirectToAction("IndexProfile");
=======
                await HandleUpdateArticle(oldArticle, article, avatar);
                return RedirectToAction("UpdateProfile", "Article", new { id = article.article_id });
            }
            else
            {
                return RedirectToAction("error");
            }
            
>>>>>>> Stashed changes
        }

        private async Task HandleUpdateProfile(Article oldArticle, Article newArticle, IFormFile avatar)
        {
            try
            {
                oldArticle.article_title = newArticle.article_title;

                if (oldArticle.article_avatar != null && avatar != null)
                {
                    SupportFile.Instance.DeleteFileAsync(oldArticle.article_avatar, "image/Article");
                    string filename = await SupportFile.Instance.SaveFileAsync(avatar, "image/Article");
                    oldArticle.article_avatar = filename;
                }
                _dbContext.Update(oldArticle);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<IActionResult> DeleteArticle(int id)
        {
            int user_id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            Article article = await _repoArticle.SearhArticleById(id);
            if (article != null&&user_id!=null&&role=="student")
            {
                HandleDeleteFactulty(article);
            }
            else
            {
                return RedirectToAction("IndexFaculty");
            }
            return RedirectToAction("IndexFaculty");
        }

        private void HandleDeleteFactulty(Article article)
        {
<<<<<<< Updated upstream
            article.article_status = "999";
=======
            List<Article_file> list = await _repoArticle_File.SearhAllArticleFileById(article.article_id);

            foreach (Article_file file in list)
            {
                _dbContext.Remove(file);
            }
            SupportFile.Instance.DeleteFileAsync(article.article_avatar, "image/Article");

            _dbContext.Remove(article);
            _dbContext.SaveChanges();
        }

        [HttpPost]
        public async Task<IActionResult> AcceptArticle(int id, int id_User)
        {
            int user_id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            //check user

            //check null
            Article article = await _repoArticle.SearhArticleById(id);
            if (article != null&&user_id!=null&&role=="coordinator")
            {
                await HandleAcceptArticle(article);
            }
            else
            {
                return BadRequest("Article not found");
            }
            return Ok();
        }

        private async Task HandleAcceptArticle(Article article)
        {
            article.article_status = "Accept";
            article.article_accept_date = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt");
>>>>>>> Stashed changes
            _dbContext.Update(article);
            _dbContext.SaveChanges();
        }
        //CreateFeedback
        public IActionResult CreateFeedBack() { return View(); }
        [HttpPost]
        public IActionResult CreateFeedBack([FromForm] Feedback feedback, int article_id)
        {
<<<<<<< Updated upstream
            if (!feedback.content.Equals(null))
            {
                HandleCreateFeedBack(article_id, feedback.content);
                return View("~/");
=======
            int user_id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            //check user

            //check null
            Article article = await _repoArticle.SearhArticleById(id);
            if (article != null && user_id != null && role == "coordinator")
            {
                await HandleRefuseArticle(article);

>>>>>>> Stashed changes
            }
            else
            {
                return View("~/404");
            }
            return View();
        }

        private void HandleCreateFeedBack(int article_id, string content)
        {
            Feedback feedback = new Feedback();
            feedback.article_id = article_id;
            feedback.us_id = (int)HttpContext.Session.GetInt32("us_id");
            feedback.date = DateTime.Now.ToString();
            feedback.content = content;
            _dbContext.Add(feedback);
            _dbContext.SaveChanges();
        }
<<<<<<< Updated upstream

        //UpdateFeedback
        public IActionResult UpdateFeedBack()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateFeedBack(int id, string newcontent)
        {
            Feedback feedback = await _repoFeedBack.SearhFeedBackById(id);
            if (!feedback.Equals(null))
            {
                HandleUpdateFeedBack(feedback, newcontent);
                return View();

            }
            else
            {
                return View();
            }
            return View();
        }

        private void HandleUpdateFeedBack(Feedback feedback, string newcontent)
        {
            feedback.content = newcontent;
            _dbContext.Update(feedback);
            _dbContext.SaveChanges();
        }

        //DeleteFeedback
        [HttpDelete]
        public IActionResult DeleteFeedback()
        {

            return View();
        }
=======
>>>>>>> Stashed changes
    }
}
