using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        private const string ResourceEscolaridade = "Escolaridades";

        #region Main Methods

        public Task<long> CreateEscolaridade(EscolaridadeModel.CreateUpdateEscolaridadeCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceEscolaridade}"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateEscolaridade(int id, EscolaridadeModel.CreateUpdateEscolaridadeCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceEscolaridade}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteEscolaridade(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceEscolaridade}/{id}"));
            return Delete<bool>(requestUrl);
        }
        #endregion

        #region Methods

        public EscolaridadeDto GetEscolaridadeById(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceEscolaridade}/{id}"));
            return Get<EscolaridadeDto>(requestUrl);
        }
        public List<EscolaridadeDto> GetEscolaridadeAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceEscolaridade}"));
            return Get<List<EscolaridadeDto>>(requestUrl);
        }
        #endregion
    }
}