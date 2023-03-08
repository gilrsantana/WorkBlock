using System.Data.Common;
using MySqlConnector;
using PontoBlockApresentacao.Models;

namespace PontoBlockApresentacao.Context;

public class MeuAppDb
{
    private readonly IConfiguration _configuration;

    public MeuAppDb(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void InsertResposta(RespostasModel respostas)
    {
        string connectionString = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        using (var conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            using (var cmd = new MySqlCommand("INSERT INTO RESPOSTAS (Tipo, " +
                                              "Questao1, " +
                                              "Questao2, " +
                                              "Questao3, " +
                                              "Questao4, " +
                                              "Questao5, " +
                                              "Questao6, " +
                                              "Questao7, " +
                                              "Questao8, " +
                                              "Questao9, " +
                                              "Questao10) " +
                                              "VALUES (@Tipo, " +
                                              "@Questao1, " +
                                              "@Questao2, " +
                                              "@Questao3, " +
                                              "@Questao4, " +
                                              "@Questao5, " +
                                              "@Questao6, " +
                                              "@Questao7, " +
                                              "@Questao8, " +
                                              "@Questao9, " +
                                              "@Questao10)", conn))
            {
                cmd.Parameters.AddWithValue("@Tipo", respostas.Tipo.ToString());
                cmd.Parameters.AddWithValue("@Questao1", respostas.Questao1);
                cmd.Parameters.AddWithValue("@Questao2", respostas.Questao2);
                cmd.Parameters.AddWithValue("@Questao3", respostas.Questao3);
                cmd.Parameters.AddWithValue("@Questao4", respostas.Questao4);
                cmd.Parameters.AddWithValue("@Questao5", respostas.Questao5);
                cmd.Parameters.AddWithValue("@Questao6", respostas.Questao6);
                cmd.Parameters.AddWithValue("@Questao7", respostas.Questao7);
                cmd.Parameters.AddWithValue("@Questao8", respostas.Questao8);
                cmd.Parameters.AddWithValue("@Questao9", respostas.Questao9);
                cmd.Parameters.AddWithValue("@Questao10", respostas.Questao10);

                cmd.ExecuteNonQuery();
            }
        }
    }
}