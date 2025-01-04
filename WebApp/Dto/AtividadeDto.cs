namespace WebApp.Dto
{
    public class AtividadeDto
    {
		public required int Id { get; set; }
        public required int EstruturaId { get; set; }
        public required string NomeEstrutura { get; set; }
        public required int LinhaAcaoId { get; set; }
        public required string NomeLinhaAcao { get; set; }
        public required int CategoriaId { get; set; }
        public required string NomeCategoria { get; set; }
        public required int ModalidadeId { get; set; }
        public required string NomeModalidade { get; set; }
        public string? Turma { get; set; }
        public string? DiaSemana { get; set; }
        public TimeSpan? HrInicial { get; set; }
        public TimeSpan? HrFinal { get; set; }
        public required int ProfissionalId { get; set; }
        public required string NomeProfissional { get; set; }
        public required int LocalidadeId { get; set; }
        public required string NomeLocalidade { get; set; }
        public bool Status { get; set; }
    }
}
