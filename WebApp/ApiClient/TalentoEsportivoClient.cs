using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        #region Main Methods

        public Task<long> CreateTalentoEsportivo(TalentoEsportivoModel.CreateUpdateTalentoEsportivoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}"));
            return Post(requestUrl, command);
        }

        #endregion

        #region Methods

        public TalentoEsportivoDto GetTalentoEsportivoById(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}/{id}"));
            return Get<TalentoEsportivoDto>(requestUrl);
        }
        public List<TalentoEsportivoDto> GetTalentoEsportivoAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}"));
            return Get<List<TalentoEsportivoDto>>(requestUrl);
        }

        #endregion
    }
}