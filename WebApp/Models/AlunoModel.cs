using System.Collections.Specialized;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Data;
using WebApp.Dto;

namespace WebApp.Models
{
    public class AlunoModel
    {
        public AlunoDto Aluno { get; set; }
        public List<AlunoDto>? Alunos { get; set; }
        public string AlunoId { get; set; }
        public SelectList ListAlunos { get; set; }
        public SelectList ListDeficiencias { get; set; }
        public int DeficienciaId { get; set; }
        public string EstadoId { get; set; }
        public SelectList ListEstados { get; set; }
        public string MunicipioId { get; set; }
        public SelectList ListMunicipios { get; set; }
        public SelectList ListAmbientes { get; set; }
        public object AmbienteId { get; set; }
        public string FomentoId { get; set; }
        public SelectList ListFomentos { get; set; }
        public int LocalidadeId { get; set; }
        public SelectList ListLocalidades { get; set; }
        public string SerieId { get; set; }
        public SelectList ListSeries { get; set; }
        public SelectList ListProfissionais { get; set; }
        public string ProfissionalId { get; set; }
        public DependenciaDto Dependecia { get; set; }
        public List<AmbienteDto>? Ambientes { get; set; }
        public MatriculaDto Matricula { get; set; }
        public SelectList ListEtnias { get; set; }
        public string EtniaId { get; set; }


        public class CreateUpdateDadosAlunoCommand
        {
            public int Id { get; set; }
            public int? AspNetUserId { get; set; }
            public int? MunicipioId { get; set; }
            public string? Nome { get; set; }
            public string? Email { get; set; }
            public string? Sexo { get; set; }
            public string? DtNascimento { get; set; }
            public string? NomeMae { get; set; }
            public string? NomePai { get; set; }
            public string? Cpf { get; set; }
            public string? Telefone { get; set; }
            public string? Celular { get; set; }
            public string? Cep { get; set; }
            public string? Endereco { get; set; }
            public string? Numero { get; set; }
            public string? Bairro { get; set; }
            public string? RedeSocial { get; set; }
            public string? Url { get; set; }
            public bool Status { get; set; }
            public bool Habilitado { get; set; }
            public int? DeficienciasId { get; set; } = new();
            public int? AmbientesId { get; set; } = new();
            public int? ProfissionalId { get; set; } = new();
            public int? ParceiroId { get; set; }
            public string? Etnia { get; set; }
            public int? ContratosId { get; set; }
            public int MatriculaId { get; set; }
            public int VoucherId { get; set; }
            public int DependenciaId { get; set; }
            public int LaudosId { get; set; }
            public string? AmbientesIds { get; set; }
            public string? DeficienciasIds { get; set; }
            public int? LocalidadeId { get; set; }
            public string? EstadoId { get; set; }
            public string? endereco { get; set; }
            public string? AreaId { get; set; }
            public string? CPF { get; set; }
            public string? DeficienciaId { get; set; }
            public string? NomecompletodoAluno { get; set; }
            public string? NomecompletodaMae { get; set; }
            public string? NomecompletodoResponsável { get; set; }
        }
    }

}
