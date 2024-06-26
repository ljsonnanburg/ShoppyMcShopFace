using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ShoppyMcShopFace.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Text;
using System.IO;

using System;
using System.Collections.Generic;
using System.Text.Json;
using CsvHelper;
using CsvHelper.Configuration;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppyMcShopFace.Models; // Adjust this namespace according to your project structure
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;


namespace ShoppyMcShopFace.Controllers;

// [SessionCheck]
[Route("products")]
public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;

    private MyContext _context;

    public ProductController(ILogger<ProductController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("storefront")]
    public IActionResult Storefront()
    {
        return RedirectToAction("Storefront", "Home");
    }

    [HttpGet("manageproducts")]
    public IActionResult ManageProducts()
    {
        return View();
    }

    



    [HttpGet("{productId}")]
    public IActionResult ViewProduct(int productId)
    {
        Console.WriteLine("Product ID: " + productId);
        Product? oneProduct = _context.Products
                                    // .Include(p => p.TagsOfProduct)
                                    // .ThenInclude(t => t.TagInCorrelation)
                                    // .ThenInclude(uwr => uwr.RSVPingUser)
                                    .FirstOrDefault(p => p.ProductId == productId);
        
        if (oneProduct == null)
        {
            return RedirectToAction("Storefront");
        }
        return View(oneProduct);
    }

    [HttpGet("newproduct")]
    public ViewResult NewProduct()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}



