using Autofac;
using Crud.Persistance.Features.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using CVBuilder.Web.Models;
using Microsoft.AspNetCore.Authentication;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CVBuilder.Application.features.Services;
using CVBuilder.Domain.Entities;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;

namespace CVBuilder.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailMessageService _emailMessageService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccountController(ILifetimeScope scope, ILogger<AccountController> logger,
        SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
           IEmailMessageService emailMessageService, IWebHostEnvironment webHostEnvironment)
        {
            _scope = scope;
            _logger = logger;
            _userManager = userManager;
            _emailMessageService = emailMessageService;
            _webHostEnvironment = webHostEnvironment;
            _signInManager = signInManager;
        }

        public async Task<bool> ValidateRecaptchaAsync()
        {
            var captchaResponse = Request.Form["g-recaptcha-response"];
            var request = "https://www.google.com/recaptcha/api/siteverify?secret=" +
                $"6Lcmzm0oAAAAAH0WA_wvWHhbvAweJTQ56j54YqN8&response={captchaResponse}";

            HttpClient httpClient = new();
            using HttpResponseMessage response = await httpClient.GetAsync(request);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            JObject convertJsonResponse = JObject.Parse(jsonResponse);
            var isSuccess = convertJsonResponse.Value<bool>("success");
            var result = (isSuccess) ? true : false;
            return result;
        }

        public IActionResult RegisterAsync(string? returnUrl = null)
        {
            var model = _scope.Resolve<RegisterModelVM>();
            model.ReturnUrl = returnUrl;
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(RegisterModelVM model)
        {
            model.ReturnUrl ??= Url.Content("~/");
            model.ResolveDependency(_scope);
            bool IsValidCaptcha=false;
            if (ModelState.IsValid && (IsValidCaptcha= await ValidateRecaptchaAsync()))
            {
                if(model.ProfilePicture != null)
                {
                    var folder = "Images/ProfilePhoto/";
                    folder += Guid.NewGuid().ToString() +"_"+ model.ProfilePicture.FileName;
                    model.ProfilePictureUrl ="/"+ folder;
                    var serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await model.ProfilePicture.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }
                var user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.Phone,
                    Address = model.Address,
                    DateOfBirth = model.DateOfBirth,
                    //Must be set Username, i set email as a UserName 
                    UserName = model.Email,
                    ProfilePictureUrl = model.ProfilePictureUrl
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {                    
                    //create email confirmation token
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    if (!string.IsNullOrEmpty(code))
                    {
                       await model.SendEmailConfitmationEmail(user, code);
                    }
                    
                    TempData["success"] = "Check Your Mail for Email Verification.";
                    return RedirectToAction("Login", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            if (IsValidCaptcha == false)
            {
                ModelState.AddModelError(string.Empty, "Captcha CLick");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
//ConfirmEmail---------------------------------------------------

        [HttpGet("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string uid, string code)
        {           
            var user = await _userManager.FindByIdAsync(uid);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{uid}'.");
            }
            code = code.Replace(' ', '+');          
            var result = await _userManager.ConfirmEmailAsync(user, code);
            //model.StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
            if (result.Succeeded)
            {
                TempData["success"] = "Thank you for confirming your email.";
                return RedirectToAction("Login", "Account");
            }
            return View("Error");
        }



        //Login---------------------------------------------------------------
        public async Task<IActionResult> LoginAsync(string returnUrl = null)
        {
            var model = _scope.Resolve<LoginVM>();
            returnUrl ??= Url.Content("~/");
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            model.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginVM model)
        {
            model.ReturnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                //check user mailconfirmed or not
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user == null)
                {
                    //ModelState.AddModelError(string.Empty, "You are not a registerd user!.Please Register First");
                    TempData["error"] = "You are not a registerd user!. \n Please Register First";  
                    return View(model);
                }
                var role = await _userManager.GetRolesAsync(user);

                if (user != null && !user.EmailConfirmed && (await _userManager.CheckPasswordAsync(user, model.Password)))
                {
                    ModelState.AddModelError(string.Empty, "Email Not Confirmed yet");
                    return View(model);
                }
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    if(role.Contains("Super Admin"))
                    {
                        return RedirectToAction("index", "Dashboard", new { area = "Admin" });
                    }
                    return RedirectToAction("profile", "user", new { area = "users" });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Worng Username or Password!.");
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

//Logout-----------------------------------------------------------------------
        public async Task<IActionResult> LogoutAsync(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.
                return RedirectToAction("Index", "Home");
            }
        }

        //ForgotPassword---------------------------------------------
        [AllowAnonymous, HttpGet("forgot-password")]
        public IActionResult ForgotPassword()
        {          
            return View();
        }

        [AllowAnonymous, HttpPost("forgot-password")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword( ForgotPasswordModel model)
        {
            model.ResolveDependency(_scope);
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);                
                // For more information on how to enable account confirmation and password reset please
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                await model.SendResetPasswordEmail(user, code);               
                ModelState.Clear();
               model.EmailSent = true;
                TempData["success"] = "Password Reset Link Sent Your Mail";
            }
            return View(model);
        }

        //ResetPassword--------------------------------
        [HttpGet("reset-password")]
        public IActionResult ResetPassword(string uid, string code)
        {
            ResetPasswordModel resetPasswordModel = new ResetPasswordModel {
                Code = code,
                UserId = uid
            };
            return View(resetPasswordModel);                
        }
        [HttpPost("reset-password")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                model.Code = model.Code.Replace(' ', '+');
                var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
                if (result.Succeeded)
                { 
                    ModelState.Clear();
                    model.IsSuccess = true;
                    TempData["success"] = "Password Reset Succesfull";
                    return View(model);
                }
               foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                
            }
                
            return View(model);
        }


    }
}
