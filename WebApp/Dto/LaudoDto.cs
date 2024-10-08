namespace WebApp.Dto
{
    public class LaudoDto
    {
        public int Id { get; set; }

        #region Ids

        public int? TalentoEsportivoId { get; set; }
        public int? VocacionalId { get; set; }
        public int QualidadeDeVidaId { get; set; }
        public int? SaudeId { get; set; }
        public int? ConsumoAlimentarId { get; set; }
        public int? SaudeBucalId { get; set; }
        public int? LocalidadeId { get; set; }
        public int? AlunoId { get; set; }
        public int? EncaminhamentoVocacionalId { get; set; }
        //public int? EncaminhamentoQualidadeVidaId { get; set; }
        public int? EncaminhamentoConsumoAlimentarId { get; set; }
        public int? EncaminhamentoSaudeBucalId { get; set; }
        public int? EncaminhamentoTalentoEsportivoId { get; set; }

        #endregion

        #region Cabeçalho

        public required string NomeAluno { get; set; }
        public required string NomeLocalidade { get; set; }
        public string? MunicipioEstado { get; set; }
        public string? Sexo { get; set; }
        public string? StatusLaudo { get; set; }
        public DateTime? DtNascimento { get; set; }
        public int? Idade { get; set; }
        public string? Email { get; set; }
        public byte[]? QrCode { get; set; }
        public decimal? Estatura { get; set; }
        public decimal? Massa { get; set; }
        public byte[]? ByteImage { get; set; }
        public string? NomeFoto { get; set; }
        public string? Modalidade { get; set; }
        //public string? Serie { get; set; }
        //public string? Turma { get; set; }
        //public int? MunicipioId { get; set; }
        //public string? NomeMunicipio { get; set; }

        #endregion

        #region Saude

        public string? ImcSaude { get; set; }

        #endregion
    }
}
