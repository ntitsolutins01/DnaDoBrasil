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
        public List<AlunoDto> Aunos { get; set; }
        public string AlunoId { get; set; }
        public SelectList ListAlunos { get; set; }

        public class CreateUpdateAlunoCommand
        {
            public string Id { get; set; }
            public string Nome { get; set; }
        }
    }

}
