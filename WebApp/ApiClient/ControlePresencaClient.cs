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
                $"{ResourceControlesPresencas}/ControlePresenca/{id}"));
            return Get<ControlePresencaDto>(requestUrl);
        }
        public ControlePresencaDto GetControlePresencaByAlunoId(int alunoId)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceControlesPresencas}/ControlePresenca/Aluno/{alunoId}"));
            return Get<ControlePresencaDto>(requestUrl);
        }
        public List<ControlePresencaDto> GetControlesPresencasAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceControlesPresencas}"));
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