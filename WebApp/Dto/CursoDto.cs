namespace WebApp.Dto
{
    public class CursoDto
    {
		public required int Id { get; set; }
		public required int TipoCursoId { get; set; }
		public required int UsuarioId { get; set; }
		public required string Titulo { get; set; }
		public required int CargaHoraria { get; set; }
		public string? Descricao { get; set; }
		public bool Status { get; set; }
		public required string TituloTipoCurso { get; set; }
		public required string NomeProfissional { get; set; }
	}
}
