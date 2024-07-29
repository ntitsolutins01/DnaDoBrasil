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
        public Task<bool> UpdateParceiro(int id, ParceiroModel.CreateUpdateParceiroCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceParceiro}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteParceiro(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceParceiro}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public async Task<ParceiroDto> GetParceiroById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceParceiro}/Parceiro/{id}"));
            return Get<ParceiroDto>(requestUrl);
        }
        public List<ParceiroDto> GetParceiroAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceParceiro}"));
            return Get<List<ParceiroDto>>(requestUrl);
        }
        public ParceiroDto GetParceiroByAspNetUserId(string aspNetUserId)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceParceiro}/AspNetUser/{aspNetUserId}"));
            return Get<ParceiroDto>(requestUrl);
        }

        #endregion

    }
}