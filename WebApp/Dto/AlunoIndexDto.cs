namespace WebApp.Dto
{
    public class AlunoIndexDto
    {
        public int Id { get; set; }
        public  string AspNetUserId { get; set; }
        public  string Nome { get; set; }
        public  string Email { get; set; }
        public  string DtNascimento { get; set; }
        public bool Status { get; set; }

        #region SearchFilter
        public string Sexo { get; set; }
        public string FomentoId { get; set; }
        public string Estado { get; set; }
        public string MunicipioId { get; set; }
        public string LocalidadeId { get; set; }
        public string DeficienciaId { get; set; }
        public string Etnia { get; set; }
        public string ProfissionalId { get; set; }

        #endregion
    }
}
