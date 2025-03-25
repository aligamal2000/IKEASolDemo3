using IKEA.BILLDemo3.Services.DepartmentServices;
using IKEA.BILLDemo3.Services.EmployeeServices;
using IKEA.DALDemo3.Persistance.Data;
using IKEA.DALDemo3.Persistance.Repositories.Departments;
using IKEA.DALDemo3.Persistance.Repositories.Empoyees;
using IKEA.DALDemo3.Persistance.UnitOfWork;
using IKEA.PLDemo3.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace IKEA.PLDemo3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Configure Services
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection"));
            });
            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();
            builder.Services.AddScoped<IEmpolyeeServices, EmployeeServices>();
            //builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();
            builder.Services.AddAutoMapper(M => M.AddProfile(typeof(MappingProfile)));
            //builder.Services.AddScoped<ApplictaonDbContext>();
             builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            //builder.Services.AddScoped<DbContextOptions<ApplictaonDbContext>>((serivce)=>
            //{
            //    var optionBuilder = new DbContextOptionsBuilder<ApplictaonDbContext>();
            //    optionBuilder.UseSqlServer("Server =DESKTOP-68C09D0; Database = IKEA_G02; trusted_Connection = true; TrustServerCertificate=true");
            //    var options = optionBuilder.Options;
            //    return options;
            //});




            #endregion

            var app = builder.Build();


            #region Cofigure Pipelines (middlewares)

      
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
            #endregion

        }
    }
}
