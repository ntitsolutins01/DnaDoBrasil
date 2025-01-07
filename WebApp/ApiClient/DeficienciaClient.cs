using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {

        private const string ResourceDeficiencia = "Deficiencias";

        #region Main Methods

        public Task<long> CreateDeficiencia(DeficienciaModel.CreateUpdateDeficienciaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceDeficiencia}"));
            return Post(requestUrl, command);
        }

        public Task<bool> UpdateDeficiencia(int id, DeficienciaModel.CreateUpdateDeficienciaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceDeficiencia}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteDeficiencia(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceDeficiencia}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public List<DeficienciaDto> GetDeficienciaAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceDeficiencia}"));
            return Get<List<DeficienciaDto>>(requestUrl);
        }

        public DeficienciaDto GetDeficienciaById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceDeficiencia}{id}"));
            return Get<DeficienciaDto>(requestUrl);

            #endregion
        }
    }
}