using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceTipoParceria = "TipoParcerias";

		#region Main Methods

		public Task<long> CreateTipoParceria(TipoParceriaModel.CreateUpdateTipoParceriaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTipoParceria}"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateTipoParceria(int id, TipoParceriaModel.CreateUpdateTipoParceriaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTipoParceria}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteTipoParceria(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTipoParceria}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public TipoParceriaDto GetTipoParceriaById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTipoParceria}/{id}"));
            return Get<TipoParceriaDto>(requestUrl);
        }
        public List<TipoParceriaDto> GetTipoParceriaAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTipoParceria}"));
            return Get<List<TipoParceriaDto>>(requestUrl);
        }

        #endregion
    }
}