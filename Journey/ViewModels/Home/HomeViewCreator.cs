using Journey.Data;
using Journey.Entities;

namespace Journey.ViewModels.Home
{
    public class HomeViewCreator
    {
        private readonly UnitOfWork unitOfWork;
        public Place Place { get; set; }
        public HomeViewCreator(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        private Dictionary<int, string> citiesList()
        {
            var citySelectedList = new Dictionary<int, string>();
            foreach (var item in unitOfWork.CityRepo.Cities())
            {
                citySelectedList.Add(item.Id, item.CityName);
            }
            return citySelectedList;
        }
        private IEnumerable<Place> serchSortAction(string searchString, string sortOrder,
            DateTime arrivalDate, DateTime departureDate, int selectCity)
        {
            var objects = unitOfWork.PlaceRepo.Places(arrivalDate, departureDate, selectCity);
            if (!string.IsNullOrEmpty(searchString))
            {
                objects = objects.Where(s => s.PlaceName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Rank_desc": objects = objects.OrderBy(s => s.Rank); break;
                case "Price": objects = objects.OrderBy(s => s.PricePerNight); break;
                case "Price_desc": objects = objects.OrderByDescending(s => s.PricePerNight); break;
                case "CreatedAt": objects = objects.OrderBy(s => s.CreatedAt); break;
                case "CreatedAt_desc": objects = objects.OrderByDescending(s => s.CreatedAt); break;
                default: objects = objects.OrderByDescending(s => s.Rank); break;
            }
            return objects;
        }
        public IndexViewModel<Place> CreateIndexView(
            DateTime? arrivalDate, DateTime? departureDate,
            string sortOrder,
            string currentFilter,
            string searchString,
            int selectCity,
            int? pageNumber)
        {
            DateTime arrivalDateLocal;
            DateTime departureDateLocal;
            if (arrivalDate == null || departureDate == null)
            {
                arrivalDateLocal = DateTime.Now.AddDays(1);
                departureDateLocal = DateTime.Now.AddDays(2);
            }
            else
            {
                arrivalDateLocal = (DateTime)arrivalDate;
                departureDateLocal = (DateTime)departureDate;
            }

            var indexView = new IndexViewModel<Place>();
            indexView.Params.Add("ArrivalDate", arrivalDateLocal.ToString("yyyy-MM-dd"));
            indexView.Params.Add("DepartureDate", departureDateLocal.ToString("yyyy-MM-dd"));
            indexView.Cities = citiesList();

            indexView.Params.Add("CurrentSort", sortOrder);
            indexView.Params.Add("RankSortParm", string.IsNullOrEmpty(sortOrder) ? "Rank_desc" : "");
            indexView.Params.Add("PriceSortParm", sortOrder == "Price" ? "Price_desc" : "Price");
            indexView.Params.Add("CreatedAtSortParm", sortOrder == "CreatedAt" ? "CreatedAt_desc" : "CreatedAt");
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            indexView.Params.Add("CurrentFilter", searchString);

            var objects = serchSortAction(searchString, sortOrder, arrivalDateLocal, departureDateLocal , selectCity);
            int pageSize = 20;
            indexView.PaginatedList = PaginatedList<Place>.Create(objects, pageNumber ?? 1, pageSize);
            return indexView;
        }
    }
}
