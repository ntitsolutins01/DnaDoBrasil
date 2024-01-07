using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using WebApp.Dto;

namespace WebApp.Models
{
	public class LaudoModel
	{
		public LaudoDto Laudo { get; set; }
		public List<LaudoDto> Laudos { get; set; }
		public List<QuestionarioDto> QuestionarioVocacional { get; set; }
		public string LaudoId { get; set; }
		public SelectList ListLaudos { get; set; }

		public class CreateUpdateLaudoCommand
		{
			public int Id { get; set; }
			public string Nome { get; set; }
			public bool Status { get; set; } = true;
			public string Descricao { get; set; }
			public int IdadeInicial { get; set; }
			public int IdadeFinal { get; set; }
			public int ScoreTotal { get; set; }
		}
	}

}