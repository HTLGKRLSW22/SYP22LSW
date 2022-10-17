namespace Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private OfferService service;

        public OfferController(OfferService service)
        {
            this.service = service;
        }

        [HttpDelete("Offer")]
        public ActionResult DeleteOffer(int offerId)
        {
            string errorMsg;
            if (offerId <= 0 || offerId == default(int)) return BadRequest("courseId cannot be smaller than zero or default!");
            if (service.DeleteOffer(out errorMsg, offerId)) return BadRequest(errorMsg);
            return Ok(Ok());
        }

        [HttpPost("Post")]
        public ActionResult<OfferDto> CreateOffer(int teacherId, [FromBody] OfferDto offerDto)
        {
            string errorMsg;
            if (offerDto == null) return BadRequest("Object cannot be null!");
            if (teacherId <= 0 || teacherId == default(int)) return BadRequest("TeacherId cannot be smaller than zero or default!");
            if (!service.CreateOffer(out errorMsg, teacherId, new Offer().CopyPropertiesFrom(offerDto))) return BadRequest(errorMsg);
            return Ok(offerDto);
        }

        [HttpPut("{id}")]
        public ActionResult<OfferDto> EditOffer(int id, [FromBody] OfferDto offerDto)
        {
            if (id < 0 || offerDto == null) return BadRequest("Id or request body incorrect");
            service.EditOffer(/*id, new Course().CopyPropertiesFrom(offerDto)*/);
            return Ok(offerDto);
        }



    }
}
