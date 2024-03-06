using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using WebApp.Dto;

namespace WebApp.Models
{
    public class ControlePresencaModel
    {
        public ControlePresencaDto ControlePresenca { get; set; }
        public List<ControlePresencaDto> ControlePresencas { get; set; }
        public string ControlePresencaId { get; set; }
        public SelectList ListControlePresencas { get; set; }
        public int LocalidadeId { get; set; }
        public SelectList ListLocalidades { get; set; }
        public string EstadoId { get; set; }
        public SelectList ListEstados { get; set; }
        public string MunicipioId { get; set; }
        public SelectList ListMunicipios { get; set; }
        public string AlunoId { get; set; }
        public SelectList ListAlunos { get; set; }

		public class CreateUpdateControlePresencaCommand
        {
            public int Id { get; set; }
			public required int Controle { get; init; }
			public int Justificativa { get; init; }
			public bool Status { get; init; } = true;
			public int? LocalidadeId { get; set; }
			public string? MunicipioId { get; set; }
			public int AlunoId { get; set; }
		}
    }

}
