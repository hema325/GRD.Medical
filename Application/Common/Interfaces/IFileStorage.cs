using Microsoft.AspNetCore.Http;

namespace Application.Common.Interfaces
{
    public interface IFileStorage
    {
        Task RemoveAsync(string filePath);
        Task<string> SaveAsync(IFormFile file);
    }
}
