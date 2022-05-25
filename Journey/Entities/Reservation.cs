using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Journey.Entities
{
    public class Reservation : Record
    {
        [DataType(DataType.Date)]
        public DateTime ArrivalDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }
        public bool IsArrived { get; set; }
        public int PlaceId { get; set; }
        public Place? Place { get; set; }
        public string? AccountId { get; set; }

        
        [NotMapped]
        public int MaxDurationDays { get; set; }

        [NotMapped]
        public IdentityUser Account { get; set; }


    }
}
