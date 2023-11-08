using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {

        #region Main Methods

        public Task<long> CreatePerfil(PerfilModel.CreateUpdateCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourcePerfil}"));
            return Post(requestUrl, command);
        }
        public Task<long> UpdatePerfil(PerfilModel.CreateUpdateCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourcePerfil}"));
            return Put(requestUrl, command);
        }
        public List<PerfilDto> GetPerfilAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourcePerfil}"));
            return Get<List<PerfilDto>>(requestUrl);
        }
        public Task<long> DeletePerfil(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourcePerfil}/{id}"));
            return Delete<string>(requestUrl);
        }

        #endregion

        #region Methods

        public PerfilDto GetPerfilById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourcePerfil}/{id}"));
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