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
            string url = "server=sgn-ginnyn-lx\\ginny;database=stationerydb;uid=sa;pwd=Xuanha2912@";
            services.AddDbContext<StationeryContext>(options => options.UseSqlServer(url));
            services.AddScoped<IUsersServices, UsersServices>();
            services.AddScoped<IItemServices, ItemServices>();
            services.AddScoped<IAdminServices, AdminServices>();
            services.AddScoped<ICategoryServices, CategoryServices>();
            services.AddScoped<IDepartmentServices, DepartmentServices>();
            services.AddScoped<IRequestServices, RequestServices>();
            services.AddScoped<IRoleServices, RoleServices>();
            services.AddScoped<ISupplierServices, SupplierServices>();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Item}/{action=Index}/{id?}");
            });
        }
    }
}
