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

    public async Task<ResponseGenerico<ReportModel>> GetWorkTimesFromEmployeeAtDate(ReportModel model)
    {     
        var OutPutModel = new ReportAtDateOutPutViewModel
        {
            Carteira = model.Carteira,
            Timestamp = ConvertToUnixTimeStamp(DateTime.ParseExact(model.Periodo.Split(" / ")[0], "dd-MMMM-yyyy", CultureInfo.InvariantCulture))
        };
        var response = await _pontoBlockReportsRepository.GetWorkTimesFromEmployeeAtDate(OutPutModel);
        var result = new ResponseGenerico<ReportModel>();
        if (response.DadosRetorno != null)
        {
            model.Data = ((ulong)(response.DadosRetorno.Date)).ToString();
            model.InicioJornada = ((ulong)(response.DadosRetorno.StartWork)).ToString();
            model.InicioPausa = ((ulong)(response.DadosRetorno.BreakStartTime)).ToString();
            model.FimPausa = ((ulong)(response.DadosRetorno.BreakEndTime)).ToString();
            model.FimJornada = ((ulong)(response.DadosRetorno.EndWork)).ToString();
            result.CodigoHttp = response.CodigoHttp;
            result.DadosRetorno = model;
            result.ErroRetorno = response.ErroRetorno;
            return result;
        }
        result.CodigoHttp = response.CodigoHttp;
        result.DadosRetorno = new ReportModel();
        result.ErroRetorno = response.ErroRetorno;
        return result;

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
