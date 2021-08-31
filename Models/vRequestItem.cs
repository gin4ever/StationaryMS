using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eProject.Models
{
    [Table("vRequestItem")]
    public class vRequestItem
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Total
        {
            get
            {
                return Quantity * Price;
            }
        }

        public int Request_Id { get; set; }
        public string Reason { get; set; }
        public DateTime DateRequest { get; set; }
        public DateTime ApprovedDate { get; set; }
        public string Status { get; set; }
        public int User_Id { get; set; }


        public string ItemCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Unit { get; set; }
        public int Category_Id { get; set; }
        public string SupplierCode { get; set; }
        public int Role_Id { get; set; }
        public string Images { get; set; }

    }
}
