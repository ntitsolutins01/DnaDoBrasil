namespace WebApp.Dto
{
    public class LaudoDto
    {
        public TalentoEsportivoDto TalentoEsportivo { get; set; }
        public VocacionalDto Vocacional { get; set; }
        public QualidadeVidaDto QualidadeDeVida { get; set; }
        public SaudeDto Saude { get; set; }
        public ConsumoAlimentarDto ConsumoAlimentar { get; set; }
        public SaudeBucalDto SaudeBucal { get; set; }
        public  AlunoDto Aluno { get; set; }
        public int Id { get; set; }
        public string Altura { get; set; }
        public string MassaCorporal { get; set; }
        public object Envergadura { get; set; }
        public object PreensaoManual { get; set; }
        public object Flexibilidade { get; set; }
        public object ImpulsaoHorizontal { get; set; }
        public object AptidaoFísica { get; set; }
    }
}
