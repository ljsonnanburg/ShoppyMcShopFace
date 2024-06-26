// Add this using statement
<<<<<<< HEAD
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.EntityFrameworkCore;
// You will need access to your models for your context file
using ShoppyMcShopFace.Models;
=======
>>>>>>> db81dc7 (S3 close to working)
using Amazon.S3;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
<<<<<<< HEAD
using Stripe;
using ShoppyMcShopFace.Models;
using ShoppyMcShopFace.Services;
using System.IO;
=======
using ShoppyMcShopFace.Models;
using System.IO;

>>>>>>> db81dc7 (S3 close to working)
// Builder code from before
var builder = WebApplication.CreateBuilder(args);

// Load AWS credentials from file
<<<<<<< HEAD
var awsCredentialsConfig = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("aws-credentials.json", optional: true, reloadOnChange: true)
    .Build()
    .GetSection("AWSCredentials");
=======
var awsCredentials = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("aws-credentials.json", optional: true, reloadOnChange: true)
    .Build()
    .GetSection("AWSCredentials")
    .Get<AWSCredentials>();
>>>>>>> db81dc7 (S3 close to working)

// Create a variable to hold your connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// All your builder.services go here
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

// Configure AWS options and services
builder.Services.AddAWSService<IAmazonS3>();

// Register your custom S3 service
builder.Services.AddTransient<AwsS3Service>();

builder.Services.AddDbContext<MyContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddSingleton(awsCredentials);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();



// // dotnet add package Stripe.net
// // Add this using statement
// using Microsoft.EntityFrameworkCore;
// // You will need access to your models for your context file
// using ShoppyMcShopFace.Models;
// using Amazon.S3;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using Stripe;
// // Builder code from before
// var builder = WebApplication.CreateBuilder(args);
// // Create a variable to hold your connection string
// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// // All your builder.services go here
// // Add services to the container.
// builder.Services.AddControllersWithViews();
// builder.Services.AddHttpContextAccessor();  
// builder.Services.AddSession();  
// // Configure AWS options and services
// builder.Services.AddAWSService<IAmazonS3>();
// // Register your custom S3 service
// builder.Services.AddTransient<AwsS3Service>();

// builder.Services.AddDbContext<MyContext>(options =>
// {
//     // options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
//     options.UseNpgsql(connectionString);
// });
// var app = builder.Build();



// // Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Home/Error");
// }
// app.UseHttpsRedirection();
// app.UseStaticFiles();
// app.UseSession();    
// app.UseRouting();

// // StripeConfiguration
// app.UseAuthorization();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

// app.Run();
