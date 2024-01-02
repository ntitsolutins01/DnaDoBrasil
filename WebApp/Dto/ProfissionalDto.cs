﻿namespace WebApp.Dto
{
    public class ProfissionalDto
    {
        public int Id { get; set; }
        public string? AspNetUserId { get; set; }
        public required string Nome { get; set; }
        public DateTime? DtNascimento { get; set; }
        public required string Email { get; set; }
        public string? Sexo { get; set; }
        public required string CpfCnpj { get; set; }
        public string? Telefone { get; set; }
        public string? Celular { get; set; }
        public string? Endereco { get; set; }
        public int? Numero { get; set; }
        public string? Cep { get; set; }
        public string? Bairro { get; set; }
        //public List<Ambiente>? Ambientes { get; set; }
        //public List<Contrato>? Contratos { get; set; }
        public int EstadoId { get; set; }
        public string? Uf { get; set; }
        public int MunicipioId { get; set; }
        public bool Status { get; set; }
        public bool Habilitado { get; set; }
        public List<AmbienteDto>? Ambientes { get; set; }
    }
}
