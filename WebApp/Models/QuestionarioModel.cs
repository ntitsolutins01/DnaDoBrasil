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
            public required string Pergunta { get; set; }
            public required string TiposLaudo { get; set; }
        }
	}

}