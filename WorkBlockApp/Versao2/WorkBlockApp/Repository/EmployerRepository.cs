using WorkBlockApp.DTOs;
using WorkBlockApp.Interfaces.IEnvironment;
using WorkBlockApp.Interfaces.IRepository;
using WorkBlockApp.Models.Domain;
using WorkBlockApp.Models.Domain.Event;
using System.Text.Json;
using System.Collections;
using WorkBlockApp.ViewModels.VMEmployer;
using System.Text;

namespace WorkBlockApp.Repository;

public class EmployerRepository : IEmployerRepository
{
    private readonly IWorkBlockAppConfiguration _appConfiguration;

    public EmployerRepository(IWorkBlockAppConfiguration appConfiguration)
    {
        _appConfiguration = appConfiguration;
    }
    public async Task<ResponseGenerico<IEnumerable<EmployerModel>>> GetEmployersAsync()
    {
        var op = "GetAll";
        var requestUri = $"{_appConfiguration.GetEmployerEndPoint()}{op}";
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

        var response = new ResponseGenerico<IEnumerable<EmployerModel>>();
        using var client = new HttpClient();
        var responseRequestApi = await client.SendAsync(request);
        var contentResponse = await responseRequestApi.Content.ReadAsStringAsync();
        var objResponse = JsonSerializer.Deserialize<ResultViewRequest<IEnumerable<EmployerModel>>>(contentResponse);

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

    public async Task<ResponseGenerico<EmployerModel>> GetEmployerAsync(int id)
    {
        var op = $"Get/{id}";
        var requestUri = $"{_appConfiguration.GetEmployerEndPoint()}{op}";
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

        var response = new ResponseGenerico<EmployerModel>();
        using var client = new HttpClient();
        var responseRequestApi = await client.SendAsync(request);
        var contentResponse = await responseRequestApi.Content.ReadAsStringAsync();
        var objResponse = JsonSerializer.Deserialize<ResultViewRequest<EmployerModel>>(contentResponse);

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

    public async Task<ResponseGenerico<EmployerModel>> GetEmployerByAddressAsync(string address)
    {
        var op = $"Get/{address}";
        var requestUri = $"{_appConfiguration.GetEmployerEndPoint()}{op}";
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

        var response = new ResponseGenerico<EmployerModel>();
        using var client = new HttpClient();
        var responseRequestApi = await client.SendAsync(request);
        var contentResponse = await responseRequestApi.Content.ReadAsStringAsync();
        var objResponse = JsonSerializer.Deserialize<ResultViewRequest<EmployerModel>>(contentResponse);

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

    public async Task<ResponseGenerico<EmployerAddedEventModel>> AddEmployerAsync(EmployerModel employer)
    {
        var op = "Add";
        var requestUri = $"{_appConfiguration.GetEmployerEndPoint()}{op}";

        var model = new EmployerAddViewModel
        {
            Address = employer.Carteira,
            Name = employer.Nome,
            TaxId = Convert.ToUInt64(employer.Cnpj.Replace(".", "").Replace("/", "").Replace("-", "")),
            LegalAddress = employer.Endereco
        };
        var response = new ResponseGenerico<EmployerAddedEventModel>();
        using var client = new HttpClient();
        var responseRequestApi = await 
            client
                .PostAsJsonAsync(
                    requestUri, 
                    new StringContent(
                        JsonSerializer.Serialize(model), 
                        Encoding.UTF8,
                         "application/json"));

        var contentResponse = await responseRequestApi.Content.ReadAsStringAsync();
        var objResponse = JsonSerializer.Deserialize<ResultViewRequest<EmployerAddedEventModel>>(contentResponse);                         

        if (objResponse == null) return response;

        response.CodigoHttp = responseRequestApi.StatusCode;
        if (responseRequestApi.IsSuccessStatusCode)
            response.DadosRetorno = objResponse.Data;
        else
        {
            if (objResponse.Errors != null)
                response.ErroRetorno = JsonSerializer
                    .Deserialize<List<string>>((string)((IEnumerable)objResponse.Errors ?? new List<string>()));
            else    
                response.ErroRetorno = 
                    JsonSerializer.Deserialize<List<string>>((string)(objResponse.Errors as IEnumerable ?? 
                    new List<string>()));
        }
        return response;
    }

    public async Task<ResponseGenerico<EmployerUpdateViewModel>> UpdateEmployerAsync(EmployerUpdateModel employer)
    {
        var op = $"Update/{employer.Carteira}";
        var requestUri = $"{_appConfiguration.GetEmployerEndPoint()}{op}";

        var model = new EmployerAddViewModel
        {
            Address = employer.NovaCarteira,
            Name = employer.Nome,
            TaxId = Convert.ToUInt64(employer.Cnpj.Replace(".", "").Replace("/", "").Replace("-", "")),
            LegalAddress = employer.Endereco
        };

        var response = new ResponseGenerico<EmployerUpdateViewModel>();
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
        var objResponse = JsonSerializer.Deserialize<ResultViewRequest<EmployerUpdateViewModel>>(contentResponse);

        if (objResponse == null) return response;

        response.CodigoHttp = responseRequestApi.StatusCode;
        if (responseRequestApi.IsSuccessStatusCode)
            response.DadosRetorno = objResponse.Data;
        else
        {
            if (objResponse.Errors != null)
                response.ErroRetorno = JsonSerializer
                    .Deserialize<List<string>>((string)((IEnumerable)objResponse.Errors ?? new List<string>()));
            else
                response.ErroRetorno =
                    JsonSerializer.Deserialize<List<string>>((string)(objResponse.Errors as IEnumerable ??
                    new List<string>()));
        }

        return response;
    }
    
    public async Task<bool> CheckIfEmployerExistsAsync(string address)
    {
        var op = $"Check/{address}";
        var requestUri = $"{_appConfiguration.GetEmployerEndPoint()}{op}";
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

        var response = new ResponseGenerico<EmployerModel>();
        using var client = new HttpClient();
        var responseRequestApi = await client.SendAsync(request);
        var contentResponse = await responseRequestApi.Content.ReadAsStringAsync();
        var objResponse = JsonSerializer.Deserialize<ResultViewRequest<EmployerModel>>(contentResponse);

        if (responseRequestApi.IsSuccessStatusCode)
        {
            response.CodigoHttp = responseRequestApi.StatusCode;
            if (objResponse != null && objResponse.Data != null)
            {
                response.DadosRetorno = objResponse.Data;
                return true;    
            }
        }
        else
        {
            response.CodigoHttp = responseRequestApi.StatusCode;
            if (objResponse is { Errors: not null })
                response.ErroRetorno = JsonSerializer
                    .Deserialize<List<string>>((string)((IEnumerable)objResponse.Errors ?? new List<string>()));
        }

        return false;
    }
}
