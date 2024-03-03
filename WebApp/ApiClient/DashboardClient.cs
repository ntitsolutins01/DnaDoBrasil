using WebApp.Dto;
using WebApp.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceDashboard = "Dashboards";

		#region Main Methods


		public DashboardIndicadoresDto GetIndicadoresByFilter(DashboardIndicadoresDto searchFilter)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceDashboard}/Indicadores"));
			return GetFiltro(requestUrl, searchFilter);
		}

		#endregion

		#region Methods

		//public Task<DashboardIndicadoresDto>  GetIndicadoresByFilter(SearchDashboardDto searchFilter)
		//      {
		//	var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
		//		$"{ResourceDashboard}/Indicadores"));
		//	var result = Get(requestUrl, searchFilter);
		//}

		#endregion
	}
}