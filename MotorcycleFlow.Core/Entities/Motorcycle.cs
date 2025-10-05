using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Core.Entities
{
    public class Motorcycle : BaseEntity
    {
        public string Identifier { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public bool IsAvailable { get; set; }
        public ICollection<Rental> Rental { get; set; }

        public Motorcycle(string identifier, int year, string model, string licensePlate)
        {
            Identifier = identifier;
            Year = year;
            Model = model;
            LicensePlate = licensePlate;
            IsAvailable = true;
            Rental = new List<Rental>();
        }

        protected Motorcycle() { }
    }
}
