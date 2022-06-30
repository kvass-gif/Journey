using AutoMapper;
using Journey.Application.Models;
using Journey.DataAccess;
using Journey.DataAccess.Repositories;

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
        var todoItems = await _unitOfWork.PlaceRepo.GetAllAsync(ti => ti.PlaceId == id);
        return _mapper.Map<IEnumerable<PlaceResponseModel>>(todoItems);
    }
    public async Task<IEnumerable<PlaceResponseModel>> GetAllByListAsync(
        CancellationToken cancellationToken = default)
    {
        var todoItems = await _unitOfWork.PlaceRepo.GetAllAsync();
        return _mapper.Map<IEnumerable<PlaceResponseModel>>(todoItems);
    }
}
