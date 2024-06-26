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
using Microsoft.EntityFrameworkCore.ChangeTracking;

using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using ShoppyMcShopFace.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ShoppyMcShopFace.Services;

using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System.IO;
using System.Threading.Tasks;
using ShoppyMcShopFace.Services;

namespace ShoppyMcShopFace.Controllers;

[AdminCheck]
[Route("managingproducts")]
public class ManagingProductController : Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly AwsS3Service _awsS3Service;
    private readonly string _bucketName = "shoppyproductimages";
    private MyContext _context;

    public ManagingProductController(ILogger<ProductController> logger, MyContext context, AwsS3Service awsS3Service)
    {
        _logger = logger;
        _context = context;
        _awsS3Service = awsS3Service;
    }

    [HttpGet("newproduct")]
    public ViewResult NewProduct()
    {
        return View();
    }

    public IActionResult ManageProducts()
    {
        return View();
    }

    [HttpPost("newproduct")]
    public async Task<IActionResult> CreateProduct(Product newProduct, Dictionary<string, string[]> Tags, IFormFile ImageFile)
    {
        // Check if the model state is valid
        if (!ModelState.IsValid)
        {
            // If the model state is invalid, return the "NewProduct" view
            return View("NewProduct");
        }

        // Create a new dictionary to store processed tags
        var processedTags = new Dictionary<string, List<string>>();

        // this block came from chatGPT and if it works I should really remember to come back and understand it later
        if (ImageFile != null && ImageFile.Length > 0)
            {
                using (var stream = ImageFile.OpenReadStream())
                {
                    var key = await _awsS3Service.UploadFileAsync(stream, ImageFile.FileName, ImageFile.ContentType, _bucketName);
                    newProduct.ImageURL = $"https://{_bucketName}.s3.amazonaws.com/{key}";
                }
            }
        // Iterate over each tag in the Tags dictionary
        foreach (var tag in Tags)
        {
            // Trim any whitespace from the tag key
            var tagKey = tag.Key.Trim();

            // Split each tag value by commas and spaces, trim whitespace, and convert to a list
            var tagValues = tag.Value
                .SelectMany(v => v.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                .Select(v => v.Trim())
                .ToList();

            // Check if the processedTags dictionary already contains the tag key
            if (processedTags.ContainsKey(tagKey))
            {
                // If it does, update the tag values by adding new values and removing duplicates
                processedTags[tagKey] = processedTags[tagKey]
                    .Union(tagValues)
                    .Distinct()
                    .ToList();
            }
            else
            {
                // If it doesn't, add the tag key and its values to the processedTags dictionary
                processedTags[tagKey] = tagValues.Distinct().ToList();
            }
        }

        Console.WriteLine(processedTags);
        foreach (var key in processedTags.Keys)
        {
            Console.WriteLine($"Key: {key}");
            foreach (var value in processedTags[key])
            {
                Console.WriteLine($"Value: {value}");
            }
        }
        // Assign the processed tags to the newProduct's Tags property
        newProduct.Tags = processedTags;

        // Set the CreatorId of the new product from the current user's session
        newProduct.CreatorId = (int)HttpContext.Session.GetInt32("UserId");

        // Set the product status to 3 for new/not-yet-staged
        newProduct.ProductStatus = 3;

        // Converts tags dictionary to JSON
        newProduct.SerializeTags();

        Console.WriteLine(newProduct.TagsJSON);
        // Add the new product to the database context
        _context.Products.Add(newProduct);

        // Save the changes to the database
        _context.SaveChanges();

        // Return the "NewProduct" view
        return View("NewProduct");
    }

    [HttpGet("generatecsv")]
    public IActionResult GenerateCSV()
    {
        List<Product> AllProducts = _context.Products
                        .Where(p => p.ProductStatus == 3)
                        .ToList();
        return View(AllProducts);
    }

    // [AdminCheck]
    [HttpGet("{productId}/edit/")]
    public IActionResult EditProduct(int productId)
    {
        // Retrieve the product data from your database
        Product toEdit = _context.Products.SingleOrDefault(p => p.ProductId == productId);
        // Pass the product object to the partial view
        return View("EditProduct", toEdit);
    }

    [HttpPost("{productId}/update")]
public IActionResult UpdateProduct(int productId, [Bind("ProductId,ProductStatus,Name,ImageURL,Price,Stock,Description,Brand,Category,TagsJSON,CreatorId,CreatedAt")] Product editedProduct)
{
    var oldProduct = _context.Products.FirstOrDefault(d => d.ProductId == productId);
    if (oldProduct == null)
    {
        ModelState.AddModelError("Name", "Product not found in DB");
        return View("EditProduct", editedProduct);
    }

    if (!ModelState.IsValid)
{
    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
    foreach (var error in errors)
    {
        Console.WriteLine(error); // Or use a logging framework
    }
    return View("EditProduct", editedProduct);
}

    // Preserve fields that should not be overwritten
    editedProduct.CreatedAt = oldProduct.CreatedAt;
    editedProduct.UpdatedAt = DateTime.UtcNow;

    // Copy values from editedProduct to oldProduct
    _context.Entry(oldProduct).CurrentValues.SetValues(editedProduct);

    // If Tags need serialization, handle it here
    // oldProduct.SerializeTags();

    _context.SaveChanges();

    return RedirectToAction("GenerateCSV");
}


    [HttpPost("{productId}/delete")]
    public RedirectToActionResult DeleteProduct(int productId)
    {
        Product? DeleteMe = _context.Products.SingleOrDefault(d => d.ProductId == productId);
        if (DeleteMe != null)
        {
            _context.Remove(DeleteMe);
            _context.SaveChanges();
        }
        return RedirectToAction("GenerateCSV");
    }

    [HttpPost("ExportToCSV")]
    public IActionResult ExportToCSV()
    {
        var products = _context.Products
                        .Where(p => p.ProductStatus == 3)
                        .ToList();

        StringBuilder csvContent = new StringBuilder();
        csvContent.AppendLine("Name,ImageURL,Price,Stock,Brand,Category,Description,TagsJSON,CreatedAt,UpdatedAt");

        foreach (var product in products)
        {
            var formattedTagsJSON = EscapeCsvField(product.TagsJSON);
            csvContent.AppendLine($"{EscapeCsvField(product.Name)},{EscapeCsvField(product.ImageURL)},{EscapeCsvField(product.Price)},{EscapeCsvField(product.Stock)},{EscapeCsvField(product.Brand)},{EscapeCsvField(product.Category)},{EscapeCsvField(product.Description)},{formattedTagsJSON},{product.CreatedAt},{product.UpdatedAt}");
        }

        byte[] buffer = Encoding.UTF8.GetBytes(csvContent.ToString());
        var result = new FileContentResult(buffer, "text/csv")
        {
            FileDownloadName = "Products.csv"
        };

        return result;
    }

    private string EscapeCsvField(string field)
    {
        if (string.IsNullOrEmpty(field))
        {
            return string.Empty;
        }

        var escapedField = field.Replace("\"", "\"\"");
        return $"\"{escapedField}\"";
    }

    private string EscapeCsvField<T>(T field)
    {
        if (field == null)
            return string.Empty;

        string fieldStr = field.ToString();
        if (typeof(T) == typeof(float?))
        {
            fieldStr = ((float?)(object)field).HasValue ? ((float?)(object)field).Value.ToString(System.Globalization.CultureInfo.InvariantCulture) : string.Empty;
        }

        if (fieldStr.Contains("\"") || fieldStr.Contains(","))
        {
            fieldStr = fieldStr.Replace("\"", "\"\"");
            fieldStr = $"\"{fieldStr}\"";
        }

        return fieldStr;
    }

    [HttpGet("uploadcsv")]
    public IActionResult UploadCSV()
    {
        return View();
    }

    [HttpPost("uploadcsv")]
public async Task<IActionResult> UploadCSV(IFormFile file)
{
    if (file == null || file.Length == 0)
    {
        ModelState.AddModelError("File", "Please upload a CSV file.");
        return View();
    }

    var products = new List<Product>();

    using (var stream = new StreamReader(file.OpenReadStream()))
    {
        using (var csv = new CsvReader(stream, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound = null,
        }))
        {
            // Read the header first
            if (await csv.ReadAsync())
            {
                csv.ReadHeader();
                while (await csv.ReadAsync())
                {
                    try
                    {
                        JsonDocument jsonDocument = JsonDocument.Parse(csv.GetField<string>("TagsJSON"));
                        
                        string formattedJson = System.Text.Json.JsonSerializer.Serialize(jsonDocument.RootElement, new JsonSerializerOptions 
                        { 
                            WriteIndented = true 
                        });
                        Console.WriteLine(formattedJson);
                        var product = new Product
                        {
                            Name = csv.GetField<string>("Name"),
                            ImageURL = csv.GetField<string>("ImageURL"),
                            Price = csv.GetField<float>("Price"),
                            Stock = csv.GetField<int>("Stock"),
                            Brand = csv.GetField<string>("Brand"),
                            Category = csv.GetField<string>("Category"),
                            Description = csv.GetField<string>("Description"),
                            TagsJSON = formattedJson,
                            // Tags = deserializedJSONString,
                            CreatorId = (int)HttpContext.Session.GetInt32("UserId"),
                            ProductStatus = 1
                            // CreatedAt = DateTime.SpecifyKind(csv.GetField<DateTime>("CreatedAt"), DateTimeKind.Utc),
                            // UpdatedAt = DateTime.SpecifyKind(csv.GetField<DateTime>("UpdatedAt"), DateTimeKind.Utc)
                        };

                        products.Add(product);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("File", $"Error parsing CSV file: {ex.Message}");
                        return View();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("File", "The CSV file is empty.");
                return View();
            }
        }
    }

    if (!ModelState.IsValid)
    {
        return View();
    }

    try
    {
        _context.Products.AddRange(products);
        await _context.SaveChangesAsync();
        ViewBag.Message = "CSV upload succeeded and products were saved to the database.";
    }
    catch (Exception ex)
    {
        // Display the detailed exception information, including inner exceptions
        var errorMessage = $"Error saving to database: {ex.Message}";
        if (ex.InnerException != null)
        {
            errorMessage += $" Inner exception: {ex.InnerException.Message}";
        }
        ViewBag.Error = errorMessage;
        ModelState.AddModelError("Database", errorMessage);
        return View();
    }

    return RedirectToAction("Storefront", "Home");
}

}
