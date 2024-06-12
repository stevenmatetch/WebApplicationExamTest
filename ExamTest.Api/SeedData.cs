using ExamTest.Shared.Models;
using Microsoft.AspNetCore.Identity;

namespace WebApplicationExamTest
{
    public static class SeedDb
    {
        public static async void Seed(UserManager<Applicationuser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);
            SeedUser(userManager);
        }

        public static void SeedUser(UserManager<Applicationuser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@gmail.com").Result == null)
            {
                var user = new Applicationuser
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    /*FirstName = "admin",
                    LastName = "admin",
                    ProfilePicture = []
                    */
                };

                var result = userManager.CreateAsync(user, "P@ssword1").Result;
               
                if(result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }

        async static Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                var role1 = new IdentityRole
                {
                    Name = "Administrator"
                };

                await roleManager.CreateAsync(role1);
            }

            if (!roleManager.RoleExistsAsync("Student").Result)
            {
                var role2 = new IdentityRole
                {
                    Name = "Student"
                };

                 await roleManager.CreateAsync(role2);
            }

            if (!roleManager.RoleExistsAsync("Teacher").Result)
            {
                var role3 = new IdentityRole
                {
                    Name = "Teacher"
                };

                await roleManager.CreateAsync(role3);
            }
        }
    }
}


