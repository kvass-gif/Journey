using Journey.Core.ResponseModels;

namespace Journey.Application.Services
{
    public interface IHomeService
    {
        Task<IEnumerable<PlaceResponseModel>> GetAllByListAsync();
    }
}