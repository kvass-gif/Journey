using Journey.Application.Models;

namespace Journey.Application.Services;

public interface IPlaceService
{
    Task<IEnumerable<PlaceResponseModel>>GetAllByListIdAsync(long id, CancellationToken cancellationToken = default);
    Task<IEnumerable<PlaceResponseModel>> GetAllByListAsync(CancellationToken cancellationToken = default);
}
