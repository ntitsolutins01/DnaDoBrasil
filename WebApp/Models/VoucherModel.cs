using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Dto;

namespace WebApp.Models
{
    public class VoucherModel
    {
        public VoucherDto Voucher { get; set; }
        public List<VoucherDto> Vouchers { get; set; }
        public string VoucherId { get; set; }
        public SelectList ListVouchers { get; set; }

        public class CreateUpdateVoucherCommand
        {
            public int Id { get; set; }
            public int? LocalId { get; set; }
            public string? Descricao { get; set; }
            public string? Turma { get; set; }
            public string? Serie { get; set; }
            public int? AlunoId { get; set; }
        }
    }

}
