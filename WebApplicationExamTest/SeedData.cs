using Microsoft.AspNetCore.Identity;
using WebApplicationExamTest.Models;

namespace WebApplicationExamTest
{
    public static class SeedData
    {
        public static void Seed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUser(userManager);
        }

        public static void SeedUser(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@gmail.com").Result == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "admin",
                    Email= "admin@gmail.com",
                };
               var result = userManager.CreateAsync(user, "P@ssword1").Result;
                if(result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }

            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if(!roleManager.RoleExistsAsync("Administrator").Result)
            {
                var role1 = new IdentityRole
                {
                    Name = "Administrator"
                };

                var role2 = new IdentityRole
                {
                    Name = "Student"
                };

                var role3 = new IdentityRole
                {
                    Name = "Teacher"
                };
                var result1 =  roleManager.CreateAsync(role1).Result;
                var result2 = roleManager.CreateAsync(role2).Result;
                var result3 = roleManager.CreateAsync(role3).Result;

            }

        }
    }
}


