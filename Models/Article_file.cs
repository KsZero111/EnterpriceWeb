using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnterpriceWeb.Models
{
    public class Article_file
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int article_file_id { get; set; }

        public int article_id { get; set; }
        [ForeignKey("article_id")]

        [Required(ErrorMessage = "Article file name not empty")]
        public string article_file_name { get; set; }

        [Required(ErrorMessage = "Article file type not empty")]
        public string article_file_type { get; set; }
    }
}
