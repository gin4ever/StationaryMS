using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProject.Models
{
    [Keyless]
    public class Report
    {
        
        public int User_Id { get; set; }
        public string Username { get; set; }
        public int Role_Id { get; set; }
        public int Department_Id { get; set; }
        public string DepartmentName { get; set; }
        public int Request_Id { get; set; }
        public string Reason { get; set; }
        public DateTime DateRequest
        {
            get; set;
        }
        public string Status { get; set; }
        public DateTime? ApprovedDate
        {
            get; set;
        }
        public int Approver { get; set; }


    }
}
