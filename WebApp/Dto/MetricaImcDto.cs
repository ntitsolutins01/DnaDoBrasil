namespace WebApp.Dto
{
    public class MetricaImcDto
    {
        public string? Sexo { get; set; }
        public int? Idade { get; set; }
        public string? Classificacao { get; set; }
        public decimal ValorInicial { get; set; }
        public decimal ValorFinal { get; set; }
        public bool Status { get; set; } = true;
        public int Id { get; set; }
    }
}
