using WorkBlockApi.ViewModels;
using WorkBlockApp.DTOs;
using WorkBlockApp.ViewModels.VMReports;

namespace WorkBlockApp.Interfaces.IRepository;

public interface IPontoBlockReportsRepository
{
    Task<ResponseGenerico<EmployeeDateViewModel>> GetWorkTimesFromEmployeeAtDate(ReportAtDateOutPutViewModel model);
    Task<ResponseGenerico<EmployeeDateViewModel>> GetWorkTimesFromEmployeeBetweenTwoDates(ReportAtDateOutPutViewModel model);

    // Task<ResponseGenerico<AdminAddedEventModel>> Add(AdministratorModel administrator);
    // Task<ResponseGenerico<AdministratorUpdateViewModel>> Update(AdministratorUpdateModel administrator);
}
