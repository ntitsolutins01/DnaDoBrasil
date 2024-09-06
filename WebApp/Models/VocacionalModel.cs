using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Dto;

namespace WebApp.Models
{
    public class VocacionalModel
    {
        public VocacionalDto Vocacional { get; set; }
        public List<VocacionalDto> Vocacionals { get; set; }
        public string VocacionalId { get; set; }
        public SelectList ListVocacionals { get; set; }

        public class CreateUpdateVocacionalCommand
        {
            public int Id { get; set; }
            public required int ProfissionalId { get; set; }
            public required int AlunoId { get; set; }
            public required string Respostas { get; set; }
            public required string StatusVocacional { get; set; }
        }
    }

}
