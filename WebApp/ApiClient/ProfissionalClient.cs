using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {

        private const string ResourceProfissional = "Profissionais";

        #region Main Methods

        public Task<long> CreateProfissional(ProfissionalModel.CreateUpdateProfissionalCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceProfissional}"));
            return Post(requestUrl, command);
        }
		public Task<bool> UpdateProfissional(int id, ProfissionalModel.CreateUpdateProfissionalCommand command)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceProfissional}/{id}"));
			return Put(requestUrl, command);
		}

		public Task<bool> DeleteProfissional(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceProfissional}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public ProfissionalDto GetProfissionalById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceProfissional}/Profissional/{id}"));
            return Get<ProfissionalDto>(requestUrl);
        }
        public List<ProfissionalDto> GetProfissionalAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceProfissional}"));
            return Get<List<ProfissionalDto>>(requestUrl);
        }
        public ProfissionalDto GetProfissionalByCpf(string cpf)
        {
	        var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
		        $"{ResourceProfissional}/Cpf/{cpf}"));
	        return Get<ProfissionalDto>(requestUrl);
        }
        public ProfissionalDto GetProfissionalByEmail(string email)
        {
	        var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
		        $"{ResourceProfissional}/Email/{email}"));
	        return Get<ProfissionalDto>(requestUrl);
        }

		#endregion

	}
}