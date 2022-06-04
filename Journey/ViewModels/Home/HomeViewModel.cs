using Journey.Entities;

namespace Journey.ViewModels.Home
{
    public class HomeViewModel
    {
        public Dictionary<int, string> Cities { get; set; }
        public Dictionary<int, string> Types { get; set; }
        public int SelectedCityId { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? DepartureDate { get; set; }
        public int SelectedTypeId { get; set; }
        public string? SearchString { get; set; }
        public string? CurrentSearchString { get; set; }
        public string CurrentSort { get; set; }
        public int LowerPrice { get; set; } = 0;
        public int UpperPrice { get; set; } = 100;
        public int BedsCount { get; set; } = 0;
        public int? PageIndex { get; set; }
        public string RankSortParam { get; set; } = "";
        public string PriceSortParam { get; set; } = "Price_desc";
        public string NewestSortParam { get; set; } = "Newest_desc";
        public PaginatedList<Place> PaginatedList { get; set; }
    }
}
