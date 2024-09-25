using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceCurso = "Cursos";

		#region Main Methods

		public Task<long> CreateCurso (CursoModel.CreateUpdateCursoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceCurso }"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateCurso (int id, CursoModel.CreateUpdateCursoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceCurso }/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteCurso (int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceCurso }/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public CursoDto GetCursoById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceCurso }/{id}"));
            return Get<CursoDto>(requestUrl);
        }
        public List<CursoDto> GetCursosAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceCurso }"));
            return Get<List<CursoDto>>(requestUrl);
        }
        public List<CursoDto> GetCursosAllByTipoCursoId(int tipoCursoId)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceCurso }/TipoCurso/{tipoCursoId}"));
            return Get<List<CursoDto>>(requestUrl);
        }

        #endregion
    }
}