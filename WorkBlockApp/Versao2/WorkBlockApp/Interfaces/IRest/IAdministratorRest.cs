using WorkBlockApp.DTOs;
using WorkBlockApp.Models.Domain;
using WorkBlockApp.Models.Event;

namespace WorkBlockApp.Interfaces.IRest;

public interface IAdministratorRest
{
    Task<ResponseGenerico<AdministratorModel>> GetById(int id);
    Task<ResponseGenerico<List<AdministratorModel>>> GetAll();
    Task<ResponseGenerico<AdminAddedEventModel>> Add(AdministratorModel administrator);
    Task<ResponseGenerico<AdministratorUpdateViewModel>> Update(AdministratorUpdateModel administrator); 
}