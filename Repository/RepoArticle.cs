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
            List<Article> article = await _appDBContext.articles.Where(art => art.article_status.Equals("1")).ToListAsync();
            return article;
        }
        public async Task<List<Article>> SearhAllArticle()
        {
            List<Article> article = await _appDBContext.articles.ToListAsync();
            return article;
        }
        public async Task<Article> SearhArticleById(int id)
        {
            Article article = await _appDBContext.articles.Where(art => art.article_id.Equals(id) && art.article_status.Equals("1")).FirstOrDefaultAsync();
            return article;
        }
    }
}
