using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceControlePresenca = "ControlePresencas";

		#region Main Methods

		public Task<long> CreateControlePresenca(ControlePresencaModel.CreateUpdateControlePresencaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceControlePresenca}"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateControlePresenca(int id, ControlePresencaModel.CreateUpdateControlePresencaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceControlePresenca}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteControlePresenca(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceControlePresenca}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public ControlePresencaDto GetControlePresencaById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceControlePresenca}/ControlePresenca/{id}"));
            return Get<ControlePresencaDto>(requestUrl);
        }
        public List<ControlePresencaDto> GetControlePresencaAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceControlePresenca}"));
            return Get<List<ControlePresencaDto>>(requestUrl);
        }

        #endregion
    }
}