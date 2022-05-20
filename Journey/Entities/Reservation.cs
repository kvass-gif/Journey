using System.ComponentModel.DataAnnotations;

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
        public int AccountId { get; set; }
        public Account? Account { get; set; }
    }
}
