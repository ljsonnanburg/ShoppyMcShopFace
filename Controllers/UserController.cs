using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ShoppyMcShopFace.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace ShoppyMcShopFace.Controllers;

// applies session check on every action in controller
// this made sense in the lecture because it was a second controller for interacting with user posts
// [SessionCheck]
[Route("users")]
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;

    private MyContext _context;

    public UserController(ILogger<UserController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("LoginScreen")]
    public IActionResult LoginScreen()
    {
        return View();
    }

    [HttpPost("create")]
    public IActionResult RegisterUser(User newUser)
    {
        if (!ModelState.IsValid)
        {
            var message = string.Join(" | ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));
            Console.WriteLine(message);
        }
        if (!ModelState.IsValid)
        {
            return View("LoginScreen");
        }
        PasswordHasher<User> hasher = new();
        newUser.Password = hasher.HashPassword(newUser, newUser.Password);
        _context.Add(newUser);
        _context.SaveChanges();
        HttpContext.Session.SetInt32("UserId", newUser.UserId);
        HttpContext.Session.SetInt32("UserLevel", newUser.UserLevel);
        if (newUser.UserLevel == 10) 
        {
            return RedirectToAction("AdminTools");
        }
        else
        {
            // instantiating empty shopping cart
            Order ShoppingCart = new Order {UserId = newUser.UserId, OrderStatus = 0 };
            _context.Add(ShoppingCart);
            _context.SaveChanges();
            return RedirectToAction("StoreFront", "Product");
            
        }
    }

    [HttpPost("login")]
    public IActionResult LoginUser(LogUser logAttempt)
    {
        if (!ModelState.IsValid)
        {
            return View("Index");
        }
        User? dbUser = _context.Users.FirstOrDefault(u => u.Email == logAttempt.LogEmail);
        if (dbUser == null)
        {
            ModelState.AddModelError("LogEmail", "Invalid Credentials (e)");
            return View("Index");
        }
        PasswordHasher<LogUser> hasher = new();
        PasswordVerificationResult pwCompareResult = hasher.VerifyHashedPassword(logAttempt, dbUser.Password, logAttempt.LogPassword);

        if (pwCompareResult == 0)
        {
            ModelState.AddModelError("LogPassword", "Invalid Credentials (p)");
            return View("Index");
        }
        HttpContext.Session.SetInt32("UserId", dbUser.UserId);
        HttpContext.Session.SetInt32("UserLevel", dbUser.UserLevel);
        if (dbUser.UserLevel == 2) 
        {
            return RedirectToAction("AdminTools");
        }
        else
        {
            return RedirectToAction("StoreFront", "Product");
            
        }
    }

    [HttpPost("logout")]
    public RedirectToActionResult Logout()
    {
        // HttpContext.Session.Clear();
        HttpContext.Session.Remove("UserId");
        HttpContext.Session.Remove("UserLevel");
        return RedirectToAction("StoreFront", "Product");
    }

    [HttpGet("AdminTools")]
    public IActionResult AdminTools()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}



