using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        private const string ResourceLocalidade = "Localidades";

        #region Main Methods

        public Task<long> CreateLocalidade(LocalidadeModel.CreateUpdateLocalidadeCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceLocalidade}"));
            return Post(requestUrl, command);
        }

        public Task<bool> UpdateLocalidade(int id, LocalidadeModel.CreateUpdateLocalidadeCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceLocalidade}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteLocalidade(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceLocalidade}/{id}"));
            return Delete<bool>(requestUrl);
        }
        #endregion

        #region Methods

        public List<LocalidadeDto> GetLocalidadeAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceLocalidade}"));
            return Get<List<LocalidadeDto>>(requestUrl);
        }
        public LocalidadeDto GetLocalidadeById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceLocalidade}/Localidade/{id}"));
            return Get<LocalidadeDto>(requestUrl);
        }
        public List<LocalidadeDto> GetLocalidadeByMunicipio(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceLocalidade}/Municipio/{id}"));
            return Get<List<LocalidadeDto>>(requestUrl);
        }
        public List<LocalidadeDto> GetLocalidadeByFomento(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceLocalidade}/Fomento/{id}"));
            return Get<List<LocalidadeDto>>(requestUrl);
        }

        #endregion
    }
}