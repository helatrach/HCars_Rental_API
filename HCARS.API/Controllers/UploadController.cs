using Azure.Storage.Blobs;
using HCARS.Domain.EntitiesModels;
using HCARS.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HCARS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        public static IFileManager _fileManagger;
        private readonly ICarService _carService;
        private readonly BlobServiceClient _blob;

        public UploadController(IFileManager fileManagger, ICarService carService, BlobServiceClient blob)
        {
            _fileManagger = fileManagger;
            _carService = carService;
            _blob = blob;
        }

        [HttpPost("UploadImage")]
        public async Task<string> Post([FromForm] FileUpload objFile)
        {
        
            try
            {
                var result = await _fileManagger.Upload(objFile);

                if(result != "Failed")
                {
                    await _carService.UpdateCarImageAsync(objFile.CarId, objFile.CarId.ToString() + objFile.files.FileName);
                }
                return result;
             
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }

        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteFile([FromForm] string fileName)
        {
           await _fileManagger.Delete(fileName);
           return Ok();
        }
        [HttpGet]
        [Route("readImage")]
        public async Task<IActionResult> Read(string fileName)
        {
            var imgData = await _fileManagger.Read(fileName);
            //to view the image directly
            return File(imgData, "image/webp");

            //to download the image 
            //return new FileContentResult(imgData, "application/octet-stream")
            //{
            //    FileDownloadName = fileName,
            //};
        }

    }
}
