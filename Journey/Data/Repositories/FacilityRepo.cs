using Journey.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Data.Repositories
{
    public class FacilityRepo
    {
        private readonly DbSet<Facility> _facilities;
        public FacilityRepo(ApplicationDbContext appDbContext)
        {
            _facilities = appDbContext.Facilities;
        }
        public IQueryable<Facility> Facilities()
        {
            var facilities = (from a in _facilities
                              orderby a.Name
                              select a);
            return facilities;
        }
        public Dictionary<bool, string> ToDictionary()
        {
            var citiesDictionary = new Dictionary<bool, string>();
            foreach (var item in Facilities())
            {
                citiesDictionary.Add(false, item.Name);
            }
            return citiesDictionary;
        }
    }
}
