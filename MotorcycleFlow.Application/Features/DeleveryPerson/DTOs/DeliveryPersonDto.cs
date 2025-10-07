using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Application.Features.DeleveryPerson.DTOs
{
    public class DeliveryPersonDto
    {
        public string identifier { get; set; }
        public string name { get; set; }
        public string cnpj { get; set; }
        public DateTime birthDate { get; set; }
        public string driverLicenseNumber { get; set; }
        public string driverLicenseType { get; set; }
        public string driverLicenseImageUrl { get; set; }
    }
}
