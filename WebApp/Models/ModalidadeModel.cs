using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using WebApp.Dto;

namespace WebApp.Models
{
    public class ModalidadeModel
    {
        public ModalidadeDto Modalidade { get; set; }
        public List<ModalidadeDto> Modalidades { get; set; }
        public string ModalidadeId { get; set; }
        public SelectList ListModalidades { get; set; }

        public class CreateUpdateModalidadeCommand
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public bool Status { get; set; }
            public string? ModalidadesIds { get; set; }
        }
    }

}
