﻿using Asp_Mvc.Data;
using Asp_Mvc.Helpers;
using Asp_Mvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Asp_Mvc.Controllers
{
    public class AuthenticationController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAddressManager _addressManager;


        public AuthenticationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IAddressManager addressManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _addressManager = addressManager;

        }


        #region SignUp
      
        [Route("signup")]
        [HttpGet]
        public IActionResult SignUp(string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            var form = new SignUpForm();

            if (returnUrl != null)
                form.ReturnUrl = returnUrl;
        
            return View(form);
        }

       
        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpForm form)
        {
            if (ModelState.IsValid)
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("admin"));
                    await _roleManager.CreateAsync(new IdentityRole("user"));
                }

                if (!_userManager.Users.Any())
                    form.RoleName = "admin";

                var user = new ApplicationUser()
                {
                    FirstName = form.FirstName,
                    LastName = form.LastName,
                    Email = form.Email,
                    UserName = form.Email
                 
          
                    
                };

                var response = await _userManager.CreateAsync(user, form.Password);
                if (response.Succeeded)
                {
                    var address = new ApplicationAddress()
                    {
                        AddressLine = form.AddressLine,
                        PostalCode = form.PostalCode,
                        City = form.City
                        
                        
                        
                    };
                   
                    await _addressManager.CreateUserAddressAsync(user, address);

                    await _userManager.AddToRoleAsync(user, form.RoleName);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    if (form.ReturnUrl == null || form.ReturnUrl == "/")
                        return RedirectToAction("Index", "Home");
                    else
                        return LocalRedirect(form.ReturnUrl);
                }

                foreach (var error in response.Errors)
                    ModelState.AddModelError(string.Empty, error.Description );
               
            }
           
            return View(form);
        }

        #endregion


        #region SignIn
        [Route("signin")]
        [HttpGet]
        public IActionResult SignIn(string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            var form = new SignInForm();

            if (returnUrl != null)
                form.ReturnUrl = returnUrl;

            ViewData["Error"] = "";
            return View(form);
        }

        [Route("signin")]
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInForm form)
        {
            if (ModelState.IsValid)
            {
                var response = await _signInManager.PasswordSignInAsync(form.Email, form.Password, isPersistent: false, false);
                if (response.Succeeded)
                    if (form.ReturnUrl == null || form.ReturnUrl == "/")
                        return RedirectToAction("Index", "Home");
                    else
                        return LocalRedirect(form.ReturnUrl);
            }

            ModelState.AddModelError(String.Empty, "Felaktig e-postadress eller lösenord");
            ViewData["Error"] = "Felaktig e-postadress eller lösenord";
            form.Password = "";

            return View(form);
        }

        #endregion


        #region SignOut
       
        public async Task<IActionResult> SignOut()
        {
            if (_signInManager.IsSignedIn(User))
                await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
           
        }

        #endregion
    }
}

