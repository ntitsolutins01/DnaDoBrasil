using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        private const string ResourceSaudeBucal = "SaudeBucais";

        #region Main Methods

        public Task<long> CreateSaudeBucal(SaudeBucalModel.CreateUpdateSaudeBucalCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceSaudeBucal}"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateSaudeBucal(int id, SaudeBucalModel.CreateUpdateSaudeBucalCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceSaudeBucal}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteSaudeBucal(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceSaudeBucal}/{id}"));
            return Delete<bool>(requestUrl);
        }


        #endregion

        #region Methods

        public List<SaudeBucalDto> GetSaudeBucalAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceSaudeBucal}"));
            return Get<List<SaudeBucalDto>>(requestUrl);
        }

        public SaudeBucalDto GetSaudeBucalById(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceSaudeBucal}/{id}"));
            return Get<SaudeBucalDto>(requestUrl);

            #endregion
        }
    }
}