using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceControleAcessoAula = "ControlesAcessosAulas";

		#region Main Methods

		public Task<long> CreateControleAcessoAula (ControleAcessoAulaModel.CreateUpdateControleAcessoAulaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceControleAcessoAula }"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateControleAcessoAula (int id, ControleAcessoAulaModel.CreateUpdateControleAcessoAulaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceControleAcessoAula }/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteControleAcessoAula (int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceControleAcessoAula }/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public ControleAcessoAulaDto GetControleAcessoAulaById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceControleAcessoAula}/{id}"));
            return Get<ControleAcessoAulaDto>(requestUrl);
        }
        public List<ControleAcessoAulaDto> GetControlesAcessosAulasAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceControleAcessoAula}"));
            return Get<List<ControleAcessoAulaDto>>(requestUrl);
        }

        #endregion
    }
}