
using Journey.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Data.Repositories
{
    public class PhotoRepo
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly DbSet<Photo> photos;
        public PhotoRepo(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            photos = appDbContext.Photos;
        }
        public IQueryable<Photo> GetAll(bool include = false)
        {
            if (include)
            {
                return photos.Include(a => a.Place);
            }
            return photos;
        }
        public void Add(Photo obj)
        {
            photos.Add(obj);
        }

        public Photo? Find(int id, bool include = false)
        {
            if (include)
            {
                return photos.Include(a => a.Place)
                    .ThenInclude(a => a!.City)
                    .SingleOrDefault(c => c.Id == id);
            }
            return photos.SingleOrDefault(c => c.Id == id);
        }
        public string? FindName(int id)
        {
            var res = (from c in photos
                       where c.Id == id
                       select c.PhotoName)
                       .SingleOrDefault();
            return res;
        }
        public void Update(Photo otherObj)
        {
            var thisObj = Find(otherObj.Id);
            if (thisObj == null)
            {
                throw new DbUpdateException();
            }
            if (otherObj.PhotoName == null)
            {
                otherObj.PhotoName = thisObj.PhotoName;
            }
            otherObj.CreatedAt = thisObj.CreatedAt;
            photos.Update(otherObj);
        }
        public void Remove(Photo obj)
        {
            photos.Remove(obj);
        }
    }
}
