namespace WebApp.Dto
{
    public class VocacionalDto
    {
        public int Id { get; set; }
        public ProfissionalDto Profissional { get; set; }
        public QuestionarioDto Questionario { get; set; }
        public string Resposta { get; set; }
    }
}
