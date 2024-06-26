using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ShoppyMcShopFace.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
// Name this anything you want with the word "Attribute" at the end
public class AdminCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Find the session, but remember it may be null so we need int?
        int? userId = context.HttpContext.Session.GetInt32("UserId");
        int? userLevel = context.HttpContext.Session.GetInt32("UserLevel");
        // Check to see if we got back null
        if(userId == null || userLevel < 10)
        {
            // Redirect to the Index page if there was nothing in session
            // "Home" here is referring to "HomeController", you can use any controller that is appropriate here
            context.Result = new RedirectToActionResult("Storefront", "Home", null);
        }
    }
}