using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Dto;

namespace WebApp.Models
{
    public class DashboardModel
    {
        public string FomentoId { get; set; }
        public SelectList ListFomentos { get; set; }
        public string LocalidadeId { get; set; }
        public SelectList ListLocalidades { get; set; }
        public string EstadoId { get; set; }
        public SelectList ListEstados { get; set; }
        public string MunicipioId { get; set; }
        public SelectList ListMunicipios { get; set; }
        public DashboardIndicadoresDto Indicadores { get; set; }
	}

}
