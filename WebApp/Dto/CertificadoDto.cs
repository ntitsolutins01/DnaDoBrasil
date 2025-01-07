namespace WebApp.Dto
{
    public class CertificadoDto
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public byte[] ImagemFrente { get; set; }
        public byte[]? ImagemVerso { get; set; }
        public string HtmlFrente { get; set; }
        public string HtmlVerso { get; set; }
        public bool Status { get; set; } = true;
    }
}