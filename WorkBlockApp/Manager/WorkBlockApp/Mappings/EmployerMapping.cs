using AutoMapper;
using WorkBlockApp.DTOs;
using WorkBlockApp.Models.Domain;
using WorkBlockApp.ViewModels.VMEmployer;

namespace WorkBlockApp.Mappings;

public class EmployerMapping : Profile
{
    public EmployerMapping()
    {
        CreateMap(typeof(ResponseGenerico<>), typeof(ResponseGenerico<>));
        CreateMap<EmployerModel, EmployerResponseUpdate>();
        CreateMap<EmployerResponseUpdate, EmployerModel>();
        CreateMap<EmployerResponse, EmployerIndexViewModel>();
        CreateMap<EmployerIndexViewModel, EmployerResponse>();
        CreateMap<EmployerResponse, EmployerModel>();
        CreateMap<EmployerModel, EmployerResponse>();
    }
}
