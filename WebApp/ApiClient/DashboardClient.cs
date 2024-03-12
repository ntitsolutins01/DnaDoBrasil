using WebApp.Dto;
using WebApp.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceDashboard = "Dashboards";

		#region Main Methods


		public DashboardDto GetDashboardByFilter(DashboardDto searchFilter)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceDashboard}"));
			return GetFiltro(requestUrl, searchFilter);
		}

        public DashboardDto GraficoControlePresencasByFilter(DashboardDto searchFilter)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceDashboard}/GraficoControlePresencas"));
            return GetFiltro(requestUrl, searchFilter);
        }
        #endregion

        #region Methods


        #endregion
    }
}