using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using WebApp.Dto;

namespace WebApp.Models
{
	public class RespostaModel
	{
		public RespostaDto Resposta { get; set; }
		public List<RespostaDto> Respostas { get; set; }
		public string RespostaId { get; set; }
		public SelectList ListRespostas { get; set; }
		public SelectList ListQuestionarios { get; set; }
        public QuestionarioDto Questionario { get; set; }

        public class CreateUpdateRespostaCommand
		{
			public int Id { get; set; }
			public string RespostaQuestionario { get; set; }
            public QuestionarioDto Questionario { get; set; }
        }
	}

}