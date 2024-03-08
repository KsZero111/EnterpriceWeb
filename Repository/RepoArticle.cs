using EnterpriceWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace EnterpriceWeb.Repository
{
    public class RepoArticle
    {
        private readonly AppDbConText _appDBContext;
        public RepoArticle(AppDbConText appDBContext)
        {
            this._appDBContext = appDBContext;
        }
        // id
        public async Task<Article> SearhArticleById(int id)
        {
            Article article = await _appDBContext.articles.Where(art => art.article_id.Equals(id)).FirstOrDefaultAsync();
            return article;
        }
        //Maketingmanager
        public async Task<List<Article>> SearhAllArticleMaketingManager()
        {
            List<Article> article = await _appDBContext.articles.Where(art => art.article_status.Equals("Accept")).ToListAsync();
            return article;
        }

        //Coordinator
        public async Task<List<Article>> SearhAllArticleCoordinator(int idMagazine, int userId)
        {
            List<Article> article = new List<Article>();
            //get f_id
            User userCoodinator = await _appDBContext.users.Where(us => us.us_id.Equals(userId)).FirstOrDefaultAsync();
            // get list user same f_id coordinator
            List<User> users = await _appDBContext.users.Where(us => us.f_id.Equals(userCoodinator.f_id)).ToListAsync();
            //get 
            //
            article = await _appDBContext.articles.Where(art => art.magazine_id.Equals(idMagazine)).Include(m => m.magazine).ToListAsync();
            return article;
        }

        // student
        public async Task<List<Article>> SearhAllArticleStudent(int idUser, int idMagazine)
        {
            List<Article> article = await _appDBContext.articles.Where(art => art.us_id.Equals(idUser) && art.magazine_id.Equals(idMagazine)).Include(m => m.magazine).ToListAsync();
            return article;
        }

    }
}
