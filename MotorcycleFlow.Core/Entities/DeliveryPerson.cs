using MotorcycleFlow.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Core.Entities
{
    public class DeliveryPerson : BaseEntity
    {

        public string Identifier { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public DateTime BirthDate { get; set; }
        public string DriverLicenseNumber { get; set; }
        public DriverLicenseTypeEnum DriverLicenseType { get; set; }
        public string DriverLicenseImageUrl { get; set; }
        public ICollection<Rental> Rentals { get; set; }

        public DeliveryPerson(string identifier, string name, string cNPJ, DateTime birthDate,
                        string driverLicenseNumber, DriverLicenseTypeEnum driverLicenseType,
                        string driverLicenseImageUrl)


        {
            Identifier = identifier;
            Name = name;
            CNPJ = cNPJ;
            BirthDate = birthDate;
            DriverLicenseNumber = driverLicenseNumber;
            DriverLicenseType = driverLicenseType;
            DriverLicenseImageUrl = driverLicenseImageUrl;
            Rentals = new List<Rental>(); 
        }

        // Construtor vazio para o EF Core
        protected DeliveryPerson() { }
    }
}
