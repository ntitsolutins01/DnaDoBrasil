using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using WebApp.Dto;

namespace WebApp.Models
{
    public class QuestaoEadModel
    {
        public string AulaId { get; set; }
        public SelectList ListAulas {get; set; }
        public QuestaoEadDto QuestaoEad { get; set; }
        public List<QuestaoEadDto> QuestoesEad { get; set; }
        public SelectList ListQuestoesEad { get; set; }
        public RespostaEadDto RespostaEad { get; set; }
        public List<RespostaEadDto> RespostasEad { get; set; }
        public SelectList ListRespostasEad { get; set; }

        public class CreateUpdateQuestaoEadCommand
        {
            public int Id { get; set; }
            public int AulaId { get; set; }
            public string Referencia { get; set; }
            public string Pergunta { get; set; }
            public List<RespostaEadDto>? Respostas { get; set; }
            public required int Questao { get; set; }
        }
    }

}