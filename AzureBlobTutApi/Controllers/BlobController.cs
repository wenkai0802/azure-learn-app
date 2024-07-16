using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace AzureBlobTutApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlobController : ControllerBase
    {
        BlobServiceClient _blobService;
        public BlobController(BlobServiceClient blobService)
        {
            _blobService = blobService;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            

            //Create a unique name for the container
            string containerName = "quickstartblobs" + Guid.NewGuid().ToString();

            // Create the container and return a container client object
            //BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
            // See https://aka.ms/new-console-template for more information

            BlobContainerClient blobContainerClient = _blobService.GetBlobContainerClient("test");
            BlobClient blobClient = blobContainerClient.GetBlobClient("wenkai.jfif");
            //blobClient.
            var content = await blobClient.DownloadStreamingAsync();
            //content.Value
            /*await foreach (BlobItem blobItem in blobContainerClient.GetBlobsAsync())
            {

                
                BlobClient blobClient = blobContainerClient.GetBlobClient(blobItem.Name);
                //blobClient.
                var content = await blobClient.DownloadContentAsync();
            }*/
            return new FileStreamResult(content.Value.Content, new MediaTypeHeaderValue(content.Value.Details.ContentType));
        }
    }
}
