using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        #region Main Methods

        public Task<long> CreateSaudeBucal(SaudeBucalModel.CreateUpdateSaudeBucalCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}"));
            return Post(requestUrl, command);
        }

        #endregion

        #region Methods

        public List<SaudeBucalDto> GetSaudeBucalAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}"));
            return Get<List<SaudeBucalDto>>(requestUrl);
        }

        public SaudeBucalDto GetSaudeBucalById(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}/{id}"));
            return Get<SaudeBucalDto>(requestUrl);

            #endregion
        }
    }
}