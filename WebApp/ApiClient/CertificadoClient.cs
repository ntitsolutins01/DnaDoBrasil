using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceCertificado = "Certificados";

		#region Main Methods

		public Task<long> CreateCertificado (CertificadoModel.CreateUpdateCertificadoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceCertificado }"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateCertificado (int id, CertificadoModel.CreateUpdateCertificadoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceCertificado }/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteCertificado (int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceCertificado }/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public CertificadoDto GetCertificadoById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceCertificado}/{id}"));
            return Get<CertificadoDto>(requestUrl);
        }
        public List<CertificadoDto> GetCertificadosAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceCertificado }"));
            return Get<List<CertificadoDto>>(requestUrl);
        }

        #endregion
    }
}