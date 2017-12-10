using System.ComponentModel.DataAnnotations;

namespace DNT2017.Models
{
    public class Message
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [StringLength(50)]
        public string Topic { get; set; }

        [Required]
        [StringLength(2000)]
        public string Text { get; set; }
    }
}