using MediatR;
using Microsoft.AspNetCore.Mvc;
using UFynd.Application.Feature.Hotel.Queries;

namespace UFynd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController: ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly ILogger<HotelController> _logger;
        public HotelController(ILogger<HotelController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Get(int hotelId, DateTime? arrivalDate)
        {
            var result = await _mediator.Send(new GetHotelRatesQuery() {HotelId = hotelId, ArrivalDate = arrivalDate});
            return Ok(result);
        }
    }
}
