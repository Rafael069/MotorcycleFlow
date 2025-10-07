using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Application.Features.Motorcycles.DTOs
{
    public class MotorcycleDto
    {
        public string Id { get; set; }
        public string Identifier { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public bool IsAvailable { get; set; }

    }
}
