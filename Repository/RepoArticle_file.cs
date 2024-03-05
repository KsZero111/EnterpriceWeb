using EnterpriceWeb.Controllers;
using EnterpriceWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace EnterpriceWeb.Repository
{
    public class RepoArticle_file
    {
        private readonly AppDbConText _appDBContext;
        public RepoArticle_file(AppDbConText appDBContext)
        {
            this._appDBContext = appDBContext;
        }
        public async Task<List<Article_file>> SearhAllArticleFileById(int idArticle)
        {
            List<Article_file> article_file = await _appDBContext.article_Files.Where(art_file => art_file.article_id.Equals(idArticle)).ToListAsync();
            return article_file;
        }
        public async Task<Article_file> SearhArticle_FileById(int id)
        {
            Article_file article_file = await _appDBContext.article_Files.Where(art_file => art_file.article_id.Equals(id)).FirstOrDefaultAsync();
            return article_file;
        }
    }
}
