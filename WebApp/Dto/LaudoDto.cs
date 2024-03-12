namespace WebApp.Dto
{
    public class LaudoDto
    {
        public int? AlunoId { get; set; }

        public string? NomeTipoLaudo { get; set; }
        public string? LaudosFinalizados { get; set; }
        public string? LaudosAndamentos { get; set; }
        public double? Progresso { get; set; }

        public int? VocacionalId { get; set; }
        public int? QualidadeDeVidaId { get; set; }
        public int? SaudeId { get; set; }
        public int? ConsumoAlimentarId { get; set; }
        public int? SaudeBucalId { get; set; }
        public int? Id { get; set; }
        public string? NomeAluno { get; set; }
        public string? MunicipioEstado { get; set; }
        public int? LocalidadeId { get; set; }
        public string? Localidade { get; set; }
        public string? Encaminhamento { get; set; }
        public string? StatusLaudo { get; set; }
    }
}
