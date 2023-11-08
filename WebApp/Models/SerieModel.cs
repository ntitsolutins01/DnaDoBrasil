using Microsoft.AspNetCore.Mvc.Rendering;
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
            public required string Nome { get; set; }
            public required string Descricao { get; set; }
            public required int IdadeInicial { get; set; }
            public required int IdadeFinal { get; set; }
            public required int ScoreTotal { get; set; }
        }
    }

}
