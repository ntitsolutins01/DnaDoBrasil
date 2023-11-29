﻿using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Dto;

namespace WebApp.Models
{
    public class ProfissionalModel
    {
        public ProfissionalDto Profissional { get; set; }
        public List<ProfissionalDto> Profissionais { get; set; }
        public string ProfissionalId { get; set; }
        public SelectList ListProfissionals { get; set; }

        public class CreateUpdateProfissionalCommand
        {
            public int Id { get; set; }
            public  int AspNetUserId { get; set; }
            public  string Nome { get; set; }
            public DateTime DtNascimento { get; set; }
            public  string Email { get; set; }
            public  string Sexo { get; set; }
            public  string Cpf { get; set; }
            public string? Telefone { get; set; }
            public string? Celular { get; set; }
            public string? Endereco { get; set; }
            public int? Numero { get; set; }
            public string? Cep { get; set; }
            public string? Bairro { get; set; }
            public bool Status { get; set; } = true;
            public int? MunicipioId { get; set; }
            public bool? Habilitado { get; set; }
        }
    }

}