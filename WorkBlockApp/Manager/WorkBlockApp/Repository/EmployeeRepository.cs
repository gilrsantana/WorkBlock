using System.Collections;
using System.Text;
using System.Text.Json;
using WorkBlockApp.DTOs;
using WorkBlockApp.Interfaces.IEnvironment;
using WorkBlockApp.Interfaces.IRepository;
using WorkBlockApp.Models.Domain;
using WorkBlockApp.Models.Domain.Event;
using WorkBlockApp.ViewModels.VMEmployee;

namespace WorkBlockApp.Repository;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IWorkBlockAppConfiguration _appConfiguration;

    public EmployeeRepository(IWorkBlockAppConfiguration appConfiguration)
    {
        _appConfiguration = appConfiguration;
    }

    public async Task<ResponseGenerico<IEnumerable<EmployeeIndexViewModel>>> GetEmployeesAsync()
    {
        var op = "GetAll";
        var requestUri = $"{_appConfiguration.GetEmployeeEndPoint()}{op}";
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

        var response = new ResponseGenerico<IEnumerable<EmployeeIndexViewModel>>();
        using var client = new HttpClient();
        var responseRequestApi = await client.SendAsync(request);
        var contentResponse = await responseRequestApi.Content.ReadAsStringAsync();
        var objResponse = JsonSerializer.Deserialize<ResultViewRequest<IEnumerable<EmployeeIndexViewModel>>>(contentResponse);

        if (responseRequestApi.IsSuccessStatusCode)
        {
            response.CodigoHttp = responseRequestApi.StatusCode;
            if (objResponse != null)
                response.DadosRetorno = objResponse.Data;
        }
        else
        {
            response.CodigoHttp = responseRequestApi.StatusCode;
            if (objResponse is { Errors: not null })
                response.ErroRetorno = JsonSerializer
                    .Deserialize<List<string>>((string)((IEnumerable)objResponse.Errors ?? new List<string>()));
        }

        return response;
    }

    public  Task<ResponseGenerico<EmployeeResponse>> GetEmployeeAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseGenerico<EmployeeResponse>> GetEmployeeByAddressAsync(string address)
    {
       var op = $"Get/{address}";
        var requestUri = $"{_appConfiguration.GetEmployeeEndPoint()}{op}";
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

        var response = new ResponseGenerico<EmployeeResponse>();
        using var client = new HttpClient();
        var responseRequestApi = await client.SendAsync(request);
        var contentResponse = await responseRequestApi.Content.ReadAsStringAsync();
        var objResponse = JsonSerializer.Deserialize<ResultViewRequest<EmployeeResponse>>(contentResponse);

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

    public async Task<ResponseGenerico<EmployeeAddedEventModel>> AddEmployeeAsync(EmployeeModel employee)
    {
        var op = "Add";
        var requestUri = $"{_appConfiguration.GetEmployeeEndPoint()}{op}";

        var model = new EmployeeAddViewModel
        {
            Address = employee.Carteira,
            Name = employee.Nome,
            TaxId = Convert.ToUInt64(employee.Pis.Replace(".", "").Replace("-", "")),
            BegginingWorkDay = (((uint)employee.InicioJornada.Hour * 100) + 
                                ((uint)employee.InicioJornada.Minute)),
            EndWorkDay = (((uint)employee.FimJornada.Hour * 100) + 
                          ((uint)employee.FimJornada.Minute)),
            EmployerAddress = employee.Empregador,
            State = 1
        };

        var response = new ResponseGenerico<EmployeeAddedEventModel>();
        using var client = new HttpClient();
        var responseRequestApi = await 
            client
                .PostAsync(
                    requestUri, 
                    new StringContent(
                        JsonSerializer.Serialize(model), 
                        Encoding.UTF8,
                         "application/json"));

        var contentResponse = await responseRequestApi.Content.ReadAsStringAsync();
        var objResponse = JsonSerializer.Deserialize<ResultViewRequest<EmployeeAddedEventModel>>(contentResponse);                         

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

    public async Task<ResponseGenerico<EmployeeUpdateViewModel>> UpdateEmployeeAsync(EmployeeUpdateModel employee)
    {
        var op = $"Update/{employee.Carteira}";
        var requestUri = $"{_appConfiguration.GetEmployeeEndPoint()}{op}";

        var model = new EmployeeAddViewModel();

        model.Name = employee.Nome;
        model.Address = employee.NovaCarteira;
        model.EmployerAddress = employee.Empregador;
        model.State = (byte)employee.Ativo;
        model.BegginingWorkDay = (((uint)employee.InicioJornada.Hour * 100) + 
                                  ((uint)employee.InicioJornada.Minute));
        model.EndWorkDay = (((uint)employee.FimJornada.Hour * 100) + 
                            ((uint)employee.FimJornada.Minute));
        var a = Convert.ToUInt64(employee.Pis.Replace(".", "").Replace("-", ""));
        model.TaxId = a;
        var response = new ResponseGenerico<EmployeeUpdateViewModel>();
        using var client = new HttpClient();
        var responseRequestApi = await
            client
                .PutAsync(
                    requestUri,
                    new StringContent(
                        JsonSerializer.Serialize(model),
                        Encoding.UTF8,
                         "application/json"));

        var contentResponse = await responseRequestApi.Content.ReadAsStringAsync();
        var objResponse = JsonSerializer.Deserialize<ResultViewRequest<EmployeeUpdateViewModel>>(contentResponse);

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

    public  Task<bool> CheckIfEmployeeExistsAsync(string address)
    {
        throw new NotImplementedException();
    }
}
