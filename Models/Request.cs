using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eProject.Models
{
    public class Request
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Request_Id { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "Reason from 1 to 250 chacracters!")]
        public string Reason { get; set; }

        public DateTime DateRequest {
            get; set;
        }

        [Required(ErrorMessage ="Status is required!")]
        
        [StringLength(20, MinimumLength = 1, 
            ErrorMessage = "Status from 1 to 20 chacracters!")]
        public string Status { get; set; }
        
        public int User_Id { get; set; }

        public DateTime? ApprovedDate {
            get; set;
        }
        public int Approver { get; set; }

    }
}
