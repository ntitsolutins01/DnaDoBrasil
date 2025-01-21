using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
	/// <summary>
	/// Contrato Client
	/// </summary>
    public partial class DnaApiClient
    {
        
        private const string ResourceContrato = "Contratos";

        #region Main Methods

        /// <summary>
        /// Inclus�o de Contrato
        /// </summary>
        /// <param name="command">Objeto para inclus�o de Contrato</param>
        /// <returns>Id de Contrato inserido</returns>
        public Task<long> CreateContrato(ContratoModel.CreateUpdateContratoCommand command)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceContrato}"));
			return Post(requestUrl, command);
		}

        /// <summary>
        ///  Altera��o de Contrato
        /// </summary>
        /// <param name="id">Id de altera��o de Contrato</param>
        /// <param name="command">Objeto de altera��o de Contrato</param>
        /// <returns>Retorna true ou false</returns>
        public Task<bool> UpdateContrato(int id, ContratoModel.CreateUpdateContratoCommand command)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceContrato}/{id}"));
			return Put(requestUrl, command);
		}

        /// <summary>
        /// Exclus�o de Contrato
        /// </summary>
        /// <param name="id">Id de exclus�o de Contrato</param>
        /// <returns>Retorna true ou false</returns>
        public Task<bool> DeleteContrato(int id)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceContrato}/{id}"));
			return Delete<bool>(requestUrl);
		}

        #endregion

        #region Methods

        /// <summary>
        /// Busca um �nico Contrato
        /// </summary>
        /// <param name="id">Id de Contrato a ser buscado</param>
        /// <returns>Retorna o objeto de Contrato</returns>
        public ContratoDto GetContratoById(int id)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceContrato}/{id}"));
			return Get<ContratoDto>(requestUrl);
		}

        /// <summary>
        /// Busca todos os Contratos Cadastrados
        /// </summary>
        /// <returns>Retorna a lista de Contratos</returns>
		public List<ContratoDto> GetContratoAll()
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceContrato}"));
			return Get<List<ContratoDto>>(requestUrl);
		}

		#endregion
	}
}