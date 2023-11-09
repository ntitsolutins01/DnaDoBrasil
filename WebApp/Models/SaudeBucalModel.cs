using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Dto;

namespace WebApp.Models
{
    public class SaudeBucalModel
    {
        public SaudeBucalDto SaudeBucal { get; set; }
        public List<SaudeBucalDto> SaudeBucals { get; set; }
        public string SaudeBucalId { get; set; }
        public SelectList ListSaudeBucals { get; set; }

        public class CreateUpdateSaudeBucalCommand
        {
            public required int ProfissionalId { get; set; }
            public required int QuestionarioId { get; set; }
            public required string Resposta { get; set; }
            public int Id { get; set; }
        }
    }

}
