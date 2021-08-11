using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eProject.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Department_Id { get; set; }

        [Required(ErrorMessage = "DepartmentName is required!")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "DepartmentName from 1 to 20 chacracters!")]
        public string DepartmentName { get; set; }

        [Required(ErrorMessage = "Budget is required!")]
        public decimal Budget { get; set; }
    }
}
