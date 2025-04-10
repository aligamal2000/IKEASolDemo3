using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace IKEA.BILLDemo3.Commen.Services.Attachments
{
    public class AttachmentServices : IAttachmentServices
    {
        private readonly List<string> AllowedExtentions = new List<string>() {".jgp",".png",".jpeg" };
        private const int FileMaximumSize = 2_097_152;
        public string UploadImage(IFormFile File, string FolderName)
        {
            var fileExtention = Path.GetExtension(File.Name);
            if (!AllowedExtentions.Contains(fileExtention))
                throw new Exception("invalid file Extention");
            if (File.Length > FileMaximumSize)
                throw new Exception("Invalid file size, over our range !!");
            var Foderpath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","files",FolderName);
            if(!Directory.Exists(Foderpath))
                Directory.CreateDirectory(Foderpath);
            var FileName = $"{Guid.NewGuid()}_{File.FileName}";
            var FilePath = Path.Combine(Foderpath,FileName); 
            using var fs = new FileStream(FilePath,FileMode.Create);
            File.CopyTo(fs);
            return FileName;
        }

        public bool DeleteImage(string FilePath)
        {
            if(File.Exists(FilePath))
            {
                File.Delete(FilePath);
                return true;
            }
            return false;
        }

      
    }
}
