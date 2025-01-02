namespace WebApp.Dto
{
    public class ControleMaterialEstoqueSaidaDto
    {
        public required int Id { get; set; }
        public required int MaterialId { get; set; }
        public required string TituloMaterial { get; set; }
        public required int Quantidade { get; set; }
        public string? Solicitante { get; set; }
    }
}
