using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class QRCodeModel
    {
        [Display(Name = "Informe o texto para QRCode")]
        public string QRCodeText { get; set; }
    }
}
