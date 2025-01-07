using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        private const string ResourceTiposLaudo = "TiposLaudos";
        #region Main Methods

        public Task<long> CreateTiposLaudo(TiposLaudoModel.CreateUpdateTiposLaudoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTiposLaudo}"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateTiposLaudo(int id, TiposLaudoModel.CreateUpdateTiposLaudoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTiposLaudo}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteTiposLaudo(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTiposLaudo}/{id}"));
            return Delete<bool>(requestUrl);
        }


        #endregion

        #region Methods

        public TiposLaudoDto GetTiposLaudoById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTiposLaudo}{id}"));
            return Get<TiposLaudoDto>(requestUrl);
        }
        public List<TiposLaudoDto> GetTiposLaudoAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTiposLaudo}"));
            return Get<List<TiposLaudoDto>>(requestUrl);
        }

        #endregion
    }
}