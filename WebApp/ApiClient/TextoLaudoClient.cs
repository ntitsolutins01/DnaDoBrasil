using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceTextoLaudo = "TextosLaudos";

		#region Main Methods

		public Task<long> CreateTextoLaudo(TextoLaudoModel.CreateUpdateTextoLaudoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTextoLaudo}"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateTextoLaudo(int id, TextoLaudoModel.CreateUpdateTextoLaudoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTextoLaudo}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteTextoLaudo(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTextoLaudo}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public TextoLaudoDto GetTextoLaudoById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTextoLaudo}{id}"));
            return Get<TextoLaudoDto>(requestUrl);
        }
        public List<TextoLaudoDto> GetTextosLaudosAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTextoLaudo}"));
            return Get<List<TextoLaudoDto>>(requestUrl);
        }

        #endregion
    }
}