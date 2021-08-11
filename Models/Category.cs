using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eProject.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Category_Id { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "Description from 1 to 250 chacracters!")]
        public string Description { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Status from 1 to 20 chacracters!")]
        public string Status { get; set; }
    }
}
