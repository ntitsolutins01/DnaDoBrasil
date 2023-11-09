using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Dto;

namespace WebApp.Models
{
    public class UsuarioModel
    {
        public int PerfilId { get; set; }
        public SelectList ListPerfis { get; set; }
        public UsuarioDto Usuario { get; set; }

		public class CreateUpdateUsuarioCommand
		{
			public required string AspNetUserId { get; set; }
			public required string Nome { get; set; }
			public required string Cpf { get; set; }
			public required string Email { get; set; }
			public required string Telefone { get; set; }
			public required string AspNetRoleId { get; set; }
			public int PerfilId { get; set; }
			public string Id { get; set; }
		}
	}
}
