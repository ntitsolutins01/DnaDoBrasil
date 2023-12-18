using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Dto;

namespace WebApp.Models
{
    public class ContratoModel
    {
        public ContratoDto Contrato { get; set; }
        public List<ContratoDto> Contratos { get; set; }
        public string ContratoId { get; set; }
        public SelectList ListContratos { get; set; }

        public class CreateUpdateContratoCommand
        {
            public int Id { get; set; }
            public required string Nome { get; set; }
            public required string? Descricao { get; set; }
            public required DateTime DtIni { get; set; }
            public required DateTime DtFim { get; set; }
            public string? Anexo { get; set; }
            public bool Status { get; set; }
        }
    }

}
