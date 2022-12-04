using EduHome.Areas.Admin.Data;
using EduHome.Data;
using EduHome.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EduHome.DAL
{
    public class DataInitilaizer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _dbContext;
        private readonly AdminUser _adminUser;
        public DataInitilaizer(IServiceProvider serviceProvider)
        {
            _adminUser = serviceProvider.GetService<IOptions<AdminUser>>().Value;
            _userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>(); 
            _dbContext = serviceProvider.GetRequiredService<AppDbContext>();
            

        }

        public async Task SeedData()
        {
            await _dbContext.Database.MigrateAsync();

            var roles = new List<string> { Constants.AdminRole, Constants.UserRole };

            foreach (var role in roles)
            {
                if(await _roleManager.RoleExistsAsync(role))
                {
                    continue;
                }
                var result = await _roleManager.CreateAsync(new IdentityRole { Name = role });
                
                if(!result.Succeeded )
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine(error.Description);
                    }
                }

                var userExist = await _userManager.FindByNameAsync(_adminUser.UserName);

                if (userExist != null)
                    return;

                var userResult = await _userManager.CreateAsync(new User
                { 
                    UserName = _adminUser.UserName,
                    Email = _adminUser.Password
                },_adminUser.Password);
                if(userResult.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine(error.Description);
                    }
                }
                else
                {
                    var existUser = await _userManager.FindByNameAsync(_adminUser.UserName);

                    await _userManager.AddToRoleAsync(existUser, Constants.AdminRole);
                }
            }
        }
    }
}
