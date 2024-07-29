using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        private const string ResourceMatricula = "Matriculas";
        #region Main Methods

        public Task<long> CreateMatricula(MatriculaModel.CreateUpdateMatriculaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceMatricula}"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateMatricula(int id, MatriculaModel.CreateUpdateMatriculaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceMatricula}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteMatricula(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceMatricula}/{id}"));
            return Delete<bool>(requestUrl);
        }


        #endregion

        #region Methods

        public MatriculaDto GetMatriculaById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceMatricula}/{id}"));
            return Get<MatriculaDto>(requestUrl);
        }
        public List<MatriculaDto> GetMatriculaAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceMatricula}"));
            return Get<List<MatriculaDto>>(requestUrl);
        }

        #endregion
    }
}