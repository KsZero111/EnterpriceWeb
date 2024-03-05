using EnterpriceWeb.Models;
using EnterpriceWeb.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriceWeb.Controllers
{
    public class ArticleController : Controller
    {
        private AppDbConText _dbContext;
        private RepoArticle _repoArticle;
        private RepoArticle_file _repoArticle_File;

        private RepoMagazine _repoMagazine;
        public ArticleController(AppDbConText dbContext)
        {
            _dbContext = dbContext;
            _repoArticle = new RepoArticle(dbContext);
            _repoMagazine = new RepoMagazine(dbContext);
            _repoArticle_File = new RepoArticle_file(dbContext);
        }

        [HttpGet]
        public async Task<IActionResult> IndexArticle(int id)
        {
            //int id = (int)HttpContext.Session.GetInt32("User_id");
            //List<Article> list_Article = await _repoArticle.SearhAllArticle(User_id, id);

            List<Article> list_Article = await _repoArticle.SearhAllArticle(1, 1);
            ViewBag.m_id = id;
            return View(list_Article);
        }


        [HttpGet]
        public async Task<IActionResult> CreateArticle(int id)
        {
            //int id = (int)HttpContext.Session.GetInt32("User_id");
            Magazine Magazine = await _repoMagazine.SearchMagazineById(id);
            ViewBag.Magazine = Magazine;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateArticle([FromForm] Article inputArticle, IFormFile avatarArticle)
        {
            await HandleCreateArticle(inputArticle.magazine_id, inputArticle.article_title, avatarArticle);
            return RedirectToAction("IndexArticle");
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
            if (TempData["UserId"] != null) id = (int)TempData["UserId"];
            Article article = await _repoArticle.SearhArticleById(id);
            return View(article);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateArticle([FromForm] Article article, IFormFile avatar)
        {
            Article oldArticle = await _repoArticle.SearhArticleById(article.article_id);
            if (oldArticle != null)
            {
                await HandleUpdateProfile(oldArticle, article, avatar);
            }
            else
            {
                return RedirectToAction("UpdateProfile");
            }
            return RedirectToAction("IndexProfile");
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
            Article article = await _repoArticle.SearhArticleById(id);
            if (article != null)
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
            article.article_status = "999";
            _dbContext.Update(article);
            _dbContext.SaveChanges();
        }

    }
}
