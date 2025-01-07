using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
	public partial class DnaApiClient
	{
        private const string ResourceUsuario = "Usuarios";
        private const string ResourceUser = "Users";

        #region Main Methods

        public Task<UsuarioModel.LoginUsuarioRequest?> LoginUsuario(UsuarioModel.LoginUsuarioRequest request)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceUser}/login"));
			return PostWithResponseBody(requestUrl, request);
		}
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
        public UsuarioDto GetUsuarioByAspNetUserId(string aspNetUserId)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceUsuario}/AspNetUser/{aspNetUserId}"));
			return Get<UsuarioDto>(requestUrl);
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