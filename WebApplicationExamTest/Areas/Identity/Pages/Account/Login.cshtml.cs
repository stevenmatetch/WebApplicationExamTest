using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WebApplicationExamTest.Models;
using System.Net.Mail;
using WebApplicationExamTest.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace WebApplicationExamTest.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<ApplicationUser> signInManager,  ILogger<LoginModel> logger,UserManager<ApplicationUser> userManager, ApplicationDbContext context, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public const string SessionId = "_Id";

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }


        [TempData]
        public string StatusMessage { get; set; }

        //[TempData]
        //public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            //if (!string.IsNullOrEmpty(ErrorMessage))
            //{
            //    ModelState.AddModelError(string.Empty, ErrorMessage);
            //}

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //returnUrl = returnUrl ?? Url.Content("~/");
            string id = "";

            if (ModelState.IsValid)
            {
               // List<ApplicationUser> list = new List<ApplicationUser>();
                ApplicationUser applicationUser = new ApplicationUser();
              
                string userEmail = Input.Email;

                if (IsValidEmail(Input.Email))
                {
                    var user = await _userManager.FindByEmailAsync(Input.Email);
                    if (user != null)
                    {                      
                        userEmail = user.UserName;
                        id = user.Id;
                        applicationUser.UserName = user.UserName;
                        applicationUser.Id = user.Id;
                      
                    }
                }

                var result = await _signInManager.PasswordSignInAsync(userEmail, Input.Password, Input.RememberMe, lockoutOnFailure: false);
               
                if (result.Succeeded)
                {
                    //var user = await _userManager.GetUserAsync(User);

                    HttpContext.Session.SetString(SessionId, id);


                    var isStudent = await _userManager.IsInRoleAsync(applicationUser, "Student");
                    var isAdmin = await _userManager.IsInRoleAsync(applicationUser, "Administrator");
                   
                    if (isStudent)
                    {                      
                        string SessionId = _contextAccessor.HttpContext.Session.Id;

                        return LocalRedirect("/Subject/Index");
                    }
                    else
                    {
                        
                        if (isAdmin)
                        {
                            return LocalRedirect("/Admin/Home");
                        }
                        else
                        {
                            return LocalRedirect("/Class/Index");
                        }
                      
                    }
                }

                StatusMessage= "Error";
                //_logger.LogInformation("User logged in.");           
            }

            return Page();
        }

        //public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        //{
        //    returnUrl = returnUrl ?? Url.Content("~/");

        //    if (ModelState.IsValid)
        //    {
        //        // This doesn't count login failures towards account lockout
        //        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        //        var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
        //        if (result.Succeeded)
        //        {
        //            _logger.LogInformation("User logged in.");
        //            return LocalRedirect(returnUrl);
        //        }
        //        if (result.RequiresTwoFactor)
        //        {
        //            return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
        //        }
        //        if (result.IsLockedOut)
        //        {
        //            _logger.LogWarning("User account locked out.");
        //            return RedirectToPage("./Lockout");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        //            return Page();
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return Page();
        //}
    }
}
