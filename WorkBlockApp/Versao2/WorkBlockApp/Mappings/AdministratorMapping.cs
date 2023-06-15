using AutoMapper;
using WorkBlockApp.DTOs;
using WorkBlockApp.Models.Domain;

namespace WorkBlockApp.Mappings;

public class AdministratorMapping : Profile
{
    public AdministratorMapping()
    {
        CreateMap(typeof(ResponseGenerico<>), typeof(ResponseGenerico<>));
        CreateMap<AdministratorModel, AdministratorResponse>();
        CreateMap<AdministratorResponse, AdministratorModel>();
    }
}