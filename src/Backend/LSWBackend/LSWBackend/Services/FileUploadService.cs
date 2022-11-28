using System;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LSWBackend.Services
{
    public class FileUploadService
    {

        public ActionResult uploadFile(IFormFile file, IWebHostEnvironment hostEnvironment, string fileExt)
        {
            try
            {
                if (Path.GetExtension(file.FileName) == fileExt)
                {
                    var folderName = "csv-Uploads";
                    string webRootPath = hostEnvironment.WebRootPath;
                    string fullPath = Path.Combine(webRootPath, folderName);

                    if (!Directory.Exists(fullPath))
                    {
                        Directory.CreateDirectory(fullPath);
                    }

                    if (file.Length > 0)
                    {
                        string fName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName!.Trim('"');
                        string wholePath = Path.Combine(fullPath, fName);
                        using (var stream = new FileStream(wholePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }
                    return new JsonResult("Upload Succesfull");
                }
                else
                {
                    return new JsonResult("Upload Failed: Only .csv is accepted");
                }
            }catch(Exception e)
            {
                return new JsonResult("Upload Failed: " + e.Message);
            }
            
        } 

    }
}

