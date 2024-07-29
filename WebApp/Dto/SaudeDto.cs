namespace WebApp.Dto
{
    public class SaudeDto
    {
        public string Id { get; set; }
        public int ProfissionalId { get; set; }
        public string NomeProfissional { get; set; }
        public int? Altura { get; set; }
        public int Massa { get; set; }
        public int? Envergadura { get; set; }
        public string? DataRealizacaoTeste { get; set; }
        public string? Imc { get; set; }
    }
}
