using Journey.Entities;

namespace Journey.ViewModels.LandLord
{
    public class PlacesViewModel
    {
        public string? SearchString { get; set; }
        public string? CurrentSearchString { get; set; }
        public int? PageIndex { get; set; }
        public string CurrentSort { get; set; }
        public string NameSortParam { get; set; }
        public string RankSortParam { get; set; }
        public string PriceSortParam { get; set; }
        public string CitySortParam { get; set; }
        public string TypeSortParam { get; set; }
        public string CreatedAtSortParam { get; set; }
        public PaginatedList<Place> PaginatedList { get; set; }
    }
}
