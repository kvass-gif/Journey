using Journey.Data;
using Journey.Entities;

namespace Journey.ViewModels.LandLord
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
                reservations = reservations.Where(s => s.Account!.UserName.Contains(searchString));
            }
            return reservations;
        }
        private IEnumerable<Reservation> sort(string sortOrder, IEnumerable<Reservation> reservations)
        {
            switch (sortOrder)
            {
                case "UserName_desc": reservations = reservations.OrderByDescending(s => s.Account!.UserName); break;
                case "Arrival": reservations = reservations.OrderBy(s => s.ArrivalDate); break;
                case "Arrival_desc": reservations = reservations.OrderByDescending(s => s.ArrivalDate); break;
                case "Departure": reservations = reservations.OrderBy(s => s.DepartureDate); break;
                case "Departure_desc": reservations = reservations.OrderByDescending(s => s.DepartureDate); break;
                case "Sum": reservations = reservations.OrderBy(s => s.Sum); break;
                case "Sum_desc": reservations = reservations.OrderByDescending(s => s.Sum); break;
                default: reservations = reservations.OrderBy(s => s.Account!.UserName); break;
            }
            return reservations;
        }
        private IEnumerable<Reservation> generatStatDictionary(IEnumerable<Reservation> reservations)
        {
            foreach (var res in reservations)
            {
                var dic = new Dictionary<int, string>();
                foreach (var stat in Enum.GetValues(typeof(Status)))
                {
                    if (
                        (Status)stat == Status.Waiting
                        ||
                        res.IsPaid && res.ArrivalDate < DateTime.Now
                        && (((Status)stat == Status.Completed)
                        || ((Status)stat == Status.InPlace))
                        ||
                        !res.IsPaid
                        && res.ArrivalDate >= DateTime.Now
                        && ((Status)stat == Status.Canceled)
                        )
                    {
                        dic.Add((int)stat, ((Status)stat).ToString());
                    }
                }
                res.StatusDictionary = dic;
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
            reservationsView.AccountSortParam = string.IsNullOrEmpty(reservationsView.CurrentSort) ? "UserName_desc" : "";
            reservationsView.ArrivalSortParam = reservationsView.CurrentSort == "Arrival_desc" ? "Arrival" : "Arrival_desc";
            reservationsView.DepartureSortParam = reservationsView.CurrentSort == "Departure_desc" ? "Departure" : "Departure_desc";
            reservationsView.SumSortParam = reservationsView.CurrentSort == "Sum_desc" ? "Sum" : "Sum_desc";
            reservationsView.PlaceName = unitOfWork.PlaceRepo.Find(reservationsView.PlaceId)!.PlaceName;
            var objs = unitOfWork.ReservationRepo.ReservationsByParams(reservationsView.PlaceId, reservationsView.Selection);
            objs = generatStatDictionary(objs);
            objs = calcSum(objs);
            objs = searchFilter(reservationsView.SearchString, objs);
            objs = sort(reservationsView.CurrentSort, objs);
            int pageSize = 20;
            reservationsView.PaginatedList = PaginatedList<Reservation>.Create(objs, reservationsView.PageIndex ?? 1, pageSize);
        }
    }
}
