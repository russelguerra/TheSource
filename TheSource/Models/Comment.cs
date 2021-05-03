using System.ComponentModel.DataAnnotations;

namespace TheSource.Models
{
    public class Comments
    {
        public Comments()
        {
        }

        public int Id { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}