using System;
using System.IO;
using System.Net.Http.Headers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LSWBackend.Services;

public class FileUploadService
{
    private readonly InitializeService _initializeService;
    public FileUploadService(InitializeService initializeService) => _initializeService = initializeService;


    public ActionResult<List<string>> UploadFile(IFormFile file, string fileExt) {
        try {
            if (Path.GetExtension(file.FileName) == fileExt) {
                string path = "InitData";

                if (!Directory.Exists(path)) {
                    Directory.CreateDirectory(path);
                }
                if (file.Length > 0) {
                    string fileName = _initializeService.GetFileName();
                    var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create);
                    file.CopyTo(stream);
                    stream.Flush();
                    stream.Close();

                    return fileName == "teachers.csv" ? _initializeService.InitializeTeachers() : _initializeService.InitializeStudents();
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

