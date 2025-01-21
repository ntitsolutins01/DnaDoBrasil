using WebApp.Models;

namespace WebApp.Dto
{
	public class DashboardEadDto
	{
        #region SearchFilter
        public string Sexo { get; set; }
		public string FomentoId { get; set; }
		public string Estado { get; set; }
		public string MunicipioId { get; set; }
		public string LocalidadeId { get; set; }
        public string TipoCursoId { get; set; }
        public string CursoId { get; set; }

        #endregion
	}
}
