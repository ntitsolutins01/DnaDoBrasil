using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using WebApp.Dto;

namespace WebApp.Models
{
	public class CursoModel
	{
		public CursoDto Curso { get; set; }
		public List<CursoDto> Cursos { get; set; }
		public SelectList ListTiposCursos { get; set; } 
        public string TipoCursoId { get; set; }
		public SelectList ListCoordenadores { get; set; }
        public string CoordenadorId { get; set; }


        public class CreateUpdateCursoCommand
		{
			public int Id { get; set; }
			public int TipoCursoId { get; set; }
			public int UsuarioId { get; set; }
			public required string Titulo { get; set; }
			public required int CargaHoraria { get; set; }
			public string? Descricao { get; set; }
			public bool Status { get; set; }
		}
	}

}
