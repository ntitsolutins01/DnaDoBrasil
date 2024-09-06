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
            public int Id { get; set; }
            public required int ProfissionalId { get; set; }
            public required int AlunoId { get; set; }
            public required string Respostas { get; set; }
            public required string StatusSaudeBucal { get; set; }
        }
    }

}
