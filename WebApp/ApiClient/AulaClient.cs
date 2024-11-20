using System.Collections;
using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceAula = "Aulas";

		#region Main Methods

		public Task<long> CreateAula (AulaModel.CreateUpdateAulaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAula }"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateAula (int id, AulaModel.CreateUpdateAulaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAula }/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteAula (int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAula }/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public AulaDto GetAulaById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAula }/Aula/{id}"));
            return Get<AulaDto>(requestUrl);
        }
        public List<AulaDto> GetAulasAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAula }"));
            return Get<List<AulaDto>>(requestUrl);
        }
        public List<AulaDto> GetAulasAllByModuloEadId(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAula}/ModuloEad/{id}"));
            return Get<List<AulaDto>>(requestUrl);
        }

        #endregion
    }
}