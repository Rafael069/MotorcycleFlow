using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Application.Interfaces
{
    public interface IImageStorageService
    {
        Task<string> UploadImageAsync(string base64Image, string fileName);
        Task<bool> DeleteImageAsync(string imageUrl);
    }
}
