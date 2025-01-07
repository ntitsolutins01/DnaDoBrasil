using System.ComponentModel.DataAnnotations;

namespace WebApp.Dto
{
    public class ControleMaterialEstoqueSaidaDto
    {
        public required int Id { get; set; }
        public required int MaterialId { get; set; }
        public required string TituloMaterial { get; set; }
        public required int Quantidade { get; set; }
        public string? Solicitante { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTimeOffset? Created { get; set; }
    }
}
