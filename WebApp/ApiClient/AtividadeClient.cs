using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceAtividade = "Atividades";

		#region Main Methods

		public Task<long> CreateAtividade (AtividadeModel.CreateUpdateAtividadeCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAtividade }"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateAtividade (int id, AtividadeModel.CreateUpdateAtividadeCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAtividade }/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteAtividade (int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAtividade }/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public AtividadeDto GetAtividadeById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAtividade }/Atividade/{id}"));
            return Get<AtividadeDto>(requestUrl);
        }
        public List<AtividadeDto> GetAtividadesAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAtividade }"));
            return Get<List<AtividadeDto>>(requestUrl);
        }

        #endregion
    }
}