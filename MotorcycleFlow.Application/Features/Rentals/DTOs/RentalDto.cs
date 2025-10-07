using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Application.Features.Rentals.DTOs
{
    public class RentalDto
    {
        public string identifier { get; set; }
        public decimal daily_rate { get; set; }
        public string delivery_person_id { get; set; }
        public string motorcycle_id { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public DateTime expected_end_date { get; set; }
        public DateTime actual_return_date { get; set; }
    }
}
