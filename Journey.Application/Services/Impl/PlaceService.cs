using AutoMapper;
using Journey.Application.Models;
using Journey.DataAccess;

namespace Journey.Application.Services.Impl;

public class PlaceService : IPlaceService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public PlaceService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<IEnumerable<PlaceResponseModel>> GetAllByListIdAsync(long id,
        CancellationToken cancellationToken = default)
    {
        var places = await _unitOfWork.PlaceRepo.GetAllAsync(ti => ti.Id == id);
        return _mapper.Map<IEnumerable<PlaceResponseModel>>(places);
    }
    public async Task<IEnumerable<PlaceResponseModel>> GetAllByListAsync(
        CancellationToken cancellationToken = default)
    {
        var places = await _unitOfWork.PlaceRepo.GetAllAsync();
        return _mapper.Map<IEnumerable<PlaceResponseModel>>(places);
    }
}
