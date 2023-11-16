using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        private const string ResourceSerie = "Serie";
        #region Main Methods

        public Task<long> CreateSerie(SerieModel.CreateUpdateSerieCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceSerie}"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateSerie(int id, SerieModel.CreateUpdateSerieCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceSerie}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteSerie(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceSerie}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public SerieDto GetSerieById(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceSerie}/{id}"));
            return Get<SerieDto>(requestUrl);
        }
        public List<SerieDto> GetSerieAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceSerie}"));
            return Get<List<SerieDto>>(requestUrl);
        }

        #endregion
    }
}