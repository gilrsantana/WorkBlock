using WorkBlockApp.DTOs;
using WorkBlockApp.Models.Domain;
using WorkBlockApp.ViewModels.VMReports;

namespace WorkBlockApp.Interfaces.IServices;

public interface IPontoBlockReportsService
{
    Task<ResponseGenerico<ReportInputViewModel>> GetWorkTimesFromEmployeeAtDate(ReportModel model);
    Task<ResponseGenerico<ReportInputViewModel>> GetWorkTimesFromEmployeeBetweenTwoDates(ReportModel model);
    // Task<ResponseGenerico<List<AdministratorResponse>>> GetAll();
    // Task<ResponseGenerico<AdminAddedEventModel>> Add(AdministratorModel administrator);
    // Task<ResponseGenerico<AdministratorUpdateViewModel>> Update(AdministratorUpdateModel administrator);  
}