namespace WebApp.Dto
{
    public class ControlePresencaDto
    {
		public int Id { get; set; }
		public required AlunoDto Aluno { get; init; }
		public required string Controle { get; init; }
		public string? Justificativa { get; init; }
		public bool Status { get; init; } = true;
	}
}
