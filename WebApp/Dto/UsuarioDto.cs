namespace WebApp.Dto
{
    public class UsuarioDto
    {
        public int Id { get; set; }
	    public required string AspNetUserId { get; set; }
	    public required string Nome { get; set; }
	    public required string Cpf { get; set; }
	    public required string Email { get; set; }
	    public required string Telefone { get; set; }
	    public required string AspNetRoleId { get; set; }
	    public int PerfilId { get; set; }
        public PerfilDto Perfil { get; set; }
    }
}
