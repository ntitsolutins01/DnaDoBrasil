namespace WebApp.Dto
{
    public class QuestaoEadDto
    {
        public int Id { get; init; }
        public required string Pergunta { get; init; }
        public List<RespostaEadDto>? Respostas { get; init; }
        public required int Quadrante { get; init; }
        public required int Questao { get; init; }
    }
}
