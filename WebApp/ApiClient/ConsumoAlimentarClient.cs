using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        #region Main Methods

        public Task<long> CreateConsumoAlimentar(ConsumoAlimentarModel.CreateUpdateConsumoAlimentarCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}"));
            return Post(requestUrl, command);
        }
        public Task<long> UpdateConsumoAlimentar(ConsumoAlimentarModel.CreateUpdateConsumoAlimentarCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}"));
            return Put(requestUrl, command);
        }
        public List<ConsumoAlimentarDto> GetConsumoAlimentarAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}"));
            return Get<List<ConsumoAlimentarDto>>(requestUrl);
        }

        public Task<long> DeleteConsumoAlimentar(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}/{id}"));
            return Delete<string>(requestUrl);
        }

        #endregion

        #region Methods

        public ConsumoAlimentarDto GetConsumoAlimentarById(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}/{id}"));
            return Get<ConsumoAlimentarDto>(requestUrl);
        }
        //public List<ConsumoAlimentarDto> GetConsumoAlimentarsAll()
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceConfiguracaoSistema}/ConsumoAlimentars"));
        //    return Get<List<ConsumoAlimentarDto>>(requestUrl);
        //}

        //public Task<bool> ExistUsuarioByIdConsumoAlimentar(string id)
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceConfiguracaoSistema}/ExistUsuarioByIdConsumoAlimentar"));
        //    return PostAsync<bool, string>(requestUrl, id);
        //}

        #endregion
    }
}