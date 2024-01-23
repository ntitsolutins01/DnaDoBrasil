using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        private const string ResourceDados = "Alunos";

        #region Main Methods

        public Task<long> CreateDados(AlunoModel.CreateUpdateDadosAlunoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceDados}"));
            return Post(requestUrl, command);
        }

        public Task<bool> UpdateDados(int id, AlunoModel.CreateUpdateDadosAlunoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceDados}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteDados(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceDados}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public List<DadosDto> GetDadosAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceDados}"));
            return Get<List<DadosDto>>(requestUrl);
        }

        public DadosDto GetDadosById(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceDados}/{id}"));
            return Get<DadosDto>(requestUrl);

            #endregion
        }
    }
}