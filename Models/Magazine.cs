using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnterpriceWeb.Models
{
    public class Magazine
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int magazine_id { get; set; }

        [Required(ErrorMessage = "Magazine closure date not empty")]
        public DateTime magazine_closure_date { get; set; }

        [Required(ErrorMessage = "Magazine final closure date not empty")]
        public DateTime magazine_final_closure_date { get; set; }

        [Required(ErrorMessage = "Magazine title not empty")]
        public string magazine_title { get; set; }

        [Required(ErrorMessage = "Magazine academic year not empty")]
        public string magazine_academic_year { get; set; }



        [Required(ErrorMessage = "Magazine status not empty")]
        public string magazine_status { get; set; }

        [Required(ErrorMessage = "Magazine deleted not empty")]
        public string magazine_deleted { get; set; }

        public Magazine()
        {
            magazine_status = "0";
            magazine_deleted = "0";
        }


    }
}
