using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using WebApplicationExamTest.Data;
using WebApplicationExamTest.Models;

namespace WebApplicationExamTest.Controllers
{
    public class RegisterController : Controller
    {
        
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        public RegisterController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public Teacher Input { get; set; }

        public string ReturnUrl { get; set; }

        public async Task<IActionResult> Index()
        {
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return View();
            //return View(await _context.Answer.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Id,FirstName,LastName,Email,Password,ConfirmPassword")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                MailAddress address = new MailAddress(Input.Email);
                string userName = address.User;
                ApplicationUser user = new ApplicationUser();

                user.UserName = userName;
                user.Email = teacher.Email;
                user.FirstName = teacher.FirstName;
                user.LastName = teacher.LastName;

                var result = await _userManager.CreateAsync(user, teacher.Password);
               
                if (result.Succeeded)
                {
                    //_logger.LogInformation("User created a new account with password.");
                    await _userManager.AddToRoleAsync(user, "Teacher");

                    //StudentClass studentclass = new StudentClass();
                    //studentclass.StudentId = user.Id;
                    //studentclass.ClassId = Convert.ToInt32(Input.Class);
                    //_context.StudentClass.Add(studentclass);
                    //await _context.SaveChangesAsync();

                  
                }
            }
            return View();
        }

   
    }
}
