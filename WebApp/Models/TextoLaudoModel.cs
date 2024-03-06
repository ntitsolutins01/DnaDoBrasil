using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using WebApp.Dto;

namespace WebApp.Models
{
    public class TextoLaudoModel
    {
        public TextoLaudoDto TextoLaudo { get; set; }
        public List<TextoLaudoDto> TextosLaudos { get; set; }
        public string TextoLaudoId { get; set; }
        public SelectList ListTextosLaudos { get; set; }

        public class CreateUpdateTextoLaudoCommand
        {
            public int Id { get; set; }
            public string Classificacao { get; set; }
            public decimal PontoInicial { get; set; }
            public decimal PontoFinal { get; set; }
            public int Aviso { get; set; }
            public int Texto { get; set; }
        }
    }

}
