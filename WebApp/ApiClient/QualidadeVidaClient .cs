using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    /// <summary>
    /// Qualidade de Vida Client
    /// </summary>
    public partial class DnaApiClient
    {
        private const string ResourceQualidadeVida = "QualidadeDeVidas";
        #region Main Methods

        /// <summary>
        /// Inclus�o de Qualidade de Vida
        /// </summary>
        /// <param name="command">Objeto de inclus�o de Qualidade de Vida</param>
        /// <returns>Id de Qualidade de Vida  inserido</returns>
        public Task<long> CreateQualidadeVida(QualidadeVidaModel.CreateUpdateQualidadeVidaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceQualidadeVida}"));
            return Post(requestUrl, command);
        }

        /// <summary>
        /// Altera��o de Qualidade de Vida
        /// </summary>
        /// <param name="id">Id de altera��o de Qualidade de Vida</param>
        /// <param name="command">Objeto de altera��o de Qualidade de Vida</param>
        /// <returns>Retorna true ou false</returns>
        public Task<bool> UpdateQualidadeVida(int id, QualidadeVidaModel.CreateUpdateQualidadeVidaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceQualidadeVida}/{id}"));
            return Put(requestUrl, command);
        }
        /// <summary>
        /// Exclus�o de Qualidade de Vida 
        /// </summary>
        /// <param name="id">Id de exclus�o de Qualidade de Vida </param>
        /// <returns>Retorna true ou false</returns>
        public Task<bool> DeleteQualidadeVida(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceQualidadeVida}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Busca uma �nica Qualidade de Vida
        /// </summary>
        /// <param name="id">Id de Qualidade de Vida a ser buscada</param>
        /// <returns>Retorna o objeto de Qualidade de Vida</returns>
        public QualidadeVidaDto GetQualidadeVidaById(int? id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceQualidadeVida}/{id}"));
            return Get<QualidadeVidaDto>(requestUrl);
        }

        /// <summary>
        /// Busca todas as Qualidades de Vida cadastradas
        /// </summary>
        /// <returns>Retorna a lista de Qualidade de Vida</returns>
        public List<QualidadeVidaDto> GetQualidadeVidaAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceQualidadeVida}"));
            return Get<List<QualidadeVidaDto>>(requestUrl);
        }

        #endregion
    }
}