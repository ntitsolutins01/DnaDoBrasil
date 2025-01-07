using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceLinhaAcao = "LinhasAcoes";

		#region Main Methods

		public Task<long> CreateLinhaAcao(LinhaAcaoModel.CreateUpdateLinhaAcaoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceLinhaAcao}"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateLinhaAcao(int id, LinhaAcaoModel.CreateUpdateLinhaAcaoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceLinhaAcao}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteLinhaAcao(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceLinhaAcao}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public LinhaAcaoDto GetLinhaAcaoById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceLinhaAcao}/{id}"));
            return Get<LinhaAcaoDto>(requestUrl);
        }
        public List<LinhaAcaoDto> GetLinhasAcoesAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceLinhaAcao}"));
            return Get<List<LinhaAcaoDto>>(requestUrl);
        }

        #endregion
    }
}