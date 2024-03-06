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
        public async Task<List<Article>> SearhAllArticleAccept()
        {
            List<Article> article = await _appDBContext.articles.Where(art => art.article_status.Equals("Accept")).ToListAsync();
            return article;
        }
        public async Task<List<Article>> SearhAllArticle(int idUser, int idMagazine)
        {
            List<Article> article = await _appDBContext.articles.Where(art => art.us_id.Equals(idUser) && art.magazine_id.Equals(idMagazine)).Include(m=>m.magazine).ToListAsync();
            return article;
        }
        public async Task<Article> SearhArticleById(int id)
        {
            Article article = await _appDBContext.articles.Where(art => art.article_id.Equals(id)).FirstOrDefaultAsync();
            return article;
        }
    }
}
