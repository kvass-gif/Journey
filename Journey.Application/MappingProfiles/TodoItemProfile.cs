using AutoMapper;
using Journey.Application.Models;
using Journey.DataAccess.Entities;

namespace Journey.MappingProciles;

public class TodoItemProfile : Profile
{
    public TodoItemProfile()
    {
        CreateMap<Place, PlaceResponseModel>();
    }
}
