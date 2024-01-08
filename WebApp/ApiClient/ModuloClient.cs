using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        private const string ResourceModulo = "Modulos";
        #region Main Methods

        public Task<long> CreateModulo(ModuloModel.CreateUpdateModuloCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceModulo}"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateModulo(int id, ModuloModel.CreateUpdateModuloCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceModulo}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteModulo(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceModulo}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public ModuloDto GetModuloById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceModulo}/Modulo/{id}"));
            return Get<ModuloDto>(requestUrl);
        }
        public List<ModuloDto> GetModuloAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceModulo}"));
            return Get<List<ModuloDto>>(requestUrl);
        }

        #endregion
    }
}