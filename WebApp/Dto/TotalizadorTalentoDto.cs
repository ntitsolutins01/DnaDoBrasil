namespace WebApp.Dto
{
    public class TotalizadorTalentoDto
    {
        public Dictionary<string, decimal>? PercTotalizadorTalentoMasculino { get; set; }
        public Dictionary<string, decimal>? PercTotalizadorTalentoFeminino { get; set; }
        public Dictionary<string, decimal>? ValorTotalizadorTalentoMasculino { get; set; }
        public Dictionary<string, decimal>? ValorTotalizadorTalentoFeminino { get; set; }
        public Dictionary<string, decimal>? PercTalento { get; set; }
    }
}
