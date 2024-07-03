using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Dto;

namespace WebApp.Models
{
    public class LinhaAcaoModel
    {
        public LinhaAcaoDto LinhaAcao { get; set; }
        public List<LinhaAcaoDto> LinhasAcoes { get; set; }
        public string LinhaAcaoId { get; set; }
        public SelectList ListLinhasAcoes { get; set; }

        public class CreateUpdateLinhaAcaoCommand
        {
            public int Id { get; set; }
            public string? Nome { get; set; }
            public bool Status { get; set; }
        }
    }

}