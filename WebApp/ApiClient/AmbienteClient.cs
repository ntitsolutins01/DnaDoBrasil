using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceAmbiente = "Ambientes";

		#region Main Methods

		public Task<long> CreateAmbiente(AmbienteModel.CreateUpdateAmbienteCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAmbiente}"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateAmbiente(int id, AmbienteModel.CreateUpdateAmbienteCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAmbiente}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteAmbiente(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAmbiente}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public AmbienteDto GetAmbienteById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAmbiente}/Ambiente/{id}"));
            return Get<AmbienteDto>(requestUrl);
        }
        public List<AmbienteDto> GetAmbienteAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAmbiente}"));
            return Get<List<AmbienteDto>>(requestUrl);
        }

        #endregion
    }
}