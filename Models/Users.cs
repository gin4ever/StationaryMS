using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace eProject.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Employee ID")]
        public int User_Id { get; set; }

        [Required(ErrorMessage = "Username is required!")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "Username from 1 to 20 chacracters!")]
        [DisplayName("Employee Name")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Password from 1 to 20 chacracters!")]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string Password { get; set; }

        
        
        [Required(ErrorMessage ="Phone is required!")]
        public string Phone { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Fullname from 1 to 50 chacracters!")]
        [DisplayName("Employee Name")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Email from 1 to 50 chacracters!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Address from 1 to 50 chacracters!")]
        public string Address { get; set; }

        public int Role_Id { get; set; }
        public int Department_Id { get; set; }
        public string Images { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

    }
}
