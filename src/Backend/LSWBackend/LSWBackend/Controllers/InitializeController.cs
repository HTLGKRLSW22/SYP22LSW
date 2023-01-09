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
public class InitializeController : ControllerBase
{
    private readonly FileUploadService _serv;
    private readonly InitializeService _initializeService;

    public InitializeController(FileUploadService serv, InitializeService initializeService) {
        _serv = serv;
        _initializeService = initializeService;
    }

    [HttpPost("File/Post")]
    [DisableRequestSizeLimit]
    public ActionResult<List<string>> UploadCSVFile() {
        try {
            return _serv.UploadFile(Request.Form.Files[0], ".csv");
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete("Reset")]
    public ActionResult<string> ResetDatabase() { 
        _initializeService.ResetDatabase();
        return Ok("Die Datenbank wurde zurückgesetzt");
    }

}
