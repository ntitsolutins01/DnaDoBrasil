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
            public int Id { get; set; }
            public string Nome { get; set; }
            public int Vo2MaxIni { get; init; }
            public int Vo2MaxFim { get; init; }
            public int VinteMetrosIni { get; init; }
            public int VinteMetrosFim { get; init; }
            public int ShutlleRunIni { get; init; }
            public int ShutlleRunFim { get; init; }
            public int FlexibilidadeIni { get; init; }
            public int FlexibilidadeFim { get; init; }
            public int PreensaoManualIni { get; init; }
            public int PreensaoManualFim { get; init; }
            public int AbdominalPranchaIni { get; init; }
            public int AbdominalPranchaFim { get; init; }
            public int ImpulsaoIni { get; init; }
            public int ImpulsaoFim { get; init; }
            public int EnvergaduraIni { get; init; }
            public int EnvergaduraFim { get; init; }
            public int PesoIni { get; init; }
            public int PesoFim { get; init; }
            public int AlturaIni { get; init; }
            public int AlturaFim { get; init; }
            public bool Status { get; set; }
            public string? TesteLaudosIds { get; set; }
        }
    }

}
