using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WorkBlockApp.DTOs;
using WorkBlockApp.Interfaces.IEnvironment;
using WorkBlockApp.Interfaces.IRepository;
using WorkBlockApp.ViewModels.VMReports;

namespace WorkBlockApp.Repository;

public class ReportsRepository : IPontoBlockReportsRepository
{
    private readonly IWorkBlockAppConfiguration _appConfiguration;

    public ReportsRepository(IWorkBlockAppConfiguration appConfiguration)
    {
        _appConfiguration = appConfiguration;
    }

    public async Task<ResponseGenerico<ReportInputViewModel>> GetWorkTimesFromEmployeeAtDate(ReportAtDateOutPutViewModel model)
    {
        var op = $"GetWorkTimesFromEmployeeAtDate?address={model.Carteira}&timestamp={model.Timestamp}";
        var requestUri = $"{_appConfiguration.GetPontoBlockReportsEndPoint()}{op}";
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

        var response = new ResponseGenerico<ReportInputViewModel>();
        using var client = new HttpClient();
        try
        {
            var responseRequestApi = await client.SendAsync(request);
            var contentResponse = await responseRequestApi.Content.ReadAsStringAsync();
            var objResponse = JsonSerializer.Deserialize<ResultViewRequest<ReportInputViewModel>>(contentResponse);

            if (objResponse == null) return response;

            response.CodigoHttp = responseRequestApi.StatusCode;
            if (responseRequestApi.IsSuccessStatusCode)
                response.DadosRetorno = objResponse.Data;
            else
            {
                if (objResponse.Errors != null)
                    response.ErroRetorno = objResponse.Errors.ToList();
            }

            return response;
        }
        catch (Exception ex)
        {
            response.ErroRetorno = new List<string>{ ex.Message };
            return response;
        }

    }

    public Task<ResponseGenerico<List<ReportInputViewModel>>> GetWorkTimesFromEmployeeBetweenTwoDates(ReportBetweenTwoDatesOutPutViewModel model)
    {
        throw new NotImplementedException();
    }
}
