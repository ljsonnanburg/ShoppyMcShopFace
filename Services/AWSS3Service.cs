// this code came from chatGPT and if it works I should really remember to come back and understand it later
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System.IO;
using System.Threading.Tasks;

namespace ShoppyMcShopFace.Services
{
    public class AwsS3Service
    {
        private readonly IAmazonS3 _s3Client;

        public AwsS3Service(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType, string bucketName)
        {
            var key = $"images/{fileName}";

            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = fileStream,
                Key = key,
                BucketName = bucketName,
                ContentType = contentType
            };

            var transferUtility = new TransferUtility(_s3Client);
            await transferUtility.UploadAsync(uploadRequest);

            return key;
        }

        public async Task DeleteFileAsync(string key, string bucketName)
        {
            var deleteObjectRequest = new DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = key
            };

            await _s3Client.DeleteObjectAsync(deleteObjectRequest);
        }
    }
}
