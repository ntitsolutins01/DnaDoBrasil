using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
	public partial class DnaApiClient
	{
		private const string ResourceEncaminhamento = "Encaminhamentos";

		#region Main Methods

		public Task<long> CreateEncaminhamento(EncaminhamentoModel.CreateUpdateEncaminhamentoCommand command)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceEncaminhamento}"));
			return Post(requestUrl, command);
		}
		public Task<bool> UpdateEncaminhamento(int id, EncaminhamentoModel.CreateUpdateEncaminhamentoCommand command)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceEncaminhamento}/{id}"));
			return Put(requestUrl, command);
		}

		public Task<bool> DeleteEncaminhamento(int id)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceEncaminhamento}/{id}"));
			return Delete<bool>(requestUrl);
		}

		#endregion

		#region Methods

		public EncaminhamentoDto GetEncaminhamentoById(int id)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceEncaminhamento}/{id}"));
			return Get<EncaminhamentoDto>(requestUrl);
		}
		public List<EncaminhamentoDto> GetEncaminhamentosAll()
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceEncaminhamento}"));
			return Get<List<EncaminhamentoDto>>(requestUrl);
		}
		public List<EncaminhamentoDto> GetEncaminhamentosByTipoLaudoId(int id)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceEncaminhamento}/TipoLaudo/{id}"));
			return Get<List<EncaminhamentoDto>>(requestUrl);
		}

		#endregion


	}
}