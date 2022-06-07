using Journey.Data;
using Journey.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Journey.ViewModels.Home
{
    public class HomeViewHandler
    {
        private readonly UnitOfWork unitOfWork;
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
        private List<SelectListItem> listFacilities(HomeViewModel indexView)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in unitOfWork.FacilityRepo.Facilities())
            {
                items.Add(new SelectListItem
                {
                    Text = item.Name.ToString(),
                    Value = item.Id.ToString()
                });
            }
            foreach (SelectListItem item in items)
            {
                if (indexView.Facilities.Contains(item.Value))
                {
                    item.Selected = true;
                }
            }
            return items;
        }
        private IEnumerable<Place> filterFacilities(IEnumerable<Place> places, List<SelectListItem> listItems)
        {
            var newPlaces = new List<Place>();
            foreach (var place in places)
            {
                var list = listItems.Where(a => a.Selected == true);
                bool[] isTrue = new bool[list.Count()];
                int i = 0;
                foreach (var item in list)
                {
                    if (place.Facilities.Where(a => a.FacilityId.ToString() == item.Value).Count() > 0)
                    {
                        isTrue[i] = true;
                    }
                    i++;
                }
                if (!isTrue.Contains(false))
                {
                    newPlaces.Add(place);
                }
            }
            if (newPlaces.Count() > 0)
            {
                places = newPlaces;
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
            indexView.ListFacilities = listFacilities(indexView);
            places = filterFacilities(places, indexView.ListFacilities);
            int pageSize = 20;
            indexView.PaginatedList = PaginatedList<Place>.Create(places, indexView.PageIndex ?? 1, pageSize);
        }
    }
}
