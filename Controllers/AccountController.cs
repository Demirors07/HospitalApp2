using System.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HospitalApp.ViewModels;

public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    // Constructor to inject SignInManager and UserManager dependencies
    public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    // Display the login page
    [HttpGet]
    public IActionResult Login(string? ReturnUrl)
    {
        return View();
    }

    // Handle the login form submission
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, string? ReturnUrl)
    {
        if (ModelState.IsValid) // Check if the model is valid
        {
            var user = await _userManager.FindByNameAsync(model.UserName); // Find the user by username

            if (user != null) // If user exists
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false); // Attempt to sign in

                if (result.Succeeded) // If login is successful
                {
                    ReturnUrl ??= Url.Content("~/"); // Set the return URL if not provided

                    return LocalRedirect(ReturnUrl); // Redirect to the return URL
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt."); // Add an error message if login fails
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt."); // Add an error message if user not found
            }
        }
        return View(model); // Return to the login view with the model
    }

    // Handle user logout
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync(); // Sign out the user
        return RedirectToAction("Login", "Account"); // Redirect to the login page
    }

    // Display the registration page
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    // Handle the registration form submission
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid) // Check if the model is valid
        {
            var user = new IdentityUser
            {
                UserName = model.UserName, // Set the username
                Email = model.Email // Set the email
            };

            var result = await _userManager.CreateAsync(user, model.Password); // Create the user

            if (result.Succeeded) // If user creation is successful
            {
                await _userManager.AddToRoleAsync(user, "User"); // Assign the "User" role to the new user
                return RedirectToAction("Index", "Home"); // Redirect to the home page
            }

            foreach (var error in result.Errors) // Loop through any errors
            {
                ModelState.AddModelError(string.Empty, error.Description); // Add the errors to the model state
            }
        }

        return View(model); // Return to the registration view with the model
    }
}
