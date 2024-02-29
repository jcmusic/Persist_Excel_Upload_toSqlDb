using Microsoft.AspNetCore.Http;

namespace Domain.Models
{
    public interface IFileUploadRequest
    {
        IFormFile File { get; set; }
    }

    public class FileUploadRequest : IFileUploadRequest
    {
        public IFormFile File { get; set; }
    }
}