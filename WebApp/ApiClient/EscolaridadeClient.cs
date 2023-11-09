using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {

        private const string ResourceConfiguracaoSistema = "ConfiguracaoSistema";

        #region Main Methods

        public Task<long> CreateEscolaridade(EscolaridadeModel.CreateUpdateEscolaridadeCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}"));
            return Post(requestUrl, command);
        }
        public Task<long> UpdateEscolaridade(EscolaridadeModel.CreateUpdateEscolaridadeCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}"));
            return Put(requestUrl, command);
        }
        public List<EscolaridadeDto> GetEscolaridadeAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}"));
            return Get<List<EscolaridadeDto>>(requestUrl);
        }

        public Task<long> DeleteEscolaridade(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}/{id}"));
            return Delete<string>(requestUrl);
        }

        #endregion

        #region Methods

        public EscolaridadeDto GetEscolaridadeById(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}/{id}"));
            return Get<EscolaridadeDto>(requestUrl);
        }
        //public List<EscolaridadeDto> GetEscolaridadesAll()
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceConfiguracaoSistema}/Escolaridades"));
        //    return Get<List<EscolaridadeDto>>(requestUrl);
        //}

        //public Task<bool> ExistUsuarioByIdEscolaridade(string id)
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceConfiguracaoSistema}/ExistUsuarioByIdEscolaridade"));
        //    return PostAsync<bool, string>(requestUrl, id);
        //}

        #endregion
    }
}