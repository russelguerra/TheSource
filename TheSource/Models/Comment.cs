using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
