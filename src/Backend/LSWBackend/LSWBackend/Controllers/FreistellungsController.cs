using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LSWBackend.Controllers;

[Route("api/[controller]")]
[ApiController]

public class FreistellungsController : ControllerBase
{

    private readonly FreistellungsService _freistellungsService;
    public FreistellungsController(FreistellungsService freistellungsService) => _freistellungsService = freistellungsService;

    [HttpPost("[action]")]
    public bool SetFreistellung([FromBody] FreistellungsDto freistellungsDto) {
        try {
            return _freistellungsService.SetFreistellung(freistellungsDto);
        }
        catch {
            return false;
        }

    }
}
