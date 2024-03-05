namespace WebApp.Dto
{
    public class QualidadeVidaDto
    {
        public int Id { get; set; }
        public int ProfissionalId { get; set; }
        public string? NomeProfissional { get; set; }
        public int AlunoId { get; set; }
        public string? NomeAluno { get; set; }
        public required RespostaDto Resposta { get; set; }
    }
}
