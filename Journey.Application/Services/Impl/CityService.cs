using AutoMapper;
using Journey.Application.Models.City;
using Journey.DataAccess.Repositories;

namespace Journey.Application.Services.Impl;

public class CityService : ICityService
{
    private readonly IMapper _mapper;
    private readonly ICityRepository _cityRepository;
    public CityService(ICityRepository cityRepository, IMapper mapper)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<CityResponseModel>> GetAllByListIdAsync(long id,
        CancellationToken cancellationToken = default)
    {
        var todoItems = await _cityRepository.GetAllAsync(ti => ti.PlaceId == id);
        return _mapper.Map<IEnumerable<CityResponseModel>>(todoItems);
    }
}
