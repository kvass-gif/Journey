using Journey.Data;
using Journey.Entities;

namespace Journey.ViewModels.Home
{
    public class HomeViewHandler
    {
        private readonly UnitOfWork unitOfWork;
        public Place Place { get; set; }
        public HomeViewHandler(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        private IEnumerable<Place> searchFilter(string? searchString, IEnumerable<Place> places)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                places = places.Where(s => s.PlaceName.Contains(searchString));
            }
            return places;
        }
        private IEnumerable<Place> sort(string sortOrder, IEnumerable<Place> places)
        {
            switch (sortOrder)
            {
                case "Price_desc": places = places.OrderByDescending(s => s.PricePerNight); break;
                case "Newest_desc": places = places.OrderByDescending(s => s.CreatedAt); break;
                default: places = places.OrderByDescending(s => s.Rank); break;
            }
            return places;
        }
        public void HandleIndexView(HomeViewModel indexView)
        {
            if (indexView.SearchString != null)
            {
                indexView.PageIndex = 1;
            }
            else
            {
                indexView.SearchString = indexView.CurrentSearchString;
            }
            if (indexView.Cities == null)
            {
                indexView.Cities = unitOfWork.CityRepo.ToDictionary();
            }
            if (indexView.Types == null)
            {
                indexView.Types = unitOfWork.TypeRepo.ToDictionary();
            }
            var places = unitOfWork.PlaceRepo.Places(indexView);
            places = searchFilter(indexView.SearchString, places);
            places = sort(indexView.CurrentSort, places);
            int pageSize = 20;
            indexView.PaginatedList = PaginatedList<Place>.Create(places, indexView.PageIndex ?? 1, pageSize);
        }
    }
}
