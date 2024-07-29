using System.Collections.Specialized;

namespace WebApp.Dto
{
    public class PerfilDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public ListDictionary Claims { get; set; }
        public string AspNetRoleId { get; set; }
    }
}
