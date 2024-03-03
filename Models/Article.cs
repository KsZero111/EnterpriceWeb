using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EnterpriceWeb.Models
{
    public class Article
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int article_id { get; set; }


        [Required]
        public int magazine_id { get; set; }
        [ForeignKey("magazine_id")]
        public Magazine? magazine { get; set; }


        [Required]
        public int us_id { get; set; }
        [ForeignKey("us_id")]
        public User? user { get; set; }



        [Required(ErrorMessage = "Article submit date not empty")]

        public string article_submit_date { get; set; }

        [Required(ErrorMessage = "Article accept date not empty")]

        public string article_accept_date { get; set; }

        public string? article_views { get; set; }


        [Required(ErrorMessage = "Article status not empty")]
        public string article_status { get; set; }
        public Article()
        {
            article_status = "0";
        }

    }
}
