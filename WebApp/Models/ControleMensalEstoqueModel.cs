using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using WebApp.Dto;

namespace WebApp.Models
{
    public class ControleMensalEstoqueModel
    {
        public ControleMensalEstoqueDto ControleMensalEstoque { get; set; }
        public List<ControleMensalEstoqueDto> ControlesMensaisEstoque { get; set; }
        public SelectList ListControlesMensaisEstoque { get; set; }
        public TipoMaterialDto TipoMaterial { get; set; }
        public List<TipoMaterialDto> TiposMateriais { get; set; }
        public SelectList ListTiposMateriais { get; set; }
        public int TipoMaterialId { get; set; }
        public MaterialDto Material { get; set; }
        public List<MaterialDto> Materiais { get; set; }
        public SelectList ListMateriais { get; set; }
        public int MaterialId { get; set; }

        public class CreateUpdateControleMensalEstoqueCommand
        {
            public int Id { get; set; }
            public required int MaterialId { get; set; }
            public int? QtdPrevista { get; set; }
            public string DataMesSaida { get; set; }
            public int? TotalSaidas { get; set; }
            public int? TotalEstoque { get; set; }
            public int? QtdMateriaisDanificadosExtraviados { get; set; }
            public string? JustificativaDanificadosExtraviados { get; set; }
            public string DataDanificadosExtraviados { get; set; }
        }
    }

}