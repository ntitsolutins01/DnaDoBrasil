namespace WebApp.Dto
{
    public class MaterialDto
    {
        public required int Id { get; set; }
        public required int TipoMaterialId { get; set; }
        public required String UnidadeMedida { get; set; }
        public String? Descricao { get; set; }
        public int? QtdAdquirida { get; set; }
    }
}
