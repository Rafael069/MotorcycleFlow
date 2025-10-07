using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Core.Events
{
    public class MotorcycleRegisteredEvent
    {
        public Guid MotorcycleId { get; set; }
        public int Year { get; set; }
        public string Model { get; set; } 
        public string LicensePlate { get; set; } 
        public DateTime RegisteredAt { get; set; }
    }
}
