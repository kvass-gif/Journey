using Journey.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Data.Repositories
{
    public class TypeRepo
    {
        private readonly DbSet<PlaceType> _types;
        public TypeRepo(ApplicationDbContext appDbContext)
        {
            _types = appDbContext.PlaceTypes;
        }
        public IQueryable<PlaceType> Types()
        {
            var objs = (from a in _types
                          orderby a.TypeName
                          select a);
            return objs;
        }
    }
}
