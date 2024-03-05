using EnterpriceWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace EnterpriceWeb.Repository
{
    public class RepoFeedBack
    {
        private readonly AppDbConText _appDBContext;
        public RepoFeedBack(AppDbConText appDBContext)
        {
            this._appDBContext = appDBContext;
        }
        public async Task<List<Feedback>> SearhAllFeedBackOfArticle(int article_id)
        {
            List<Feedback> feedbacks = await _appDBContext.Feedback.Where(fb => fb.article_id.Equals(article_id)).ToListAsync();
            return feedbacks;
        }
        public async Task<Feedback> SearhFeedBackById(int id)
        {
            Feedback feedback = await _appDBContext.Feedback.Where(fb => fb.article_id.Equals(id)).FirstOrDefaultAsync();
            return feedback;
        }
    }
}