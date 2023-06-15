using System.Dynamic;
using System.Net;

namespace WorkBlockApp.DTOs;

public class ResponseGenerico<T> where T : class
{
    public HttpStatusCode CodigoHttp { get; set; }
    public T? DadosRetorno { get; set; }
    public List<string>? ErroRetorno { get; set; }
}