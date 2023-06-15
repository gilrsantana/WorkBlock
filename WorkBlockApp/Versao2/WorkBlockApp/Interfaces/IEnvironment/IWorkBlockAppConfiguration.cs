namespace WorkBlockApp.Interfaces.IEnvironment;

public interface IWorkBlockAppConfiguration
{
    public string GetAdministratorEndPoint();
    public string GetEmployerEndPoint();
    public string GetEmployeeEndPoint();
    public string GetPontoBlockEndPoint();
    public string GetPontoBlockReportsEndPoint();
    public string GetUtilEndPoint();
}