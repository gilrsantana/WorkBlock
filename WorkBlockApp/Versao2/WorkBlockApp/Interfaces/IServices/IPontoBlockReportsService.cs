using WorkBlockApi.ViewModels;
using WorkBlockApp.DTOs;
using WorkBlockApp.Models.Domain;
using WorkBlockApp.ViewModels.VMReports;

namespace WorkBlockApp.Interfaces.IServices;

public interface IPontoBlockReportsService
{
    Task<ResponseGenerico<EmployeeDateViewModel>> GetData(ReportModel model);
    // Task<ResponseGenerico<EmployeeDateViewModel>> GetWorkTimesFromEmployeeAtDate(ReportModel model);
    // Task<ResponseGenerico<EmployeeDateViewModel>> GetWorkTimesFromEmployeeBetweenTwoDates(ReportModel model);
    // Task<ResponseGenerico<AdminAddedEventModel>> Add(AdministratorModel administrator);
    // Task<ResponseGenerico<AdministratorUpdateViewModel>> Update(AdministratorUpdateModel administrator);  
}