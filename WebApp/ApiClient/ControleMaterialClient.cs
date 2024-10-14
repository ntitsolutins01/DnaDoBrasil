using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
	public partial class DnaApiClient
	{
		private const string ResourceControleMaterial = "ControlesMateriais";

		#region Main Methods

		public Task<long> CreateControleMaterial(ControleMaterialModel.CreateUpdateControleMaterialCommand command)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceControleMaterial}"));
			return Post(requestUrl, command);
		}
		public Task<bool> UpdateControleMaterial(int id, ControleMaterialModel.CreateUpdateControleMaterialCommand command)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceControleMaterial}/{id}"));
			return Put(requestUrl, command);
		}

		public Task<bool> DeleteControleMaterial(int id)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceControleMaterial}/{id}"));
			return Delete<bool>(requestUrl);
		}

		#endregion

		#region Methods

		public ControleMaterialDto GetControleMaterialById(int id)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceControleMaterial}/ControleMaterial/{id}"));
			return Get<ControleMaterialDto>(requestUrl);
		}
		public List<ControleMaterialDto> GetControlesMateriaisAll()
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceControleMaterial}"));
			return Get<List<ControleMaterialDto>>(requestUrl);
		}

		//public ControleMaterialDto GetControleMaterialByAlunoIdDisciplinaId(int alunoId, int disciplinaId)
		//{
		//	var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
		//		$"{ResourceControleMaterial}/Aluno/{alunoId}/Disciplina/{disciplinaId}"));
		//	return Get<ControleMaterialDto>(requestUrl);
		//}

		#endregion


	}
}