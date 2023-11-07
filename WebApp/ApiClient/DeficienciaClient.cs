using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        #region Main Methods

        public Task<long> CreateDeficiencia(DeficienciaModel.CreateUpdateDeficienciaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}"));
            return Post(requestUrl, command);
        }
        public Task<long> UpdateDeficiencia(DeficienciaModel.CreateUpdateDeficienciaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}"));
            return Put(requestUrl, command);
        }
        public List<DeficienciaDto> GetDeficienciaAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}"));
            return Get<List<DeficienciaDto>>(requestUrl);
        }

        public Task<long> DeleteDeficiencia(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}/{id}"));
            return Delete<string>(requestUrl);
        }

        #endregion

        #region Methods

        public DeficienciaDto GetDeficienciaById(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}/{id}"));
            return Get<DeficienciaDto>(requestUrl);
        }
        //public List<DeficienciaDto> GetDeficienciasAll()
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceConfiguracaoSistema}/Deficiencias"));
        //    return Get<List<DeficienciaDto>>(requestUrl);
        //}

        //public Task<bool> ExistUsuarioByIdDeficiencia(string id)
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceConfiguracaoSistema}/ExistUsuarioByIdDeficiencia"));
        //    return PostAsync<bool, string>(requestUrl, id);
        //}

        #endregion
    }
}