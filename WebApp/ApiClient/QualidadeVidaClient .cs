using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        private const string ResourceQualidadeVida = "QualidadeDeVidas";
        #region Main Methods

        public Task<long> CreateQualidadeVida(QualidadeVidaModel.CreateUpdateQualidadeVidaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceQualidadeVida}"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateQualidadeVida(int id, QualidadeVidaModel.CreateUpdateQualidadeVidaCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceQualidadeVida}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteQualidadeVida(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceQualidadeVida}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public QualidadeVidaDto GetQualidadeVidaById(int? id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceQualidadeVida}/{id}"));
            return Get<QualidadeVidaDto>(requestUrl);
        }
        public List<QualidadeVidaDto> GetQualidadeVidaAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceQualidadeVida}"));
            return Get<List<QualidadeVidaDto>>(requestUrl);
        }

        #endregion
    }
}