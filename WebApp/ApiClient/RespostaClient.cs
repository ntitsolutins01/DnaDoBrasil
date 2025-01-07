using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        private const string ResourceResposta = "Respostas";
        #region Main Methods

        public Task<long> CreateResposta(RespostaModel.CreateUpdateRespostaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceResposta}"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateResposta(int id, RespostaModel.CreateUpdateRespostaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceResposta}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteResposta(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceResposta}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public RespostaDto GetRespostaById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceResposta}{id}"));
            return Get<RespostaDto>(requestUrl);
        }
        public List<RespostaDto> GetRespostaAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceResposta}"));
            return Get<List<RespostaDto>>(requestUrl);
        }

        #endregion
    }
}