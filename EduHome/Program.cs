using EduHome.Areas.Admin.Data;
using EduHome.DAL;
using EduHome.Data;
using EduHome.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EduHome
{
    public class Program
    {
       public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            Constants.RootPath = builder.Environment.WebRootPath;
            Constants.SliderPath = Path.Combine(Constants.RootPath, "assets", "img", "slider");
            Constants.TeacherPath = Path.Combine(Constants.RootPath, "assets", "img", "teacher");
            Constants.BlogPath = Path.Combine(Constants.RootPath, "assets", "img", "blog");
          
            Constants.CoursePath = Path.Combine(Constants.RootPath, "assets", "img", "course");
         
            Constants.EventPath = Path.Combine(Constants.RootPath, "assets", "img", "event");

            builder.Services
                .AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration
                .GetConnectionString("DefaultConnection"),
                builder =>
                {
                    builder.MigrationsAssembly(nameof(EduHome));
                }));

            builder.Services.AddIdentity<User, IdentityRole>(option =>
            {
                option.Lockout.MaxFailedAccessAttempts = 3;
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);

                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireLowercase = false;
                option.Password.RequiredLength = 1;

                option.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            builder.Services.Configure<AdminUser>(builder.Configuration.GetSection("AdminUser"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //using (var scope = app.Services.CreateScope())
            //{
            //    var serviceProdvider = scope.ServiceProvider;

            //    var dataIntializer = new DataInitilaizer(serviceProdvider);
            //    await dataIntializer.SeedData();
            //};

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                 name: "default",
                 pattern: "{controller=Home}/{action=Index}/{id?}"
               );
            });

             await app.RunAsync();
        }
    }
}