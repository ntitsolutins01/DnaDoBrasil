using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using WebApp.Dto;

namespace WebApp.Models
{
	public class SerieModel
	{
		public SerieDto Serie { get; set; }
		public List<SerieDto> Series { get; set; }
		public string SerieId { get; set; }
		public SelectList ListSeries { get; set; }

		public class CreateUpdateSerieCommand
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