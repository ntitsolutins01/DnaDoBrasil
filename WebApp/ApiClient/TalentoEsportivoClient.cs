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
        public Task<long> UpdateTalentoEsportivo(TalentoEsportivoModel.CreateUpdateTalentoEsportivoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}"));
            return Put(requestUrl, command);
        }
        public List<TalentoEsportivoDto> GetTalentoEsportivoAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}"));
            return Get<List<TalentoEsportivoDto>>(requestUrl);
        }

        public Task<long> DeleteTalentoEsportivo(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}/{id}"));
            return Delete<string>(requestUrl);
        }

        #endregion

        #region Methods

        public TalentoEsportivoDto GetTalentoEsportivoById(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}/{id}"));
            return Get<TalentoEsportivoDto>(requestUrl);
        }
        //public List<TalentoEsportivoDto> GetTalentoEsportivosAll()
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceConfiguracaoSistema}/TalentoEsportivos"));
        //    return Get<List<TalentoEsportivoDto>>(requestUrl);
        //}

        //public Task<bool> ExistUsuarioByIdTalentoEsportivo(string id)
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceConfiguracaoSistema}/ExistUsuarioByIdTalentoEsportivo"));
        //    return PostAsync<bool, string>(requestUrl, id);
        //}

        #endregion
    }
}