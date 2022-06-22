using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Studying.Application.Utils.Uploads
{
    public static class UploadDocumentService
    {
        public static string UploadBase64Document(string base64Document, string connString, string container)
        {
            var fileName = Guid.NewGuid().ToString() + ".pdf";

            byte[] documentBytes = Convert.FromBase64String(base64Document);

            var blobClient = new BlobClient(connString, container, fileName);

            using (var stream = new MemoryStream(documentBytes))
            {
                blobClient.Upload(stream);
            }

            return blobClient.Uri.AbsoluteUri;
        }
    }
}
