using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using WebApp.Dto;

namespace WebApp.Models
{
    public class TesteLaudoModel
    {
        public TesteLaudoDto TesteLaudo { get; set; }
        public List<TesteLaudoDto> TesteLaudos { get; set; }
        public string TesteLaudoId { get; set; }
        public SelectList ListTesteLaudos { get; set; }

        public class CreateUpdateTesteLaudoCommand
        {
            public string Classificacao { get; set; }
            public decimal PontoInicial { get; set; }
            public decimal PontoFinal { get; set; }
            public int Aviso { get; set; }
            public int Texto { get; set; }
            public int Id { get; set; }
        }
    }

}
