using Journey.Application.Models.City;

namespace Journey.Application.Services;

public interface ICityService
{
    Task<IEnumerable<CityResponseModel>>GetAllByListIdAsync(long id, CancellationToken cancellationToken = default);
}
