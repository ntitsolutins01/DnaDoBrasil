using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceContrato = "Contratos";

		#region Main Methods

		public Task<long> CreateContrato(ContratoModel.CreateUpdateContratoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceContrato}"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateContrato(int id, ContratoModel.CreateUpdateContratoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceContrato}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteContrato(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceContrato}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public List<ContratoDto> GetContratoAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceContrato}"));
            return Get<List<ContratoDto>>(requestUrl);
        }
        public ContratoDto GetContratoById(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceContrato}/{id}"));
            return Get<ContratoDto>(requestUrl);
        }
        //public List<ContratoDto> GetContratosAll()
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceContrato}/Contratos"));
        //    return Get<List<ContratoDto>>(requestUrl);
        //}

        //public Task<bool> ExistUsuarioByIdContrato(string id)
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceContrato}/ExistUsuarioByIdContrato"));
        //    return PostAsync<bool, string>(requestUrl, id);
        //}

        #endregion
    }
}