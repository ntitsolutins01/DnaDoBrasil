using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    /// <summary>
    /// Sa�de Client
    /// </summary>
    public partial class DnaApiClient
    {
        private const string ResourceSaude = "Saudes";

        #region Main Methods

        /// <summary>
        /// Inclus�o de Sa�de
        /// </summary>
        /// <param name="command">Objeto de Inclus�o de Sa�de</param>
        /// <returns>Id de Sa�de inserido</returns>
        public Task<long> CreateSaude(SaudeModel.CreateUpdateSaudeCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceSaude}"));
            return Post(requestUrl, command);
        }

        /// <summary>
        /// Altera��o de Sa�de
        /// </summary>
        /// <param name="id">Id de altera��o de Sa�de</param>
        /// <param name="command">Objeto de altera��o de Sa�de</param>
        /// <returns>Retorna true ou false</returns>
        public Task<bool> UpdateSaude(int id, SaudeModel.CreateUpdateSaudeCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceSaude}/{id}"));
            return Put(requestUrl, command);
        }

        /// <summary>
        /// Exclus�o de Sa�de
        /// </summary>
        /// <param name="id">Id de exclus�o de Sa�de</param>
        /// <returns>Retorna true ou false</returns>
        public Task<bool> DeleteSaude(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceSaude}/{id}"));
            return Delete<bool>(requestUrl);
        }


        #endregion

        #region Methods

        /// <summary>
        /// busca uma �nica Sa�de
        /// </summary>
        /// <param name="id">Id de Sa�de a ser buscada</param>
        /// <returns>Retorna o objeto de Sa�de</returns>
        public SaudeDto GetSaudeById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceSaude}/{id}"));
            return Get<SaudeDto>(requestUrl);
        }

        /// <summary>
        /// Todas as Sa�de cadastradas
        /// </summary>
        /// <returns>Retorna a lista de Sa�de</returns>
        public List<SaudeDto> GetSaudeAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceSaude}"));
            return Get<List<SaudeDto>>(requestUrl);
        }

        #endregion
    }
}