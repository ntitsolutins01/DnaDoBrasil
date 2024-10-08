using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Dto;

namespace WebApp.Models
{
    public class TalentoEsportivoModel
    {
        public TalentoEsportivoDto TalentoEsportivo { get; set; }
        public List<TalentoEsportivoDto> TalentoEsportivos { get; set; }
        public string TalentoEsportivoId { get; set; }
        public SelectList ListTalentoEsportivos { get; set; }

        public class CreateUpdateTalentoEsportivoCommand
        {
            public int Id { get; internal set; }
            public int? AlunoId { get; set; }
            public int? ProfissionalId { get; set; }
            public decimal? Altura { get; set; }
            public decimal? MassaCorporal { get; set; }
            public decimal? Flexibilidade { get; set; }
            public decimal? PreensaoManual { get; set; }
            public decimal? Velocidade { get; set; }
            public decimal? ImpulsaoHorizontal { get; set; }
            public decimal? AptidaoFisica { get; set; }
            public decimal? Agilidade { get; set; }
            public bool? Abdominal { get; set; }
            public string StatusTalentosEsportivos { get; set; }
        }
    }

}
