using WebApp.Dto;
using WebApp.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceEvento = "Eventos";
	    private const string ResourceFotoEvento = "FotosEvento";

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
        public Task<long> CreateFotoEvento(List<CreateFotoEventoDto> list)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceFotoEvento}"));
			return Post(requestUrl, list);
		}

		#endregion

		#region Methods

		public EventoDto GetEventoById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceEvento}{id}"));
            return Get<EventoDto>(requestUrl);
        }
        public List<FotoEventoDto> GetFotosAllByEventoId(int eventoId)
        {
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceFotoEvento}/Fotos/{eventoId}"));
			return Get<List<FotoEventoDto>>(requestUrl);
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