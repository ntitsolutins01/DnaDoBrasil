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
            public int? Flexibilidade { get; set; }
            public int? PreensaoManual { get; set; }
            public int? Velocidade { get; set; }
            public int? ImpulsaoHorizontal { get; set; }
            public int? AptidaoFisica { get; set; }
            public int? Agilidade { get; set; }
            public bool? Abdominal { get; set; }
            public int Id { get; internal set; }

        }

    }

}
