using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PontoBlockApresentacao.Models
{
    public class QuestoesModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public List<OpcaoRespostaModel> ListaDeRespostas { get; set; }
        public string RespostaSelecionada { get; set; }

        public static string SetTitulo(int questao) 
        {
            switch (questao)
            {
                case 1:
                    return "1- Minha relação com controle de ponto é:";
                case 2:
                    return "2- Como o registro de ponto é realizado atualmente na empresa?";
                case 3:
                    return "3- Você já teve algum problema com o registro de ponto atual?";
                case 4:
                    return "4- Como você avalia a facilidade de uso do sistema de registro de ponto atual?";
                case 5:
                    return "5- Você gostaria de ter mais opções de registro de ponto (como registro por aplicativo, reconhecimento facial, etc.)?";
                case 6:
                    return "6- Você já teve algum problema com o registro de horas extras?";
                case 7:
                    return "7- Você se sente seguro com a forma que os registros de ponto são feitos atualmente?";
                case 8:
                    return "8- Você tem acesso ao seu histórico de registros de ponto com facilidade?";
                case 9:
                    return "9- Você acha que um sistema de registro de ponto baseado em blockchain poderia ser uma solução para os problemas atuais?";
                case 10:
                    return "10- Você acha que um sistema de registro de ponto baseado em blockchain poderia ser mais seguro que o atual?";
                case 11:
                    return "1- Como o registro de ponto é realizado atualmente na empresa?";
                case 12:
                    return "2- Quais são as dificuldades enfrentadas com o processo atual de registro de ponto?";
                case 13:
                    return "3- Qual é a importância do registro de ponto para a empresa?";
                case 14:
                    return "4- Existe algum problema de confiabilidade com o processo atual de registro de ponto?";
                case 15:
                    return "5- É desejável a possibilidade de consultar o registro de ponto de forma online?";
                case 16:
                    return "6- É importante que o registro de ponto seja imutável e seguro?";
                case 17:
                    return "7- Como o registro de ponto atualmente influencia nas decisões gerenciais?";
                case 18:
                    return "8- Os colaboradores tem acesso ao seu histórico de registros?";
                case 19:
                    return "9- O sistema atual já apresentou falhas que ocasionaram a perda de dados?";
                case 20:
                    return "10- Você acredita que um sistema de controle de registro de ponto baseado em blockchain possa ser mais seguro que o atual?";
                
            }
            return "";
        }
    }
}