using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Dto;

namespace WebApp.Models
{
    public class SaudeModel
    {
        public SaudeDto Saude { get; set; }
        public List<SaudeDto> Saudes { get; set; }
        public string SaudeId { get; set; }
        public SelectList ListSaudes { get; set; }
        
        public class CreateUpdateSaudeCommand
        {
	        public int Id { get; set; }
            public required int ProfissionalId { get; set; }
            public int? Altura { get; set; }
            public int Massa { get; set; }
            public int? Envergadura { get; set; }
        }
    }

}
