﻿using System;
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


namespace WebApplicationExamTest.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;


        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }
        //gehe


        [TempData]
        public string StatusMessage { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }
            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string Class { get; set; }

            public string Role { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            SelectListItem newItem;
            List<SelectListItem> classes = new List<SelectListItem>();
            List<Class> myClasses = new List<Class>();
            var DatabaseClasses = _context.Class;

            foreach (Class item in DatabaseClasses)
            {
                myClasses.Add(item);
            }


            foreach (var item in myClasses)
            {
                newItem = new SelectListItem();
                newItem.Text = item.Title;
                newItem.Value = item.Id.ToString();
                classes.Add(newItem);
            }

            ViewData["Classes"] = classes;

            if (ViewData["Classes"] == null)
            {
                newItem = new SelectListItem();
                newItem.Text = "";
                newItem.Value = "";
                classes.Add(newItem);
            }
            //
            List<string> myRoles = new List<string>();
            myRoles.Add("Student");
            myRoles.Add("Teacher");
            List<SelectListItem> Roles = new List<SelectListItem>();

            for (int i = 0; i < myRoles.Count; i++)
            {
                newItem = new SelectListItem();
                newItem.Text = myRoles[i];
                newItem.Value = myRoles[i];
                Roles.Add(newItem);

            }            

            ViewData["Roles"] = Roles;
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
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

                SelectListItem newItem;
                List<SelectListItem> classes = new List<SelectListItem>();
                List<Class> myClasses = new List<Class>();
                var DatabaseClasses = _context.Class;

                //var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    //_logger.LogInformation("User created a new account with password.");
                    await _userManager.AddToRoleAsync(user, Input.Role);

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    StudentClass studentclass = new StudentClass();
                    studentclass.StudentId = user.Id;
                    studentclass.ClassId = Convert.ToInt32(Input.Class);
                    _context.StudentClass.Add(studentclass);
                    await _context.SaveChangesAsync();
                    StatusMessage = "You created an account";

                    foreach (Class item in DatabaseClasses)
                    {
                        myClasses.Add(item);
                    }

                    foreach (var item in myClasses)
                    {
                        newItem = new SelectListItem();
                        newItem.Text = item.Title;
                        newItem.Value = item.Id.ToString();
                        classes.Add(newItem);
                    }

                    ViewData["Classes"] = classes;
                    return Redirect("/Identity/Account/Login");
                    //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    //{
                    //    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    //}
                    //else
                    //{
                    //    await _signInManager.SignInAsync(user, isPersistent: false);
                    //    return LocalRedirect(returnUrl);
                    //}
                }

                foreach (var error in result.Errors)
                {
                    StatusMessage = "Error: " + error.Description;
                    //ModelState.AddModelError(string.Empty, error.Description);
                }



                //SelectListItem newItem;
                //List<SelectListItem> classes = new List<SelectListItem>();
                //List<Class> myClasses = new List<Class>();
                //var DatabaseClasses = _context.Class;

                foreach (Class item in DatabaseClasses)
                {
                    myClasses.Add(item);
                }

                foreach (var item in myClasses)
                {
                    newItem = new SelectListItem();
                    newItem.Text = item.Title;
                    newItem.Value = item.Id.ToString();
                    classes.Add(newItem);
                }

                ViewData["Classes"] = classes;
                return Redirect("/Identity/Account/Login");



                //return Page();
            }
            //StatusMessage = "Error";

            // If we got this far, something failed, redisplay form
            return Redirect("/Identity/Account/Login");
        }
    }
}
