using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Dto;

namespace WebApp.Models
{
    public class EscolaridadeModel
    {
        public EscolaridadeDto Escolaridade { get; set; }
        public List<EscolaridadeDto> Escolaridades { get; set; }
        public string EscolaridadeId { get; set; }
        public SelectList ListEscolaridades { get; set; }

        public class CreateUpdateEscolaridadeCommand
        {
            public string Nome { get; set; }
            public string Descricao { get; set; }
            public int Id { get; set; }
        }
    }

}
