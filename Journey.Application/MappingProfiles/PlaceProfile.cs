using AutoMapper;
using Journey.Application.Models;
using Journey.DataAccess.Entities;

namespace Journey.MappingProciles;

public class PlaceProfile : Profile
{
    public PlaceProfile()
    {
        CreateMap<Place, PlaceResponseModel>();
    }
}
