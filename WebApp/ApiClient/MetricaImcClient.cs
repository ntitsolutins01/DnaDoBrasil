using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceMetricaImc = "MetricasImc";

		#region Main Methods

		public Task<long> CreateMetricaImc (MetricaImcModel.CreateUpdateMetricaImcCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceMetricaImc }"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateMetricaImc (int id, MetricaImcModel.CreateUpdateMetricaImcCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceMetricaImc }/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteMetricaImc (int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceMetricaImc }/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public MetricaImcDto GetMetricaImcById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceMetricaImc }/MetricaImc /{id}"));
            return Get<MetricaImcDto>(requestUrl);
        }
        public List<MetricaImcDto> GetMetricasImcAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceMetricaImc }"));
            return Get<List<MetricaImcDto>>(requestUrl);
        }

        #endregion
    }
}