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
            public int ProfissionalId { get; init; }
            public required int AlunoId { get; init; }
            public string[] ListQualidadeDeVida { get; set; }
        }
    }

}
