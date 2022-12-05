using System;
using System.IO;
using System.Net.Http.Headers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LSWBackend.Services;

public class FileUploadService
{

    public ActionResult UploadFile(IFormFile file, IWebHostEnvironment hostEnvironment, string fileExt) {
        try {
            if (Path.GetExtension(file.FileName) == fileExt) {
                string path = Path.Combine(hostEnvironment.WebRootPath, "InitData");

                if (!Directory.Exists(path)) {
                    Directory.CreateDirectory(path);
                }
                if (file.Length > 0) {
                    var stream = new FileStream(Path.Combine(path, ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName!.Trim('"')), FileMode.Create);
                    file.CopyTo(stream);
                }
                return new JsonResult("Upload Succesfull");
            }
            else {
                return new JsonResult("Upload Failed: Only .csv is accepted");
            }
        }
        catch (Exception e) {
            return new JsonResult("Upload Failed: " + e.Message);
        }

    }

}

