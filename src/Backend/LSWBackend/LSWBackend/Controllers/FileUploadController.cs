using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LSWBackend.Controllers;

[Produces("application/json")]
[Route("[controller]")]
[ApiController]
public class FileUploadController : ControllerBase
{
    private readonly IWebHostEnvironment _host;
    private readonly FileUploadService _serv;

    public FileUploadController(IWebHostEnvironment env, FileUploadService serv) {
        _host = env;
        _serv = serv;
    }

    [HttpPost("File/Post")]
    [DisableRequestSizeLimit]
    public ActionResult UploadCSVFile() {
        return _serv.UploadFile(Request.Form.Files[0], _host, ".csv");
    }

}
