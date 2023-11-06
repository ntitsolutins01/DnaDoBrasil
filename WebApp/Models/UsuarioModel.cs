using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Dto;

namespace WebApp.Models
{
    public class UsuarioModel
    {
        public int CodPerfil { get; set; }
        public SelectList ListPerfil { get; set; }
        public UsuarioDto Usuario { get; set; }
    }
}
