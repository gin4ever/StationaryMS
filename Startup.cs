using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Services;
using eProject.Repository;
using Microsoft.EntityFrameworkCore;
namespace eProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string url = "server=DESKTOP-TEK2ARS\\SQLEXPRESS;database=stationerydb;uid=sa;pwd=123";
            services.AddDbContext<StationeryContext>(options => options.UseSqlServer(url));
            services.AddScoped<IUsersServices, UsersServices>();
            services.AddScoped<IItemServices, ItemServices>();
            services.AddScoped<IAdminServices, AdminServices>();
            services.AddScoped<ICategoryServices, CategoryServices>();
            services.AddScoped<IDepartmentServices, DepartmentServices>();
            services.AddScoped<IRequestServices, RequestServices>();
            services.AddScoped<IRoleServices, RoleServices>();
            services.AddScoped<ISupplierServices, SupplierServices>();
            services.AddScoped<IRequestItemServices, RequestItemServices>();
            services.AddScoped<IItemCategorySupplierServices, ItemCategorySupplierServices>();
            services.AddScoped<IUserRoleDepartment, UserRoleDepartment>();
            services.AddScoped<IRequestDetailServices, RequestDetailServices>();//add scope RequestDetailService

            services.AddSession();

            services.AddHttpContextAccessor();
            services.AddDistributedMemoryCache();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=User}/{action=Login}/{id?}");
            });
        }
    }
}
