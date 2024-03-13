using Microsoft.EntityFrameworkCore;
using EnterpriceWeb.Models;

namespace EnterpriceWeb.Models
{
    public class AppDbConText : DbContext
    {
        public AppDbConText(DbContextOptions options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Faculty> faculties { get; set; }
        public DbSet<Article> articles { get; set; }
        public DbSet<Article_file> article_Files { get; set; }
        public DbSet<Feedback> feedbacks { get; set; }
        public DbSet<Magazine> magazines { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Faculty>().HasData
                (
                    new Faculty { f_id = 1, f_name = "faculty1", f_status = "1" },
                    new Faculty { f_id = 2, f_name = "faculty2", f_status = "1" },
                    new Faculty { f_id = 3, f_name = "faculty3", f_status = "1" },
                    new Faculty { f_id = 999, f_name = "All", f_status = "1" }
                );
            modelBuilder.Entity<User>().HasData
                (
                    new User
                    {
                        us_id = 1,
                        f_id = 999,
                        us_name = "admin",
                        us_password = "admin",
                        us_role = "admin",
                        us_gmail = "admin",
                        us_phone = "07788880524",
                        us_image = "fad8651c-32cc-4620-ba7d-c74efa72e006pngwing.com (2).jpg"
                    }
                    ,
                    new User
                    {
                        us_id = 2,
                        f_id = 999,
                        us_name = "marketingmanager",
                        us_password = "123",
                        us_role = "marketingmanager",
                        us_gmail = "marketingmanager",
                        us_phone = "07788880524",
                        us_image = "fad8651c-32cc-4620-ba7d-c74efa72e006pngwing.com (2).jpg"
                    },
                    //Coordinator
                    new User
                    {
                        us_id = 3,
                        f_id = 1,
                        us_name = "coordinator1",
                        us_password = "123",
                        us_role = "coordinator",
                        us_gmail = "hpthkl@gmail.com",
                        us_phone = "07788880524",
                        us_image = "fad8651c-32cc-4620-ba7d-c74efa72e006pngwing.com (2).jpg"
                    },
                    new User
                    {
                        us_id = 4,
                        f_id = 2,
                        us_name = "coordinator2",
                        us_password = "123",
                        us_role = "coordinator",
                        us_gmail = "hpthkl1@gmail.com",
                        us_phone = "07788880524",
                        us_image = "fad8651c-32cc-4620-ba7d-c74efa72e006pngwing.com (2).jpg"
                    },
                     new User
                     {
                         us_id = 5,
                         f_id = 2,
                         us_name = "coordinator3",
                         us_password = "123",
                         us_role = "coordinator",
                         us_gmail = "hpthkl3@gmail.com",
                         us_phone = "07788880524",
                         us_image = "fad8651c-32cc-4620-ba7d-c74efa72e006pngwing.com (2).jpg"
                     },

                    //Student
                    new User
                    {
                        us_id = 6,
                        f_id = 1,
                        us_name = "student1",
                        us_password = "123",
                        us_role = "student",
                        us_gmail = "student1",
                        us_phone = "07788880524",
                        us_image = "fad8651c-32cc-4620-ba7d-c74efa72e006pngwing.com (2).jpg"
                    },
                     new User
                     {
                         us_id = 7,
                         f_id = 2,
                         us_name = "student2",
                         us_password = "123",
                         us_role = "student",
                         us_gmail = "student2",
                         us_phone = "07788880524",
                         us_image = "fad8651c-32cc-4620-ba7d-c74efa72e006pngwing.com (2).jpg"
                     },
                     new User
                     {
                         us_id = 8,
                         f_id = 3,
                         us_name = "student3",
                         us_password = "123",
                         us_role = "student",
                         us_gmail = "student3",
                         us_phone = "07788880524",
                         us_image = "fad8651c-32cc-4620-ba7d-c74efa72e006pngwing.com (2).jpg"
                     }

                );
            modelBuilder.Entity<Magazine>().HasData
               (
                   new Magazine
                   {
                       magazine_id = 1,
                       magazine_closure_date = new DateTime(2024, 4, 3),
                       magazine_final_closure_date = new DateTime(2024, 5, 3),
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


        public DbSet<EnterpriceWeb.Models.Feedback>? Feedback { get; set; }
    }
}