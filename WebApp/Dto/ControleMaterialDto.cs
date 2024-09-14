namespace WebApp.Dto
{
    public class ControleMaterialDto
    {
        public required int Id { get; set; }
        public required string NomeLinhaAcao { get; set; }
        public required string Descricao { get; set; }
        public required string UnidadeMedida { get; set; }
        public required int Quantidade { get; set; }
        public int? Saida { get; set; }
        public int? Disponivel { get; set; }
        public bool Status { get; set; }
    }
}
