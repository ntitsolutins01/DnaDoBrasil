namespace WebApp.Dto
{
	public class DashboardDto
	{
		public int AvaliacoesDna { get; set; }
		public int LaudosAndamentos { get; set; }
		public int LaudosFinalizados { get; set; }
		public int CadastrosMasculinos { get; set; }
		public int CadastrosFemininos { get; set; }
		public int AlunosCadastrados { get; set; }
		public int LaudosMasculinos { get; set; }
		public int LaudosFemininos { get; set; }
		public int[]? ListPresencasAnual { get; set; }
		public int[]? ListFaltasAnual { get; set; }
        public StatusLaudosDto? StatusLaudos { get; set; }

        #region SearchFilter
        public string Sexo { get; set; }
		public string FomentoId { get; set; }
		public string Estado { get; set; }
		public string MunicipioId { get; set; }
		public string LocalidadeId { get; set; }
        public string DeficienciaId { get; set; }
        public string Etnia { get; set; }

        #endregion
	}
}
