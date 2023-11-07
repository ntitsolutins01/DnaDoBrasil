using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        protected DnaApiClient()
        {
            throw new NotImplementedException();
        }

        private const string ResourceAluno = "Alunos";

        #region Main Methods

        public Task<long> CreateAluno(AlunoModel.CreateUpdateAlunoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAluno}"));
            return Post(requestUrl, command);
        }
        public Task<long> UpdateAluno(AlunoModel.CreateUpdateAlunoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAluno}"));
            return Put(requestUrl, command);
        }
        public List<AlunoDto> GetAlunoAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAluno}"));
            return Get<List<AlunoDto>>(requestUrl);
        }

        public Task<long> DeleteAluno(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAluno}/{id}"));
            return Delete<string>(requestUrl);
        }

        #endregion

        #region Methods

        public AlunoDto GetAlunoById(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceAluno}/{id}"));
            return Get<AlunoDto>(requestUrl);
        }
        //public List<DeficienciaDto> GetDeficienciasAll()
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceAluno}/Deficiencias"));
        //    return Get<List<DeficienciaDto>>(requestUrl);
        //}

        //public Task<bool> ExistUsuarioByIdAluno(string id)
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        $"{ResourceAluno}/ExistUsuarioByIdAluno"));
        //    return PostAsync<bool, string>(requestUrl, id);
        //}

        #endregion
    }
}