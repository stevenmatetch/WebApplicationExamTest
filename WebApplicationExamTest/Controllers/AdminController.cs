using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;
using WebApplicationExamTest.Data;
using WebApplicationExamTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Mail;
using System.Text.Encodings.Web;
using System.Text;
using System;
using WebApplicationExamTest.Migrations;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApplicationExamTest.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [TempData]
        public string StatusMessage { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }

        public IActionResult Home()
        {
            return View();
        }
        public IActionResult Register()
        {
            SelectListItem newItem;
            List<SelectListItem> Roles = new List<SelectListItem>();
            List<string> myRoles = new List<string>();
            var DatabaseRoles = _context.Roles;

            foreach (var role in DatabaseRoles)
            {
                if (role.Name == "Teacher" || role.Name == "Administrator")
                {
                    myRoles.Add(role.Name);
                }
            }

            for (int i = 0; i < myRoles.Count; i++)
            {
                newItem = new SelectListItem();
                newItem.Text = myRoles[i];
                newItem.Value = myRoles[i];
                Roles.Add(newItem);

            }
            ViewData["Roles"] = Roles;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(InputModel Input)
        {

            if (ModelState.IsValid)
            {
                MailAddress address = new MailAddress(Input.Email);
                string userName = address.User;
               
                var user = new ApplicationUser
                {
                    UserName = userName,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName
                };

                //var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Input.Role);
                    await _context.SaveChangesAsync();
                    //StatusMessage = "You created an account";
                    return Redirect("/Admin/Home");
                }

                foreach (var error in result.Errors)
                {
                    StatusMessage = "Error: " + error.Description;
                }

            }
            return Redirect("/Admin/Home");
            // return Redirect("/Identity/Account/Login");
        }
    }
}
