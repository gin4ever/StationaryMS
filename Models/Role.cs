using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eProject.Models
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Role_Id { get; set; }

        [Required(ErrorMessage = "RoleName is required!")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "RoleName from 1 to 20 chacracters!")]
        public string RoleName { get; set; }
    }
}
