using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using WebApp.Dto;

namespace WebApp.Models
{
    public class ControleMaterialEstoqueSaidaModel
    {
        public ControleMaterialEstoqueSaidaDto ControleMaterialEstoqueSaida { get; set; }
        public List<ControleMaterialEstoqueSaidaDto> ControlesMateriaisEstoquesSaidas { get; set; }
        public SelectList ListControlesMateriaisEstoquesSaidas { get; set; }
        public TipoMaterialDto TipoMaterial { get; set; }
        public List<TipoMaterialDto> TiposMateriais { get; set; }
        public SelectList ListTiposMateriais { get; set; }
        public int TipoMaterialId { get; set; }
        public MaterialDto Material { get; set; }
        public List<MaterialDto> Materiais { get; set; }
        public SelectList ListMateriais { get; set; }
        public int MaterialId { get; set; }

        public class CreateUpdateControleMaterialEstoqueSaidaCommand
        {
            public int Id { get; set; }
            public int MaterialId { get; set; }
            public  int Quantidade { get; set; }
            public string? Solicitante { get; set; }
        }
    }

}