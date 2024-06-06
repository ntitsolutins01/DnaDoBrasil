using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using WebApp.Dto;

namespace WebApp.Models
{
	public class CursoModel
	{
		public CursoDto Curso { get; set; }
		public List<CursoDto> MetricasImc { get; set; }

		public class CreateUpdateCursoCommand
		{
			public int Id { get; set; }
			public string? NomeCurso { get; set; }
			public bool Status { get; set; } = true;
		}
	}

}
