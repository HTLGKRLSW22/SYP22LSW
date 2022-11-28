using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LSWBackend.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly FileUploadService _service;

        public FileUploadController(IWebHostEnvironment enviroment, FileUploadService service)
        {
            this._hostingEnvironment = enviroment;
            this._service = service;
        }

        [HttpPost("File/Post")]
        [DisableRequestSizeLimit]
        public ActionResult UploadCSVFile()
        {
            return _service.uploadFile(Request.Form.Files[0], _hostingEnvironment, ".csv");
        }

    }
}
