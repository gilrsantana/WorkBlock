using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WorkBlockApp.DTOs;
using WorkBlockApp.Interfaces.IRepository;
using WorkBlockApp.Interfaces.IServices;
using WorkBlockApp.Models.Domain;
using WorkBlockApp.ViewModels.VMReports;

namespace WorkBlockApp.Services;

public class ReportsService : IPontoBlockReportsService
{
    private readonly IPontoBlockReportsRepository _pontoBlockReportsRepository;

    public ReportsService(IPontoBlockReportsRepository pontoBlockReportsRepository)
    {
        _pontoBlockReportsRepository = pontoBlockReportsRepository;
    }

    public async Task<ResponseGenerico<ReportInputViewModel>> GetWorkTimesFromEmployeeAtDate(ReportModel model)
    {     
        var OutPutModel = new ReportAtDateOutPutViewModel
        {
            Carteira = model.Carteira,
            Timestamp = ConvertToUnixTimeStamp(DateTime.ParseExact(model.Periodo.Split(" / ")[0], "dd-MMMM-yyyy", CultureInfo.InvariantCulture))
        };
        var response = await _pontoBlockReportsRepository.GetWorkTimesFromEmployeeAtDate(OutPutModel);
        return response;
    }

    public Task<ResponseGenerico<ReportInputViewModel>> GetWorkTimesFromEmployeeBetweenTwoDates(ReportModel model)
    {
        throw new NotImplementedException();
    }

    private ulong ConvertToUnixTimeStamp(DateTime date)
    {
        return (ulong)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
    }
}
