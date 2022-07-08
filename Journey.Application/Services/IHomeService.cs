using Journey.Application.Models;

namespace Journey.Application.Services
{
    public interface IHomeService
    {
        Task<IEnumerable<PlaceResponseModel>> GetAllByListAsync();
        Task<IEnumerable<PlaceResponseModel>> GetAllByListAsync(string placeName);
    }
}
