using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnterpriceWeb.Models
{
    public class Feedback
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fb_id { get; set; }
        [Required]
        public int us_id {  get; set; }
        [ForeignKey("us_id")]
        public User? User { get; set; }
        [Required(ErrorMessage = "content not empty")]
        public string content {  get; set; }
        public string date {  get; set; }
        public int article_id {  get; set; }
        [ForeignKey("article_id")]
        public Article? Article { get; set; }

    }
}
