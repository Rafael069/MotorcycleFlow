using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Application.Features.DeleveryPerson.DTOs
{
    public class CreateDeliveryPersonDto
    {
        public string identifier { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string cnpj { get; set; } = string.Empty;
        public DateTime birthDate { get; set; }
        public string driverLicenseNumber { get; set; } = string.Empty;
        public string driverLicenseType { get; set; } = string.Empty;
        public string driverLicenseImage { get; set; } = string.Empty; //64
    }
}
