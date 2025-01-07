using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceModalidade = "Modalidades";

		#region Main Methods

		public Task<long> CreateModalidade(ModalidadeModel.CreateUpdateModalidadeCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceModalidade}"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateModalidade(int id, ModalidadeModel.CreateUpdateModalidadeCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceModalidade}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteModalidade(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceModalidade}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public ModalidadeDto GetModalidadeById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceModalidade}/{id}"));
            return Get<ModalidadeDto>(requestUrl);
        }
        public List<ModalidadeDto> GetModalidadeAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceModalidade}"));
            return Get<List<ModalidadeDto>>(requestUrl);
        }

        #endregion
    }
}