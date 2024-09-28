

using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Dto
{
    public class AulaDto
    {
		public required int Id { get; set; }
		public required int CargaHoraria { get; set; }
		public required string NomeProfessor { get; set; }
		public required int ProfessorId { get; set; }
		public required string TituloModuloEad { get; set; }
		public required string Titulo { get; set; }
		public string? Descricao { get; set; }
		public bool Status { get; set; }
        public SelectList? ListProfessores { get; set; }
    }
}
