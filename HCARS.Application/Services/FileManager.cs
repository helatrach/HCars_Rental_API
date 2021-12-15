using Azure.Storage.Blobs;
using HCARS.Domain.EntitiesModels;
using HCARS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCARS.Services.Services
{
    public class FileManager : IFileManager
    {

        private readonly BlobServiceClient _blob;
        public FileManager(BlobServiceClient blob)
        {
            _blob = blob;
        }

        private BlobClient GetBlobClient(string containerName, string fileName)
        {
            var blobContainer = _blob.GetBlobContainerClient(containerName);
            return blobContainer.GetBlobClient(fileName);
        }

        public async Task<string> Upload(FileUpload model)
        {
            try
            {

                if (model.files.Length > 0)
                {
                    var blobClient = GetBlobClient("upload", model.CarId.ToString() + model.files.FileName);
                    await blobClient.UploadAsync(model.files.OpenReadStream());

                    return "\\Upload\\" + model.CarId.ToString() + model.files.FileName;

                }
                else
                {
                    return "Failed";
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public async Task<byte[]> Read(string fileName)
        {
            var blobClient = GetBlobClient("upload", fileName);
            var imgDownloadded = await blobClient.DownloadStreamingAsync();

            using (MemoryStream ms = new MemoryStream())
            {
                await imgDownloadded.Value.Content.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
        public async Task Delete(string fileName)
        {
            var blobClient = GetBlobClient("upload", fileName);

            await blobClient.DeleteAsync();
        }
    }
}
