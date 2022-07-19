using AutoMapper;
using Journey.Core.ResponseModels;
using Journey.DataAccess.Entities;

namespace Journey.MappingProciles;

public class PlaceProfile : Profile
{
    public PlaceProfile()
    {
        CreateMap<Place, PlaceResponseModel>();
    }
}