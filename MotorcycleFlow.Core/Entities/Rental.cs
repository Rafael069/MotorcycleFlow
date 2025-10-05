using MotorcycleFlow.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Core.Entities
{
    public class Rental : BaseEntity
    {

        public string DeliveryPersonId { get; set; }

        public string MotorcycleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public RentalPlanEnum Plan { get; set; }

        public decimal TotalCost { get; set; }
        public RentalStatusEnum Status { get; set; }
        public DeliveryPerson DeliveryPerson { get; set; }
        public Motorcycle Motorcycle { get; set; }

        // Builder to create new rentals
        public Rental(string deliveryPersonId, string motorcycleId, DateTime startDate,
                     DateTime expectedEndDate, RentalPlanEnum plan)

        {
            DeliveryPersonId = deliveryPersonId;
            MotorcycleId = motorcycleId;
            StartDate = startDate;
            ExpectedEndDate = expectedEndDate;
            Plan = plan;
            Status = RentalStatusEnum.Active; // Default
            TotalCost = 0; // It will be calculated later
        }


        protected Rental() 
        {
            // Inicializa as propriedades para evitar warnings
            DeliveryPersonId = string.Empty;
            MotorcycleId = string.Empty;
            DeliveryPerson = null!;
            Motorcycle = null!;
        }
    }


}
