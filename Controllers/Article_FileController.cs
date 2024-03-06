using EnterpriceWeb.Models;
using EnterpriceWeb.Repository;
using EnterpriceWeb.Support;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriceWeb.Controllers
{
    public class Article_FileController : Controller
    {
        private AppDbConText _dbContext;
        private RepoArticle_file _repoArticle_File;
        private RepoArticle _repoArticle;
        private ISession session;
        List<string> typeImage = new List<string>()
        {
            FileType.JPEG,
        };
        public Article_FileController(AppDbConText dbContext,IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _repoArticle_File = new RepoArticle_file(dbContext);
            _repoArticle= new RepoArticle(dbContext);
            session = httpContextAccessor.HttpContext.Session;
        }

        [HttpGet]
        public async Task<IActionResult> IndexArticle_File(int article_id)
        {
            int user_id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            Article article = await _repoArticle.SearhArticleById(article_id);
            if (user_id != null && (user_id==article.us_id || role=="coordinator"|| role == "marketingmanager"))
            {
                List<Article_file> list_Article_file = await _repoArticle_File.SearhAllArticleFileById(article_id);
                ViewBag.ArticleId = article_id;
                return View(list_Article_file);
            }
            return View("error");
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle_File(IFormFile article_file, int idArticle)
        {
            int user_id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            Article article = await _repoArticle.SearhArticleById(idArticle);
            if (user_id != null && (user_id == article.us_id || role == "coordinator"))
            {
                await HandleCreateArticle_File(article_file, idArticle);
                _dbContext.SaveChanges();
                return Ok(new { idArticle });
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task HandleCreateArticle_File(IFormFile ip_article_File, int id)
        {
            Article_file art_file = new Article_file();
            //check file type
            string filename = await SupportFile.Instance.SaveFileAsync(ip_article_File, "image/Article_File");
            art_file.article_file_name = filename;
            art_file.article_id = id;
            art_file.article_file_type = ip_article_File.ContentType;
            _dbContext.Add(art_file);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteArticle_File(int id)
        {
            int user_id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            Article article = await _repoArticle.SearhArticleById(id);
            if (user_id != null && (user_id == article.us_id || role == "coordinator"))
            {
                Article_file article_file = await _repoArticle_File.SearhArticle_FileById(id);
                SupportFile.Instance.DeleteFileAsync(article_file.article_file_name, "image/Article_File");
                _dbContext.Remove(article_file);
                _dbContext.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}
