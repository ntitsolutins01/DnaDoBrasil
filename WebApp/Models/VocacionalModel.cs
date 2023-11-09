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
            public required int ProfissionalId { get; set; }
            public required int QuestionarioId { get; set; }
            public required string Resposta { get; set; }
            public int Id { get; set; }
        }
    }

}
