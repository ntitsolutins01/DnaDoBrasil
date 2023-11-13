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
                $"{ResourceConfiguracaoSistema}"));
            return Get<List<LocalidadeDto>>(requestUrl);
        }
        public LocalidadeDto GetLocalidadeById(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}/{id}"));
            return Get<LocalidadeDto>(requestUrl);
        }
        //public List<LocalidadeDto> GetLocalidadesAll()
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceConfiguracaoSistema}/Localidades"));
        //    return Get<List<LocalidadeDto>>(requestUrl);
        //}

        //public Task<bool> ExistUsuarioByIdLocalidade(string id)
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceConfiguracaoSistema}/ExistUsuarioByIdLocalidade"));
        //    return PostAsync<bool, string>(requestUrl, id);
        //}

        #endregion
    }
}