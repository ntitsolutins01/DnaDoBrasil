using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Dto;

namespace WebApp.Models
{
    public class MatriculaModel
    {
        public MatriculaDto Matricula { get; set; }
        public List<MatriculaDto> Matriculas { get; set; }
        public string MatriculaId { get; set; }
        public SelectList ListMatriculas { get; set; }

        public class CreateUpdateMatriculaCommand
        {
            public int Id { get; set; }
            public DateTime DtVencimentoParq { get; set; }
            public DateTime DtVencimentoAtestadoMedico { get; set; }
            public string? NomeResponsavel1 { get; set; }
            public string? ParentescoResponsavel1 { get; set; }
            public string? CpfResponsavel1 { get; set; }
            public string? NomeResponsavel2 { get; set; }
            public string? ParentescoResponsavel2 { get; set; }
            public string? CpfResponsavel2 { get; set; }
            public string? NomeResponsavel3 { get; set; }
            public string? ParentescoResponsavel3 { get; set; }
            public string? CpfResponsavel3 { get; set; }
            public int LocalId { get; set; }
            public required int AlunoId { get; set; }
        }
    }

}
