using Microsoft.AspNetCore.Http;
using System.Net;
using System.IO;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace MVCProject.Helper
{
    public static class DocumentSetting
    {
        public static string UploadFile(IFormFile file, string FolderName)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);
            string fileName = $"{Guid.NewGuid()}{file.Name}";
            string filePath = Path.Combine(folderPath, fileName);
            var fs = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fs);
            return fileName;
        }
        public static void DeleteFile(string folderName, string FileName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName, FileName);   
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

    }
}
