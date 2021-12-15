using HCARS.Domain.EntitiesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCARS.Services.IServices
{
    public interface IFileManager
    {
        Task<string> Upload(FileUpload model);
        Task<byte[]> Read(string fileName);
        Task Delete(string fileName);
    }
}
