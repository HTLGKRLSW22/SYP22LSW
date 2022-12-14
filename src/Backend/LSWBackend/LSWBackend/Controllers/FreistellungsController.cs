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
    public ActionResult<ExemptionDto> SetFreistellung([FromBody] ExemptionDto freistellungsDto) {
        bool functioned = _freistellungsService.SetFreistellung(freistellungsDto);
        return !functioned ? (ActionResult<ExemptionDto>)BadRequest("") : (ActionResult<ExemptionDto>)freistellungsDto;
    }
}
