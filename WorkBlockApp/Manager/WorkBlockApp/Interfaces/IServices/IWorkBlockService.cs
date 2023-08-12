using WorkBlockApp.DTOs;
using WorkBlockApp.Models.Domain;

namespace WorkBlockApp.Interfaces.IServices;

public interface IWorkBlockService
{
    Task<ResponseGenerico<AdministratorModel>> Get(int id);
    Task<ResponseGenerico<List<AdministratorModel>>> GetAll();
}