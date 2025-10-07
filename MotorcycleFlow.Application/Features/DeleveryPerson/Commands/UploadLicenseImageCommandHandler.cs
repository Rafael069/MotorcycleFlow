using MediatR;
using MotorcycleFlow.Application.Common.Results;
using MotorcycleFlow.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Application.Features.DeleveryPerson.Commands
{
    public class UploadLicenseImageCommandHandler : IRequestHandler<UploadLicenseImageCommand, Result<string>>
    {
        private readonly IDeliveryPersonRepository _deliveryPersonRepository;
        private readonly IImageStorageService _imageStorageService;

        public UploadLicenseImageCommandHandler(
            IDeliveryPersonRepository deliveryPersonRepository,
            IImageStorageService imageStorageService)
        {
            _deliveryPersonRepository = deliveryPersonRepository;
            _imageStorageService = imageStorageService;
        }

        public async Task<Result<string>> Handle(UploadLicenseImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // 1. Search for delivery person
                var deliveryPerson = await _deliveryPersonRepository.GetByIdAsync(request.DeliveryPersonId);
                if (deliveryPerson == null)
                {
                    return Result<string>.Failure(Error.NotFound);
                }

                // 2. Validar formato da imagem (PNG/BMP)
                if (!IsValidImageFormat(request.DriverLicenseImageBase64))
                {
                    return Result<string>.Failure(Error.Validation);
                }

                // 3. Upload da imagem
                var imageUrl = await _imageStorageService.UploadImageAsync(
                    request.DriverLicenseImageBase64,
                    $"{deliveryPerson.DriverLicenseNumber}-license"
                );

                // 4. Update image URL on delivery person
                deliveryPerson.DriverLicenseImageUrl = imageUrl;
                await _deliveryPersonRepository.UpdateAsync(deliveryPerson);

                return Result<string>.Success(imageUrl);
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(Error.Failure("DeliveryPerson.UploadImage", ex.Message));
            }
        }

        private bool IsValidImageFormat(string base64Image)
        {
            return true; // Placeholder
        }
    }
}
