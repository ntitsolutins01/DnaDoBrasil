using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
	{
		private const string ResourceDivisaoAdministrativa = "DivisaoAdministrativa";

		#region Methods

		public MunicipioDto GetMunicipiosByid(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceDivisaoAdministrativa}/{id}"));
            return Get<MunicipioDto>(requestUrl);
        }
        public List<EstadoDto> GetEstadosAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceDivisaoAdministrativa}/Estados"));
            return Get<List<EstadoDto>>(requestUrl);
        }

        #endregion
    }
}