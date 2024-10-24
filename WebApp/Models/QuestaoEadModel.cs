using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using WebApp.Dto;

namespace WebApp.Models
{
    public class QuestaoEadModel
    {
        public QuestaoEadDto QuestaoEad { get; set; }
        public List<QuestaoEadDto> QuestoesEad { get; set; }
        public SelectList ListQuestoesEad { get; set; }
        public RespostaEadDto RespostaEad { get; set; }
        public List<RespostaEadDto> RespostasEad { get; set; }
        public SelectList ListRespostasEas { get; set; }

        public class CreateUpdateQuestaoEadCommand
        {
            public int Id { get; set; }
            public bool Status { get; set; }
        }
    }

}