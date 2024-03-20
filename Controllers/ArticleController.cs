using EnterpriceWeb.Mailutils;
using EnterpriceWeb.Models;
using EnterpriceWeb.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;

namespace EnterpriceWeb.Controllers
{
    public class ArticleController : Controller
    {
        private AppDbConText _dbContext;
        private RepoArticle _repoArticle;
        private RepoFeedBack _repoFeedBack;
        private RepoArticle_file _repoArticle_File;
        private RepoFaculty _repoFuculty;


        private ISession session;
        private RepoAccount _repoAccount;
        private RepoMagazine _repoMagazine;

        private SendMailSystem mailSystem;
        public ArticleController(AppDbConText dbContext, IHttpContextAccessor httpContextAccessor, IEmailSender emailSender, IWebHostEnvironment hostEnvironment)
        {
            _dbContext = dbContext;
            _repoArticle = new RepoArticle(dbContext);
            _repoMagazine = new RepoMagazine(dbContext);
            _repoArticle_File = new RepoArticle_file(dbContext);
            _repoFeedBack = new RepoFeedBack(dbContext);
            _repoAccount = new RepoAccount(dbContext);
            _repoFuculty = new RepoFaculty(dbContext);


            session = httpContextAccessor.HttpContext.Session;
            mailSystem = new SendMailSystem(emailSender, hostEnvironment);
        }

        [HttpGet]
        public async Task<IActionResult> IndexArticle(int id)
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

            //put data to view
            ViewBag.m_id = id;
            ViewBag.role = role;

            //get list article by role
            // Same  magazine, faculty
            if (role.Equals("coordinator"))
            {
                list_Article = await _repoArticle.SearhAllArticleCoordinator(id, user_id);
            }
            // Same magazine, faculty,user_id
            else if (role.Equals("student"))
            {
                list_Article = await _repoArticle.SearhAllArticleStudent(user_id, id);
            }
            // Same magazine, accept
            else if (role.Equals("marketingmanager"))
            {
                list_Article = await _repoArticle.SearhAllArticleMaketingManager();
            }
            else
            {
                return RedirectToAction("NotFound", "Home");
            }

            if (list_Article.Count() < 0) return RedirectToAction("NotFound", "Home");
            return View(list_Article);


        }

        private List<Article> HandleGetListArticle(int id, int user_id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<IActionResult> CreateArticle(int id)
        {
            int user_id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            Magazine Magazine = await _repoMagazine.SearchMagazineById(id);
            if (user_id != null && role == "student" && Magazine != null)
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
            int user_id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            if (user_id != null && role == "student")
            {
                await HandleCreateArticle(inputArticle.magazine_id, inputArticle.article_title, avatarArticle);
                User user = await _repoAccount.SearchCoordinatorByUserIdOfStudent(user_id);
                mailSystem.Sendgmail(user);
                return RedirectToAction("IndexArticle", "Article", new { id = inputArticle.magazine_id });
            }
            else
            {
                return View("error");
            }
        }

        private async Task HandleCreateArticle(int magazine_id, string article_title, IFormFile avatarArticle)
        {
            Article article = new Article();
            article.us_id = (int)session.GetInt32("User_id"); ;
            article.magazine_id = magazine_id;
            article.article_title = article_title;
            string filename = await SupportFile.Instance.SaveFileAsync(avatarArticle, "image/Article");
            article.article_avatar = filename;
            article.article_submit_date = DateTime.Now;
            _dbContext.Add(article);
            _dbContext.SaveChanges();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateArticle(int id)
        {
            int user_id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            Article article = await _repoArticle.SearhArticleById(id);
            if (user_id != null && role == "student" && article.us_id == user_id)
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
            if (user_id != null && role == "student" && oldArticle != null)
            {
                await HandleUpdateArticle(oldArticle, article, avatar);
            }
            else
            {
                return RedirectToAction("error");
            }
            return RedirectToAction("IndexArticle", "Article", new { id = oldArticle.magazine_id });
        }
        private async Task HandleUpdateArticle(Article oldArticle, Article newArticle, IFormFile avatar)
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

            if (article != null && user_id != null && role == "student")
            {
                await HandleDeleteArticleFile(id);
                await HandleDeleteArticle(article);

            }

            return RedirectToAction("IndexProfile");
        }

        private async Task HandleDeleteArticle(Article article)
        {
            SupportFile.Instance.DeleteFileAsync(article.article_avatar, "image/Article");
            _dbContext.Remove(article);
            await _dbContext.SaveChangesAsync();
        }

        private async Task HandleDeleteArticleFile(int id)
        {
            List<Article_file> list = await _repoArticle_File.SearhAllArticleFileById(id);

            foreach (Article_file file in list)
            {
                _dbContext.Remove(file);
                SupportFile.Instance.DeleteFileAsync(file.article_file_name, "image/Article_File");
            }

            await _dbContext.SaveChangesAsync();
        }

        [HttpPost]
        public async Task<IActionResult> AcceptArticle(int id)
        {
            int user_id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            Article article = await _repoArticle.SearhArticleById(id);
            if (article != null && user_id != null && role == "coordinator")
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
            _dbContext.Update(article);
            _dbContext.SaveChanges();
        }


        [HttpPost]
        public async Task<IActionResult> RefuseArticle(int id)
        {
            int user_id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            Article article = await _repoArticle.SearhArticleById(id);
            if (article != null && user_id != null && role == "coordinator")
            {
                await HandleRefuseArticle(article);
            }
            else
            {
                return BadRequest("Article not found");
            }
            return Ok();
        }

        private async Task HandleRefuseArticle(Article article)
        {
            article.article_status = "Refuse";
            article.article_accept_date = "";
            _dbContext.Update(article);
            _dbContext.SaveChanges();
        }



        public class DownLoadArticle
        {
            public int id { get; set; }
            public string title { get; set; }
        }


        public string DownLoadwithCheckbox(string arrArticle)
        {

            return arrArticle;
        }


        public string DownLoadAll(string arrArticleAll)
        {
            return arrArticleAll;
        }

        public async Task<IActionResult> Download(string arrArticle)
        {
           
            List<string> titles = new List<string>();
            List<DownLoadArticle> lst_selected_article = JsonConvert.DeserializeObject<List<DownLoadArticle>>(arrArticle);


            int user_id = (int)session.GetInt32("User_id");
            string role = session.GetString("role");
            if (user_id != null && role != "admin")
            {
                if (lst_selected_article.Count > 0)
                {
                    List<MemoryStream> memories = new List<MemoryStream>();

                    foreach (DownLoadArticle article in lst_selected_article)
                    {
                        List<Article_file> lis_files = await _repoArticle_File.SearhAllArticleFileById(article.id);

                        MemoryStream memory = mailSystem.DownloadSingleFile(lis_files);
                        memories.Add(memory);
                        titles.Add(article.title);
                    }
                    if (memories.Count > 1)
                    {
                        MemoryStream memori = await mailSystem.DownloadProcessAsync(memories, titles);
                        return File(memori.ToArray(), "application/zip", "selected_article.zip");

                    }
                    else if (memories.Count == 1)
                    {
                        return File(memories.First().ToArray(), "application/zip", "selected_article.zip");
                    }
                }
                    return RedirectToAction("Index", "Article");
                }
            else
            {

                return RedirectToAction("NotFound", "Home");
            }


        }
    }
}
