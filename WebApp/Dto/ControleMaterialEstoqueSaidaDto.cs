namespace WebApp.Dto
{
    public class ControleMaterialEstoqueSaidaDto
    {
        public required int Id { get; set; }
        public required int MaterialId { get; set; }
        public required int Quantidade { get; set; }
        public String? Solicitante { get; set; }
    }
}
