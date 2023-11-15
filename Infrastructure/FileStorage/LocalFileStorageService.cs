using Microsoft.AspNetCore.Http;

namespace Infrastructure.FileStorage
{
    internal class LocalFileStorageService: IFileStorage
    {
        public async Task<string> SaveAsync(IFormFile file)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var fileType = file.ContentType.Substring(0, file.ContentType.IndexOf('/'));
            var filePath = Path.Combine("Files", fileType + "s", fileName);
            
            var directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath!);

            using var fileStream = File.Create(Path.Combine(Directory.GetCurrentDirectory(), filePath));
            await file.CopyToAsync(fileStream);

            return filePath;
        }

        public Task RemoveAsync(string filePath)
        {
            File.Delete(Path.Combine(Directory.GetCurrentDirectory(), filePath));
            return Task.CompletedTask;
        }
    }
}
