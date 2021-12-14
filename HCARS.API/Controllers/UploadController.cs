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
        public static IWebHostEnvironment _environment;
        private readonly ICarService _carService;

        public UploadController(IWebHostEnvironment environment, ICarService carService)
        {
            _environment = environment;
            _carService = carService;
        }

        [HttpPost("UploadImage")]
        public async Task<string> Post([FromForm] FileUpload objFile)
        {
            var path = _environment.WebRootPath + "\\Upload\\";
            try
            {

                if (objFile.files.Length > 0)
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    using (FileStream fileStream = System.IO.File.Create(path + objFile.CarId.ToString() + objFile.files.FileName))
                    {
                        objFile.files.CopyTo(fileStream);
                        fileStream.Flush();
                        await _carService.UpdateCarImageAsync(objFile.CarId, objFile.CarId.ToString() + objFile.files.FileName);
                        return "\\Upload\\" + objFile.CarId.ToString() + objFile.files.FileName;
                    }
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

        [HttpPost("Delete")]
        public IActionResult DeleteFile([FromForm] string fileName)
        {
            var path = _environment.WebRootPath + "\\Upload\\";
            FileInfo file = new FileInfo(path + fileName);
            if (file.Exists)
            {
                file.Delete();
                return Ok("File deleted successfully");
            }
            else
            {
                return Ok("This file does not exists") ;
            }
        }

    }
}
