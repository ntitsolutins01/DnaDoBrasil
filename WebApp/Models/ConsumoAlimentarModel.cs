using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Dto;

namespace WebApp.Models
{
    public class ConsumoAlimentarModel
    {
        public ConsumoAlimentarDto ConsumoAlimentar { get; set; }
        public List<ConsumoAlimentarDto> ConsumoAlimentars { get; set; }
        public string ConsumoAlimentarId { get; set; }
        public SelectList ListConsumoAlimentars { get; set; }

        public class CreateUpdateConsumoAlimentarCommand
        {
            public required int ProfissionalId { get; set; }
            public required int QuestionarioId { get; set; }
            public required string Resposta { get; set; }
            public int Id { get; set; }
        }
    }

}
