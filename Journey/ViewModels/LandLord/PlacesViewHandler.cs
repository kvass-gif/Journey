using Journey.Data;
using Journey.Entities;

namespace Journey.ViewModels.LandLord
{
    public class PlacesViewHandler
    {
        private readonly UnitOfWork unitOfWork;
        public PlacesViewHandler(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        private IQueryable<Place> searchFilter(string? searchString, IQueryable<Place> places)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                places = places.Where(s => s.PlaceName.Contains(searchString));
            }
            return places;
        }
        private IQueryable<Place> sort(string sortOrder, IQueryable<Place> places)
        {
            switch (sortOrder)
            {
                case "Name_desc": places = places.OrderByDescending(s => s.PlaceName); break;
                case "Price": places = places.OrderBy(s => s.PricePerNight); break;
                case "Price_desc": places = places.OrderByDescending(s => s.PricePerNight); break;
                case "CreatedAt": places = places.OrderBy(s => s.CreatedAt); break;
                case "CreatedAt_desc": places = places.OrderByDescending(s => s.CreatedAt); break;
                case "City": places = places.OrderBy(s => s.City!.CityName); break;
                case "City_desc": places = places.OrderByDescending(s => s.City!.CityName); break;
                case "Type": places = places.OrderBy(s => s.PlaceType!.TypeName); break;
                case "Type_desc": places = places.OrderByDescending(s => s.PlaceType!.TypeName); break;
                case "Rank": places = places.OrderBy(s => s.Rank); break;
                case "Rank_desc": places = places.OrderByDescending(s => s.Rank); break;
                default: places = places.OrderBy(s => s.PlaceName); break;
            }
            return places;
        }
        public void HandlePlacesView(PlacesViewModel placesView, string accountId)
        {
            if (placesView.SearchString != null)
            {
                placesView.PageIndex = 1;
            }
            else
            {
                placesView.SearchString = placesView.CurrentSearchString;
            }
            placesView.CurrentSort = placesView.CurrentSort;
            placesView.NameSortParam = string.IsNullOrEmpty(placesView.CurrentSort) ? "Name_desc" : "";
            placesView.PriceSortParam = placesView.CurrentSort == "Price_desc" ? "Price" : "Price_desc";
            placesView.CreatedAtSortParam = placesView.CurrentSort == "CreatedAt_desc" ? "CreatedAt" : "CreatedAt_desc";
            placesView.RankSortParam = placesView.CurrentSort == "Rank_desc" ? "Rank" : "Rank_desc";
            placesView.CitySortParam = placesView.CurrentSort == "City" ? "City_desc" : "City";
            placesView.TypeSortParam = placesView.CurrentSort == "Type" ? "Type_desc" : "Type";
            var places = unitOfWork.PlaceRepo.Places(accountId);
            places = searchFilter(placesView.SearchString, places);
            places = sort(placesView.CurrentSort, places);
            int pageSize = 20;
            placesView.PaginatedList = PaginatedList<Place>.Create(places, placesView.PageIndex ?? 1, pageSize);
        }
        public Place HandleSinglePlaceModel(Place place)
        {
            if (place.Cities == null)
            {
                place.Cities = unitOfWork.CityRepo.ToDictionary();
            }
            if (place.PlaceTypes == null)
            {
                place.PlaceTypes = unitOfWork.TypeRepo.ToDictionary();
            }
            return place;
        }
    }
}
