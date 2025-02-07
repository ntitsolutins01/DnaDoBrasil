﻿namespace WebApp.Dto
{
    public class MatriculaDto
    {
        public string Id { get; set; }
        public string? DtVencimentoParq { get; set; }
        public string? DtVencimentoAtestadoMedico { get; set; }
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
        public AlunoDto Aluno { get; set; }

    }
}
