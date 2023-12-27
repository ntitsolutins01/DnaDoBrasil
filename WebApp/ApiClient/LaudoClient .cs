using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        private const string ResourceLaudo = "Laudos";
        #region Main Methods

        public Task<long> CreateLaudo(LaudoModel.CreateUpdateLaudoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceLaudo}"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateLaudo(int id, LaudoModel.CreateUpdateLaudoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceLaudo}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteLaudo(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceLaudo}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public LaudoDto GetLaudoById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceLaudo}/Laudo/{id}"));
            return Get<LaudoDto>(requestUrl);
        }
        public List<LaudoDto> GetLaudoAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceLaudo}"));
            return Get<List<LaudoDto>>(requestUrl);
        }

        #endregion
    }
}