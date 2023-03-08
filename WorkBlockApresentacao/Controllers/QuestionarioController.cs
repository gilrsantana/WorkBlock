using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PontoBlockApresentacao.Models;
using PontoBlockApresentacao.Context;

namespace PontoBlockApresentacao.Controllers
{

    public class QuestionarioController : Controller
    {
        private readonly ILogger<QuestionarioController> _logger;
        private IConfiguration configuration;

        public QuestionarioController(ILogger<QuestionarioController> logger, IConfiguration iconfig)
        {
            _logger = logger;
            configuration = iconfig;
        }

        [HttpGet]
        public IActionResult Index(int? tipo)
        {
            TempData["tipo"] = tipo;
            if (tipo == 1)
            {
                GrupoModel grupo = new GrupoModel();
                List<QuestoesModel> questoes;
                questoes = new List<QuestoesModel>();

                for (int i = 1; i <= 10; i++)
                {
                    questoes.Add(new QuestoesModel
                    {
                        Id = i,
                        Titulo = QuestoesModel.SetTitulo(i),
                        ListaDeRespostas = OpcaoRespostaModel.SetRespostas(i)
                    });
                }
                grupo.Questoes = questoes;
                grupo.Respostas = new RespostasModel();
                return View(grupo);
            }
            else if (tipo == 2)
            {
                GrupoModel grupo = new GrupoModel();
                List<QuestoesModel> questoes;
                questoes = new List<QuestoesModel>();

                for (int i = 11; i <= 20; i++)
                {
                    questoes.Add(new QuestoesModel
                    {
                        Id = i,
                        Titulo = QuestoesModel.SetTitulo(i),
                        ListaDeRespostas = OpcaoRespostaModel.SetRespostas(i)
                    });
                }
                grupo.Questoes = questoes;
                grupo.Respostas = new RespostasModel();
                return View(grupo);
            }

            return View(new GrupoModel());
        }

        [HttpPost]
        public IActionResult Index([FromForm] RespostasModel respostas)
        {
            try
            {
                MeuAppDb banco = new MeuAppDb(configuration);
                banco.InsertResposta(respostas);
                TempData["mensagem"] = MensagemModel.Serializar("Mensagem enviada com sucesso.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                TempData["mensagem"] = MensagemModel.Serializar(e.ToString(), TipoMensagem.Erro);
            }
            
            return View("Index", new GrupoModel());
        }
        
    }
}