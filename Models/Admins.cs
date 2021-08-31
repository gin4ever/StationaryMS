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
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Fullname from 1 to 50 chacracters!")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Email from 1 to 50 chacracters!")]
        public string Email { get; set; }
        public string Images { get; set; }
    }
}
