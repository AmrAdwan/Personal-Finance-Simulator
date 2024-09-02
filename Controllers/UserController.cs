using Microsoft.AspNetCore.Mvc;
using finance.Models;
using System.Threading.Tasks;
using finance.DAL.Interfaces;
using finance.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using finance.ViewModels;
using System.Security.Claims;

namespace finance
{
  public class UserController : Controller
  {
    private readonly IUserRepository _userRepository;
    private readonly SignInManager<User> _signInManager;


    // Inject the SignInManager alongside the IUserRepository
    public UserController(IUserRepository userRepository, SignInManager<User> signInManager)
    {
      _userRepository = userRepository;
      _signInManager = signInManager;
    }

    // GET: Display the registration form
    [HttpGet]
    public IActionResult Register()
    {
      return View(new Register()); // Initialize an empty model for the form
    }

    // POST: Process the registration form submission
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(Register model)
    {
      if (ModelState.IsValid)
      {
        var user = new User
        {
          Name = model.Name,
          Email = model.Email
        };

        // Use CreateUserAsync to hash the password and add the user to the database
        await _userRepository.CreateUserAsync(user, model.Password);

        return RedirectToAction("Index", "Home");
      }

      // If we get here, something failed, redisplay form
      return View(model);
    }


    [HttpGet]
    public IActionResult Login()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(Login model)
    {
      if (ModelState.IsValid)
      {
        var result = await _userRepository.LoginAsync(model.Email, model.Password);
        // var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
        if (result)
        {
          // Redirect to the Simulation page after successful login
          return RedirectToAction("Index", "/Simulation");
          // return LocalRedirect(returnUrl ?? "/Simulation"); 
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
      }

      return View(model); // Return to the login view if validation failed or login failed
    }




    // Diagnostic Action to Test Authentication
    [HttpGet]
    public IActionResult TestAuth()
    {
      if (User.Identity.IsAuthenticated)
      {
        // The user is signed in, return some confirmation view
        return Content($"User is authenticated. User ID: {User.FindFirstValue(ClaimTypes.NameIdentifier)}");
      }
      else
      {
        // The user is not signed in, redirect to login
        return RedirectToAction("Login");
      }
    }
  }
}
