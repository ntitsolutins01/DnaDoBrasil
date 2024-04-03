namespace WebApp.Dto
{
    public class UsuarioDto
    {
        public int Id { get; set; }
	    public  string? AspNetUserId { get; set; }
	    public  string? Nome { get; set; }
	    public  string? CpfCnpj { get; set; }
        public string TipoPessoa { get; set; }
        public  string? Email { get; set; }
        public  string? Url { get; set; }
	    public  string? AspNetRoleId { get; set; }
	    public int PerfilId { get; set; }
        public PerfilDto Perfil { get; set; }
        public string? Uf { get; set; }
        public int? MunicipioId { get; set; }
    }
}
