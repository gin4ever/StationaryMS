using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using eProject.Models;
namespace eProject.Repository
{
    public class StationeryContext : DbContext
    {
        public StationeryContext(DbContextOptions options) : base(options) { }
        public DbSet<Users> Users { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Request> Request { get; set; }
        public DbSet<RequestDetail> RequestDetail { get; set; }
        public DbSet<Admins> Admins { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<vRequestItem> vRequestItem { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<vUserRoleDepartment> vUserRoleDepartment { get; set; }
        public DbSet<vItemCategorySupplier> vItemCategorySupplier { get; set; }
        public DbSet<Noti> Noti { get; set; }
    }
}
