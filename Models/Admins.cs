using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eProject.Models
{
    public class Admins
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Admin_Id { get; set; }

        [Required(ErrorMessage = "Username is required!")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Username from 1 to 20 chacracters!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Password from 1 to 20 chacracters!")]
        public string Password { get; set; }
    }
}
