namespace WebApp.Dto
{
    public class LaudoDto
    {
        public int? Id { get; set; }
        public int? TalentoEsportivoId { get; set; }
        public int? VocacionalId { get; set; }
        public int? QualidadeDeVidaId { get; set; }
        public int? SaudeId { get; set; }
        public int? ConsumoAlimentarId { get; set; }
        public int? SaudeBucalId { get; set; }
        public int? AlunoId { get; set; }
        public required string NomeAluno { get; set; }
        public int? LocalidadeId { get; set; }
        public required string NomeLocalidade { get; set; }
        public string? MunicipioEstado { get; set; }
        public string? Sexo { get; set; }
        public string? StatusLaudo { get; set; }
        public DateTime? DtNascimento { get; set; }
    }
}
