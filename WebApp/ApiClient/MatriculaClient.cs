using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        #region Main Methods

        public Task<long> CreateMatricula(MatriculaModel.CreateUpdateMatriculaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}"));
            return Post(requestUrl, command);
        }

        public List<MatriculaDto> GetMatriculaAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}"));
            return Get<List<MatriculaDto>>(requestUrl);
        }


        #endregion

        #region Methods

        public MatriculaDto GetMatriculaById(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceConfiguracaoSistema}/{id}"));
            return Get<MatriculaDto>(requestUrl);
        }
        //public List<MatriculaDto> GetMatriculasAll()
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceConfiguracaoSistema}/Matriculas"));
        //    return Get<List<MatriculaDto>>(requestUrl);
        //}

        //public Task<bool> ExistUsuarioByIdMatricula(string id)
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceConfiguracaoSistema}/ExistUsuarioByIdMatricula"));
        //    return PostAsync<bool, string>(requestUrl, id);
        //}

        #endregion
    }
}