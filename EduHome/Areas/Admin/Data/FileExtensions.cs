using System.IO;

namespace EduHome.Areas.Admin.Data
{
    public static class FileExtensions
    {
        public static bool IsImage(this IFormFile file)
        {
            if (!file.ContentType.Contains("image"))
            {
                return false;
            }
            return true;
        }

        public static bool IsAllowedSize(this IFormFile file, int mb)
        {
            if (file.Length > 1024 * 1024 * mb)
            {
                return false;
            }
            return true;
        }

        public static async Task<string> GenerateFile(this IFormFile file, string rootPath)
        {
            var unicalName = $"{Guid.NewGuid()}-{file.FileName}";

            using FileStream fileStream = new(Path.Combine(rootPath, unicalName), FileMode.Create);
            await file.CopyToAsync(fileStream);
            fileStream.Close();

            return unicalName;
        }
    }
}
