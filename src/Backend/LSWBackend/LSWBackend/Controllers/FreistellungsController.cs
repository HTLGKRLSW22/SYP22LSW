using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LSWBackend.Controllers;

[Route("[controller]")]
[ApiController]
public class FreistellungsController : ControllerBase
{

    private readonly FreistellungsService _freistellungsService;
    public FreistellungsController(FreistellungsService freistellungsService) => _freistellungsService = freistellungsService;

    [HttpPost("[action]")]
    public ActionResult<FreistellungsDto> SetFreistellung([FromBody] FreistellungsDto freistellungsDto) {
        bool functioned = _freistellungsService.SetFreistellung(freistellungsDto);
        return !functioned ? (ActionResult<FreistellungsDto>)BadRequest("Did not worked out") : (ActionResult<FreistellungsDto>)freistellungsDto;
    }
}
