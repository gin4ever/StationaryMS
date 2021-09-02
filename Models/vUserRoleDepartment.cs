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
    [Table("vUserRoleDepartment")]
    public class vUserRoleDepartment
    {
        [Key]
        public int User_Id { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Images { get; set; }

        public int Role_Id { get; set; }
        public string RoleName { get; set; }

        public int Department_Id { get; set; }
        public string DepartmentName { get; set; }
        public decimal Budget { get; set; }
    }
}
