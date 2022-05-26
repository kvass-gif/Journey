using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Journey.Entities
{
    public enum Status
    {
        Waiting,
        InPlace,
        Canceled,
        Completed,
    }
    public class Reservation : Record
    {
        [DataType(DataType.Date)]
        public DateTime ArrivalDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }
        public bool IsPaid { get; set; } = false;
        public Status Status { get; set; } =Status.Waiting;
        public int PlaceId { get; set; }
        public Place? Place { get; set; }
        public string? AccountId { get; set; }

        
        [NotMapped]
        public int MaxDurationDays { get; set; }

        [NotMapped]
        public IdentityUser? Account { get; set; }

        [NotMapped]
        public int? Sum { get; set; }
        [NotMapped]
        public Dictionary<int, string>? StatusDictionary { get; set; } 


    }
}
