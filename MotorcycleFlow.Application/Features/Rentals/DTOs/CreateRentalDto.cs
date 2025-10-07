using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Application.Features.Rentals.DTOs
{
    public class CreateRentalDto
    {
        public string delivery_person_id { get; set; }
        public string motorcycle_id { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public DateTime expected_end_date { get; set; }
        public int plan { get; set; } // 7, 15, 30, 45, 50
    }
}
