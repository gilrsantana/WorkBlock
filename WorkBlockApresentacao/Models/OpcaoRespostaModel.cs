using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PontoBlockApresentacao.Models
{
    public class OpcaoRespostaModel
    {
        public string Id { get; set; }
        public string Valor { get; set; }   

        public static List<OpcaoRespostaModel> SetRespostas(int questao) 
        {
            List<OpcaoRespostaModel> lista;
            switch (questao)
            {
                case 1: 
                    lista = new List<OpcaoRespostaModel>
                    {
                        new OpcaoRespostaModel{Id="111", Valor="Nunca utilizei"},
                        new OpcaoRespostaModel{Id="112", Valor="Já utilizei"},
                        new OpcaoRespostaModel{Id="113", Valor="Utilizo atualmente"},
                        new OpcaoRespostaModel{Id="114", Valor="Não sei do que se trata"}
                    };
                    return lista;

                case 2: 
                    lista = new List<OpcaoRespostaModel>
                    {
                        new OpcaoRespostaModel{Id="121", Valor="Folha de papel"},
                        new OpcaoRespostaModel{Id="122", Valor="Planilha eletrônica"},
                        new OpcaoRespostaModel{Id="123", Valor="Sistema de ponto eletrônico"},
                        new OpcaoRespostaModel{Id="124", Valor="Não utiliza nenhum meio de registro de ponto"}
                    };
                    return lista;
                case 3: 
                    lista = new List<OpcaoRespostaModel>
                    {
                        new OpcaoRespostaModel{Id="131", Valor="Não"},
                        new OpcaoRespostaModel{Id="132", Valor="Sim, mas não é frequente"},
                        new OpcaoRespostaModel{Id="133", Valor="Sim, e é frequente"},
                    };
                    return lista;
                case 4: 
                    lista = new List<OpcaoRespostaModel>
                    {
                        new OpcaoRespostaModel{Id="141", Valor="Muito fácil"},
                        new OpcaoRespostaModel{Id="142", Valor="Fácil"},
                        new OpcaoRespostaModel{Id="143", Valor="Regular"},
                        new OpcaoRespostaModel{Id="144", Valor="Difícil"},
                        new OpcaoRespostaModel{Id="145", Valor="Muito difícil"}
                    };
                    return lista;
                case 5: 
                    lista = new List<OpcaoRespostaModel>
                    {
                        new OpcaoRespostaModel{Id="151", Valor="Sim"},
                        new OpcaoRespostaModel{Id="152", Valor="Não"}
                    };
                    return lista;
                case 6: 
                    lista = new List<OpcaoRespostaModel>
                    {
                        new OpcaoRespostaModel{Id="161", Valor="Não"},
                        new OpcaoRespostaModel{Id="162", Valor="Sim, mas não é frequente"},
                        new OpcaoRespostaModel{Id="163", Valor="Sim, e é frequente"}
                    };
                    return lista;
                case 7: 
                    lista = new List<OpcaoRespostaModel>
                    {
                        new OpcaoRespostaModel{Id="171", Valor="Sim, eu confio plenamente"},
                        new OpcaoRespostaModel{Id="172", Valor="Sim, eu confio parcialmente"},
                        new OpcaoRespostaModel{Id="173", Valor="Eu não tenho confiança"}
                    };
                    return lista;
                case 8: 
                    lista = new List<OpcaoRespostaModel>
                    {
                        new OpcaoRespostaModel{Id="181", Valor="Sim, eu tenho acesso direto"},
                        new OpcaoRespostaModel{Id="182", Valor="Sim, mediante solicitação ao responsável"},
                        new OpcaoRespostaModel{Id="183", Valor="Não tenho acesso"}
                    };
                    return lista;
                case 9: 
                    lista = new List<OpcaoRespostaModel>
                    {
                        new OpcaoRespostaModel{Id="191", Valor="Sim"},
                        new OpcaoRespostaModel{Id="192", Valor="Não"},
                        new OpcaoRespostaModel{Id="193", Valor="Não tenho certeza"}
                    };
                    return lista;
                case 10: 
                    lista = new List<OpcaoRespostaModel>
                    {
                        new OpcaoRespostaModel{Id="1A1", Valor="Sim"},
                        new OpcaoRespostaModel{Id="1A2", Valor="Não"},
                        new OpcaoRespostaModel{Id="1A3", Valor="Não tenho certeza"}
                    };
                    return lista;
                case 11: 
                    lista = new List<OpcaoRespostaModel>
                    {
                        new OpcaoRespostaModel{Id="211", Valor="Folha de papel"},
                        new OpcaoRespostaModel{Id="212", Valor="Planilha eletrônica"},
                        new OpcaoRespostaModel{Id="213", Valor="Sistema de ponto eletrônico"},
                        new OpcaoRespostaModel{Id="214", Valor="Não utiliza nenhum meio de registro de ponto"}
                    };
                    return lista;
                case 12: 
                    lista = new List<OpcaoRespostaModel>
                    {
                        new OpcaoRespostaModel{Id="221", Valor="Falta de confiabilidade"},
                        new OpcaoRespostaModel{Id="222", Valor="Dificuldade de acesso à informação"},
                        new OpcaoRespostaModel{Id="223", Valor="Processo demorado"},
                        new OpcaoRespostaModel{Id="224", Valor="O sistema atual nos atende plenamente"}
                    };
                    return lista;
                case 13: 
                    lista = new List<OpcaoRespostaModel>
                    {
                        new OpcaoRespostaModel{Id="231", Valor="Controle de jornada de trabalho"},
                        new OpcaoRespostaModel{Id="232", Valor="Cálculo de pagamentos e férias"},
                        new OpcaoRespostaModel{Id="233", Valor="Tomada de decisões gerenciais"}
                    };
                    return lista;
                case 14: 
                    lista = new List<OpcaoRespostaModel>
                    {
                        new OpcaoRespostaModel{Id="241", Valor="Sim"},
                        new OpcaoRespostaModel{Id="242", Valor="Não"},
                        new OpcaoRespostaModel{Id="243", Valor="Não sei informar"}
                    };
                    return lista;
                case 15: 
                    lista = new List<OpcaoRespostaModel>
                    {
                        new OpcaoRespostaModel{Id="251", Valor="Sim"},
                        new OpcaoRespostaModel{Id="252", Valor="Não"},
                        new OpcaoRespostaModel{Id="253", Valor="Não sei informar"}
                    };
                    return lista;
                case 16: 
                    lista = new List<OpcaoRespostaModel>
                    {
                        new OpcaoRespostaModel{Id="261", Valor="Sim"},
                        new OpcaoRespostaModel{Id="262", Valor="Não"},
                        new OpcaoRespostaModel{Id="263", Valor="Não sei informar"}
                    };
                    return lista;
                case 17: 
                    lista = new List<OpcaoRespostaModel>
                    {
                        new OpcaoRespostaModel{Id="271", Valor="Muito"},
                        new OpcaoRespostaModel{Id="272", Valor="Pouco"},
                        new OpcaoRespostaModel{Id="273", Valor="Não influencia"},
                        new OpcaoRespostaModel{Id="274", Valor="Não sei informar"}
                    };
                    return lista;
                case 18: 
                    lista = new List<OpcaoRespostaModel>
                    {
                        new OpcaoRespostaModel{Id="281", Valor="Sim, através de comprovante de registro de ponto"},
                        new OpcaoRespostaModel{Id="282", Valor="Sim, mediante solicitação ao responsável"},
                        new OpcaoRespostaModel{Id="283", Valor="Sim, através da aplicação de registro de ponto"},
                        new OpcaoRespostaModel{Id="284", Valor="Sim, através de solicitação ao responsável"},
                        new OpcaoRespostaModel{Id="284", Valor="Não tem acesso"}
                    };
                    return lista;
                case 19:
                    lista = new List<OpcaoRespostaModel>
                    {
                        new OpcaoRespostaModel{Id="291", Valor="Sim, mas não é frequente" },
                        new OpcaoRespostaModel{Id="292", Valor="Sim, e é frequente" },
                        new OpcaoRespostaModel{Id="293", Valor="Não apresentou falhas que ocasionaram perda de dados" }
                    };
                    return lista;
                case 20: 
                    lista = new List<OpcaoRespostaModel>
                    {
                        new OpcaoRespostaModel{Id="2A1", Valor="Sim"},
                        new OpcaoRespostaModel{Id="2A2", Valor="Não"},
                        new OpcaoRespostaModel{Id="2A3", Valor="Não sei informar"}
                    };
                    return lista;
            }

            return new List<OpcaoRespostaModel>();
        }
    }
}