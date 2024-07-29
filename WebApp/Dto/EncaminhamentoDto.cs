namespace WebApp.Dto
{
    public class EncaminhamentoDto
    {
        public int Id { get; set; }
        public required int TipoLaudoId{ get; set; }
        public required string Nome { get; set; }
        public required string Parametro { get; set; }
        public string? Descricao { get; set; }
        public bool Status { get; set; } = true;
    }
}
