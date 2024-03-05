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
        List<string> typeImage = new List<string>()
        {
            FileType.JPEG,


        };
        public Article_FileController(AppDbConText dbContext)
        {
            _dbContext = dbContext;
            _repoArticle_File = new RepoArticle_file(dbContext);
        }

        [HttpGet]
        public async Task<IActionResult> IndexArticle_File(int article_id)
        {
           
            List<Article_file> list_Article_file = await _repoArticle_File.SearhAllArticleFileById(article_id);
            ViewBag.ArticleId = article_id;
            return View(list_Article_file);
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle_File(IFormFile article_file, int idArticle)
        {

            await HandleCreateArticle_File(article_file, idArticle);
            _dbContext.SaveChanges();

            return RedirectToAction("IndexArticle_File","Article_FileController","ariticle_id="+idArticle);
        }

        private async Task HandleCreateArticle_File(IFormFile ip_article_File, int id)
        {
            Article_file art_file = new Article_file();

            if (typeImage.Contains("asd"))
            {

            }

            string filename = await SupportFile.Instance.SaveFileAsync(ip_article_File, "image/Article_File");
            art_file.article_file_name = filename;
            art_file.article_id = id;
            art_file.article_file_type = ip_article_File.ContentType;
            _dbContext.Add(art_file);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteArticle_File(int id)
        {
            Article_file article_file = await _repoArticle_File.SearhArticle_FileById(id);
            SupportFile.Instance.DeleteFileAsync(article_file.article_file_name, "image/Article_File");
            _dbContext.Remove(article_file);
            _dbContext.SaveChanges();
            return RedirectToAction("IndexArticle_File");
        }
    }
}
