﻿using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using WebApp.Dto;

namespace WebApp.Models
{
	public class EventoModel
	{
        public string EstadoId { get; set; }
        public SelectList ListEstados { get; set; }
        public string MunicipioId { get; set; }
        public SelectList ListMunicipios { get; set; }
        public int LocalidadeId { get; set; }
        public SelectList ListLocalidades { get; set; }
        public string AlunoId { get; set; }
        public SelectList ListAlunos { get; set; }
        public EventoDto Evento { get; set; }
        public List<EventoDto> Eventos { get; set; }
        public List<ControlePresencaDto> ControlesPresencas { get; set; }
        public AlunoIndexDto? Convidado { get; set; }


        public class CreateUpdateEventoCommand
		{
			public int Id { get; set; }
            public int LocalidadeId { get; init; }
            public string? Titulo { get; init; }
            public string? Descricao { get; init; }
            public string? DataEvento { get; init; }
            public bool Status { get; set; } = true;
           
        }
	}

}
