using NuGet.Protocol.Core.Types;
using WebApp.Dto;
using WebApp.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceDashboardEad = "DashboardsEad";

        #region Main Methods

        public Task<DashboardEadDto?> GetIndicadoresEadAlunosByFilter(DashboardEadDto searchFilter)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceDashboardEad}/Indicadores"));
			return GetFiltro(requestUrl, searchFilter);
		}

        #endregion

    }
}