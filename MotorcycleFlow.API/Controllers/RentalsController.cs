using MediatR;
using Microsoft.AspNetCore.Mvc;
using MotorcycleFlow.Application.Features.Rentals.Commands;
using MotorcycleFlow.Application.Features.Rentals.DTOs;

namespace MotorcycleFlow.API.Controllers
{
    [ApiController]
    [Route("api/rentals")]
    public class RentalsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentalsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRental([FromBody] CreateRentalDto createDto)
        {
            var command = new CreateRentalCommand(
                createDto.delivery_person_id,
                createDto.motorcycle_id,
                createDto.start_date,
                createDto.end_date,
                createDto.expected_end_date,
                createDto.plan
            );

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.Error.Message });

            
            return Ok(result.Value);
        }


        [HttpPut("{id}/return")]
        public async Task<IActionResult> ReturnRental(string id, [FromBody] ReturnRentalDto returnDto)
        {
            var command = new ReturnRentalCommand(id, returnDto.actual_return_date);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.Error.Message });

            return Ok(new { finalCost = result.Value });
        }
    }
}
