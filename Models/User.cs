using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EnterpriceWeb.Models
{
    public class User
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int us_id { get; set; }

        [Required(ErrorMessage = "Faculty not empty")]
        public int f_id { get; set; }
        [ForeignKey("f_id")]
        public Faculty? faculty { get; set; }


        [Required(ErrorMessage = "User name not empty")]
        public string us_name { get; set; }


        [Required(ErrorMessage = "Password not empty")]
        public string us_password { get; set; }

        [Required(ErrorMessage = "Role not empty")]
        public string us_role { get; set; }

        public string? us_phone { get; set; }

        public string? us_gender { get; set; }

        public string? us_image { get; set; }

        public string? us_gmail { get; set; }
        public string? us_end_year { get; set; }

        public string? us_start_year { get; set; }

    }
}
