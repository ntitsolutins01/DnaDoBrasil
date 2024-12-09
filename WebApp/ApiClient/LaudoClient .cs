using System.Drawing.Printing;
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
                $"{ResourceLaudo}/{id}"));
            return Get<LaudoDto>(requestUrl);
        }
        public LaudoDto GetLaudoByAluno(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceLaudo}/Aluno/{id}"));
            return Get<LaudoDto>(requestUrl);
        }
        public PaginatedListDto<LaudoDto> GetLaudosAll(int pageNumber = 1, int pageSize = 10)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceLaudo}"), $"PageNumber={pageNumber}&PageSize={pageSize}");
            return Get<PaginatedListDto<LaudoDto>>(requestUrl);
        }
        public EncaminhamentoDto GetEncaminhamentoBySaudeId(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceLaudo}/Encaminhamento/Saude/{id}"));
            return Get<EncaminhamentoDto>(requestUrl);
        }
        public List<EncaminhamentoDto> GetEncaminhamentoByQualidadeDeVidaId(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceLaudo}/Encaminhamento/QualidadeDeVida/{id}"));
            return Get<List<EncaminhamentoDto>>(requestUrl);
        }
        public List<EncaminhamentoDto> GetEncaminhamentoByVocacional()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceLaudo}/Encaminhamentos/Vocacional"));
            return Get<List<EncaminhamentoDto>>(requestUrl);
        }
        public DesempenhoDto GetDesempenhoByAluno(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceLaudo}/Desempenho/{id}"));
            return Get<DesempenhoDto>(requestUrl);
        }
        public Task<LaudosFilterDto?> GetLaudosByFilter(LaudosFilterDto searchFilter)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceLaudo}/Filter"));
            return GetFiltro(requestUrl, searchFilter);
        }

        #endregion

    }
}