using Microsoft.EntityFrameworkCore;

namespace EnterpriceWeb.Models
{
    public class AppDbConText : DbContext
    {
        public AppDbConText(DbContextOptions options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Faculty> faculties { get; set; }
        public DbSet<Article> articles { get; set; }
        public DbSet<Article_file> article_Files { get; set; }
        //public DbSet<Feedback> feedbacks { get; set; }
        public DbSet<Magazine> magazines { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Faculty>().HasData
                (
                    new Faculty { f_id = 1, f_name = "faculty1", f_status = "1" },
                    new Faculty { f_id = 2, f_name = "faculty2", f_status = "1" },
                    new Faculty { f_id = 3, f_name = "faculty3", f_status = "1" }
                );
            modelBuilder.Entity<User>().HasData
                (
                    new User { us_id = 1, f_id = 1, us_name = "thaihuynh", us_password = "huynhthai", us_role = "admin" }
                );
            modelBuilder.Entity<Magazine>().HasData
               (
                   new Magazine
                   {
                       magazine_id = 1,
                       magazine_closure_date = "03/02/2024",
                       magazine_final_closure_date = "27/03/2024",
                       magazine_academic_year = "2019-2024",
                       magazine_title = "Tabloid",
                       magazine_status = "0",
                       magazine_deleted = "0"
                   }
               );

            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<User>().HasOne(u => u.Faculties)
            //   .WithMany()
            //   .HasForeignKey(u => u.f_id)
            //   .HasPrincipalKey(f => f.f_id);
            //base.OnModelCreating(modelBuilder);
        }
    }
}