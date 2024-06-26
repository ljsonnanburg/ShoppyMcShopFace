// Not sure if any of this is needed if limiting to one image per product for MVP

// using Amazon.S3;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using ShoppyMcShopFace.Models;
// using ShoppyMcShopFace.Services;
// using System.IO;
// using System.Threading.Tasks;

// namespace ShoppyMcShopFace.Controllers
// {
//     public class ImageController : Controller
//     {
//         private readonly AwsS3Service _awsS3Service;
//         private readonly MyContext _context;
//         private readonly string _bucketName = "your-s3-bucket-name";

//         public ImageController(AwsS3Service awsS3Service, MyContext context)
//         {
//             _awsS3Service = awsS3Service;
//             _context = context;
//         }

//         [HttpPost]
//         public async Task<IActionResult> UploadImage(IFormFile file, int productId)
//         {
//             if (file == null || file.Length == 0)
//             {
//                 return BadRequest("File is empty.");
//             }

//             using (var stream = file.OpenReadStream())
//             {
//                 var key = await _awsS3Service.UploadFileAsync(stream, file.FileName, file.ContentType, _bucketName);

//                 // Save image metadata to database
//                 var productImage = new ProductImage
//                 {
//                     FileName = file.FileName,
//                     S3Key = key,
//                     ProductId = productId
//                 };

//                 _context.ProductImages.Add(productImage);
//                 await _context.SaveChangesAsync();
//             }

//             return Ok(new { FileName = file.FileName, S3Key = file.FileName });
//         }

//         [HttpDelete]
//         public async Task<IActionResult> DeleteImage(int id)
//         {
//             var productImage = await _context.ProductImages.FindAsync(id);
//             if (productImage == null)
//             {
//                 return NotFound();
//             }

//             await _awsS3Service.DeleteFileAsync(productImage.S3Key, _bucketName);

//             // Remove image metadata from database
//             _context.ProductImages.Remove(productImage);
//             await _context.SaveChangesAsync();

//             return Ok();
//         }
//     }
// }
