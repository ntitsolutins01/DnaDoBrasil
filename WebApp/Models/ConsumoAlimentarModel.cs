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
            public int Id { get; set; }
            public required int ProfissionalId { get; set; }
            public required int AlunoId { get; set; }
            public required string Respostas { get; set; }
            public required string StatusConsumoAlimentar { get; set; }
        }
    }

}
