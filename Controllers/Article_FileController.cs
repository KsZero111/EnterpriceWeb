using EnterpriceWeb.Models;
using EnterpriceWeb.Repository;
using EnterpriceWeb.Support;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace EnterpriceWeb.Controllers
{
    public class Article_FileController : Controller
    {
        private AppDbConText _dbContext;
        private RepoArticle_file _repoArticle_File;
        private RepoArticle _repoArticle;
        private ISession session;
        private RepoFeedBack _repoFeedback;
        List<string> typeImage = new List<string>()
        {
            FileType.JPEG,
        };
        public Article_FileController(AppDbConText dbContext,IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _repoArticle_File = new RepoArticle_file(dbContext);
            _repoArticle= new RepoArticle(dbContext);
            _repoFeedback = new RepoFeedBack(dbContext);
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
                dynamic feedback_article_file = new ExpandoObject();
                List<Article_file> list_Article_file = await _repoArticle_File.SearhAllArticleFileById(article_id);
                ViewBag.ArticleId = article_id;
                ViewBag.role = role;
                List<Feedback> feedbacks=await _repoFeedback.SearhAllFeedBackOfArticle(article_id);
                feedback_article_file.Article_File = list_Article_file;
                feedback_article_file.Feedback = feedbacks;
                return View(feedback_article_file);
            }
            return RedirectToAction("NotFound", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle_File(IFormFile article_file, int idArticle)
        {
            int user_id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            Article article = await _repoArticle.SearhArticleById(idArticle);
            if (user_id != null && (user_id == article.us_id || role == "coordinator") && article_file!=null)
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
        public IActionResult CreateFeedBack()
        {
            return View();
        }
        //CreateFeedback
        [HttpPost]
        public IActionResult CreateFeedBack([FromForm] Feedback feedback, int id)
        {
            if (!feedback.content.Equals(null))
            {
                HandleCreateFeedBack(id, feedback.content);
               
            }
            else
            {
                return RedirectToAction("NotFound","Home");
            }
            return RedirectToAction("IndexArticle_File", "Article_File", new { article_id = id });
        }

        private void HandleCreateFeedBack(int article_id, string content)
        {
            Feedback feedback = new Feedback();
            feedback.article_id = article_id;
            feedback.us_id = (int)session.GetInt32("User_id");
            feedback.date = DateTime.Now.ToString();
            feedback.content = content;
            _dbContext.Add(feedback);
            _dbContext.SaveChanges();
        }
        //UpdateFeedback
        [HttpPut]
        public async Task<IActionResult> UpdateFeedBack(int id, string newcontent)
        {
            Feedback feedback = await _repoFeedback.SearhFeedBackById(id);
            if (!feedback.Equals(null))
            {
                HandleUpdateFeedBack(feedback, newcontent);
                return View();
            }
            return Ok();
        }

        private void HandleUpdateFeedBack(Feedback feedback, string newcontent)
        {
            feedback.content = newcontent;
            _dbContext.Update(feedback);
            _dbContext.SaveChanges();
        }

        //DeleteFeedback
        [HttpDelete]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            Feedback feedback = await _repoFeedback.SearhFeedBackById(id);
            if (feedback != null)
            {
                _dbContext.Remove(feedback);
                _dbContext.SaveChanges();
                return Ok();
            }
            return BadRequest();

        }
    }
}
