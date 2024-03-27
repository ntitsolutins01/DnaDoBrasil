using WebApp.Dto;
using WebApp.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceDashboard = "Dashboards";

        #region Main Methods


        public Task<DashboardDto?> GetGraficosPizzaBarraByFilter(DashboardDto searchFilter)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceDashboard}/GraficosPizzaBarra"));
            return GetFiltro(requestUrl, searchFilter);
        }
        public Task<DashboardDto?> GetIndicadoresAlunosByFilter(DashboardDto searchFilter)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceDashboard}/Indicadores"));
			return GetFiltro(requestUrl, searchFilter);
		}
        public Task<DashboardDto?> GetControlePresencaByFilter(DashboardDto searchFilter)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceDashboard}/ControlePresenca"));
			return GetFiltro(requestUrl, searchFilter);
		}
        public Task<DashboardDto?> GetLaudosPeriodoByFilter(DashboardDto searchFilter)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceDashboard}/LaudosPeriodo"));
			return GetFiltro(requestUrl, searchFilter);
		}
        public Task<DashboardDto?> GetStatusLaudosByFilter(DashboardDto searchFilter)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceDashboard}/StatusLaudos"));
			return GetFiltro(requestUrl, searchFilter);
		}
        public Task<DashboardDto?> GetEvolutivoByFilter(DashboardDto searchFilter)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceDashboard}/Evolutivo"));
			return GetFiltro(requestUrl, searchFilter);
		}

        //public DashboardDto GraficoControlePresencasByFilter(DashboardDto searchFilter)
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceDashboard}/GraficoControlePresencas"));
        //    return GetFiltro(requestUrl, searchFilter);
        //}
        #endregion

        #region Methods


        #endregion
    }
}