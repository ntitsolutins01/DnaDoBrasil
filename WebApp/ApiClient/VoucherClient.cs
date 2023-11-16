using WebApp.Dto;
using WebApp.Models;

namespace WebApp.ApiClient
{
    public partial class DnaApiClient
    {
        private const string ResourceVoucher = "Vouchers";
        #region Main Methods

        public Task<long> CreateVoucher(VoucherModel.CreateUpdateVoucherCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceVoucher}"));
            return Post(requestUrl, command);
        }
        public Task<bool> UpdateVoucher(int id, VoucherModel.CreateUpdateVoucherCommand command)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceVoucher}/{id}"));
            return Put(requestUrl, command);
        }

        public Task<bool> DeleteVoucher(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceVoucher}/{id}"));
            return Delete<bool>(requestUrl);
        }


        #endregion

        #region Methods

        public VoucherDto GetVoucherById(string id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceVoucher}/{id}"));
            return Get<VoucherDto>(requestUrl);
        }
        public List<VoucherDto> GetVoucherAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"{ResourceVoucher}"));
            return Get<List<VoucherDto>>(requestUrl);
        }

        #endregion
    }
}