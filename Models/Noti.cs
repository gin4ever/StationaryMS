using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eProject.Models
{
    [Table("viewNotification")]
    public class Noti
    {
        [Key]
        public int Request_Id { get; set; } = 0;
        public int User_Id { get; set; } = 0;
        public int Approver { get; set; } = 0;
        public string Title { get; set; } = "";
        public string Reason { get; set; } = "";
        public bool IsRead { get; set; } = false;
        public DateTime DateRequest { get; set; }

        public string CreatedDateSt => this.DateRequest.ToString("dd-MMM-yyyy HH:mm:ss");
        public string IsReadSt => this.IsRead ? "YES" : "NO";

        public string FromUserName { get; set; } = "";
        public string ToUserName { get; set; } = "";
    }
}
