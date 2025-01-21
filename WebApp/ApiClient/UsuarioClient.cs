using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    /// <summary>
    /// Usuário Client
    /// </summary>
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

        /// <summary>
        /// Inclusão de Usuário
        /// </summary>
        /// <param name="command">Objeto de inclusão de Usuário</param>
        /// <returns>Id do Usuário inserido</returns>
        public Task<long> CreateUsuario(UsuarioModel.CreateUpdateUsuarioCommand command)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceUsuario}"));
			return Post(requestUrl, command);
		}

        /// <summary>
        ///  Alteração de Usuário
        /// </summary>
        /// <param name="id">Id de alteração de Usuário</param>
        /// <param name="command">Objeto de alteração de Usuário</param>
        /// <returns>Retorna true ou false</returns>
        public Task<bool> UpdateUsuario(int id, UsuarioModel.CreateUpdateUsuarioCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceUsuario}/{id}"));
            return Put(requestUrl, command);
        }

        /// <summary>
        /// Exclusão de Usuário
        /// </summary>
        /// <param name="id">Id de Exclusão de Usuário</param>
        /// <returns>Retorna true ou false</returns>
        public Task<bool> DeleteUsuario(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceUsuario}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        /// <summary>
        ///  Busca todos os Usuário cadastrados
        /// </summary>
        /// <returns>Retorna a lista de Usuário</returns>
        public List<UsuarioDto> GetUsuarioAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceUsuario}"));
            return Get<List<UsuarioDto>>(requestUrl);
        }

        /// <summary>
        ///  Busca um único Usuário
        /// </summary>
        /// <param name="id">Id de Usuário a ser buscado</param>
        /// <returns>Retorna o objeto do Usuário</returns>
        public UsuarioDto GetUsuarioById(string id)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceUsuario}/{id}"));
			return Get<UsuarioDto>(requestUrl);
		}

        /// <summary>
        /// Busca Usuário por Id de Rede 
        /// </summary>
        /// <param name="aspNetUserId">id de Usuário por rede </param>
        /// <returns>retorna um objeto de Usuário</returns>
        public UsuarioDto GetUsuarioByAspNetUserId(string aspNetUserId)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceUsuario}/AspNetUser/{aspNetUserId}"));
			return Get<UsuarioDto>(requestUrl);
		}

        /// <summary>
        /// Busca Usuário por Cpf
        /// </summary>
        /// <param name="cpf">cpf</param>
        /// <returns>retona uma Lista por Cpf</returns>
        public UsuarioDto GetUsuarioByCpf(string cpf)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceUsuario}/Cpf/{cpf}"));
            return Get<UsuarioDto>(requestUrl);
        }

        /// <summary>
        /// Busca Usuário por Email
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>Retorna a lista por email </returns>
        public UsuarioDto GetUsuarioByEmail(string email)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceUsuario}/Email/{email}"));
            return Get<UsuarioDto>(requestUrl);
        }

        #endregion

    }
}