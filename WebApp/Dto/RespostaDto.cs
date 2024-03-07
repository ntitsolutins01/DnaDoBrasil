namespace WebApp.Dto
{
    public class RespostaDto
    {
        public int Id { get; set; }
        public int QuestionarioId { get; set; }
        public required string Pergunta { get; set; }
        public int TipoLaudoId { get; set; }
        public required string NomeTipoLaudo { get; set; }
        public required string RespostaQuestionario { get; set; }
        public required decimal ValorPesoResposta { get; set; }
    }
}
