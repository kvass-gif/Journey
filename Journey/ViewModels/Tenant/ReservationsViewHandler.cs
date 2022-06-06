using Journey.Data;
using Journey.Entities;

namespace Journey.ViewModels.Tenant
{
    public class ReservationsViewHandler
    {
        private readonly UnitOfWork unitOfWork;
        public ReservationsViewHandler(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        private IEnumerable<Reservation> searchFilter(string? searchString, IEnumerable<Reservation> reservations)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                reservations = reservations.Where(s => s.Place.PlaceName.Contains(searchString));
            }
            return reservations;
        }
        private IEnumerable<Reservation> sort(string sortOrder, IEnumerable<Reservation> reservations)
        {
            switch (sortOrder)
            {
                case "PlaceName_desc": reservations = reservations.OrderByDescending(s => s.Place.PlaceName); break;
                case "Arrival": reservations = reservations.OrderBy(s => s.ArrivalDate); break;
                case "Arrival_desc": reservations = reservations.OrderByDescending(s => s.ArrivalDate); break;
                case "Departure": reservations = reservations.OrderBy(s => s.DepartureDate); break;
                case "Departure_desc": reservations = reservations.OrderByDescending(s => s.DepartureDate); break;
                case "Sum": reservations = reservations.OrderBy(s => s.Sum); break;
                case "Sum_desc": reservations = reservations.OrderByDescending(s => s.Sum); break;
                default: reservations = reservations.OrderBy(s => s.Place.PlaceName); break;
            }
            return reservations;
        }
        private IEnumerable<Reservation> calcSum(IEnumerable<Reservation> reservations)
        {
            foreach (var item in reservations)
            {
                item.Sum = (item.DepartureDate - item.ArrivalDate).Days * item.Place!.PricePerNight;
            }
            return reservations;
        }
        public void HandleReservationsView(ReservationsViewModel reservationsView)
        {
            if (reservationsView.SearchString != null)
            {
                reservationsView.PageIndex = 1;
            }
            else
            {
                reservationsView.SearchString = reservationsView.CurrentSearchString;
            }
            reservationsView.CurrentSort = reservationsView.CurrentSort;
            reservationsView.AccountSortParam = string.IsNullOrEmpty(reservationsView.CurrentSort) ? "PlaceName_desc" : "";
            reservationsView.ArrivalSortParam = reservationsView.CurrentSort == "Arrival_desc" ? "Arrival" : "Arrival_desc";
            reservationsView.DepartureSortParam = reservationsView.CurrentSort == "Departure_desc" ? "Departure" : "Departure_desc";
            reservationsView.SumSortParam = reservationsView.CurrentSort == "Sum_desc" ? "Sum" : "Sum_desc";
            var objs = unitOfWork.ReservationRepo.ReservationsByTenantId(reservationsView.AccountId, reservationsView.Selection);
            objs = calcSum(objs);
            objs = searchFilter(reservationsView.SearchString, objs);
            objs = sort(reservationsView.CurrentSort, objs);
            int pageSize = 20;
            reservationsView.PaginatedList = PaginatedList<Reservation>.Create(objs, reservationsView.PageIndex ?? 1, pageSize);
        }
    }
}
