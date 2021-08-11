using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eProject.Models
{
    public class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string SupplierCode { get; set; }

        [Required(ErrorMessage = "SupplierName is required!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "SupplierName from 1 to 50 chacracters!")]
        public string SupplierName { get; set; }

        [Required(ErrorMessage = "ContactName is required!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "ContactName from 1 to 50 chacracters!")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "Phone is required!")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Phone from 1 to 20 chacracters!")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Status is required!")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Status from 1 to 20 chacracters!")]
        public string Status { get; set; }
    }
}
