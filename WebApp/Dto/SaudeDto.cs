namespace WebApp.Dto
{
    public class SaudeDto
    {
        public string Id { get; set; }
        public required int ProfissionalId { get; set; }
        public int? Altura { get; set; }
        public int Massa { get; set; }
        public int? Envergadura { get; set; }
    }
}
