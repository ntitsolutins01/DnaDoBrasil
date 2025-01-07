using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceControlesPresencas = "ControlesPresencas";

		#region Main Methods

		public Task<long> CreateControlePresenca(ControlePresencaModel.CreateUpdateControlePresencaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceControlesPresencas}"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateControlePresenca(int id, ControlePresencaModel.CreateUpdateControlePresencaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceControlesPresencas}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteControlePresenca(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceControlesPresencas}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public ControlePresencaDto GetControlePresencaById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceControlesPresencas}{id}"));
            return Get<ControlePresencaDto>(requestUrl);
        }
        public List<ControlePresencaDto> GetControlePresencaByAlunoId(int alunoId)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceControlesPresencas}Aluno/{alunoId}"));
            return Get<List<ControlePresencaDto>>(requestUrl);
        }
        public PaginatedListDto<ControlePresencaDto> GetControlesPresencasAll(int pageNumber = 1, int pageSize = 10)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceControlesPresencas}"), $"PageNumber={pageNumber}&PageSize={pageSize}");
            return Get<PaginatedListDto<ControlePresencaDto>>(requestUrl);
        }
        public List<ControlePresencaDto> GetControlesPresencasByEventoId(int eventoId)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceControlesPresencas}Evento/{eventoId}"));
            return Get<List<ControlePresencaDto>>(requestUrl);
        }
        public Task<ControlesPresencasFilterDto?> GetControlesPresencasByFilter(ControlesPresencasFilterDto searchFilter)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceControlesPresencas}/Filter"));
            return GetFiltro(requestUrl, searchFilter);
        }

        #endregion

        
    }
}