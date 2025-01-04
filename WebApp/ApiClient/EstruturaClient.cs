using System.Collections;
using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceEstrutura = "Estruturas";

		#region Main Methods

		public Task<long> CreateEstrutura (EstruturaModel.CreateUpdateEstruturaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceEstrutura }"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateEstrutura (int id, EstruturaModel.CreateUpdateEstruturaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceEstrutura }/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteEstrutura (int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceEstrutura }/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public EstruturaDto GetEstruturaById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceEstrutura }/Estrutura/{id}"));
            return Get<EstruturaDto>(requestUrl);
        }
        public List<EstruturaDto> GetEstruturasAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceEstrutura }"));
            return Get<List<EstruturaDto>>(requestUrl);
        }
        public List<EstruturaDto> GetEstruturasByLocalidade(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceEstrutura}/Localidade/{id}"));
            return Get<List<EstruturaDto>>(requestUrl);
        }
        #endregion


    }
}