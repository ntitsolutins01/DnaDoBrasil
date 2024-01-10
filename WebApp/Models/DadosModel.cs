using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Dto;

namespace WebApp.Models
{
    public class DadosModel
    {
        public DadosDto Dados { get; set; }
        public List<DadosDto> Dadoss { get; set; }
        public string DadosId { get; set; }
        public SelectList ListDadoss { get; set; }
        public SelectList ListAmbientes { get; set; }
        public object AmbienteId { get; set; }
        public List<AmbienteDto> Ambientes { get; set; }

        public class CreateUpdateDadosCommand
        {
            public int Id { get; set; }
            public int? AspNetUserId { get; set; }
            public int? MunicipioId { get; set; }
            public  string? Nome { get; set; }
            public  string? Email { get; set; }
            public  string? Sexo { get; set; }
            public string? DtNascimento { get; set; }
            public string? NomeMae { get; set; }
            public string? NomePai { get; set; }
            public string? Cpf { get; set; }
            public string? Telefone { get; set; }
            public string? Celular { get; set; }
            public string? Cep { get; set; }
            public string? Endereco { get; set; }
            public int? Numero { get; set; }
            public string? Bairro { get; set; }
            public string? RedeSocial { get; set; }
            public string? Url { get; set; }
            public bool Status { get; set; }
            public bool Habilitado { get; set; }
            public int ?DeficienciasId { get; set; } = new();
            public int? AmbientesId { get; set; } = new();
            public int? ParceiroId { get; set; }
            public int? Etnia { get; set; }
            public int? ContratosId { get; set; }
            public int MatriculaId { get; set; }
            public int VoucherId { get; set; }
            public int DependenciaId { get; set; }
            public int LaudosId { get; set; }
            public string? AmbientesIds { get; set; }
        }
    }

}
