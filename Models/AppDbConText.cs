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
                    new User { us_id = 1, f_id = 1, us_name = "thaihuynh", us_password = "huynhthai", us_role = "admin" ,us_image= "07e16bf5-2afc-40c2-8b5d-a1246d96dc0ecart3.jpg",us_gmail="hpthkl@gmail.com" }
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
            modelBuilder.Entity<Article>().HasData
               (
                   new Article
                   {
                       article_id = 1,
                       magazine_id = 1,
                       us_id = 1,
                       article_title="Bủh",
                       article_avatar= "07e16bf5-2afc-40c2-8b5d-a1246d96dc0ecart3.jpg",
                       article_submit_date = "12/12/2024",
                       article_accept_date = "23/12/2024",
                       article_views = "111",
                       article_status = "0"
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