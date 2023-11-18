using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
	public partial class DnaApiClient
	{
        private const string ResourceUsuario = "Usuarios";

        #region Main Methods

        public Task<long> CreateUsuario(UsuarioModel.CreateUpdateUsuarioCommand command)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceUsuario}"));
			return Post(requestUrl, command);
		}
        public Task<bool> UpdateUsuario(int id, UsuarioModel.CreateUpdateUsuarioCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceUsuario}/{id}"));
            return Put(requestUrl, command);
        }
        public Task<bool> DeleteUsuario(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceUsuario}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods
        public List<UsuarioDto> GetUsuarioAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceUsuario}"));
            return Get<List<UsuarioDto>>(requestUrl);
        }
        public UsuarioDto GetUsuarioById(string id)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceUsuario}/{id}"));
			return Get<UsuarioDto>(requestUrl);
		}
		public List<ModuloDto> GetModuloAll()
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceUsuario}/Modulos"));
			return Get<List<ModuloDto>>(requestUrl);
		}
        public UsuarioDto GetUsuarioByCpf(string cpf)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceUsuario}/Cpf/{cpf}"));
            return Get<UsuarioDto>(requestUrl);
        }
        public UsuarioDto GetUsuarioByEmail(string email)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceUsuario}/Email/{email}"));
            return Get<UsuarioDto>(requestUrl);
        }

        #endregion
    }
}