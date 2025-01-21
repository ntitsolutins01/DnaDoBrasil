using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    /// <summary>
    /// Serie Client
    /// </summary>
    public partial class DnaApiClient
    {
        private const string ResourceSerie = "Series";
        #region Main Methods

        /// <summary>
        /// Inclusão de Série
        /// </summary>
        /// <param name="command">Objeto de inclusão de Série</param>
        /// <returns>Id de Serie inserido</returns>
        public Task<long> CreateSerie(SerieModel.CreateUpdateSerieCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceSerie}"));
            return Post(requestUrl, command);
        }

        /// <summary>
        /// Alteração de Série
        /// </summary>
        /// <param name="id">Id de alteração de Série</param>
        /// <param name="command">Objeto de alteração de Série</param>
        /// <returns>Retorna true ou false</returns>
        public Task<bool> UpdateSerie(int id, SerieModel.CreateUpdateSerieCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceSerie}/{id}"));
            return Put(requestUrl, command);
        }

        /// <summary>
        /// Exclusão de Série
        /// </summary>
        /// <param name="id">Id de Exclusão de Série</param>
        /// <returns>Retorna true ou false</returns>
        public Task<bool> DeleteSerie(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceSerie}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Busca uma única Série
        /// </summary>
        /// <param name="id">Id de Série a ser buscada</param>
        /// <returns>Retorna o objeto de Série</returns>
        public SerieDto GetSerieById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceSerie}/{id}"));
            return Get<SerieDto>(requestUrl);
        }

        /// <summary>
        /// Busca todas as Série cadastradas
        /// </summary>
        /// <returns>Retorna a lista de Série</returns>
        public List<SerieDto> GetSerieAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceSerie}"));
            return Get<List<SerieDto>>(requestUrl);
        }

        #endregion
    }
}