

namespace WebApp.Dto
{
    public class ControleAcessoAulaDto
    {
        public int Id { get; set; }
        public required AulaDto Aula { get; set; }
        public bool IdentificacaoAluno { get; set; }
        public bool AulaRequisito { get; set; }
        public bool PermanenciaAula { get; set; }
        public TimeSpan? TempoPermanecia { get; set; }
        public required string LiberacaoAula { get; set; }
        public DateTime? DataLiberacao { get; set; }
        public DateTime DataEncerramento { get; set; }
        public bool Status { get; set; } = true;
    }
}
