using EnterpriceWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace EnterpriceWeb.Repository
{
    public class RepoAccount
    {
        private readonly AppDbConText appDBContext;
        public RepoAccount(AppDbConText _appDBContext)
        {
            appDBContext = _appDBContext;
        }
        public async Task<List<User>> SearhAllUser()
        {
            List<User> users = await appDBContext.users.ToListAsync();
            return users;
        }

        public async Task<string> SearchRoleUser(int userId)
        {
            User user = await appDBContext.users.Where(u => u.us_id == userId).FirstOrDefaultAsync();
            return user.us_role;

        }

        public async Task<User> SearhUserByName(string name)
        {
            User user = await appDBContext.users.FirstOrDefaultAsync();
            return user;
        }
        public async Task<User> SearhUserById(int id)
        {
            User user = await appDBContext.users.Where(us => us.us_id.Equals(id)).FirstOrDefaultAsync();
            return user;
        }
    }
}
