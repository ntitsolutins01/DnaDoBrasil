namespace WebApp.Dto
{
    public class VoucherDto
    {
        public string Id { get; set; }
        public int LocalId { get; set; }
        public string? Descricao { get; set; }
        public string? Turma { get; set; }
        public string? Serie { get; set; }
        public required int AlunoId { get; set; }

    }
}
