namespace WebApp.Dto
{
    public class QuestaoEadDto
    {
        public int Id { get; set; }
        public required string Pergunta { get; set; }
        public List<RespostaEadDto>? Respostas { get; set; }
        public required int Quadrante { get; set; }
        public required int Questao { get; set; }
    }
}
