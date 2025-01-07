using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        private const string ResourceQuestionario = "Questionarios";
        #region Main Methods

        public Task<long> CreateQuestionario(QuestionarioModel.CreateUpdateQuestionarioCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceQuestionario}"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateQuestionario(int id, QuestionarioModel.CreateUpdateQuestionarioCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceQuestionario}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteQuestionario(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceQuestionario}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public QuestionarioDto GetQuestionarioById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceQuestionario}{id}"));
            return Get<QuestionarioDto>(requestUrl);
        }
        public List<QuestionarioDto> GetQuestionarioAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceQuestionario}"));
            return Get<List<QuestionarioDto>>(requestUrl);
        }
        public List<QuestionarioDto> GetQuestionarioByTipoLaudo(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceQuestionario}/TipoLaudo/{id}"));
            return Get<List<QuestionarioDto>>(requestUrl);
        }

        #endregion
    }
}