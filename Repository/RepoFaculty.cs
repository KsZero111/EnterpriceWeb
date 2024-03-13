using EnterpriceWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace EnterpriceWeb.Repository
{   
    public class RepoFaculty
    {
        private readonly AppDbConText _appDBContext;
        public RepoFaculty(AppDbConText appDBContext)
        {
            this._appDBContext = appDBContext;
        }
        public async Task<List<Faculty>> SearhAllFaculty()
        {
            List<Faculty> faculties = await _appDBContext.faculties.Where(f=>f.f_status.Equals("1")).ToListAsync();
            return faculties;
        }
        public async Task<string[]> GetNameAllFaculty()
        {
            string[] faculties = await _appDBContext.faculties.Where(f => f.f_status.Equals("1")).Select(f=>f.f_name).ToArrayAsync();
            return faculties;
        }
        public async Task<int[]> GetIdAllFaculty()
        {
            int[] facultiesId = await _appDBContext.faculties.Where(f => f.f_status.Equals("1")).Select(f => f.f_id).ToArrayAsync();
            return facultiesId;
        }
        public async Task<Faculty> SearhFacultyByName(string name)
        {
            Faculty faculties = await _appDBContext.faculties.Where(f => f.f_name.Equals(name) && f.f_status.Equals("1")).FirstOrDefaultAsync();
            return faculties;
        }
        public async Task<Faculty> SearhFacultyById(int id)
        {
            Faculty faculties = await _appDBContext.faculties.Where(f => f.f_id.Equals(id) && f.f_status.Equals("1")).FirstOrDefaultAsync();
            return faculties;
        }
    }
}
