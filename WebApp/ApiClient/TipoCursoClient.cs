using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceTipoCurso = "TiposCursos";

		#region Main Methods

		public Task<long> CreateTipoCurso (TipoCursoModel.CreateUpdateTipoCursoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTipoCurso }"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateTipoCurso (int id, TipoCursoModel.CreateUpdateTipoCursoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTipoCurso }/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteTipoCurso (int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTipoCurso }/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public TiposCursoDto GetTipoCursoById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTipoCurso}/{id}"));
            return Get<TiposCursoDto>(requestUrl);
        }
        public List<TiposCursoDto> GetTipoCursosAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTipoCurso}"));
            return Get<List<TiposCursoDto>>(requestUrl);
        }

        #endregion
    }
}