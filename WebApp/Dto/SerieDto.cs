namespace WebApp.Dto
{
    public class SerieDto
    {
        public string Id { get; set; }
        public required string Nome { get; set; }
        public required string Descricao { get; set; }
        public required int IdadeInicial { get; set; }
        public required int IdadeFinal { get; set; }
        public required int ScoreTotal { get; set; }
    }
}
