using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Dto;

namespace WebApp.Models
{
    public class DashboardEadModel
    {
        public string TipoCursoId { get; set; }
        public SelectList ListTipoCursos { get; set; }
        public string LocalidadeId { get; set; }
        public SelectList ListLocalidades { get; set; }
        public string CursoId { get; set; }
        public SelectList ListCursos { get; set; }
        public string ProfessorId { get; set; }
        public SelectList ListProfessores { get; set; }
        public DashboardEadDto DashboardEad { get; set; }
        public SelectList ListDeficiencias { get; set; }
        public string DeficienciaId { get; set; }
        public SelectList ListEtnias { get; set; }
        public string EtniaId { get; set; }
        public DataGrafico DataGrafico { get; set; }
        public SelectList ListFomentos { get; set; }
        public SelectList ListEstados { get; set; }
        public SelectList ListMunicipios { get; set; }
    }

}
