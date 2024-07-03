using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceComunidade = "Comunidades";

		#region Main Methods

		public Task<long> CreateComunidade (ComunidadeModel.CreateUpdateComunidadeCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceComunidade }"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateComunidade (int id, ComunidadeModel.CreateUpdateComunidadeCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceComunidade }/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteComunidade (int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceComunidade }/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public ComunidadeDto GetComunidadeById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceComunidade }/Comunidade/{id}"));
            return Get<ComunidadeDto>(requestUrl);
        }
        public List<ComunidadeDto> GetComunidadesAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceComunidade }"));
            return Get<List<ComunidadeDto>>(requestUrl);
        }

        #endregion
    }
}