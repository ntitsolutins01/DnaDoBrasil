using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        #region Main Methods

        public Task<long> CreateSerie(SerieModel.CreateUpdateSerieCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}"));
            return Post(requestUrl, command);
        }
        public Task<long> UpdateSerie(SerieModel.CreateUpdateSerieCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}"));
            return Put(requestUrl, command);
        }
        public List<SerieDto> GetSerieAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}"));
            return Get<List<SerieDto>>(requestUrl);
        }

        public Task<long> DeleteSerie(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}/{id}"));
            return Delete<string>(requestUrl);
        }

        #endregion

        #region Methods

        public SerieDto GetSerieById(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}/{id}"));
            return Get<SerieDto>(requestUrl);
        }
        //public List<SerieDto> GetSeriesAll()
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceConfiguracaoSistema}/Series"));
        //    return Get<List<SerieDto>>(requestUrl);
        //}

        //public Task<bool> ExistUsuarioByIdSerie(string id)
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceConfiguracaoSistema}/ExistUsuarioByIdSerie"));
        //    return PostAsync<bool, string>(requestUrl, id);
        //}

        #endregion
    }
}