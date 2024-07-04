namespace WebApp.Dto
{
    public class LaudoDto
    {
        public int? TalentoEsportivoId { get; init; }
        public int? VocacionalId { get; init; }
        public int? QualidadeDeVidaId { get; init; }
        public int? SaudeId { get; init; }
        public int? ConsumoAlimentarId { get; init; }
        public int? SaudeBucalId { get; init; }
        public int? AlunoId { get; init; }
        public int? Id { get; init; }
        public string? NomeAluno { get; init; }
        public string? MunicipioEstado { get; init; }
        public int? LocalidadeId { get; init; }
        public string? Localidade { get; init; }
        public string? Encaminhamento { get; init; }
        public string? StatusLaudo { get; set; }
        public string? Sexo { get; set; }
        public DateTime DtNascimento { get; set; }
    }
}
