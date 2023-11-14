using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        private const string ResourcePerfil = "Perfis";

        #region Main Methods

        public Task<long> CreatePerfil(PerfilModel.CreateUpdateCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourcePerfil}"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdatePerfil(int id, PerfilModel.CreateUpdateCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourcePerfil}/{id}"));
            return Put(requestUrl, command);
        }
        public Task<bool> DeletePerfil(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourcePerfil}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public List<PerfilDto> GetPerfilAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourcePerfil}"));
            return Get<List<PerfilDto>>(requestUrl);
        }
        public PerfilDto GetPerfilById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourcePerfil}/{id}"));
            return Get<PerfilDto>(requestUrl);
		}
        public PerfilDto GetPerfilByAspNetRoleId(string aspNetRoleId)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourcePerfil}/AspNetRoleId/{aspNetRoleId}"));
            return Get<PerfilDto>(requestUrl);
		}
		public UsuarioDto GetUsuarioByName(string name)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceUsuario}/{name}"));
			return Get<UsuarioDto>(requestUrl);
		}
		public UsuarioDto GetUsuarioByCpf(string cpf)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceUsuario}?cpf={cpf}"));
			return Get<UsuarioDto>(requestUrl);
		}
		public UsuarioDto GetUsuarioByEmail(string email)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceUsuario}?email={email}"));
			return Get<UsuarioDto>(requestUrl);
		}

		#endregion
	}
}