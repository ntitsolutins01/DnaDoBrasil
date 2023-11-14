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
        public List<AmbienteDto> GetAmbienteAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAmbiente}"));
            return Get<List<AmbienteDto>>(requestUrl);
        }

        #endregion

        #region Methods

        public AmbienteDto GetAmbienteById(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAmbiente}/{id}"));
            return Get<AmbienteDto>(requestUrl);
        }


        //public List<DeficienciaDto> GetDeficienciasAll()
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceAmbiente}/Deficiencias"));
        //    return Get<List<DeficienciaDto>>(requestUrl);
        //}

        //public Task<bool> ExistUsuarioByIdAmbiente(string id)
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceAmbiente}/ExistUsuarioByIdAmbiente"));
        //    return PostAsync<bool, string>(requestUrl, id);
        //}

        #endregion
    }
}