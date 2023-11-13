using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        #region Main Methods

        public Task<long> CreateQualidadeVida(QualidadeVidaModel.CreateUpdateQualidadeVidaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}"));
            return Post(requestUrl, command);
        }
        public List<QualidadeVidaDto> GetQualidadeVidaAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}"));
            return Get<List<QualidadeVidaDto>>(requestUrl);
        }


        #endregion

        #region Methods

        public QualidadeVidaDto GetQualidadeVidaById(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}/{id}"));
            return Get<QualidadeVidaDto>(requestUrl);
        }
        //public List<QualidadeVidaDto> GetQualidadeVidasAll()
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceConfiguracaoSistema}/QualidadeVidas"));
        //    return Get<List<QualidadeVidaDto>>(requestUrl);
        //}

        //public Task<bool> ExistUsuarioByIdQualidadeVida(string id)
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceConfiguracaoSistema}/ExistUsuarioByIdQualidadeVida"));
        //    return PostAsync<bool, string>(requestUrl, id);
        //}

        #endregion
    }
}