using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using eProject.Models;
using eProject.Repository;
using Microsoft.Data.SqlClient;


namespace eProject.Services
{
    public class NotiServices : INotiServices
    {
        private StationeryContext context;
        public NotiServices(StationeryContext context)
        {
            this.context = context;
        }
        public List<Noti> GetNotifications(bool bIsGetOnlyUnread)
        {
            var notification = context.Noti.ToList();
            return notification;
        }
    }
}
