using WebApp.Dto;

namespace WebApp.Models
{
	public class AulaModel
	{
        public AulaDto Aula { get; set; }
        public List<AulaDto> Aulas { get; set; }

        public class CreateUpdateAulaCommand
		{
            public required int Id { get; set; }
            public required int CargaHoraria { get; set; }
            public required int ProfessorId { get; set; }
            public required int MuduloEadId { get; set; }
            public required string Titulo { get; set; }
            public string? Descricao { get; set; }
            public bool Status { get; set; } = true;
           
        }
	}

}
