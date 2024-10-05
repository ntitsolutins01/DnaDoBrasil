namespace WebApp.Dto
{
    public class LaudoDto
    {
        public int Id { get; init; }

        #region Ids

        public int? TalentoEsportivoId { get; init; }
        public int? VocacionalId { get; init; }
        public int? QualidadeDeVidaId { get; init; }
        public int? SaudeId { get; init; }
        public int? ConsumoAlimentarId { get; init; }
        public int? SaudeBucalId { get; init; }
        public int? LocalidadeId { get; init; }
        public int? AlunoId { get; init; }
        public int? EncaminhamentoVocacionalId { get; init; }
        //public int? EncaminhamentoQualidadeVidaId { get; init; }
        public int? EncaminhamentoConsumoAlimentarId { get; init; }
        public int? EncaminhamentoSaudeBucalId { get; init; }
        public int? EncaminhamentoTalentoEsportivoId { get; init; }

        #endregion

        #region Cabeçalho

        public required string NomeAluno { get; init; }
        public required string NomeLocalidade { get; init; }
        public string? MunicipioEstado { get; init; }
        public string? Sexo { get; init; }
        public string? StatusLaudo { get; init; }
        public DateTime? DtNascimento { get; init; }
        public int? Idade { get; init; }
        public string? Email { get; init; }
        public byte[]? QrCode { get; init; }
        public decimal? Estatura { get; init; }
        public decimal? Massa { get; init; }
        public byte[]? ByteImage { get; init; }
        public string? NomeFoto { get; init; }
        public string? Modalidade { get; init; }
        //public string? Serie { get; init; }
        //public string? Turma { get; init; }
        //public int? MunicipioId { get; init; }
        //public string? NomeMunicipio { get; init; }

        #endregion

        #region Saude

        public string? ImcSaude { get; init; }

        #endregion
    }
}
