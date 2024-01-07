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
        public List<AlunoDto> Alunos { get; set; }
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

        public class CreateUpdateAlunoCommand
        {
            public string Id { get; set; }
            public string Nome { get; set; }
        }
    }

}
