using Azure.Storage.Blobs;
using System;
using System.IO;
using System.Threading.Tasks;
using TinifyAPI;

namespace API.Studying.Application.Utils.Uploads
{
    public static class UploadImageService
    {
        public static async Task<string> UploadBase64ImageAsync(string base64Image, string connString, string container)
        {
            Tinify.Key = "JYSgltTvjDyYwymgHHpx5FkVpvZqzgKj";
            var fileName = Guid.NewGuid().ToString() + ".jpg";

            byte[] imageBytes = Convert.FromBase64String(base64Image);
            var resultData = await Tinify.FromBuffer(imageBytes).ToBuffer();

            var blobClient = new BlobClient(connString, container, fileName);

            using (var stream = new MemoryStream(resultData))
            {
                blobClient.Upload(stream);
            }

            return blobClient.Uri.AbsoluteUri;
        }
    }
}
