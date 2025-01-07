using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceModuloEad = "ModulosEad";

		#region Main Methods

		public Task<long> CreateModuloEad (ModuloEadModel.CreateUpdateModuloEadCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceModuloEad}"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateModuloEad (int id, ModuloEadModel.CreateUpdateModuloEadCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceModuloEad}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteModuloEad (int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceModuloEad}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public ModuloEadDto GetModuloEadById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceModuloEad}{id}"));
            return Get<ModuloEadDto>(requestUrl);
        }
        public List<ModuloEadDto> GetModulosEadAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceModuloEad}"));
            return Get<List<ModuloEadDto>>(requestUrl);
        }
        public List<ModuloEadDto> GetModulosEadAllByCursoId(int cursoId)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceModuloEad}/Curso/{cursoId}"));
            return Get<List<ModuloEadDto>>(requestUrl);
        }
        #endregion
    }
}