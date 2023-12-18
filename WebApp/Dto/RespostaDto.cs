namespace WebApp.Dto
{
    public class RespostaDto
    {
        public int Id { get; set; }
        public string RespostaQuestionario { get; set; }
        public QuestionarioDto Questionario { get; set; }
    }
}
