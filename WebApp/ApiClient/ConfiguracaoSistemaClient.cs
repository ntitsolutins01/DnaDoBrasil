using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        private const string ResourceConfiguracaoSistema = "ConfiguracaoSistema";
        #region Main Methods

        public Task<long> CreateModulo(ConfiguracaoSistemaModel.CreateUpdateModuloCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}/Modulo"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateModulo(int id, ConfiguracaoSistemaModel.CreateUpdateModuloCommand command)
        {
	        var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
		        $"{ResourceConfiguracaoSistema}/{id}"));
	        return Put(requestUrl, command);
        }

        public Task<bool> DeleteModulo(int id)
        {
	        var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
		        $"{ResourceConfiguracaoSistema}/{id}"));
	        return Delete<bool>(requestUrl);
        }
        public Task<long> CreateFuncionalidade(FuncionalidadeModel.CreateUpdateFuncionalidadeCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}/Funcionalidade"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateFuncionalidade(int id, FuncionalidadeModel.CreateUpdateFuncionalidadeCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}/Funcionalidade/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteFuncionalidade(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}/Funcionalidade/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public List<ModuloDto> GetModulosAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}/Modulos"));
            return Get<List<ModuloDto>>(requestUrl);
        }
        public ModuloDto GetModuloById(int id)
        {
	        var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
		        $"{ResourceConfiguracaoSistema}/Modulo/{id}"));
	        return Get<ModuloDto>(requestUrl);
        }
        public FuncionalidadeDto GetFuncionalidadeById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}/Funcionalidade/{id}"));
            return Get<FuncionalidadeDto>(requestUrl);
        }
        public List<FuncionalidadeDto> GetFuncionalidadesAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}/Funcionalidades"));
            return Get<List<FuncionalidadeDto>>(requestUrl);
        }

		#endregion
	}
}