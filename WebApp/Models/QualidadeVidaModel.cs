using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Dto;

namespace WebApp.Models
{
    public class QualidadeVidaModel
    {
        public QualidadeVidaDto QualidadeVida { get; set; }
        public List<QualidadeVidaDto> QualidadeVidas { get; set; }
        public string QualidadeVidaId { get; set; }
        public SelectList ListQualidadeVidas { get; set; }
        
        public class CreateUpdateQualidadeVidaCommand
        {
	        public int Id { get; set; }
            public required int ProfissionalId { get; set; }
            public required int QuestionarioId { get; set; }
            public required string Resposta { get; set; }
        }
    }

}
