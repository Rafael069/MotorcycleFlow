using global::MotorcycleFlow.Application.Features.DeleveryPerson.Commands;
using global::MotorcycleFlow.Application.Features.DeleveryPerson.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MotorcycleFlow.Application.Common.Results;



namespace MotorcycleFlow.API.Controllers
{
    [ApiController]
    [Route("api/delivery-people")]
    public class DeliveryPersonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeliveryPersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeliveryPerson([FromBody] CreateDeliveryPersonDto createDto)
        {
            try
            {
                var command = new CreateDeliveryPersonCommand(
                    createDto.identifier,
                    createDto.name,
                    createDto.cnpj,
                    createDto.birthDate,
                    createDto.driverLicenseNumber,
                    createDto.driverLicenseType,
                    createDto.driverLicenseImage
                );

                var result = await _mediator.Send(command);

                if (!result.IsSuccess)
                    return BadRequest(new { message = result.Error.Message });

                return Ok(result.Value); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Erro interno: {ex.Message}" });
            }
        }

        [HttpPost("{id}/license-image")]
        public async Task<IActionResult> UploadLicenseImage(string id, [FromBody] UploadLicenseImageDto uploadDto)
        {
            var command = new UploadLicenseImageCommand(id, uploadDto.driverLicenseImage);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.Error.Message });

            return Ok(new { imageUrl = result.Value });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeliveryPersonById(string id)
        {
            
            return Ok(new { message = "Endpoint to be implemented" });
        }
    }
}

