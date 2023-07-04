using WorkBlockApp.DTOs;
using WorkBlockApp.ViewModels.VMReports;

namespace WorkBlockApp.Interfaces.IRepository;

public interface IPontoBlockReportsRepository
{
    Task<ResponseGenerico<ReportInputViewModel>> GetWorkTimesFromEmployeeAtDate(ReportAtDateOutPutViewModel model);
    Task<ResponseGenerico<List<ReportInputViewModel>>> GetWorkTimesFromEmployeeBetweenTwoDates(ReportBetweenTwoDatesOutPutViewModel model);

    // Task<ResponseGenerico<AdminAddedEventModel>> Add(AdministratorModel administrator);
    // Task<ResponseGenerico<AdministratorUpdateViewModel>> Update(AdministratorUpdateModel administrator);
}
