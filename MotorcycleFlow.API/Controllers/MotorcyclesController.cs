using MediatR;
using Microsoft.AspNetCore.Mvc;
using MotorcycleFlow.Application.Features.Motorcycles.Commands;
using MotorcycleFlow.Application.Features.Motorcycles.DTOs;
using MotorcycleFlow.Application.Features.Motorcycles.Queries;

namespace MotorcycleFlow.API.Controllers
{
    [ApiController]
    [Route("api/motorcycles")]
    public class MotorcyclesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MotorcyclesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMotorcycle([FromBody] CreateMotorcycleDto createDto)
        {
            var command = new CreateMotorcycleCommand(
                createDto.Identifier,
                createDto.Year,
                createDto.Model,
                createDto.LicensePlate
            );

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.Error.Message });

            return CreatedAtAction(nameof(GetMotorcycleById), new { id = result.Value.Id }, result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetMotorcycles([FromQuery] string? licensePlate)
        {
            var query = new GetMotorcyclesQuery(licensePlate);
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.Error.Message });

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMotorcycleById(string id)
        {
            var query = new GetMotorcycleByIdQuery(id);
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return NotFound(new { message = result.Error.Message });

            return Ok(result.Value);
        }

        [HttpPut("{id}/license-plate")]
        public async Task<IActionResult> UpdateLicensePlate(string id, [FromBody] UpdateLicensePlateDto updateDto)
        {
            var command = new UpdateLicensePlateCommand(id, updateDto.LicensePlate);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.Error.Message });

            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMotorcycle(string id)
        {
            var command = new DeleteMotorcycleCommand(id);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.Error.Message });

            return NoContent();
        }
    }
}