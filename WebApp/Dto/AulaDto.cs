

namespace WebApp.Dto
{
    public class AulaDto
    {
        public required int Id { get; set; }
        public required int CargaHoraria { get; set; }
        public required int NomeProfessor { get; set; }
        public required int ModuloEadId { get; set; }
        public required string Titulo { get; set; }
        public string? Descricao { get; set; }
        public bool Status { get; set; } = true;
    }
}
