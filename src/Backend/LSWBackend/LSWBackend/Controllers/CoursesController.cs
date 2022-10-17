using Backend.Dtos;
using ChinookPlaylists;
using LSWDbLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LSWBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private CoursesService _service;

        public CourseController(CoursesService service)
        {
            _service = service;
        }

        [HttpPut("{id}")]
        public ActionResult<OfferDto> EditCourse(int id, [FromBody] OfferDto offerDto)
        {
            if (id < 0 || offerDto == null) return BadRequest("Id or request body incorrect");
            new Offer().CopyPropertiesFrom(_service.EditCourse(id, offerDto));
            return Ok(offerDto);
        }

        [HttpGet("[action]")]
        public ActionResult<List<OfferDto>> GetTeacherCourses(int teacherId)
        {
            if (teacherId < 0) return BadRequest($"TeacherId {teacherId} not valid");
            return Ok(_service.GetTeacherCourses(teacherId)
                .Select(x => new OfferDto().CopyPropertiesFrom(x))
                .ToList());
        }



    }
}
