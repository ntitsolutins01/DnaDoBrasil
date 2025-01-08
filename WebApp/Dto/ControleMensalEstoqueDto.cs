namespace WebApp.Dto
{
    public class ControleMensalEstoqueDto
    {
        public required int Id { get; set; }
        public required int MaterialId { get; set; }
        public int? QtdPrevista { get; set; }
        public DateTime? DataMesSaida { get; set; }
        public int? TotalSaidas { get; set; }
        public int? TotalEstoque { get; set; }
        public int? QtdMateriaisDanificadosExtraviados { get; set; }
        public string? JustificativaDanificadosExtraviados { get; set; }
        public DateTime? DataDanificadosExtraviados { get; set; }
    }
}
