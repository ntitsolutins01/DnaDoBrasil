using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        private const string ResourceConsumoAlimentar = "ConsumosAlimentares";
        #region Main Methods

        public Task<long> CreateConsumoAlimentar(ConsumoAlimentarModel.CreateUpdateConsumoAlimentarCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConsumoAlimentar}"));
            return Post(requestUrl, command);
        }

        public Task<bool> UpdateConsumoAlimentar(int id, ConsumoAlimentarModel.CreateUpdateConsumoAlimentarCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConsumoAlimentar}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteConsumoAlimentar(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConsumoAlimentar}/{id}"));
            return Delete<bool>(requestUrl);
        }


        #endregion

        #region Methods

        public ConsumoAlimentarDto GetConsumoAlimentarById(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConsumoAlimentar}/{id}"));
            return Get<ConsumoAlimentarDto>(requestUrl);
        }
        public List<ConsumoAlimentarDto> GetConsumoAlimentarAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConsumoAlimentar}"));
            return Get<List<ConsumoAlimentarDto>>(requestUrl);
        }
        #endregion
    }
}