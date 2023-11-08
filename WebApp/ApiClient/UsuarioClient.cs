using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
	public partial class DnaApiClient
	{

		#region Main Methods

		public Task<long> CreateUsuario(UsuarioModel.CreateUpdateUsuarioCommand command)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceUsuario}"));
			return Post(requestUrl, command);
		}
		public Task<long> UpdateUsuario(UsuarioModel.CreateUpdateUsuarioCommand command)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceUsuario}"));
			return Put(requestUrl, command);
		}
		public List<UsuarioDto> GetUsuarioAll()
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceUsuario}"));
			return Get<List<UsuarioDto>>(requestUrl);
		}

		public Task<long> DeleteUsuario(string id)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceUsuario}/{id}"));
			return Delete<string>(requestUrl);
		}

		#endregion

		#region Methods

		public UsuarioDto GetUsuarioById(string id)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceUsuario}/{id}"));
			return Get<UsuarioDto>(requestUrl);
		}
		public List<ModuloDto> GetModuloAll()
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceConfiguracaoSistema}/Modulos"));
			return Get<List<ModuloDto>>(requestUrl);
		}

		//public Task<bool> ExistUsuarioByIdUsuario(string id)
		//{
		//    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
		//        $"{ResourceUsuario}/ExistUsuarioByIdUsuario"));
		//    return PostAsync<bool, string>(requestUrl, id);
		//}

		#endregion
	}
}