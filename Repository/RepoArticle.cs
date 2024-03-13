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
            List<Article> article = await _appDBContext.articles.Where(art => art.article_status.Equals("Accept")).Include(m => m.magazine).ToListAsync();
            return article;
        }

        //Coordinator
        public async Task<List<Article>> SearhAllArticleCoordinator(int idMagazine, int userId)
        {
            List<Article> listArticle = new List<Article>();
            //get f_id
            User userCoodinator = await _appDBContext.users.Where(us => us.us_id.Equals(userId)).FirstOrDefaultAsync();
            // get list user same f_id coordinator
            List<User> users = await _appDBContext.users.Where(us => us.f_id.Equals(userCoodinator.f_id) && !us.us_id.Equals(userCoodinator.us_id)).ToListAsync();
            //get list article and addrange
            foreach (User user in users)
            {
                //same magazine
                List<Article> temp = new List<Article>();
                temp = await _appDBContext.articles.Where(art => art.us_id.Equals(user.us_id) && art.magazine_id.Equals(idMagazine)).Include(m => m.magazine).ToListAsync();
                if (temp != null) listArticle.AddRange(temp);
            }
            return listArticle;
        }

        // student
        public async Task<List<Article>> SearhAllArticleStudent(int idUser, int idMagazine)
        {
            List<Article> article = await _appDBContext.articles.Where(art => art.us_id.Equals(idUser) && art.magazine_id.Equals(idMagazine)).Include(m => m.magazine).ToListAsync();
            return article;
        }

        //Dashboard
        public async Task<List<Article>> SearhAllArticleDashboard(int idUser)
        {
            List<Article> article = await _appDBContext.articles.Where(art => art.us_id.Equals(idUser)).Include(m => m.magazine).ToListAsync();
            return article;
        }
    }
}
