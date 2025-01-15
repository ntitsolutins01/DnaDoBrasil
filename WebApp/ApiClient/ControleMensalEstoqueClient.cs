using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        private const string ResourceControleMensalEstoque = "ControlesMensaisEstoque";

        #region Main Methods

        public Task<long> CreateControleMensalEstoque(ControleMensalEstoqueModel.CreateUpdateControleMensalEstoqueCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceControleMensalEstoque}"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateControleMensalEstoque(int id, ControleMensalEstoqueModel.CreateUpdateControleMensalEstoqueCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceControleMensalEstoque}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteControleMensalEstoque(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceControleMensalEstoque}/{id}"));
            return Delete<bool>(requestUrl);
        }

        #endregion

        #region Methods

        public ControleMensalEstoqueDto GetControleMensalEstoqueById(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceControleMensalEstoque}/ControleMensalEstoque/{id}"));
            return Get<ControleMensalEstoqueDto>(requestUrl);
        }
        public List<ControleMensalEstoqueDto> GetControlesMensaisEstoqueAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceControleMensalEstoque}"));
            return Get<List<ControleMensalEstoqueDto>>(requestUrl);
        }
        public List<ControleMensalEstoqueDto> GetControlesMensaisEstoqueByMaterialId(int materialId)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceControleMensalEstoque}/ControleMensalEstoque/{materialId}"));
            return Get<List<ControleMensalEstoqueDto>>(requestUrl);
        }
        #endregion
    }
}