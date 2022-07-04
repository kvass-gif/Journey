using AutoMapper;
using Journey.Application.Models;
using Journey.DataAccess.Repositories;

namespace Journey.Application.Services.Impl
{
    public class HomeService : IHomeService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public HomeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlaceResponseModel>> GetAllByListAsync()
        {
            var places = await _unitOfWork.PlaceRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<PlaceResponseModel>>(places);
        }

       
    }
}
