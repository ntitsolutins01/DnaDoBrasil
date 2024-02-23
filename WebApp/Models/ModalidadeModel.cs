using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using WebApp.Dto;

namespace WebApp.Models
{
    public class ModalidadeModel
    {
        public AmbienteDto Ambiente { get; set; }
        public List<AmbienteDto> Ambientes { get; set; }
        public string AmbienteId { get; set; }
        public SelectList ListAmbientes { get; set; }

        public class CreateUpdateModalidadeCommand
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public bool Status { get; set; }
            public string? AmbientesIds { get; set; }
        }
    }

}
