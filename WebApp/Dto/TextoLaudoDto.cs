namespace WebApp.Dto
{
    public class TextoLaudoDto
    {
        public int Id { get; set; }
        public int? Idade { get; init; }
        public string? NomeTipoLaudo { get; init; }
        public string Classificacao { get; set; }
        public decimal PontoInicial { get; set; }
        public decimal PontoFinal { get; set; }
        public string Aviso { get; set; }
        public string Texto { get; set; }
        public string Sexo { get; set; }
    }
}
