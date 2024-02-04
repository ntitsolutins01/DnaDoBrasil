using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceDashboard = "Dashboards";

		#region Main Methods

        #endregion

        #region Methods

        public DashboardIndicadoresDto GetIndicadoresByFilter()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceDashboard}/Indicadores"));
            return Get<DashboardIndicadoresDto>(requestUrl);
        }

        #endregion
    }
}