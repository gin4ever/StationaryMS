using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eProject.Models
{

    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ItemCode { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "Description from 1 to 250 chacracters!")]
        public string Description { get; set; }

        [Required]
        [Range(1,500, ErrorMessage = "Price from 1 to 500$!")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Stock from 1 to 1000")]
        public int Stock { get; set; }

        [Required(ErrorMessage ="Unit is required!")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Unit from 1 to 20 chacracters!")]
        public string Unit { get; set; }
        public int Category_Id { get; set; }
        public string SupplierCode { get; set; }
        public int Role_Id { get; set; }
        public string Images  { get; set; }
    }
}
