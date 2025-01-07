using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceCategoria = "Categorias";

		#region Main Methods

		public Task<long> CreateCategoria (CategoriaModel.CreateUpdateCategoriaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceCategoria }"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateCategoria (int id, CategoriaModel.CreateUpdateCategoriaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceCategoria }/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteCategoria (int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceCategoria }/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public CategoriaDto GetCategoriaById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceCategoria}/{id}"));
            return Get<CategoriaDto>(requestUrl);
        }
        public List<CategoriaDto> GetCategoriasAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceCategoria}"));
            return Get<List<CategoriaDto>>(requestUrl);
        }

        #endregion
    }
}