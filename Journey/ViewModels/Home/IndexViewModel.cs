namespace Journey.ViewModels.Home
{
    public class IndexViewModel<T>
    {
        public IndexViewModel()
        {
            Params = new Dictionary<string, string>();
        }
        public PaginatedList<T> PaginatedList { get; set; }
        public Dictionary<string, string> Params { get; set; }
        public Dictionary<int, string> Cities { get; set; }
        public Dictionary<int, string> Types { get; set; }
    }
}
