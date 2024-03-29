using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Dto;

namespace WebApp.Models
{
    public class UsuarioModel
    {
        public int PerfilId { get; set; }
        public SelectList ListPerfis { get; set; }
        public UsuarioDto Usuario { get; set; }
        public List<UsuarioDto> Usuarios { get; set; }
        public string LocalidadeId { get; set; }
        public SelectList ListLocalidades { get; set; }
        public string EstadoId { get; set; }
        public SelectList ListEstados { get; set; }
        public string MunicipioId { get; set; }
        public SelectList ListMunicipios { get; set; }

        public class CreateUpdateUsuarioCommand
		{
			public string AspNetUserId { get; set; }
			public string Nome { get; set; }
			public string CpfCnpj { get; set; }
			public string Email { get; set; }
			public string AspNetRoleId { get; set; }
			public int PerfilId { get; set; }
			public string Id { get; set; }
            public string TipoPessoa { get; set; }
            public int MunicipioId { get; set; }
            public int LocalidadeId { get; set; }
        }
	}
}
