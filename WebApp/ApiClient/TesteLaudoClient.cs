using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
	    private const string ResourceTesteLaudo = "TesteLaudos";

		#region Main Methods

		public Task<long> CreateTesteLaudo(TesteLaudoModel.CreateUpdateTesteLaudoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTesteLaudo}"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateTesteLaudo(int id, TesteLaudoModel.CreateUpdateTesteLaudoCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTesteLaudo}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteTesteLaudo(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTesteLaudo}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public TesteLaudoDto GetTesteLaudoById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTesteLaudo}/TesteLaudo/{id}"));
            return Get<TesteLaudoDto>(requestUrl);
        }
        public List<TesteLaudoDto> GetTesteLaudoAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceTesteLaudo}"));
            return Get<List<TesteLaudoDto>>(requestUrl);
        }

        #endregion
    }
}