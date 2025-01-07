using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        private const string ResourceDisciplina = "Disciplinas";
        #region Main Methods

        public Task<long> CreateDisciplina(DisciplinaModel.CreateUpdateDisciplinaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceDisciplina}"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateDisciplina(int id, DisciplinaModel.CreateUpdateDisciplinaCommand command)
        {
	        var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
		        $"{ResourceDisciplina}/{id}"));
	        return Put(requestUrl, command);
        }
        public Task<bool> DeleteDisciplina(int id)
        {
	        var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
		        $"{ResourceDisciplina}/{id}"));
	        return Delete<bool>(requestUrl);
        }
        
        #endregion

        #region Methods

        public List<DisciplinaDto> GetDisciplinasAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceDisciplina}"));
            return Get<List<DisciplinaDto>>(requestUrl);
        }
        public DisciplinaDto GetDisciplinaById(int id)
        {
	        var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
		        $"{ResourceDisciplina}/{id}"));
	        return Get<DisciplinaDto>(requestUrl);
        }
		#endregion
	}
}