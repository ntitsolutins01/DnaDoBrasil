namespace WebApp.Dto
{
    public class ConsumoAlimentarDto
    {
        public string Id { get; set; }
        public required int ProfissionalId { get; set; }
        public required int QuestionarioId { get; set; }
        public required string Resposta { get; set; }
    }
}
