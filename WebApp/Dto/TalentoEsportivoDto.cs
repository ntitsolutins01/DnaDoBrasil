﻿namespace WebApp.Dto
{
    public class TalentoEsportivoDto
    {
        public int Id { get; init; }
        public int ProfissionalId { get; set; }
        public decimal? Flexibilidade { get; set; }
        public decimal? PreensaoManual { get; set; }
        public decimal? Velocidade { get; set; }
        public decimal? ImpulsaoHorizontal { get; set; }
        public decimal? Vo2Max { get; set; }
        public decimal? Abdominal { get; set; }
        public decimal? Imc { get; set; }
        public decimal? ShuttleRun { get; set; }
        public string? Encaminhamento { get; set; }
        public decimal? Altura { get; set; }
        public decimal? Peso { get; set; }
        public decimal? Envergadura { get; set; }
        public int AlunoId { get; set; }
    }
}
