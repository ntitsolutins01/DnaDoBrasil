using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        private const string ResourceDependencia = "Dependencias";
        #region Main Methods

        public Task<long> CreateDependencia(DependenciaModel.CreateUpdateDependenciaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceDependencia}"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateDependencia(int id, DependenciaModel.CreateUpdateDependenciaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceDependencia}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteDependencia(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceDependencia}/{id}"));
            return Delete<bool>(requestUrl);
        }


        #endregion

        #region Methods

        public DependenciaDto GetDependenciaById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceDependencia}/{id}"));
            return Get<DependenciaDto>(requestUrl);
        }
        public List<DependenciaDto> GetDependenciasAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceDependencia}"));
            return Get<List<DependenciaDto>>(requestUrl);
        }

        #endregion
    }
}