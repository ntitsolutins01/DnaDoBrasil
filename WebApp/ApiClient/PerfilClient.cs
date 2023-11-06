using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        private const string ResourcePerfil = "Perfis";
        private const string ResourceConfiguracaoSistema = "ConfiguracaoSistema";

        #region Main Methods

        public Task<long> CreatePerfil(PerfilModel.CreateCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourcePerfil}"));
            return Post(requestUrl, command);
        }
        public List<PerfilDto> GetPerfilAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourcePerfil}"));
            return Get<List<PerfilDto>>(requestUrl);
        }

        //public Task<long> DeletePerfil(DeletePerfilCommand command)
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourcePerfil}/DeletePerfil"));
        //    return Post(requestUrl, command);
        //}

        //public List<PerfilDto> GetPerfilAll(PerfilModel.CreateCommand command)
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourcePerfil}", ""));
        //    return Post<List<PerfilDto>>(requestUrl, command);
        //}

        #endregion

        #region Methods

        //public PerfilDto GetPerfilById(string id)
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourcePerfil}/GetPerfilById/{id}"));
        //    return Get<PerfilDto>(requestUrl);
        //}
        public List<ModuloDto> GetModuloAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}/Modulos"));
            return Get<List<ModuloDto>>(requestUrl);
        }

        //public Task<bool> ExistUsuarioByIdPerfil(string id)
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourcePerfil}/ExistUsuarioByIdPerfil"));
        //    return PostAsync<bool, string>(requestUrl, id);
        //}

        #endregion
    }
}