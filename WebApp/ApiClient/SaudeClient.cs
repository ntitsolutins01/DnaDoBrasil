using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        private const string ResourceSaude = "Saudes";

        #region Main Methods

        public Task<long> CreateSaude(SaudeModel.CreateUpdateSaudeCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceSaude}"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateSaude(int id, SaudeModel.CreateUpdateSaudeCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceSaude}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteSaude(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceSaude}/{id}"));
            return Delete<bool>(requestUrl);
        }


        #endregion

        #region Methods

        public SaudeDto GetSaudeById(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceSaude}/{id}"));
            return Get<SaudeDto>(requestUrl);
        }
        public List<SaudeDto> GetSaudeAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceSaude}"));
            return Get<List<SaudeDto>>(requestUrl);
        }

        #endregion
    }
}