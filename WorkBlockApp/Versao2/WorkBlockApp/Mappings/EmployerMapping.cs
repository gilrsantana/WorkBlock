using AutoMapper;
using WorkBlockApp.DTOs;
using WorkBlockApp.Models.Domain;

namespace WorkBlockApp.Mappings;

public class EmployerMapping : Profile
{
    public EmployerMapping()
    {
        CreateMap(typeof(ResponseGenerico<>), typeof(ResponseGenerico<>));
        CreateMap<EmployerModel, EmployerResponse>();
        CreateMap<EmployerResponse, EmployerModel>();
    }
}
