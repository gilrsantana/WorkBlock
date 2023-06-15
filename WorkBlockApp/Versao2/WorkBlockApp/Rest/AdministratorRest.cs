using System.Collections;
using System.Dynamic;
using System.Text;
using System.Text.Json;
using WorkBlockApp.DTOs;
using WorkBlockApp.Interfaces.IEnvironment;
using WorkBlockApp.Interfaces.IRest;
using WorkBlockApp.Models.Domain;
using WorkBlockApp.Models.Event;
using WorkBlockApp.ViewModels.VMAdministrator;

namespace WorkBlockApp.Rest;

public class AdministratorRest : IAdministratorRest
{
    private readonly IWorkBlockAppConfiguration _appConfiguration;

    public AdministratorRest(IWorkBlockAppConfiguration appConfiguration)
    {
        _appConfiguration = appConfiguration;
    }

    public async Task<ResponseGenerico<AdministratorModel>> GetById(int id)
    {
        var op = $"Get/{id}";
        var requestUri = $"{_appConfiguration.GetAdministratorEndPoint()}{op}";
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

        var response = new ResponseGenerico<AdministratorModel>();
        using var client = new HttpClient();
        var responseRequestApi = await client.SendAsync(request);
        var contentResponse = await responseRequestApi.Content.ReadAsStringAsync();
        var objResponse = JsonSerializer.Deserialize<ResultViewRequest<AdministratorModel>>(contentResponse);

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

    public async Task<ResponseGenerico<AdminAddedEventModel>> Add(AdministratorModel administrator)
    {
        var op = "Add";
        var requestUri = $"{_appConfiguration.GetAdministratorEndPoint()}{op}";

        var model = new AdminAddViewModel
        {
            Address = administrator.Carteira,
            Name = administrator.Nome,
            TaxId = Convert.ToUInt64(administrator.Cpf.Replace(".", "").Replace("-", "")),
            State = 1
        };
        var response = new ResponseGenerico<AdminAddedEventModel>();
        using var client = new HttpClient();
        var responseRequestApi = await 
            client.PostAsync(
                requestUri,
                new StringContent(
                    JsonSerializer.Serialize(model), 
                    Encoding.UTF8, 
                    "application/json"));

        var contentResponse = await responseRequestApi.Content.ReadAsStringAsync();
        var objResponse = JsonSerializer.Deserialize<ResultViewRequest<AdminAddedEventModel>>(contentResponse);

        if (objResponse == null) return response;
        
        response.CodigoHttp = responseRequestApi.StatusCode;
        if (responseRequestApi.IsSuccessStatusCode) 
            response.DadosRetorno = objResponse.Data;
        else
        {
            if (objResponse.Errors != null)
                response.ErroRetorno = JsonSerializer
                    .Deserialize<List<string>>((string)((IEnumerable)objResponse.Errors ?? new List<string>()));
            else response.ErroRetorno =
                JsonSerializer.Deserialize<List<string>>((string)(objResponse.Errors as IEnumerable ??
                                                                  new List<string>()));
        }
        
        return response;
    }

    public async Task<ResponseGenerico<AdministratorUpdateViewModel>> Update(AdministratorUpdateModel administrator)
    {
        var op = $"Update/{administrator.Carteira}";
        var requestUri = $"{_appConfiguration.GetAdministratorEndPoint()}{op}";

        var model = new AdminAddViewModel
        {
            Address = administrator.NovaCarteira,
            Name = administrator.Nome,
            TaxId = Convert.ToUInt64(administrator.Cpf.Replace(".", "").Replace("-", "")),
            State = (byte)administrator.Ativo
        };
        var response = new ResponseGenerico<AdministratorUpdateViewModel>();
        using var client = new HttpClient();
        var responseRequestApi = await 
            client.PutAsync(
                requestUri,
                new StringContent(
                    JsonSerializer.Serialize(model), 
                    Encoding.UTF8, 
                    "application/json"));

        var contentResponse = await responseRequestApi.Content.ReadAsStringAsync();
        var objResponse = JsonSerializer.Deserialize<ResultViewRequest<AdministratorUpdateViewModel>>(contentResponse);

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
                response.ErroRetorno = JsonSerializer.Deserialize<List<string>>((string)(objResponse.Errors as IEnumerable ?? new List<string>()));
        }
        
        return response;
    }

    public async Task<ResponseGenerico<List<AdministratorModel>>> GetAll()
    {
        var op = "GetAll";
        var requestUri = $"{_appConfiguration.GetAdministratorEndPoint()}{op}";
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

        var response = new ResponseGenerico<List<AdministratorModel>>();
        using var client = new HttpClient();
        var responseRequestApi = await client.SendAsync(request);
        var contentResponse = await responseRequestApi.Content.ReadAsStringAsync();
        var objResponse = JsonSerializer.Deserialize<ResultViewRequest<List<AdministratorModel>>>(contentResponse);

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
}