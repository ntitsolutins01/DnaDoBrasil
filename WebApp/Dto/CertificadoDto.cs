namespace WebApp.Dto
{
    public class CertificadoDto
    {
        public required int Id { get; set; }
        public required int CursoId { get; set; }
        public required byte[] ImagemFrente { get; set; }
        public byte[]? ImagemVerso { get; set; }
        public string? NomeFotoFrente { get; set; }
        public string? NomeFotoVerso { get; set; }
        public required string HtmlFrente { get; set; }
        public required string HtmlVerso { get; set; }
        public bool Status { get; set; } = true;
    }
}