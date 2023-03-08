using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PontoBlockApresentacao.Models
{
    public class GrupoModel
    {
        public List<QuestoesModel> Questoes { get; set; }
        public RespostasModel Respostas { get; set; }
    }
}