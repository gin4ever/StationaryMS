using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eProject.Models
{
     [Keyless]
    public class vRequestItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Total
        {
            get
            {
                return Quantity * Price;
            }
        }
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Unit { get; set; }
        public int Category_Id { get; set; }
        public string SupplierCode { get; set; }
        public int Role_Id { get; set; }
        public string Images { get; set; }
        public int Request_Id { get; set; }
        public string Reason { get; set; }
        public DateTime DateRequest
        {
            get; set;
        }
        public DateTime? ApprovedDate
        {
            get; set;
        }
        public string Status { get; set; }

        public int User_Id { get; set; }
        public int Approver { get; set; }
        public string Username { get; set; }
        public int Department_Id { get; set; }

    }
}
