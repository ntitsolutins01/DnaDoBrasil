using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using WebApp.Dto;

namespace WebApp.Models
{
    public class MaterialModel
    {
        public MaterialDto Material { get; set; }
        public List<MaterialDto> Materiais { get; set; }
        public SelectList ListMateriais { get; set; }
        public TipoMaterialDto TipoMaterial { get; set; }
        public List<TipoMaterialDto> TiposMateriais { get; set; }
        public SelectList ListTiposMateriais { get; set; }

        public class CreateUpdateMaterialCommand
        {
            public int Id { get; set; }
            public int TipoMaterialId { get; set; }
            public string UnidadeMedida { get; set; }
            public string? Descricao { get; set; }
            public int? QtdAdquirida { get; set; }
        }
    }

}