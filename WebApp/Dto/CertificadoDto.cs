namespace WebApp.Dto
{
    public class CertificadoDto
    {
        public required int Id { get; set; }
        public required int CursoId { get; set; }
        public string? TituloCurso { get; set; }
        public required string ImagemFrente { get; set; }
        public string? ImagemVerso { get; set; }
        public string? NomeImagemFrente { get; set; }
        public string? NomeImagemVerso { get; set; }
        public required string HtmlFrente { get; set; }
        public required string HtmlVerso { get; set; }
        public bool Status { get; set; } = true;
    }
}