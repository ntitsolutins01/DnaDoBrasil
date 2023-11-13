using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {

        private const string ResourceParceiro = "Parceiros";

        #region Main Methods

        public Task<long> CreateParceiro(ParceiroModel.CreateUpdateParceiroCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceParceiro}"));
            return Post(requestUrl, command);
        }
        public List<ParceiroDto> GetParceiroAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}"));
            return Get<List<ParceiroDto>>(requestUrl);
        }


        #endregion

        #region Methods

        public ParceiroDto GetParceiroById(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}/{id}"));
            return Get<ParceiroDto>(requestUrl);
        }
        //public List<ParceiroDto> GetParceirosAll()
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceConfiguracaoSistema}/Parceiros"));
        //    return Get<List<ParceiroDto>>(requestUrl);
        //}

        //public Task<bool> ExistUsuarioByIdParceiro(string id)
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceConfiguracaoSistema}/ExistUsuarioByIdParceiro"));
        //    return PostAsync<bool, string>(requestUrl, id);
        //}

        #endregion
    }
}