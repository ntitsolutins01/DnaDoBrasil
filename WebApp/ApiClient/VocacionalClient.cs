using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        private const string ResourceVocacional = "Vocacionais";
        #region Main Methods

        public Task<long> CreateVocacional(VocacionalModel.CreateUpdateVocacionalCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceVocacional}"));
            return Post(requestUrl, command);
        }
       

        public Task<bool> UpdateVocacional(int id, VocacionalModel.CreateUpdateVocacionalCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceVocacional}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteVocacional(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceVocacional}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public VocacionalDto GetVocacionalById(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceVocacional}/{id}"));
            return Get<VocacionalDto>(requestUrl);
        }
        public List<VocacionalDto> GetVocacionalAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceVocacional}"));
            return Get<List<VocacionalDto>>(requestUrl);
        }

        #endregion
    }
}