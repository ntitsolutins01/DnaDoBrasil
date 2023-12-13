namespace WebApp.Dto
{
    public class ContratoDto
    {
        public string Id { get; set; }
        public required string Nome { get; set; }
        public required string? Descricao { get; set; }
        public required DateTime DtIni { get; set; }
        public required DateTime DtFim { get; set; }
        public bool Status { get; set; } = true;
		public string? Anexo { get; set; }

    }
}
