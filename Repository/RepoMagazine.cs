using EnterpriceWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace EnterpriceWeb.Repository
{
    public class RepoMagazine
    {
        private readonly AppDbConText _appContext;
        public  RepoMagazine(AppDbConText appContext)
        {
            _appContext = appContext;
        }
        public async Task<List<Magazine>> SearchAllMagazine()
        {
            List<Magazine> ListMagazine = await _appContext.magazines.Where(m=>m.magazine_deleted.Equals("0")).ToListAsync();
            return ListMagazine;
        } 

        public async Task<Magazine> SearchMagazineById(int idMagazine)
        {
            Magazine Magazine = await _appContext.magazines.Where(m=>m.magazine_id.Equals(idMagazine)).FirstOrDefaultAsync();
            return Magazine;
        }

        public async Task<Magazine> SearchMagazineByTitle(string title)
        {
            Magazine Magazine = await _appContext.magazines.Where(m => m.magazine_title.Equals(title)).FirstOrDefaultAsync();
            return Magazine;
        }
    }
}
