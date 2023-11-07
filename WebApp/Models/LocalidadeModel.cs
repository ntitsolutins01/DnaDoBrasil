using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Dto;

namespace WebApp.Models
{
    public class LocalidadeModel
    {
        public LocalidadeDto Localidade { get; set; }
        public List<LocalidadeDto> Localidades { get; set; }
        public string LocalidadeId { get; set; }
        public SelectList ListLocalidades { get; set; }
        
        public class CreateUpdateLocalidadeCommand
        {
	        public string Id { get; set; }
	        public string Nome { get; set; }
	        public string Descricao { get; set; }
		}
    }

}
