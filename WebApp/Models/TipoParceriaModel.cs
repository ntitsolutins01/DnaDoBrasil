using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Dto;

namespace WebApp.Models
{
    public class TipoParceriaModel
    {
        public TipoParceriaDto TipoParceria { get; set; }
        public List<TipoParceriaDto> TipoParcerias { get; set; }
        public string TipoParceriaId { get; set; }
        public SelectList ListTipoParcerias { get; set; }

        public class CreateUpdateTipoParceriaCommand
        {
            public int Id { get; set; }
            public string? Nome { get; set; }
            public int? Parceria { get; set; }
            public bool Status { get; set; }
        }
    }

}