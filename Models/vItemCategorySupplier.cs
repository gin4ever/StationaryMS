using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eProject.Models
{
    //[Keyless]
    [Table("vItemCategorySupplier")]
    public class vItemCategorySupplier
    {
        [Key]
        public string ItemCode { get; set; }
        public string Images { get; set; }

        public string Description { get; set; }

        public string SupplierName { get; set; }
    }
}
