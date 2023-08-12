using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WorkBlockApi.ViewModels;
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

    public async Task<ResponseGenerico<EmployeeDateViewModel>> GetData(ReportModel model)
    {
        var epoch1 = model.Periodo.Split(" / ")[0];
        var epoch2 = model.Periodo.Split(" / ")[1];
        if (epoch1 == epoch2)
        {
            return await GetWorkTimesFromEmployeeAtDate(model);
        }
        return await GetWorkTimesFromEmployeeBetweenTwoDates(model);
    }
    private async Task<ResponseGenerico<EmployeeDateViewModel>> GetWorkTimesFromEmployeeAtDate(ReportModel model)
    {     
        var OutPutModel = new ReportAtDateOutPutViewModel
        {
            Carteira = model.Carteira,
            StartTimestamp = ConvertToUnixTimeStamp(DateTime.ParseExact(model.Periodo.Split(" / ")[0], "dd-MMMM-yyyy", CultureInfo.InvariantCulture))
        };
        var result = await _pontoBlockReportsRepository.GetWorkTimesFromEmployeeAtDate(OutPutModel);
        return result;

    }

    private async Task<ResponseGenerico<EmployeeDateViewModel>> GetWorkTimesFromEmployeeBetweenTwoDates(ReportModel model)
    {
        var OutPutModel = new ReportAtDateOutPutViewModel
        {
            Carteira = model.Carteira,
            StartTimestamp = ConvertToUnixTimeStamp(DateTime.ParseExact(model.Periodo.Split(" / ")[0], "dd-MMMM-yyyy", CultureInfo.InvariantCulture)),
            EndTimestamp = ConvertToUnixTimeStamp(DateTime.ParseExact(model.Periodo.Split(" / ")[1], "dd-MMMM-yyyy", CultureInfo.InvariantCulture))
        };
        var result = await _pontoBlockReportsRepository.GetWorkTimesFromEmployeeBetweenTwoDates(OutPutModel);
        return result;
    }

    private ulong ConvertToUnixTimeStamp(DateTime date)
    {
        return (ulong)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
    }
}
