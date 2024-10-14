namespace WebApp.Dto
{
    public class VocacionalDto
    {
        public int Id { get; set; }
        public required ProfissionalDto Profissional { get; set; }
        public required QuestionarioDto Questionario { get; set; }
        public required string Resposta { get; set; }
    }
}
