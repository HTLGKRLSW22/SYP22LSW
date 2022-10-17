using Backend.Dtos;
using LSWDbLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LSWBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private OfferService _service;

        public CourseController(OfferService service)
        {
            this._service = service;
        }

        [HttpDelete("Delete")]
        public ActionResult DeleteCourse(int courseId)
        {
            string errorMsg;
            if (courseId <= 0 || courseId == default(int)) return BadRequest("courseId cannot be smaller than zero or default!");
            if (_service.DeleteCourse(out errorMsg, courseId)) return BadRequest(errorMsg);
            return Ok();
        }

        [HttpPost("Post")]
        public ActionResult<OfferDto> CreateCourse(int teacherId, [FromBody] OfferDto offerDto)
        {
            string errorMsg;
            if (offerDto == null) return BadRequest("Object cannot be null!");
            if (teacherId <= 0 || teacherId == default(int)) return BadRequest("TeacherId cannot be smaller than zero or default!");
            if (!_service.CreateCourse(out errorMsg, teacherId/*, new Course().CopyPropertiesFrom(offerDto)*/)) return BadRequest(errorMsg);
            return Ok(offerDto);
        }

        [HttpPut("{id}")]
        public ActionResult<OfferDto> EditCourse(int id, [FromBody] OfferDto offerDto)
        {
            if (id < 0 || offerDto == null) return BadRequest("Id or request body incorrect");
            _service.EditCourse(/*id, new Course().CopyPropertiesFrom(offerDto)*/);
            return Ok(offerDto);
        }

        [HttpGet("GetCourse")]
        public ActionResult<OfferDto> GetCourse()
        {
            return Ok(_service.GetCourse());
        }

    }
}
