using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceEvento = "Eventos";

		#region Main Methods

		public Task<long> CreateEvento (EventoModel.CreateUpdateEventoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceEvento}"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateEvento (int id, EventoModel.CreateUpdateEventoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceEvento}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteEvento (int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceEvento}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public EventoDto GetEventoById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceEvento}/Evento/{id}"));
            return Get<EventoDto>(requestUrl);
        }
        public List<EventoDto> GetEventosAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceEvento}"));
            return Get<List<EventoDto>>(requestUrl);
        }

        #endregion
    }
}