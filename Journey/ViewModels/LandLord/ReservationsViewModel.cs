using Journey.Entities;

namespace Journey.ViewModels.LandLord
{
    public class ReservationsViewModel
    {
        public int PlaceId { get; set; }
        public string? SearchString { get; set; }
        public string? CurrentSearchString { get; set; }
        public int? PageIndex { get; set; }
        public string CurrentSort { get; set; }
        public string AccountSortParam { get; set; }
        public string ArrivalSortParam { get; set; }
        public string DepartureSortParam { get; set; }
        public string SumSortParam { get; set; }
        public string PlaceName { get; set; }
        public string Selection { get; set; } = "All reservations";
        public string Current { get; set; } = "Current";
        public string Waiting { get; set; } = "Waiting";
        public string InPlace { get; set; } = "InPlace";
        public string Canceled { get; set; } = "Canceled";
        public string Completed { get; set; } = "Completed";
        public string AllReservations { get; set; } = "All reservations";
        public string AccountId { get; set; }
        public PaginatedList<Reservation> PaginatedList { get; set; }
    }
}
