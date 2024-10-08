using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        private const string ResourceTalentoEsportivo = "TalentosEsportivos";
        #region Main Methods

        public Task<long> CreateTalentoEsportivo(TalentoEsportivoModel.CreateUpdateTalentoEsportivoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTalentoEsportivo}"));
            return Post(requestUrl, command);
        }

        public Task<bool> UpdateTalentoEsportivo(int id, TalentoEsportivoModel.CreateUpdateTalentoEsportivoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTalentoEsportivo}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteTalentoEsportivo(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTalentoEsportivo}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public TalentoEsportivoDto GetTalentoEsportivoByAluno(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"Laudos/TalentoEsportivo/Aluno/{id}"));
            return Get<TalentoEsportivoDto>(requestUrl);
        }
        public List<TalentoEsportivoDto> GetTalentoEsportivoAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTalentoEsportivo}"));
            return Get<List<TalentoEsportivoDto>>(requestUrl);
        }

        #endregion
    }
}