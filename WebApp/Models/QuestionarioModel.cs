using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using WebApp.Dto;

namespace WebApp.Models
{
	public class QuestionarioModel
	{
		public QuestionarioDto Questionario { get; set; }
		public List<QuestionarioDto> Questionarios { get; set; }
		public string QuestionarioId { get; set; }
		public SelectList ListQuestionarios { get; set; }

		public class CreateUpdateQuestionarioCommand
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