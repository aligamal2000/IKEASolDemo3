using IKEA.BILLDemo3.Commen.Services.Attachments;
using IKEA.BILLDemo3.Services.DepartmentServices;
using IKEA.BILLDemo3.Services.EmployeeServices;
using IKEA.DALDemo3.Models.identity;
using IKEA.DALDemo3.Persistance.Data;
using IKEA.DALDemo3.Persistance.Repositories.Departments;
using IKEA.DALDemo3.Persistance.Repositories.Empoyees;
using IKEA.DALDemo3.Persistance.UnitOfWork;
using IKEA.PLDemo3.Mapping;
using Microsoft.AspNetCore.Identity;
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
            // Allow DI => USERManager SignInManager RoleManager
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true; ////$%
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 1;

                options.User.RequireUniqueEmail = true;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(5);
            }).AddEntityFrameworkStores<ApplicationDbContext>();


            builder.Services.AddAuthentication().AddCookie(options =>
            {
                options.LoginPath = "/Account/LogIn";
                options.AccessDeniedPath = "/Home/Error";
                options.ExpireTimeSpan = TimeSpan.FromDays(2);
                options.ForwardSignOut = "/Account/LogIn";
            });

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection"));
            });
            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();
            builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();
            //builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();
            builder.Services.AddAutoMapper(M => M.AddProfile(typeof(MappingProfile)));
            //builder.Services.AddScoped<ApplictaonDbContext>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAttachmentServices, AttachmentServices>();
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
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
            #endregion

        }
    }
}
