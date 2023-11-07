using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        #region Main Methods

        public Task<long> CreateTiposLaudo(TiposLaudoModel.CreateUpdateTiposLaudoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}"));
            return Post(requestUrl, command);
        }
        public Task<long> UpdateTiposLaudo(TiposLaudoModel.CreateUpdateTiposLaudoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}"));
            return Put(requestUrl, command);
        }
        public List<TiposLaudoDto> GetTiposLaudoAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}"));
            return Get<List<TiposLaudoDto>>(requestUrl);
        }

        public Task<long> DeleteTiposLaudo(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}/{id}"));
            return Delete<string>(requestUrl);
        }

        #endregion

        #region Methods

        public TiposLaudoDto GetTiposLaudoById(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}/{id}"));
            return Get<TiposLaudoDto>(requestUrl);
        }
        //public List<DeficienciaDto> GetDeficienciasAll()
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceConfiguracaoSistema}/Deficiencias"));
        //    return Get<List<DeficienciaDto>>(requestUrl);
        //}

        //public Task<bool> ExistUsuarioByIdTiposLaudo(string id)
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceConfiguracaoSistema}/ExistUsuarioByIdTiposLaudo"));
        //    return PostAsync<bool, string>(requestUrl, id);
        //}

        #endregion
    }
}