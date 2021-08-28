﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eProject.Models
{
    public class RequestDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Request_Id { get; set; }
        public string ItemCode { get; set; }

        [Required(ErrorMessage = "Quantity is required!")]
        [Range(1,1000, ErrorMessage = "Quantity must from 1 to 1000 items!")]
        public int Quantity { get; set; }

        [Required]
        [Range(1, 500, ErrorMessage = "Price from 1 to 500$!")]
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }
        [Required]
        [Range(1, 500, ErrorMessage = "Price from 1 to 500$!")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}
