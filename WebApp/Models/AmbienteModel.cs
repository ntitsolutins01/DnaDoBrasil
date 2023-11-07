using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Dto;

namespace WebApp.Models
{
    public class AmbienteModel
    {
        public AmbienteDto Ambiente { get; set; }
        public List<AmbienteDto> Ambientes { get; set; }
        public string AmbienteId { get; set; }
        public SelectList ListAmbientes { get; set; }

        public class CreateUpdateAmbienteCommand
        {
            public string TipoLaudo { get; set; }
        }
    }

}
