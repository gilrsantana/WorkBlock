using WorkBlockApp.DTOs;
using WorkBlockApp.Models.Domain;
using WorkBlockApp.Models.Event;

namespace WorkBlockApp.Interfaces.IServices;

public interface IAdministratorService
{
    Task<ResponseGenerico<AdministratorResponse>> GetById(int id);
    Task<ResponseGenerico<AdministratorUpdateModel>?> GetByAddress(string address);
    Task<ResponseGenerico<List<AdministratorResponse>>> GetAll();
    Task<ResponseGenerico<AdminAddedEventModel>> Add(AdministratorModel administrator);
    Task<ResponseGenerico<AdministratorUpdateViewModel>> Update(AdministratorUpdateModel administrator);   
}