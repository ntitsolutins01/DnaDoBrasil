using System.Collections.Specialized;

namespace WebApp.Dto
{
    public class PerfilDto
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public ListDictionary Claims { get; set; }
    }
}
