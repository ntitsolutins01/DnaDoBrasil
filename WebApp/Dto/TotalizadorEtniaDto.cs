namespace WebApp.Dto
{
    public class TotalizadorEtniaDto
    {
        public Dictionary<string, decimal>? PercTotalizadorEtniaMasculino { get; set; }
        public Dictionary<string, decimal>? PercTotalizadorEtniaFeminino { get; set; }
        public Dictionary<string, decimal>? ValorTotalizadorEtniaMasculino { get; set; }
        public Dictionary<string, decimal>? ValorTotalizadorEtniaFeminino { get; set; }
        public Dictionary<string, decimal>? PercEtnia { get; set; }
    }
}
