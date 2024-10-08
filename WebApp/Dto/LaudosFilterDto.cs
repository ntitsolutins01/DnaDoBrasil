namespace WebApp.Dto
{
	public class LaudosFilterDto
    {

		#region SearchFilter
		public string FomentoId { get; set; }
		public string Estado { get; set; }
		public string MunicipioId { get; set; }
		public string LocalidadeId { get; set; }
        public string TipoLaudoId { get; set; }

        #endregion

        public PaginatedListDto<LaudoDto>? Laudos { get; set; }
    }
}
