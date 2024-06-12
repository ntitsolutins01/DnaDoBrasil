namespace WebApp.Dto
{
    public class CursoDto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? NomeFoto { get; set; }
        public int CoordenadorId { get; set; }
        public int TipoCursoId { get; set; }
        public bool Status { get; set; } = true;
    }
}
