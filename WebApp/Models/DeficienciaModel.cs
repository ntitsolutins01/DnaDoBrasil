﻿using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Dto;

namespace WebApp.Models
{
    public class DeficienciaModel
    {
        public DeficienciaDto Deficiencia { get; set; }
        public List<DeficienciaDto> Deficiencias { get; set; }
        public string DeficienciaId { get; set; }
        public SelectList ListDeficiencias { get; set; }

        public class CreateUpdateDeficienciaCommand
        {
            public string TipoLaudo { get; set; }
        }
    }

}
