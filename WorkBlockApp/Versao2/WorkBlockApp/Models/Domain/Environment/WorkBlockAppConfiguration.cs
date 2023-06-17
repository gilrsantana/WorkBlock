using System.Xml;
using WorkBlockApp.Interfaces.IEnvironment;

namespace WorkBlockApp.Models.Environment;

public class WorkBlockAppConfiguration : IWorkBlockAppConfiguration
{
    private readonly IConfiguration _configuration;
    public WorkBlockAppConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetAdministratorEndPoint()
    {
        var endPoint = _configuration.GetValue<string>("AdministratorApiEndPoint") ?? "";
        return $"{GetBaseApiUrl()}{endPoint}";
    }

    public string GetEmployerEndPoint()
    {
        var endPoint = _configuration.GetValue<string>("EmployerApiEndpoint") ?? "";
        return $"{GetBaseApiUrl()}{endPoint}";
    }

    public string GetEmployeeEndPoint()
    {
        var endPoint = _configuration.GetValue<string>("EmployeeApiEndPoint") ?? "";
        return $"{GetBaseApiUrl()}{endPoint}";
    }

    public string GetPontoBlockEndPoint()
    {
        var endPoint = _configuration.GetValue<string>("PontoBlockApiEndPoint") ?? "";
        return $"{GetBaseApiUrl()}{endPoint}";
    }

    public string GetPontoBlockReportsEndPoint()
    {
        var endPoint = _configuration.GetValue<string>("PontoBlockReportsApiEndPoint") ?? "";
        return $"{GetBaseApiUrl()}{endPoint}";
    }

    public string GetUtilEndPoint()
    {
        var endPoint = _configuration.GetValue<string>("UtilEndPoint") ?? "";
        return $"{GetBaseApiUrl()}{endPoint}";
    }
    
    private string GetBaseApiUrl()
    {
        return _configuration.GetValue<string>("BaseApiURL") ?? "";
    }
}