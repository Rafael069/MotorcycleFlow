using MediatR;
using MotorcycleFlow.Application.Common.Results;
using MotorcycleFlow.Application.Features.DeleveryPerson.DTOs;
using MotorcycleFlow.Application.Interfaces;
using MotorcycleFlow.Core.Entities;
using MotorcycleFlow.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Application.Features.DeleveryPerson.Commands
{
    public class CreateDeliveryPersonCommandHandler : IRequestHandler<CreateDeliveryPersonCommand, Result<DeliveryPersonDto>>
    {
        private readonly IDeliveryPersonRepository _deliveryPersonRepository;
        private readonly IImageStorageService _imageStorageService;

        public CreateDeliveryPersonCommandHandler(
            IDeliveryPersonRepository deliveryPersonRepository,
            IImageStorageService imageStorageService)
        {
            _deliveryPersonRepository = deliveryPersonRepository;
            _imageStorageService = imageStorageService;
        }

        public async Task<Result<DeliveryPersonDto>> Handle(CreateDeliveryPersonCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // 1. Validate unique CNPJ
                var existingByCnpj = await _deliveryPersonRepository.GetByCnpjAsync(request.CNPJ);
                if (existingByCnpj != null)
                {
                    return Result<DeliveryPersonDto>.Failure(Error.Conflict);
                }

                // 2. Validate single driver's license
                var existingByLicense = await _deliveryPersonRepository.GetByDriverLicenseNumberAsync(request.DriverLicenseNumber);
                if (existingByLicense != null)
                {
                    return Result<DeliveryPersonDto>.Failure(Error.Conflict);
                }

                // 3. Convert DriverLicenseType string to enum
                if (!Enum.TryParse<DriverLicenseTypeEnum>(request.DriverLicenseType, out var driverLicenseType))
                {
                    return Result<DeliveryPersonDto>.Failure(Error.Validation);
                }

                // 4. Upload CNH image (if provided)
                string imageUrl = null;
                if (!string.IsNullOrEmpty(request.DriverLicenseImageBase64))
                {
                    imageUrl = await _imageStorageService.UploadImageAsync(
                        request.DriverLicenseImageBase64,
                        $"{request.DriverLicenseNumber}-license");
                }

                // 5. Create DeliveryPerson
                var deliveryPerson = new DeliveryPerson(
                    identifier: request.Identifier,
                    name: request.Name,
                    cNPJ: request.CNPJ,
                    birthDate: request.BirthDate,
                    driverLicenseNumber: request.DriverLicenseNumber,
                    driverLicenseType: driverLicenseType,
                    driverLicenseImageUrl: imageUrl
                );

                await _deliveryPersonRepository.AddAsync(deliveryPerson);

                // 6. Return DTO
                var deliveryPersonDto = new DeliveryPersonDto
                {
                    identifier = deliveryPerson.Identifier,
                    name = deliveryPerson.Name,
                    cnpj = deliveryPerson.CNPJ,
                    birthDate = deliveryPerson.BirthDate,
                    driverLicenseNumber = deliveryPerson.DriverLicenseNumber,
                    driverLicenseType = deliveryPerson.DriverLicenseType.ToString(),
                    driverLicenseImageUrl = deliveryPerson.DriverLicenseImageUrl
                };

                return Result<DeliveryPersonDto>.Success(deliveryPersonDto);
            }
            catch (Exception ex)
            {
                return Result<DeliveryPersonDto>.Failure(Error.Failure("DeliveryPerson.Create", ex.Message));
            }
        }
    }
}
