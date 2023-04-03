using Azure.Storage.Blobs;
using MusicAPI.Models;

namespace MusicAPI.Helper
{
    public static class FileHelper
    {
        public static async Task<string> UploadFile(IFormFile file)
        {
            string connectString = @"DefaultEndpointsProtocol=https;AccountName=musicappstorageaccount;AccountKey=t8JIMdLgQm0UjbTGnJxaqZKvTnCTpL8nI4YD9qpq6NLw1goR+TFGA18c2eWqmxAxQ6ZElC1b1UkB+AStUMJDOA==;EndpointSuffix=core.windows.net";
            string containerName = "songscover";

            BlobContainerClient blobContainerClient = new BlobContainerClient(connectString, containerName);
            BlobClient blobClient = blobContainerClient.GetBlobClient(file.FileName);

            var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            await blobClient.UploadAsync(memoryStream);

            return blobClient.Uri.AbsoluteUri;
        }

        public static async Task<string> UploadAudio(IFormFile file)
        {
            string connectString = @"DefaultEndpointsProtocol=https;AccountName=musicappstorageaccount;AccountKey=t8JIMdLgQm0UjbTGnJxaqZKvTnCTpL8nI4YD9qpq6NLw1goR+TFGA18c2eWqmxAxQ6ZElC1b1UkB+AStUMJDOA==;EndpointSuffix=core.windows.net";
            string containerName = "audiocover";

            BlobContainerClient blobContainerClient = new BlobContainerClient(connectString, containerName);
            BlobClient blobClient = blobContainerClient.GetBlobClient(file.FileName);

            var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            await blobClient.UploadAsync(memoryStream);

            return blobClient.Uri.AbsoluteUri;
        }
    }
}
