using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
	public partial class DnaApiClient
	{
		private const string ResourceNota = "Notas";

		#region Main Methods

		public Task<long> CreateNota(NotaModel.CreateUpdateNotaCommand command)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceNota}"));
			return Post(requestUrl, command);
		}
		public Task<bool> UpdateNota(int id, NotaModel.CreateUpdateNotaCommand command)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceNota}/{id}"));
			return Put(requestUrl, command);
		}

		public Task<bool> DeleteNota(int id)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceNota}/{id}"));
			return Delete<bool>(requestUrl);
		}

		#endregion

		#region Methods

		public NotaDto GetNotaById(int id)
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceNota}/Nota/{id}"));
			return Get<NotaDto>(requestUrl);
		}
		public List<NotaDto> GetNotasAll()
		{
			var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
				$"{ResourceNota}"));
			return Get<List<NotaDto>>(requestUrl);
		}

		//public NotaDto GetNotaByAlunoIdDisciplinaId(int alunoId, int disciplinaId)
		//{
		//	var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
		//		$"{ResourceNota}/Aluno/{alunoId}/Disciplina/{disciplinaId}"));
		//	return Get<NotaDto>(requestUrl);
		//}

		#endregion


	}
}